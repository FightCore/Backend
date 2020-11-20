using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.ViewModels.FrameData;
using FightCore.FrameData;
using FightCore.KuroganeHammer.Services;
using FightCore.Services.Games;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FrameDataController : BaseApiController
    {
        private readonly IKuroganeHammerService _kuroganeHammerService;
        private readonly ICharacterService _characterService;
        private readonly FrameDataContext _frameDataContext;

        public FrameDataController(IKuroganeHammerService kuroganeHammerService,
            ICharacterService characterService,
            FrameDataContext frameDataContext,
            IMapper mapper): base(mapper)
        {
            _kuroganeHammerService = kuroganeHammerService;
            _characterService = characterService;
            _frameDataContext = frameDataContext;
        }

        [HttpGet("{characterId}")]
        public async Task<IActionResult> GetForCharacter(long characterId)
        {
            var character = await _characterService.GetWithAllByIdAsync(characterId);

            if (character == null || character.GameId != 2)
            {
                return NotFound();
            }

            switch (character.GameId)
            {
                case 2:
                    var frameData = await _frameDataContext.Characters
                        .Include(@char => @char.CharacterStatistics)
                        .Include(@char => @char.Moves)
                        .ThenInclude(move => move.Hitboxes)
                        .Include(@char => @char.CharacterInfo)
                        .FirstOrDefaultAsync(@char => @char.FightCoreId == characterId);

                    return MappedOk<CharacterFrameDataViewModel>(frameData);
                default:
                    return NotFound();
            }
        }

        [HttpGet("moves")]
        public async Task<IActionResult> GetMoves()
        {
            var moves = await _frameDataContext.Characters
                .Include(@char => @char.CharacterStatistics)
                .Include(@char => @char.Moves)
                .ThenInclude(move => move.Hitboxes)
                .Include(@char => @char.CharacterInfo)
                .ToListAsync();

            return MappedOk<List<CharacterFrameDataViewModel>>(moves);
        }
    }
}