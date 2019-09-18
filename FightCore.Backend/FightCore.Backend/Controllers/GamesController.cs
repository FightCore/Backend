using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.ViewModels;
using FightCore.Backend.ViewModels.Characters;
using FightCore.Backend.ViewModels.Errors;
using FightCore.Models;
using FightCore.Models.Characters;
using FightCore.Services;
using FightCore.Services.Games;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace FightCore.Backend.Controllers
{
    /// <summary>
    /// An endpoint to get information about games.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [Produces(HttpContentTypes.ApplicationJson)]
    public class GamesController : BaseApiController
    {
        private readonly IGameService _gameService;
        private readonly ICharacterService _characterService;
        private readonly ICachingService _cachingService;
        private readonly IStageService _stageService;

        /// <inheritdoc />
        public GamesController(
            IGameService gameService,
            IStageService stageService,
            ICharacterService characterService,
            ICachingService cachingService,
            IMapper mapper) : base(mapper)
        {
            _gameService = gameService;
            _characterService = characterService;
            _stageService = stageService;
            _characterService = characterService;
            _cachingService = cachingService;
        }

        /// <summary>
        /// Get all games
        /// </summary>
        /// <remarks>
        /// Gets all games known in the FightCore API.
        ///
        /// Some of these games might not be in use yet or be deprecated.
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(200, "All of the games in the system", typeof(List<GameViewModel>))]
        public async Task<IActionResult> GetAllGames()
        {
            var cacheKey = $"{nameof(Game)}s";
            var gamesJson = await _cachingService.GetAsync(cacheKey);

            if (!string.IsNullOrWhiteSpace(gamesJson))
            {
                var gameViewModels = JsonConvert.DeserializeObject<List<GameViewModel>>(gamesJson);

                return Ok(gameViewModels);
            }

            var games = await _gameService.GetAllAsync();
            var mappedGames = Mapper.Map<List<GameViewModel>>(games);

            await _cachingService.AddAsync(cacheKey,
                JsonConvert.SerializeObject(mappedGames));

            return Ok(mappedGames);
        }
        
        /// <summary>
        /// Gets a singular game based on the provided <paramref name="gameId"/>.
        /// </summary>
        /// <param name="gameId">The id of the game to be found.</param>
        /// <returns>The game found.</returns>
        [HttpGet("{gameId}")]
        [SwaggerResponse(200, "The game with the given id.", typeof(GameViewModel))]
        [SwaggerResponse(404, "Game is not found.", typeof(NotFoundErrorViewModel))]
        public async Task<IActionResult> GetGame(long gameId)
        {
            var cacheKey = $"{nameof(Game)}{gameId}";

            var gameJson = await _cachingService.GetAsync(cacheKey);

            if (!string.IsNullOrWhiteSpace(gameJson))
            {
                var gameViewmodel = JsonConvert.DeserializeObject<GameViewModel>(gameJson);
                return Ok(gameViewmodel);
            }

            var game = await _gameService.GetByIdAsync(gameId);

            if (game == null)
            {
                return NotFound(NotFoundErrorViewModel.Create(nameof(Game), gameId));
            }

            var gameViewModel = Mapper.Map<GameViewModel>(game);
            await _cachingService.AddAsync(cacheKey, JsonConvert.SerializeObject(gameViewModel));

            return Ok(gameViewModel);
        }

        /// <summary>
        /// Gets all characters from a specific game.
        /// </summary>
        /// <param name="gameId">The id of the <see cref="Game"/> wanting the characters from.</param>
        /// <returns>The characters from that game.</returns>
        [HttpGet("{gameId}/characters")]
        [SwaggerResponse(200, "Gets all characters in a game", typeof(List<GetCharacterListViewModel>))]
        [SwaggerResponse(404, "Game not found or has no characters", typeof(NotFoundErrorViewModel))]
        public async Task<IActionResult> GetAllCharacters(long gameId)
        {
            var characters = await _characterService.FindRangeAsync(character => character.GameId == gameId);

            if (characters == null || !characters.Any())
            {
                return NotFound(NotFoundErrorViewModel.Create(nameof(Character), gameId));
            }
            
            return MappedOk<List<GetCharacterListViewModel>>(characters);
        }
        
        /// <summary>
        /// Gets a character based on the <paramref name="gameId"/> and the <paramref name="characterId"/>.
        /// </summary>
        /// <param name="gameId">
        /// The id of the <see cref="Game"/> to be searched for.
        /// </param>
        /// <param name="characterId">
        /// The id of the <see cref="Character"/> to be searched for.
        /// </param>
        /// <returns>The character found for the specified game.</returns>
        [HttpGet("{gameId}/characters/{characterId}")]
        [SwaggerResponse(404, "Game not found or character not found.", typeof(NotFoundErrorViewModel))]
        [SwaggerResponse(200, "The character requested.", typeof(GetCharacterViewModel))]
        public async Task<IActionResult> GetCharacter(long gameId, long characterId)
        {
            var characterModel = await _characterService
                .FindAsync(character => character.GameId == gameId && character.Id == characterId);

            if (characterModel == null)
            {
                return NotFound(NotFoundErrorViewModel.Create(nameof(Character), characterId));
            }
            
            return MappedOk<GetCharacterViewModel>(characterModel);
        }

        /// <summary>
        /// Gets all stages known for a specific game.
        /// </summary>
        /// <param name="gameId">
        /// The id of the <see cref="Game"/> to be searched for.
        /// </param>
        /// <returns>A list of stages found.</returns>
        [HttpGet("{gameId}/stages")]
        [SwaggerResponse(404, "Game not found or has no stages.", typeof(NotFoundErrorViewModel))]
        [SwaggerResponse(200, "The stages the game has.", typeof(List<Stage>))]
        public async Task<IActionResult> GetAllStages(long gameId)
        {
            var stages = await _stageService.FindRangeAsync(stage => stage.GameId == gameId);
            
            if (stages == null || !stages.Any())
            {
                return NotFound();
            }
            
            return Ok(stages);
        }

        /// <summary>
        /// Gets a stage based on the provided ids for the game and stage.
        /// </summary>
        /// <param name="gameId">
        /// The id of the <see cref="Game"/> to be searched for.
        /// </param>
        /// <param name="stageId">
        /// The id of the <see cref="Stage"/> to be searched for.
        /// </param>
        /// <returns>The found stage.</returns>
        [HttpGet("{gameId}/stages/{stageId}")]
        [SwaggerResponse(404, "Game not found or stage not found.", typeof(NotFoundErrorViewModel))]
        [SwaggerResponse(200, "Stage found for game and stage id.", typeof(Stage))]
        public async Task<IActionResult> GetStage(long gameId, long stageId)
        {
            var stageModel = await _stageService.FindAsync(stage => stage.GameId == gameId && stage.Id == stageId);

            if (stageModel == null)
            {
                return NotFound(NotFoundErrorViewModel.Create(nameof(Stage), stageId));
            }

            return Ok(stageModel);
        }
    }
}