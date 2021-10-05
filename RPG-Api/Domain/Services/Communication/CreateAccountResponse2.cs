using RPG.Api.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Api.Domain.Services.Communication
{
    public class CreateAccountResponse2 : BaseResponse
    {
        public PersonalDataResource PersonalData { get; private set; }

        public CreateAccountResponse2(bool success, string message, PersonalDataResource personalData) : base(success, message)
        {
            PersonalData = personalData;
        }
    }
}
