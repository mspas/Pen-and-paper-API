using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Api.Domain.Services.Communication
{
    public class SearchGameParameters : SearchParameters
    {
        public string title { get; set; }
        public string categoriesPattern { get; set; }
        public bool isAvaliable { get; set; }
        public string[] categories { get; set; }

        public SearchGameParameters(int pageNumber, int pageSize, string title, string categoriesPattern, bool isAvaliable) : base(pageNumber, pageSize)
        {
            this.title = title;
            this.categoriesPattern = categoriesPattern;
            this.isAvaliable = isAvaliable;
        }

        public SearchGameParameters()
        {
        }
    }
}
