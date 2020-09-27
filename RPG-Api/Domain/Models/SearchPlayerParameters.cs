using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Api.Domain.Models
{
    public class SearchPlayerParameters
    {
        public SearchPlayerParameters(int pageNumber, int pageSize, string name)
        {
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
            this.name = name;
        }

        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string name { get; set;  }
    }
}
