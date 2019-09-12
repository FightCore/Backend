using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.ViewModels.Errors;
using FightCore.Backend.ViewModels.Stages;
using FightCore.Models.Characters;
using FightCore.Services.Games;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FightCore.Backend.Controllers
{
    /// <inheritdoc />
    [Route("[controller]")]
    [ApiController]
    public class StagesController : BaseApiController
    {
        private readonly IStageService _stageService;

        /// <inheritdoc />
        public StagesController(IMapper mapper, IStageService stageService) : base(mapper)
        {
            _stageService = stageService;
        }

        /// <summary>
        /// Gets all stages.
        /// </summary>
        /// <returns>A list of all stages.</returns>
        [SwaggerResponse(200, "All of the stages", typeof(List<StageViewModel>))]
        [HttpGet]
        public async Task<IActionResult> GetAllStages()
        {
            var stages = await _stageService.GetAllWithGame();

            return MappedOk<List<StageViewModel>>(stages);
        }

        /// <summary>
        /// Gets a stage based on the provided <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id of the stage to be gotten.</param>
        /// <returns>The stage found.</returns>
        [HttpGet("{id}")]
        [SwaggerResponse(200, "All of the stages", typeof(StageViewModel))]
        [SwaggerResponse(404, "The stage was not found", typeof(NotFoundErrorViewModel))]
        public async Task<IActionResult> GetStage(long id)
        {
            var stage = await _stageService.GetWithGame(id);

            if (stage == null)
            {
                return NotFound(NotFoundErrorViewModel.Create(nameof(Stage), id));
            }

            return MappedOk<StageViewModel>(stage);
        }

    }
}