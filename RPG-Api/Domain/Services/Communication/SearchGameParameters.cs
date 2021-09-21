using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Api.Domain.Services.Communication
{
    public class SearchGameParameters : SearchParameters
    {
        public string title { get; set; }
        public string selectedCategories { get; set; }
        public bool showOnlyAvailable { get; set; }
        public string[] categories { get; set; }

        public SearchGameParameters(int pageNumber, int pageSize, string title, string selectedCategories, bool showOnlyAvailable) : base(pageNumber, pageSize)
        {
            this.title = title;
            this.selectedCategories = selectedCategories;
            this.showOnlyAvailable = showOnlyAvailable;
        }

        public SearchGameParameters()
        {
        }
    }
}
