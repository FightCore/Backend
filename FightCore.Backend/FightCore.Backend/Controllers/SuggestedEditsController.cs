using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Models.Enums;
using FightCore.Services.Characters;
using FightCore.Services.Games;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Backend.Controllers
{
    /// <inheritdoc />
    [Route("edits")]
    [ApiController]
    public class SuggestedEditsController : BaseApiController
    {
        private readonly ISuggestedEditService _suggestedEditService;
        private readonly ICharacterService _characterService;
        private readonly DbContext _dbContext;

        /// <inheritdoc />
        public SuggestedEditsController(
            IMapper mapper,
            ISuggestedEditService suggestedEditService,
            ICharacterService characterService,
            DbContext dbContext
        ) : base(mapper)
        {
            _suggestedEditService = suggestedEditService;
            _characterService = characterService;
            _dbContext = dbContext;
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> ApproveEdit([FromRoute]long id)
        {
            var edit = await _suggestedEditService.GetByIdAsync(id);

            if (edit.IsApproved)
            {
                return BadRequest();
            }

            var userId = GetUserIdFromClaims(User);
            if (!userId.HasValue)
            {
                return Unauthorized();
            }

            var character = await _characterService.GetWithAllByIdAsync(edit.EntityId);

            if (character == null)
            {
                return NotFound();
            }

            if (character.Contributors.All(contributor => contributor.UserId != userId))
            {
                return Unauthorized();
            }

            switch (edit.Editable)
            {
                case Editables.GeneralInformations:
                    edit.Original = character.GeneralInformation;
                    character.GeneralInformation = edit.Target;
                    break;
            }

            edit.ApprovedByUserId = userId.Value;
            await _dbContext.SaveChangesAsync();

            return Accepted();
        }
    }
}
