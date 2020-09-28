using RPG.Api.Domain.Models;
using RPG.Api.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Api.Domain.Repositories.Profile
{
    public interface IPersonalDataRepository
    {
        Task<PersonalData> GetProfileByName(string name);
        Task<PersonalData> GetProfileById(int id);
        Task<List<PersonalData>> FindProfilesAsync(SearchProfileParameters searchParameters);
        Task<int> CountProfilesAsync(SearchProfileParameters searchProfileParameters);
        Task<BaseResponse> UpdateProfile(PersonalData toUpdate);
    }
}
