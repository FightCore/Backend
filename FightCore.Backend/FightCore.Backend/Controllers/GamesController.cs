using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.ViewModels;
using FightCore.Services;
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
    public class GamesController : BaseApiController
    {
        private readonly IGameService _gameService;

        /// <inheritdoc />
        public GamesController(IGameService gameService, IMapper mapper) : base(mapper)
        {
            _gameService = gameService;
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
    }
}