using RPG.Api.Domain.Models;
using RPG.Api.Domain.Models.Enums;
using RPG.Api.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Api.Domain.Repositories.Profile
{
    public interface IAccountRepository
    {
        Task<CreateAccountResponse> AddAsync(Account account, ERole[] userRoles);
        Account FindByLoginAsync(string login);
        Account FindByIdAsync(int id);
        Task<BaseResponse> EditPasswordAsync(Account account);
    }
}
