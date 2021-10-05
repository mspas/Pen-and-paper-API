using Microsoft.EntityFrameworkCore;
using RPG.Api.Domain.Models;
using RPG.Api.Domain.Models.Enums;
using RPG.Api.Domain.Repositories.Profile;
using RPG.Api.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Api.Persistence.Repositories.Profile
{
    public class AccountRepository : IAccountRepository
    {
        private readonly RpgDbContext _context;

        public AccountRepository(RpgDbContext context)
        {
            _context = context;
        }

        public async Task<CreateAccountResponse> AddAsync(Account account, ERole[] userRoles)
        {
            var userRoleNames = userRoles.Select(ur => ur.ToString());
            var roles = await _context.Roles.Where(r => userRoleNames.Contains(r.Name)).ToListAsync();

            foreach (var role in roles)
            {
                account.UserRoles.Add(new UserRole { RoleId = role.Id });
            }

            _context.Accounts.Add(account);
            return new CreateAccountResponse(true, null, account);
        }

        public Account FindByLoginAsync(string login)
        {
            return _context.Accounts.Include(u => u.UserRoles)
                                       .ThenInclude(ur => ur.Role)
                                       .SingleOrDefault(u => u.login == login);
        }

        public Account FindByIdAsync(int id)
        {
            return _context.Accounts.Include(u => u.UserRoles)
                                       .ThenInclude(ur => ur.Role)
                                       .SingleOrDefault(u => u.Id == id);
        }

        public async Task<BaseResponse> EditPasswordAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
            return new BaseResponse(true, "Success!");
        }
    }
}
