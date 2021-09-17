using AutoMapper;
using RPG.Api.Domain.Models;
using RPG.Api.Domain.Repositories;
using RPG.Api.Domain.Repositories.RForum;
using RPG.Api.Domain.Repositories.RGame;
using RPG.Api.Domain.Services.Communication;
using RPG.Api.Domain.Services.Profile;
using RPG.Api.Domain.Services.SGame;
using RPG.Api.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RPG.Api.Services.SGame
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IGameToPersonRepository _gameToPersonRepository;
        private readonly IForumRepository _forumRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPersonalDataService _personalDataService;

        public GameService(IGameRepository gameRepository, IGameToPersonRepository gameToPersonRepository, IForumRepository forumRepository, IUnitOfWork unitOfWork, IMapper mapper, IPersonalDataService personalDataService)
        {
            _gameRepository = gameRepository;
            _gameToPersonRepository = gameToPersonRepository;
            _forumRepository = forumRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _personalDataService = personalDataService;
        }

        public async Task<GameResponse> AddGameAsync(Game game)
        {
            var gameResponse = await _gameRepository.AddGameAsync(game);

            var gameToPerson = new GameToPerson
            {
                gameId = game.Id,
                playerId = game.masterId,
                isGameMaster = true,
                isAccepted = true,
                isMadeByPlayer = true
            };
            var g2pResponse = await _gameToPersonRepository.AddG2PAsync(gameToPerson);

            await _unitOfWork.CompleteAsync();

            await _forumRepository.AddForumAsync(new Forum());
            await _unitOfWork.CompleteAsync();

            if (gameResponse.Success && g2pResponse.Success)
            {
                return gameResponse;
            }
            return new GameResponse(false, "Failed", null);
        }

        public async Task<BaseResponse> DeleteGameAsync(int gameId)
        {
            var game = await _gameRepository.GetGameAsync(gameId);
            var gameResource = _mapper.Map<Game, GameResource>(game);
            gameResource.participants.ForEach(p => _gameToPersonRepository.DeleteG2P(p));
            var response = _gameRepository.DeleteGame(game);
            await _unitOfWork.CompleteAsync();
            return response;
        }

        public Task<GameResponse> EditGameAsync(Game game)
        {
            throw new NotImplementedException();
        }

        public async Task<SearchGameResponse> FindGamesAsync(string title, string[] categories, bool showOnlyAvailable, int pageNumber, int pageSize)
        {
            var foundGames = await _gameRepository.FindGamesAsync(title, categories, showOnlyAvailable, pageNumber, pageSize);
            var countAll = await _gameRepository.CountGamesAsync(title, categories, showOnlyAvailable, pageNumber, pageSize);

            var gamesListResource = _mapper.Map<List<Game>, List<GameResource>>(foundGames);

            foreach (GameResource g in gamesListResource)
            {
                var participantsProfiles = new List<PersonalDataResource>();

                foreach (GameToPerson g2p in g.participants)
                {
                    var profile = await _personalDataService.GetProfileAsync(g2p.playerId);
                    var profileResource = _mapper.Map<PersonalData, PersonalDataResource>(profile);
                    participantsProfiles.Add(profileResource);
                }

                g.participantsProfiles = participantsProfiles;
            }

            double temp = (double)countAll / (double)pageSize;
            int maxPages = (int) Math.Ceiling(temp);

            var urlBaseParameters = "&title=" + title + "&categories=%5B\"" + string.Join("\",\"", categories) + "\"%5D&showOnlyAvailable=" + showOnlyAvailable.ToString() + "&pageSize = " + pageSize.ToString();

            var previousPage = pageNumber < 2 ? null :
                "?pageNumber=" + (pageNumber - 1).ToString() + urlBaseParameters;
            var nextPage = pageNumber == maxPages ? null :
                "?pageNumber=" + (pageNumber + 1).ToString() + urlBaseParameters;

            return new SearchGameResponse(gamesListResource, countAll, maxPages, previousPage, nextPage);
        }


        public async Task<Game> GetGameAsync(int gameId)
        {
            return await _gameRepository.GetGameAsync(gameId);
        }
    }
}
