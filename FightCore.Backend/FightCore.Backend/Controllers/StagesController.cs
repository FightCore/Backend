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

        [HttpGet]
        public async Task<IActionResult> GetAllStages()
        {
            var stages = await _stageService.GetAllWithGame();

            return MappedOk<List<StageViewModel>>(stages);
        }

        [HttpGet("{id}")]
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