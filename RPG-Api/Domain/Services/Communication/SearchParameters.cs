using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Api.Domain.Services.Communication
{
    public class SearchParameters
    {
        public SearchParameters(int pageNumber, int pageSize)
        {
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
        }
        public SearchParameters()
        {
        }

        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}
