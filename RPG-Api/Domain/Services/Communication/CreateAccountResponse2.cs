using RPG.Api.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Api.Domain.Services.Communication
{
    public class CreateAccountResponse2 : BaseResponse
    {
        public AccountResource Account { get; private set; }

        public CreateAccountResponse2(bool success, string message, AccountResource account) : base(success, message)
        {
            Account = account;
        }
    }
}
