using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using RPG.Api.Resources;
using RPG.Api.Domain.Models;
using RPG.Api.Persistence;
using RPG.Api.Domain.Services.SGame;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPG.Api.Domain.Services.Profile;
using RPG.Api.Domain.Services.Communication;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RPG.Api.Domain.Controllers
{
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IPersonalDataService _personalDataService;
        private readonly IMapper _mapper;

        public GameController(IGameService gameService, IPersonalDataService personalDataService, IMapper mapper)
        {
            _gameService = gameService;
            _personalDataService = personalDataService;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public async Task<GameResource> Get(int id)
        {
            var game = await _gameService.GetGameAsync(id);
            var resource = _mapper.Map<Game, GameResource>(game);

            var participantsProfiles = new List<PersonalDataResource>();

            foreach (GameToPerson g2p in game.participants)
            {
                var profile = await _personalDataService.GetProfileAsync(g2p.playerId);
                var profileResource = _mapper.Map<PersonalData, PersonalDataResource>(profile);
                participantsProfiles.Add(profileResource);
            }

            resource.participantsProfiles = participantsProfiles;

            return resource;
        }

        [HttpGet("search")]
        public async Task<SearchGameResponse> Search([FromQuery] string title, string categories, bool showOnlyAvailable, int pageNumber, int pageSize)
        {
            string[] selectedCategories;
            title = title ?? "";

            if (categories.Length > 6) 
             selectedCategories = categories.Substring(2, categories.Length - 4).Split(new string[] { "\",\"" }, StringSplitOptions.None);
            else 
                selectedCategories = new string[] { "Fantasy", "SciFi", "Mafia", "Cyberpunk", "Steampunk", "PostApo", "Zombie", "AltHistory", "Other" };

            var searchResults = await _gameService.FindGamesAsync(title, selectedCategories, showOnlyAvailable, pageNumber, pageSize);
            return searchResults;
        }

        // POST api/<controller>
         
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Game game)
        {
            var response = await _gameService.AddGameAsync(game);
            if (response.Success)
            {
                return Ok(response.Game.Id);
            }
            return NotFound();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Game game)
        {
            var response = await _gameService.EditGameAsync(game);
            if (response.Success)
            {
                return Ok(response.Game.Id);
            }
            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _gameService.DeleteGameAsync(id);
            if (response.Success)
            {
                return Ok(response.Success);
            }
            return NoContent();
        }
    }
}
