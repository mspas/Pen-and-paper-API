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
        Task<List<PersonalData>> FindProfilesByLoginAsync(SearchProfileParameters searchParameters);
        Task<List<PersonalData>> FindProfilesByFirstNameAsync(SearchProfileParameters searchParameters);
        Task<List<PersonalData>> FindProfilesByLastNameAsync(SearchProfileParameters searchParameters);
        Task<int> CountProfilesAsync(SearchProfileParameters searchProfileParameters);
        Task<int> CountProfilesByLoginAsync(SearchProfileParameters searchProfileParameters);
        Task<int> CountProfilesByFirstNameAsync(SearchProfileParameters searchProfileParameters);
        Task<int> CountProfilesByLastNameAsync(SearchProfileParameters searchProfileParameters);
        Task<BaseResponse> UpdateProfile(PersonalData toUpdate);
    }
}
