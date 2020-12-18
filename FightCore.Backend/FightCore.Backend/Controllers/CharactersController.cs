using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.ViewModels.Characters;
using FightCore.Backend.ViewModels.Characters.Edits;
using FightCore.Backend.ViewModels.Errors;
using FightCore.Backend.ViewModels.Posts;
using FightCore.Backend.ViewModels.User;
using FightCore.Models.Characters;
using FightCore.Models.Enums;
using FightCore.Services;
using FightCore.Services.Characters;
using FightCore.Services.Games;
using FightCore.Services.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IProcessingService _processingService;
        private readonly ICharacterFacadeService _characterFacadeService;
        private readonly ISuggestedEditService _suggestedEditService;

        /// <inheritdoc />
        public CharactersController(
            ICharacterService characterService,
            ICharacterFacadeService characterFacadeService,
            IPostService postService,
            IProcessingService processingService,
            DbContext dbContext,
            ICachingService cachingService,
            ISuggestedEditService suggestedEditService,
            IMapper mapper) : base(mapper)
        {
            _characterService = characterService;
            _dbContext = dbContext;
            _cachingService = cachingService;
            _postService = postService;
            _processingService = processingService;
            _characterFacadeService = characterFacadeService;
            _suggestedEditService = suggestedEditService;
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
            var character = await _characterService.GetWithAllByIdAsync(id);

            if (character == null)
            {
                return NotFound(NotFoundErrorViewModel.Create(nameof(Character), id));
            }

            var mappedCharacter = Mapper.Map<GetCharacterViewModel>(character);
            var contributors = await _suggestedEditService.GetContributorsForEntity(id);

            contributors = contributors.Where(contributor =>
                mappedCharacter.Contributors.All(contributor2 => contributor2.User.Name != contributor)).ToList();

            mappedCharacter.Contributors.AddRange(contributors.Select(name =>
                new ContributorViewModel()
                {
                    ContributorType = ContributorType.Contributor,
                    User = new UserViewModel()
                    {
                        Name = name
                    }
                }).ToList());
            return Ok(mappedCharacter);
        }

        [HttpGet("popular")]
        public async Task<IActionResult> GetPopularCharacters()
        {
            var characterIds = await _suggestedEditService.GetPopularCharacterIds();

            var characters = await _characterService.GetWithAllByIdsAsync(characterIds, false);
            var characterViewModels = Mapper.Map<List<GetCharacterListViewModel>>(characters);
            return Ok(characterViewModels);
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

            posts = _processingService.ProcessPosts(posts, userId);

            return MappedOk<List<PostViewModel>>(posts);
        }

        [HttpGet("{id}/edits")]
        [Authorize]
        public async Task<IActionResult> GetEditsForCharacter(long id)
        {
            var userId = GetUserIdFromClaims(User);
            var character = await _characterService.GetWithAllByIdAsync(id);
            List<SuggestedEdit> edits = null;

            if (character.Contributors.Any(contributor => contributor.UserId == userId))
            {
                edits = await _suggestedEditService.GetAllOpenForCharacter(id);

            }
            else
            {
                edits = await _suggestedEditService.GetOpenEditsForCharacterAndUser(id, userId.Value);
            }

            var editDtos = Mapper.Map<List<SuggestedEditViewModel>>(edits);
            return Ok(editDtos);

        }

        [HttpGet("{id}/edits/history")]
        public async Task<IActionResult> GetHistory(long id)
        {
            var edits = await _suggestedEditService.GetAllDoneForCharacter(id);

            var editViewModels = Mapper.Map<List<SuggestedEditViewModel>>(edits);
            return Ok(editViewModels);
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

            character = _characterService.Add(character);
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
            var currentCharacter = await _characterService.GetWithAllByIdAsync(id, false);

            if (currentCharacter == null)
            {
                return NotFound(NotFoundErrorViewModel.Create(nameof(Character), id));
            }

            var userId = GetUserIdFromClaims(User);

            if (!userId.HasValue)
            {
                return Unauthorized(new UnauthorizedErrorViewModel());
            }


            var character = Mapper.Map<Character>(characterViewModel);
            _characterFacadeService.UpdateCharacter(currentCharacter, character, userId.Value);
            await _dbContext.SaveChangesAsync();

            return Accepted();
        }
    }
}