using Microsoft.EntityFrameworkCore;
using RPG.Api.Domain.Models;
using RPG.Api.Domain.Repositories.RGame;
using RPG.Api.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Api.Persistence.Repositories.RGame
{
    public class GameRepository : BaseRepository, IGameRepository
    {
        public GameRepository(RpgDbContext context) : base(context)
        {
        }

        public async Task<GameResponse> AddGameAsync(Game game)
        {
            await _context.Games.AddAsync(game);
            return new GameResponse(true, null, game);
        }

        public BaseResponse DeleteGame(Game game)
        {
            _context.Games.Remove(game);
            return new BaseResponse(true, null);
        }

        public GameResponse EditGame(Game game)
        {
            _context.Games.Update(game);
            return new GameResponse(true, null, game);
        }

        public async Task<Game> GetGameAsync(int gameId)
        {
            return await _context.Games.Include(p => p.gameMaster)
                                    .Include(p => p.participants)
                                    .Include(p => p.skillSetting)
                                    .Include(p => p.sessions)
                                    .FirstAsync(mbox => mbox.Id == gameId);
        }

        public async Task<List<Game>> FindGamesAsync(string title, string[] categories, bool showOnlyAvailable, int pageNumber, int pageSize)
        {
            List<Game> results;

            Console.WriteLine(title, categories[0], showOnlyAvailable, pageNumber, pageSize);

            if (showOnlyAvailable)
            {
                results = await _context.Games.Include(p => p.gameMaster)
                    .Include(p => p.participants)
                    .Where(p => (p.title.StartsWith(title)) &&
                            (categories.Contains(p.category)) &&
                            (p.status == "Active" || (p.status == "Ongoing" && p.hotJoin == true)) &&
                            (p.maxplayers - p.nofparticipants > 0))
                    .OrderBy(p => p.title)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToListAsync();
            }
            else
            {
                results = await _context.Games.Include(p => p.gameMaster)
                    .Include(p => p.participants)
                    .Where(p => (p.title.StartsWith(title)) && (categories.Contains(p.category)))
                    .OrderBy(p => p.title)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToListAsync();
            }

            return results;
        }

        public async Task<int> CountGamesAsync(string title, string[] categories, bool showOnlyAvailable, int pageNumber, int pageSize)
        {
            int count = 0;

            if (showOnlyAvailable)
            {
                count = await _context.Games.Where(p => (p.title.StartsWith(title)) &&
                                                  (categories.Contains(p.category)) &&
                                                  (p.status == "Active" || (p.status == "Ongoing" && p.hotJoin == true)) &&
                                                  (p.maxplayers - p.nofparticipants > 0))
                                            .CountAsync();
            }
            else
            {
                count = await _context.Games.Where(p => (p.title.StartsWith(title)) &&
                                                  (categories.Contains(p.category)))
                                            .CountAsync();
            }

            return count;
        }

        public async Task<Game> GetGameSnapAsync(int gameId)
        {
            return await _context.Games.Include(p => p.gameMaster).FirstAsync(mbox => mbox.Id == gameId);
        }

        public async Task<List<Game>> GetGameListAsync()
        {
            return await _context.Games.Include(p => p.gameMaster)
                                    .Include(p => p.participants)
                                    .Include(p => p.skillSetting)
                                    .Include(p => p.sessions)
                                    .ToListAsync();
        }
    }
}
