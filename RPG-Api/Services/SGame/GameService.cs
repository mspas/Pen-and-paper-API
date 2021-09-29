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
        private const string emptyArrayInUrlMarkup = "%5B%5D";

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
            /*var forumId = await _forumRepository.AddForumAsync(new Forum());
            await _unitOfWork.CompleteAsync();

            game.forumId = forumId;*/

            var gameResponse = await _gameRepository.AddGameAsync(game);
            await _unitOfWork.CompleteAsync();

            var gameToPerson = new GameToPerson
            {
                gameId = gameResponse.Game.Id,
                playerId = game.masterId,
                isGameMaster = true,
                isAccepted = true,
                isMadeByPlayer = true
            };
            var g2pResponse = await _gameToPersonRepository.AddG2PAsync(gameToPerson);
            await _unitOfWork.CompleteAsync();


            if (!gameResponse.Success || !g2pResponse.Success)
            {
                return new GameResponse(false, "Failed", null);
            }
            return gameResponse;
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

        public async Task<GameResponse> EditGameAsync(int id, Game toUpdate)
        {
            var oldGame = await _gameRepository.GetGameAsync(id);
            var gameToUpdate = UpdateDataGameAsync(toUpdate, oldGame);
            var response = _gameRepository.EditGame(gameToUpdate);

            await _unitOfWork.CompleteAsync();

            return response;
        }

        private Game UpdateDataGameAsync(Game newData, Game gameToUpdate)
        {
            gameToUpdate.title = newData.title;
            gameToUpdate.category = newData.category;
            gameToUpdate.date = newData.date;
            gameToUpdate.description = newData.description;
            gameToUpdate.storyDescription = newData.storyDescription;
            gameToUpdate.status = newData.status;
            gameToUpdate.photoName = newData.photoName;
            gameToUpdate.comment = newData.comment;
            gameToUpdate.needInvite = newData.needInvite;
            gameToUpdate.maxplayers = newData.maxplayers;
            gameToUpdate.hotJoin = newData.hotJoin;

            return gameToUpdate;
        }

        private string PrepareNewURL(SearchGameParameters searchParameters, int maxPages, int pageDifference)
        {
            if (searchParameters.pageNumber + pageDifference < 2 || searchParameters.pageNumber > maxPages - 1)
                return null;

            var categoriesString = searchParameters.selectedCategories.Length > emptyArrayInUrlMarkup.Length
            ? "%5B\"" + string.Join("\",\"", searchParameters.categories) + "\"%5D" : emptyArrayInUrlMarkup;

            return "?title=" + searchParameters.title + "&selectedCategories=" + categoriesString + "&showOnlyAvailable=" + searchParameters.showOnlyAvailable.ToString() 
                + "&pageSize=" + searchParameters.pageSize.ToString() + "&pageNumber=" + (searchParameters.pageNumber + pageDifference).ToString();
        }

        public async Task<SearchGameResponse> FindGamesAsync(SearchGameParameters searchParameters)
        {
            var foundGames = await _gameRepository.FindGamesAsync(searchParameters);
            var countAll = await _gameRepository.CountGamesAsync(searchParameters);

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

            double temp = (double)countAll / (double)searchParameters.pageSize;
            int maxPages = (int) Math.Ceiling(temp);

            return new SearchGameResponse(gamesListResource, countAll, maxPages, PrepareNewURL(searchParameters, maxPages, -1), PrepareNewURL(searchParameters, maxPages, 1));
        }


        public async Task<Game> GetGameAsync(int gameId)
        {
            return await _gameRepository.GetGameAsync(gameId);
        }
    }
}
