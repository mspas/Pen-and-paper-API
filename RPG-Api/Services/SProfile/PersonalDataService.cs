using Microsoft.AspNetCore.Mvc;
using RPG.Api.Domain.Models;
using RPG.Api.Domain.Repositories.Profile;
using RPG.Api.Domain.Services.Profile;
using RPG.Api.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RPG.Api.Domain.Repositories;
using AutoMapper;
using RPG.Api.Resources;

namespace RPG.Api.Services.Profile
{
    public class PersonalDataService : IPersonalDataService
    {
        private readonly IPersonalDataRepository _personalDataRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PersonalDataService(IPersonalDataRepository personalDataRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _personalDataRepository = personalDataRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private string PrepareNewURL(SearchProfileParameters searchParameters, int maxPages, int pageDifference)
        {
            if (searchParameters.pageNumber + pageDifference < 2 || searchParameters.pageNumber > maxPages - 1)
                return null;

            return "?login=" + searchParameters.login + "&firstName=" + searchParameters.firstName + "&lastName=" + searchParameters.lastName 
                + "&pageSize=" + searchParameters.pageSize.ToString() + "&pageNumber=" + (searchParameters.pageNumber + pageDifference).ToString();
        }

        public async Task<SearchProfileResponse> FindProfilesAsync(SearchProfileParameters searchParameters)
        {
            //List<PersonalData> results = new List<PersonalData>();
            //int countAll = 0;
            var results = await _personalDataRepository.FindProfilesAsync(searchParameters);
            int countAll = await _personalDataRepository.CountProfilesAsync(searchParameters);

            /*if (searchParameters.login.Length > 0)
            {
                var resultsLogin = await _personalDataRepository.FindProfilesByLoginAsync(searchParameters);
                countAll += await _personalDataRepository.CountProfilesByLoginAsync(searchParameters);

                results = results.Union(resultsLogin).ToList();
            }

            if (searchParameters.firstName.Length > 0)
            {
                var resultsFirstName = await _personalDataRepository.FindProfilesByFirstNameAsync(searchParameters);
                countAll += await _personalDataRepository.CountProfilesByFirstNameAsync(searchParameters);

                results = results.Union(resultsFirstName).ToList();
            }

            if (searchParameters.lastName.Length > 0)
            {
                var resultsLastName = await _personalDataRepository.FindProfilesByLastNameAsync(searchParameters);
                countAll += await _personalDataRepository.CountProfilesByLastNameAsync(searchParameters);

                results = results.Union(resultsLastName).ToList();
            }*/

            var profilesListResource = _mapper.Map<List<PersonalData>, List<PersonalDataResource>>(results);

            double temp = (double)countAll / (double)searchParameters.pageSize;
            int maxPages = (int)Math.Ceiling(temp);

            return new SearchProfileResponse(profilesListResource, countAll, maxPages, PrepareNewURL(searchParameters, maxPages, -1), PrepareNewURL(searchParameters, maxPages, 1));
        }

        public async Task<BaseResponse> EditProfileDataAsync(int id, PersonalData newProfile)
        {
            var oldProfile= _personalDataRepository.GetProfileById(id).Result;
            var toUpdateProfile = UpdateDataProfileAsync(newProfile, oldProfile);
            var response = await _personalDataRepository.UpdateProfile(toUpdateProfile);

            await _unitOfWork.CompleteAsync();

            return response;
        }

        public async Task<PersonalData> GetProfileAsync(string login)
        {
            return await _personalDataRepository.GetProfileByName(login);
        }

        public Task<PersonalData> GetProfileAsync(int id)
        {
            return _personalDataRepository.GetProfileById(id);
        }


        private PersonalData UpdateDataProfileAsync(PersonalData newProfile, PersonalData profileToUpdate)
        {
            profileToUpdate.firstname = newProfile.firstname;
            profileToUpdate.lastname = newProfile.lastname;
            profileToUpdate.email = newProfile.email;
            profileToUpdate.age = newProfile.age;
            profileToUpdate.city = newProfile.city;

            return profileToUpdate;
        }
    }
}
