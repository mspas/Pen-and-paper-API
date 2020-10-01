using Microsoft.EntityFrameworkCore;
using RPG.Api.Domain.Models;
using RPG.Api.Domain.Repositories.RForum;
using RPG.Api.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Api.Persistence.Repositories.RForum
{
    public class MessageForumRepository : BaseRepository, IMessageForumRepository
    {
        public MessageForumRepository(RpgDbContext context) : base(context)
        {
        }

        public async Task<MessageForumResponse> AddMessageAsync(MessageForum message)
        {
            await _context.MessagesForum.AddAsync(message);
            return new MessageForumResponse(true, null, message);
        }

        public BaseResponse DeleteMessage(MessageForum message)
        {
            _context.MessagesForum.Remove(message);
            return new BaseResponse(true, null);
        }

        public MessageForumResponse EditMessage(MessageForum message)
        {
            _context.MessagesForum.Update(message);
            return new MessageForumResponse(true, null, message);
        }

        public async Task<MessageForum> GetMessageAsync(int messageId)
        {
            return await _context.MessagesForum.FirstAsync(mbox => mbox.Id == messageId);
        }

        public async Task<MessageForum> GetTopicLastMessageAsync(int topicId)
        {
            return await _context.MessagesForum.Where(mbox => mbox.topicId == topicId).OrderByDescending(mbox => mbox.sendDdate).FirstOrDefaultAsync();
        }

        public async Task<List<MessageForum>> GetMessageListAsync(int topicId, int pageNumber, int pageSize)
        {
            return await _context.MessagesForum.Where(mbox => mbox.topicId == topicId).OrderBy(mbox => mbox.sendDdate).Skip((pageNumber - 1) * pageSize ).Take(pageSize).ToListAsync();
        }

        public async Task<int> CountMessagesAsync(int topicId)
        {
            return await _context.MessagesForum.Where(mbox => mbox.topicId == topicId).CountAsync();
        }

    }
}
