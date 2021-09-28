using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using RPG.Api.Resources;
using RPG.Api.Domain.Models;
using RPG.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPG.Api.Domain.Services.Profile;
using RPG.Api.Domain.Services.Communication;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mdRPG.Controllers
{
    [Route("api/pdata")]
    public class PersonalDataController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPersonalDataService _personalDataService;


        public PersonalDataController(IPersonalDataService personalDataService, IMapper mapper)
        {
            _mapper = mapper;
            _personalDataService = personalDataService;
        }


        [HttpGet("{user}")]
        public async Task<PersonalDataResource> Get(string user)
        {
            var profile = await _personalDataService.GetProfileAsync(user);
            var resource = _mapper.Map<PersonalData, PersonalDataResource>(profile);
            return resource;
        }

        [HttpGet("search")]
        public async Task<SearchProfileResponse> Search([FromQuery] SearchProfileParameters searchParameters)
        {
            if (searchParameters == null)
                return null;

            searchParameters.login = searchParameters.login ?? "";
            searchParameters.firstName = searchParameters.firstName ?? "";
            searchParameters.lastName = searchParameters.lastName ?? "";

            var result = await _personalDataService.FindProfilesAsync(searchParameters);
            return result;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]PersonalData profile)
        {
            var response = await _personalDataService.EditProfileDataAsync(id, profile);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
