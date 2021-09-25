using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RPG.Api.Domain.Models;
using RPG.Api.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPG.Api.Domain.Services.Profile;
using AutoMapper;

namespace RPG_Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Message")]
    public class MessageController : Controller
    {
        private IMessageService _messageService;
        private readonly IMapper mapper;

        public MessageController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            this.mapper = mapper;
        }



        // GET api/<controller>/5 but its a id of relation person-person 
        [HttpGet("{relationId}")]
        public async Task<List<Message>> Get(int relationId)
        {
            return await _messageService.GetMessageListAsync(relationId);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Message message)
        {
            var response = await _messageService.AddMessageAsync(message);
            if (response.Success)
                return Ok(response);
            return NotFound(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id)
        {
            var response = await _messageService.EditMessageAsync(id);
            if (response.Success)
                return Ok(response);
            return NotFound(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _messageService.DeleteMessageAsync(id);
            if (response.Success)
                return Ok(response);
            return NotFound(response);
        }
    }
}