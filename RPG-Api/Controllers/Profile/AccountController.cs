
using AutoMapper;
using RPG.Api.Resources;
using RPG.Api.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RPG.Api.Domain.Services.Profile;
using RPG.Api.Domain.Models.Enums;
using RPG.Api.Domain.Services.Communication;
using Microsoft.AspNetCore.Authorization;

namespace mdRPG.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public AccountController(IMapper mapper, IAccountService accountService)
        {
            _mapper = mapper;
            _accountService = accountService;
        }

        [HttpPost("/api/account")]
        public async Task<IActionResult> CreateAccount([FromBody] AccountCredentialsResource accountCredentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account = _mapper.Map<AccountCredentialsResource, Account>(accountCredentials);

            var response = await _accountService.CreateUserAsync(account, ERole.Common);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            var profileResource = _mapper.Map<PersonalData, PersonalDataResource>(response.Account.PersonalData);
            return Ok(new CreateAccountResponse2(response.Success, response.Message, profileResource));
        }

        [Authorize]
        [HttpPut("/api/account/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ChangePassword passwordData)
        {
            var response = await _accountService.ChangePasswordAsync(id, passwordData);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
