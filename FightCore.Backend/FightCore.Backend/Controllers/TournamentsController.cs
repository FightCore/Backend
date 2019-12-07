using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FightCore.Backend.Controllers
{
    /// <inheritdoc />
    [Route("[controller]")]
    [ApiController]
    public class TournamentsController : BaseApiController
    {
        private readonly ITournamentService _tournamentService;

        /// <inheritdoc />
        public TournamentsController(ITournamentService tournamentService, IMapper mapper) : base(mapper)
        {
            _tournamentService = tournamentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTournaments()
        {
            return Ok(_tournamentService.GetTournaments(1, "nl"));
        }
    }
}