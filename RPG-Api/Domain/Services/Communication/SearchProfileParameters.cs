using RPG.Api.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Api.Domain.Services.Communication
{
    public class SearchProfileParameters : SearchParameters
    {
        public SearchProfileParameters()
        {
        }

        public SearchProfileParameters(int pageNumber, int pageSize, string name) : base(pageNumber, pageSize)
        {
            this.name = name;
        }

        public string name { get; set;  }
    }
}
