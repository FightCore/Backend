using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.ViewModels.Characters;
using FightCore.Backend.ViewModels.Errors;
using FightCore.Backend.ViewModels.Posts;
using FightCore.Models;
using FightCore.Models.Characters;
using FightCore.Services;
using FightCore.Services.Encryption;
using FightCore.Services.Games;
using FightCore.Services.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace FightCore.Backend.Controllers
{
    /// <inheritdoc />
    [Route("[controller]")]
    [ApiController]
    [Produces(HttpContentTypes.ApplicationJson)]
    public class CharactersController : BaseApiController
    {
        private readonly ICharacterService _characterService;
        private readonly ICachingService _cachingService;
        private readonly DbContext _dbContext;
        private readonly IPostService _postService;
        private readonly IEncryptionService _encryptionService;

        /// <inheritdoc />
        public CharactersController(
            ICharacterService characterService,
            IPostService postService,
            IEncryptionService encryptionService,
            DbContext dbContext,
            ICachingService cachingService,
            IMapper mapper) : base(mapper)
        {
            _characterService = characterService;
            _dbContext = dbContext;
            _cachingService = cachingService;
            _postService = postService;
            _encryptionService = encryptionService;
        }
        
        /// <summary>
        /// Gets all characters.
        /// </summary>
        /// <returns>A list of all characters.</returns>
        [HttpGet]
        [SwaggerResponse(200, "All characters known to the system.", typeof(List<GetCharacterListViewModel>))]
        public async Task<IActionResult> GetAllCharacters()
        {
            var cacheKey = $"{nameof(Character)}s";
            var characters = await _characterService.GetAllWithGamesAsync();

            var charactersJson = await _cachingService.GetAsync(cacheKey);

            if (!string.IsNullOrWhiteSpace(charactersJson))
            {
                var cachedViewModels = Deserialize<List<GetCharacterViewModel>>(charactersJson);

                return Ok(cachedViewModels);
            }

            var characterViewModels = Mapper.Map<List<GetCharacterListViewModel>>(characters);
            await _cachingService.AddAsync(cacheKey, Serialize(characterViewModels));

            return Ok(characterViewModels);
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
                return NotFound(NotFoundErrorViewModel.Create(nameof(Character), id));
            }

            return MappedOk<GetCharacterViewModel>(character);
        }

        /// <summary>
        /// Gets a single character based on the provided <paramref name="id"/>.
        /// </summary>
        /// <param name="id">
        /// The id of the <see cref="Character"/> to be searched for.
        /// </param>
        /// <returns>The found <see cref="Character"/></returns>
        [HttpGet("{id}/posts")]
        [SwaggerResponse(200, "The character for the id.", typeof(List<PostViewModel>))]
        public async Task<IActionResult> GetCharacterPosts(long id)
        {
            var posts = await _postService.GetForCharacterIdAsync(id);
            var userId = GetUserIdFromClaims(User);

            foreach (var post in posts)
            {
                post.Body = _encryptionService.Decrypt(post.Body, post.Iv);

                if (!userId.HasValue)
                {
                    continue;
                }

                if (post.Likes.Any(like => like.UserId == userId))
                {
                    post.Liked = true;
                }
            }

            return MappedOk<List<PostViewModel>>(posts);
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

            await _cachingService.RemoveAsync($"{nameof(Character)}s");
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
                return NotFound(NotFoundErrorViewModel.Create(nameof(Character), id));
            }

            var userId = GetUserIdFromClaims(User);

            if (!userId.HasValue)
            {
                return Unauthorized(new UnauthorizedErrorViewModel());
            }

            // If the user is not in the contributors, don't allow him/her to edit the character.
            if (trackedCharacter.Contributors.All(contributor => contributor.UserId != userId))
            {
                return Forbid();
            }

            var character = Mapper.Map<Character>(characterViewModel);

            _characterService.UpdateCharacter(trackedCharacter, character);
            await _dbContext.SaveChangesAsync();

            await _cachingService.RemoveAsync($"{nameof(Character)}s");
            await _cachingService.RemoveAsync($"{nameof(Character)}{character.Id}");

            return Accepted();
        }
    }
}