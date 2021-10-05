using RPG.Api.Domain.Models;
using RPG.Api.Domain.Repositories;
using RPG.Api.Domain.Repositories.RForum;
using RPG.Api.Domain.Repositories.RGame;
using RPG.Api.Domain.Services;
using RPG.Api.Domain.Services.Communication;
using RPG.Api.Domain.Services.SForum;
using RPG.Api.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Api.Services.SForum
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;
        private readonly ITopicToPersonRepository _topicToPersonRepository;
        private readonly IMessageForumRepository _messageForumRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IPhotoService _photoService;
        private readonly IUnitOfWork _unitOfWork;

        public TopicService(ITopicRepository topicRepository, ITopicToPersonRepository topicToPersonRepository, IMessageForumRepository messageForumRepository, IGameRepository gameRepository, IPhotoService photoService, IUnitOfWork unitOfWork)
        {
            _topicRepository = topicRepository;
            _topicToPersonRepository = topicToPersonRepository;
            _messageForumRepository = messageForumRepository;
            _gameRepository = gameRepository;
            _photoService = photoService;
            _unitOfWork = unitOfWork;
        }

        public async Task<TopicResponse> AddTopicAsync(Topic topic, string bodyMessage)
        {
            var message = new MessageForum(topic.createDate, bodyMessage, topic.authorId);

            var game = await _gameRepository.GetGameAsync(topic.forumId);

            var responseTopic = await _topicRepository.AddTopicAsync(topic);
            await _unitOfWork.CompleteAsync();

            message.topicId = topic.Id;

            var responseT2P = new TopicToPersonResponse(true, null, null);
            if (topic.isPublic)
            {
                foreach (GameToPerson player in game.participants)
                {
                    var t2p = new TopicToPerson(topic.forumId, topic.Id, player.playerId, topic.createDate.AddHours(-1));
                    if (player.Id == topic.authorId)
                        t2p.lastActivitySeen = topic.createDate;
                    responseT2P = await _topicToPersonRepository.AddT2PAsync(t2p);
                }
            }
            var responseMsg = await _messageForumRepository.AddMessageAsync(message);
            await _unitOfWork.CompleteAsync();

            if (!responseTopic.Success)
                return new TopicResponse(false, "Failed topic creation", null);
            if (!responseT2P.Success)
                return new TopicResponse(false, "Failed to create connection to topic", null);
            if (!responseMsg.Success)
                return new TopicResponse(false, "Failed to post message to topic", null);
            return responseTopic;
        }

        public async Task<BaseResponse> DeleteTopicAsync(int topicId)
        {
            var topic = await _topicRepository.GetTopicAsync(topicId);

            var res = await DeleteImagesFromPosts(topic.Messages.ToList());

            if (!res.Success)
            {
                return res;
            }

            var response = _topicRepository.DeleteTopic(topic);
            await _unitOfWork.CompleteAsync();
            return response;
        }

        private async Task<BaseResponse> DeleteImagesFromPosts(List<MessageForum> messages)
        {
            var response = new BaseResponse(true, null);

            foreach (MessageForum message in messages)
            {
                var fileNamesList = ExtractFileNames(message);

                if (fileNamesList == null || fileNamesList.Count < 1)
                {
                    return response;
                }

                foreach (string fileName in fileNamesList)
                {
                    var res = await _photoService.DeletePhotoAsync(3, message.senderId, fileName);
                    if (!res.Success)
                    {
                        response = res;
                    }
                }
            }

            return response;
        }

        private List<string> ExtractFileNames(MessageForum message)
        {
            var arraySplitImg = message.bodyMessage.Split("<img src=\"");

            List<string> arraySplit = new List<string>();
            List<string> postFileNames = new List<string>();

            if (arraySplitImg.Length < 2)
            {
                return postFileNames;
            }

            for (int i = 0; i < arraySplitImg.Length; i++)
            {
                var element = arraySplitImg[i].Split("\" alt=\"findme\">");
                for (int j = 0; j < element.Length; j++)
                    arraySplit.Add(element[j]);
            }

            for (int i = 0; i < arraySplit.Count; i++)
            {
                if (i % 2 != 0)
                {
                    postFileNames.Add(arraySplit[i]);
                }
            }

            return postFileNames;
        }

        public async Task<TopicResponse> EditTopicAsync(Topic topic)
        {
            var toUpdate = await _topicRepository.GetTopicAsync(topic.Id);
            if (toUpdate == null)
            {
                return new TopicResponse(false, "Topic not found", null);
            }

            toUpdate.lastActivityDate = topic.lastActivityDate;
            toUpdate.isPublic = topic.isPublic;
            toUpdate.topicName = topic.topicName;

            var response = _topicRepository.EditTopic(topic);
            await _unitOfWork.CompleteAsync();
            return response;
        }

        public async Task<Topic> GetTopicAsync(int userId, int topicId)
        {
            var topic = await _topicRepository.GetTopicAsync(topicId);
            if (topic.isPublic == false)
            {
                if (topic.UsersConnected.Count > 0)
                {
                    foreach (TopicToPerson per in topic.UsersConnected)
                    {
                        if (per.userId == userId)
                            return topic;
                    }
                    return null;
                }
                else
                    return null;
            }
            return topic;
        }

        public async Task<List<Topic>> GetTopicListAsync(int forumId)
        {
            return await _topicRepository.GetTopicListAsync(forumId);
        }
    }
}
