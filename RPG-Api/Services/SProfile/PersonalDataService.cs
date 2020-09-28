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

        public async Task<SearchProfileResponse> FindProfilesAsync(SearchProfileParameters searchParameters)
        {
            var results = await _personalDataRepository.FindProfilesAsync(searchParameters);
            var countAll = await _personalDataRepository.CountProfilesAsync(searchParameters);

            var profilesListResource = _mapper.Map<List<PersonalData>, List<PersonalDataResource>>(results);

            double temp = (double)countAll / (double)searchParameters.pageSize;
            int maxPages = (int)Math.Ceiling(temp);

            var urlBaseParameters = "&pageSize=" + searchParameters.pageSize.ToString() + "&name=" + searchParameters.name;

            var previousPage = searchParameters.pageNumber < 2 ? null :
                "?pageNumber=" + (searchParameters.pageNumber - 1).ToString() + urlBaseParameters;
            var nextPage = searchParameters.pageNumber == maxPages ? null :
                "?pageNumber=" + (searchParameters.pageNumber + 1).ToString() + urlBaseParameters;

            return new SearchProfileResponse(profilesListResource, countAll, maxPages, previousPage, nextPage);
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
