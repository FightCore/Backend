using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IEditFacadeService _editFacadeService;
        private readonly DbContext _dbContext;

        /// <inheritdoc />
        public SuggestedEditsController(
            IMapper mapper,
            ISuggestedEditService suggestedEditService,
            ICharacterService characterService,
            IEditFacadeService editFacadeService,
            DbContext dbContext
        ) : base(mapper)
        {
            _suggestedEditService = suggestedEditService;
            _editFacadeService = editFacadeService;
            _characterService = characterService;
            _dbContext = dbContext;
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> ApproveEdit([FromRoute]long id)
        {
            var edit = await _suggestedEditService.GetByIdAsync(id);

            if (edit == null || edit.IsApproved)
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

            _editFacadeService.ApproveEdit(edit, character);
            edit.ApprovedByUserId = userId.Value;
            await _dbContext.SaveChangesAsync();

            return Accepted();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteSuggestedEdit(long id)
        {
            var edit = await _suggestedEditService.GetByIdAsync(id);
            if (edit == null || edit.IsApproved)
            {
                return BadRequest();
            }

            var character = await _characterService.GetWithAllByIdAsync(edit.EntityId);

            var userId = GetUserIdFromClaims(User);
            if (!userId.HasValue || character.Contributors.All(contributor => contributor.UserId != userId.Value) && edit.UserId != userId)
            {
                return Unauthorized();
            }

            _suggestedEditService.Remove(edit);
            await _dbContext.SaveChangesAsync();

            return Accepted();
        }
    }
}
