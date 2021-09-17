﻿using RPG.Api.Domain.Models;
using RPG.Api.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Api.Domain.Repositories.RGame
{
    public interface IGameRepository
    {
        Task<Game> GetGameAsync(int gameId);
        Task<Game> GetGameSnapAsync(int gameId);
        Task<List<Game>> GetGameListAsync();
        Task<List<Game>> FindGamesAsync(string title, string[] categories, bool showOnlyAvailable, int pageNumber, int pageSize);
        Task<int> CountGamesAsync(string title, string[] categories, bool showOnlyAvailable, int pageNumber, int pageSize);
        Task<GameResponse> AddGameAsync(Game game);
        GameResponse EditGame(Game game);
        BaseResponse DeleteGame(Game game);
    }
}
