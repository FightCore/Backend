using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.Error;
using FightCore.Backend.ViewModels.Characters;
using FightCore.Backend.ViewModels.Errors;
using FightCore.Models.Characters;
using FightCore.Services.Games;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces(HttpContentTypes.APPLICATION_JSON)]
    public class CharactersController : BaseApiController
    {
        private readonly ICharacterService _characterService;
        private readonly DbContext _dbContext;

        public CharactersController(
            ICharacterService characterService,
            DbContext dbContext,
            IMapper mapper) : base(mapper)
        {
            _characterService = characterService;
            _dbContext = dbContext;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllCharacters()
        {
            var characters = await _characterService.GetAllWithGamesAsync();

            return MappedOk<List<GetCharacterViewModel>>(characters);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCharacter(long id)
        {
            var character = await _characterService.GetWithGameByIdAsync(id);

            if (character == null)
            {
                return NotFound(NotFoundErrorViewModel.Create(ErrorEntities.CharacterEntity, id));
            }

            return MappedOk<GetCharacterViewModel>(character);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCharacter(CreateCharacterViewModel characterViewModel)
        {
            var character = Mapper.Map<Character>(characterViewModel);

            character = await _characterService.AddAsync(character);
            await _dbContext.SaveChangesAsync();

            return Created("characters", character.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCharacter(long id, UpdateCharacterViewModel characterViewModel)
        {
            var trackedCharacter = await _characterService.GetByIdAsync(id);

            if (trackedCharacter == null)
            {
                return NotFound(NotFoundErrorViewModel.Create(ErrorEntities.CharacterEntity, id));
            }

            var character = Mapper.Map<Character>(characterViewModel);

            _characterService.UpdateCharacter(trackedCharacter, character);
            await _dbContext.SaveChangesAsync();

            return Accepted();
        }
    }
}