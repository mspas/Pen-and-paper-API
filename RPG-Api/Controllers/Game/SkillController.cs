﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mdRPG.Models;
using mdRPG.Persistence;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mdRPG.Controllers
{
    [Route("api/[controller]")]
    public class SkillController : Controller
    {


        private readonly RpgDbContext context;

        public SkillController(RpgDbContext context)
        {
            this.context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> CreateSkill([FromBody] Skill skill)
        {
            context.Skills.Add(skill);
            await context.SaveChangesAsync();
            return Ok(skill);
        }


        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var toDelete = context.Skills.Find(id);
            if (toDelete == null)
            {
                return NotFound();
            }

            context.Skills.Remove(toDelete);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}