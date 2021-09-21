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

        public SearchProfileParameters(int pageNumber, int pageSize, string login, string firstName, string lastName) : base(pageNumber, pageSize)
        {
            this.login = login;
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public string login { get; set;  }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}
