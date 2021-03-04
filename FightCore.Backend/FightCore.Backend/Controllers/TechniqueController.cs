using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.ViewModels.Techniques;
using FightCore.Services.Techniques;
using Microsoft.AspNetCore.Mvc;

namespace FightCore.Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces(HttpContentTypes.ApplicationJson)]
    public class TechniqueController  : BaseApiController
    {
        private readonly ITechniqueService _techniqueService;

        public TechniqueController(
            ITechniqueService techniqueService,
            IMapper mapper) : base(mapper)
        {
            _techniqueService = techniqueService;
        }

        [HttpGet]
        [Route("games/{gameId:long}/techniques")]
        public async Task<IActionResult> GetAllForGame(long gameId)
        {
            var techniques = await _techniqueService.GetTechniquesForGame(gameId);

            var techniqueDtos = Mapper.Map<List<TechniqueDTO>>(techniques);

            return Ok(techniqueDtos);
        }
    }
}
