using RPG.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Api.Domain.Services.Communication
{
    public class SearchProfileResponse : SearchResponse
    {
        public SearchProfileResponse(List<PersonalData> profilesResult, int count, int maxPages, string previousPage, string nextPage) : base(count, maxPages, previousPage, nextPage)
        {
            this.profilesResult = profilesResult;
        }

        public List<PersonalData> profilesResult { get; set; }
    }
}
