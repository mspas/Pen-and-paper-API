﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RPG.Api.Domain.Models;
using RPG.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPG.Api.Domain.Services.SForum;
using RPG.Api.Resources;
using RPG.Api.Domain.Services.Communication;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mdRPG.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class MessageForumController : Controller
    {
        private readonly IMessageForumService _messageForumService;

        public MessageForumController(IMessageForumService messageForumService)
        {
            _messageForumService = messageForumService;
        }

        [HttpGet]
        public async Task<MessageForumPaginatedResponse> Get([FromQuery] int gameId, int topicId, int pageNumber, int pageSize) // page == 0 => all pages, pages == -1, only one message
        {
            return await _messageForumService.GetMessageListAsync(gameId, topicId, pageNumber, pageSize);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MessageForum message)
        {
            var response = await _messageForumService.AddMessageAsync(message);

            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MessageForum message)
        {
            var response = await _messageForumService.EditMessageAsync(message);

            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _messageForumService.DeleteMessageAsync(id);

            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
