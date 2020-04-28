using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FightCore.Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MatchupsController : ControllerBase
    {
        [HttpGet("{chracterId}")]
        public async Task<IActionResult> GetMatchup(long characterId)
        {
            return NotFound();
        }
    }
}