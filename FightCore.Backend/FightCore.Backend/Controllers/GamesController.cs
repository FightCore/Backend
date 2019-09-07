using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.Error;
using FightCore.Backend.ViewModels;
using FightCore.Backend.ViewModels.Characters;
using FightCore.Backend.ViewModels.Errors;
using FightCore.Models;
using FightCore.Models.Characters;
using FightCore.Services;
using FightCore.Services.Games;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FightCore.Backend.Controllers
{
    /// <summary>
    /// An endpoint to get information about games.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [Produces(HttpContentTypes.APPLICATION_JSON)]
    public class GamesController : BaseApiController
    {
        private readonly IGameService _gameService;

        private readonly ICharacterService _characterService;

        private readonly IStageService _stageService;

        /// <inheritdoc />
        public GamesController(
            IGameService gameService,
            IStageService stageService,
            ICharacterService characterService,
            IMapper mapper) : base(mapper)
        {
            _gameService = gameService;
            _characterService = characterService;
            _stageService = stageService;
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
            var games = await _gameService.GetAllAsync();

            return MappedOk<List<GameViewModel>>(games);
        }
        
        [HttpGet("{gameId}")]
        [SwaggerResponse(200, "All of the games in the system", typeof(List<GameViewModel>))]
        public async Task<IActionResult> GetGame(long gameId)
        {
            var game = await _gameService.GetByIdAsync(gameId);

            if (game == null)
            {
                return NotFound(NotFoundErrorViewModel.Create(ErrorEntities.GameEntity, gameId));
            }
            
            return MappedOk<GameViewModel>(game);
        }

        /// <summary>
        /// Gets all characters from a specific game.
        /// </summary>
        /// <param name="gameId">The id of the <see cref="Game"/> wanting the characters from.</param>
        /// <returns>The characters from that game.</returns>
        [HttpGet("{gameId}/characters")]
        [SwaggerResponse(200, "Gets all characters in a game", typeof(List<Character>))]
        public async Task<IActionResult> GetAllCharacters(long gameId)
        {
            var characters = await _characterService.FindRangeAsync(character => character.GameId == gameId);

            if (characters == null || !characters.Any())
            {
                return NotFound(NotFoundErrorViewModel.Create(ErrorEntities.GameEntity, gameId));
            }
            
            return MappedOk<List<GetCharacterListViewModel>>(characters);
        }
        
        [HttpGet("{gameId}/characters/{characterId}")]
        public async Task<IActionResult> GetCharacter(long gameId, long characterId)
        {
            var characterModel = await _characterService
                .FindAsync(character => character.GameId == gameId && character.Id == characterId);

            if (characterModel == null)
            {
                return NotFound(NotFoundErrorViewModel.Create(ErrorEntities.CharacterEntity, characterId));
            }
            
            return MappedOk<GetCharacterViewModel>(characterModel);
        }

        [HttpGet("{gameId}/stages")]
        public async Task<IActionResult> GetAllStages(long gameId)
        {
            var stages = await _stageService.FindRangeAsync(stage => stage.GameId == gameId);
            
            if (stages == null || !stages.Any())
            {
                return NotFound();
            }
            
            return Ok(stages);
        }

        [HttpGet("{gameId}/stages/{stageId}")]
        public async Task<IActionResult> GetStage(long gameId, long stageId)
        {
            var stageModel = await _stageService.FindAsync(stage => stage.GameId == gameId && stage.Id == stageId);

            if (stageModel == null)
            {
                return NotFound(NotFoundErrorViewModel.Create(ErrorEntities.StageEntity, stageId));
            }

            return Ok(stageModel);
        }
    }
}