using RPG.Api.Domain.Models;
using RPG.Api.Domain.Repositories.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RPG.Api.Domain.Services.Communication;

namespace RPG.Api.Persistence.Repositories.Profile
{
    public class PersonalDataRepository : BaseRepository, IPersonalDataRepository
    {
        public PersonalDataRepository(RpgDbContext context) : base(context)
        {
        }

        public async Task<List<PersonalData>> GetList()
        {
            var accounts = await _context.Accounts.Include(mbox => mbox.PersonalData).ToListAsync();
            var pdata = new List<PersonalData>();
            foreach (Account acc in accounts)
            {
                pdata.Add(acc.PersonalData);
            }
            return pdata;
        }
        
        public async Task<PersonalData> GetProfileByName(string name)
        {
            var account =  await _context.Accounts.Include(mbox => mbox.PersonalData).SingleOrDefaultAsync(mbox => mbox.PersonalData.login == name);
            return account?.PersonalData;
        }

        public async Task<PersonalData> GetProfileById(int id)
        {
            var account =  await _context.Accounts.Include(mbox => mbox.PersonalData).SingleOrDefaultAsync( mbox => mbox.Id == id);
            return account?.PersonalData;
        }

        public async Task<BaseResponse> UpdateProfile(PersonalData toUpdate)
        {
            var account = await _context.Accounts.Include(mbox => mbox.PersonalData).FirstAsync(mbox => mbox.Id == toUpdate.Id);
            account.PersonalData = toUpdate;
            _context.Accounts.Update(account);
            return new BaseResponse(true, null);
        }

        public async Task<List<PersonalData>> FindProfilesAsync(SearchProfileParameters searchParameters)
        {
            var results = new List<PersonalData>();
            var accounts = await _context.Accounts.Include(mbox => mbox.PersonalData)
                                                  .Where(mbox => 
                                                    (mbox.login.Contains(searchParameters.login) && searchParameters.login.Length > 1) || 
                                                    (mbox.PersonalData.firstname.Contains(searchParameters.firstName) && searchParameters.firstName.Length > 1) || 
                                                    (mbox.PersonalData.lastname.Contains(searchParameters.lastName) && searchParameters.lastName.Length > 1)
                                                   )
                                                  .OrderBy(p => p.PersonalData.login)
                                                  .Skip((searchParameters.pageNumber - 1) * searchParameters.pageSize)
                                                  .Take(searchParameters.pageSize).ToListAsync();

            foreach (Account acc in accounts)
            {
                results.Add(acc.PersonalData);
            }

            return results;
        }

        public async Task<int> CountProfilesAsync(SearchProfileParameters searchParameters)
        {
            return await _context.Accounts
                            .Where(mbox =>
                                (mbox.login.Contains(searchParameters.login) && searchParameters.login.Length > 1) ||
                                (mbox.PersonalData.firstname.Contains(searchParameters.firstName) && searchParameters.firstName.Length > 1) ||
                                (mbox.PersonalData.lastname.Contains(searchParameters.lastName) && searchParameters.lastName.Length > 1)
                            )
                            .OrderBy(p => p.PersonalData.login)
                            .CountAsync();
        }
    }
}
