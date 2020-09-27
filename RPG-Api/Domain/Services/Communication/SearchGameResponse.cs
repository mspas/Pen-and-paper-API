using RPG.Api.Domain.Models;
using RPG.Api.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Api.Domain.Services.Communication
{
    public class SearchGameResponse : SearchResponse
    {
        public SearchGameResponse(List<GameResource> gamesResult, int count, int maxPages, string previousPage, string nextPage) : base(count, maxPages, previousPage, nextPage)
        {
            this.gamesResult = gamesResult;
        }

        public List<GameResource> gamesResult { get; set; }
    }
}
