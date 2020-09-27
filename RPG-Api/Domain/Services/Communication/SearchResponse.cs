using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Api.Domain.Services.Communication
{
    public class SearchResponse
    {
        public SearchResponse(int count, int maxPages, string previousPage, string nextPage)
        {
            this.count = count;
            this.maxPages = maxPages;
            this.previousPage = previousPage;
            this.nextPage = nextPage;
        }

        public int count { get; set; }
        public int maxPages { get; set; }
        public string previousPage { get; set; }
        public string nextPage { get; set; }
    }
}
