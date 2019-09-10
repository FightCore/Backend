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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace FightCore.Backend.Controllers
{
    /// <inheritdoc />
    [Route("[controller]")]
    [ApiController]
    [Produces(HttpContentTypes.APPLICATION_JSON)]
    public class CharactersController : BaseApiController
    {
        private readonly ICharacterService _characterService;
        private readonly DbContext _dbContext;

        /// <inheritdoc />
        public CharactersController(
            ICharacterService characterService,
            DbContext dbContext,
            IMapper mapper) : base(mapper)
        {
            _characterService = characterService;
            _dbContext = dbContext;
        }
        
        /// <summary>
        /// Gets all characters.
        /// </summary>
        /// <returns>A list of all characters.</returns>
        [HttpGet]
        [SwaggerResponse(200, "All characters known to the system.", typeof(List<GetCharacterListViewModel>))]
        public async Task<IActionResult> GetAllCharacters()
        {
            var characters = await _characterService.GetAllWithGamesAsync();

            return MappedOk<List<GetCharacterListViewModel>>(characters);
        }

        /// <summary>
        /// Gets a single character based on the provided <paramref name="id"/>.
        /// </summary>
        /// <param name="id">
        /// The id of the <see cref="Character"/> to be searched for.
        /// </param>
        /// <returns>The found <see cref="Character"/></returns>
        [HttpGet("{id}")]
        [SwaggerResponse(200, "The character for the id.", typeof(List<GetCharacterListViewModel>))]
        [SwaggerResponse(404, "No character found for that id.", typeof(NotFoundErrorViewModel))]
        public async Task<IActionResult> GetCharacter(long id)
        {
            var character = await _characterService.GetWithGameByIdAsync(id);

            if (character == null)
            {
                return NotFound(NotFoundErrorViewModel.Create(ErrorEntities.CharacterEntity, id));
            }

            return MappedOk<GetCharacterViewModel>(character);
        }

        /// <summary>
        /// Creates a new character based on the provided body.
        /// </summary>
        /// <param name="characterViewModel">
        /// The character to be created.
        /// </param>
        /// <returns>
        /// Where the character is created.
        /// </returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCharacter(CreateCharacterViewModel characterViewModel)
        {
            var character = Mapper.Map<Character>(characterViewModel);

            character = await _characterService.AddAsync(character);
            await _dbContext.SaveChangesAsync();

            return Created("characters", character.Id);
        }

        /// <summary>
        /// Updates a character based on the provided <paramref name="id"/> and body.
        /// </summary>
        /// <param name="id">
        /// The id of the existing character to override.
        /// </param>
        /// <param name="characterViewModel">
        /// The new values to override it with.
        /// </param>
        /// <returns>
        /// If the action was executed successfully.
        /// </returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCharacter(long id, UpdateCharacterViewModel characterViewModel)
        {
            var trackedCharacter = await _characterService.GetByIdAsync(id);

            if (trackedCharacter == null)
            {
                return NotFound(NotFoundErrorViewModel.Create(ErrorEntities.CharacterEntity, id));
            }

            var userId = GetUserIdFromClaims(User);

            if (!userId.HasValue)
            {
                return Unauthorized();
            }

            // If the user is not in the contributors, don't allow him/her to edit the character.
            if (trackedCharacter.Contributors.All(contributor => contributor.UserId != userId))
            {
                return Forbid();
            }

            var character = Mapper.Map<Character>(characterViewModel);

            _characterService.UpdateCharacter(trackedCharacter, character);
            await _dbContext.SaveChangesAsync();

            return Accepted();
        }
    }
}