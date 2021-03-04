using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.ViewModels.FrameData;
using FightCore.FrameData;
using FightCore.Services.Games;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FrameDataController : BaseApiController
    {
        private readonly ICharacterService _characterService;
        private readonly FrameDataContext _frameDataContext;

        public FrameDataController(
            ICharacterService characterService,
            FrameDataContext frameDataContext,
            IMapper mapper): base(mapper)
        {
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
                        .AsSplitQuery()
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
                .AsSplitQuery()
                .ToListAsync();

            return MappedOk<List<CharacterFrameDataViewModel>>(moves);
        }

        [HttpGet("moves/{moveId}")]
        public async Task<IActionResult> GetMove(long moveId)
        {
            var move = await _frameDataContext.Move
                .Include(move => move.Hitboxes)
                .Include(move => move.Character)
                .AsSplitQuery()
                .FirstOrDefaultAsync(move => move.Id == moveId);

            return MappedOk<ExtendedMoveViewModel>(move);
        }

        [HttpGet("characters")]
        public async Task<IActionResult> GetCharacters()
        {
            var characters = await _frameDataContext.Characters
                .Include(character => character.CharacterStatistics)
                .Include(character => character.CharacterInfo)
                .ToListAsync();

            return MappedOk<List<CharacterFrameDataViewModel>>(characters);
        }
    }
}