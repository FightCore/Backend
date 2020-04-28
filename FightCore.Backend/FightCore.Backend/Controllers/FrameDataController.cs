using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FightCore.KuroganeHammer.Services;
using FightCore.Services.Games;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FightCore.Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FrameDataController : ControllerBase
    {
        private readonly IKuroganeHammerService _kuroganeHammerService;
        private readonly ICharacterService _characterService;

        public FrameDataController(IKuroganeHammerService kuroganeHammerService,
            ICharacterService characterService)
        {
            _kuroganeHammerService = kuroganeHammerService;
            _characterService = characterService;
        }

        [HttpGet("{characterId}")]
        public async Task<IActionResult> GetForCharacter(long characterId)
        {
            var character = await _characterService.GetWithGameByIdAsync(characterId);

            if (character == null)
            {
                return NotFound();
            }

            switch (character.GameId)
            {
                case 6:
                    return Ok(await _kuroganeHammerService.GetCharacterAttributes(character.Name, "ultimate"));
                default: 
                    return NotFound();
            }
        }
    }
}