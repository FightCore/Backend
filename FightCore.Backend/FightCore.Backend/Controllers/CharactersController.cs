using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FightCore.Models.Characters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FightCore.Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces(HttpContentTypes.APPLICATION_JSON)]
    public class CharactersController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCharacters()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCharacter(long id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCharacter(Character character)
        {
            return Created("characters", character.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCharacter(long id, Character character)
        {
            return Accepted();
        }
    }
}