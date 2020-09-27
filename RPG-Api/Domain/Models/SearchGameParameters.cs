using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Api.Domain.Models
{
    public class SearchGameParameters
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string title { get; set; }
        public string categoriesPattern { get; set; }
        public bool isAvaliable { get; set; }
        public string[] categories { get; set; }

        public SearchGameParameters(int pageNumber, int pageSize, string title, string categoriesPattern, bool isAvaliable)
        {
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
            this.title = title;
            this.categoriesPattern = categoriesPattern;
            this.isAvaliable = isAvaliable;
        }

        public SearchGameParameters()
        {
        }
    }
}
