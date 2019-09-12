using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.ViewModels.Errors;
using FightCore.Backend.ViewModels.User;
using FightCore.Models;
using FightCore.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace FightCore.Backend.Controllers
{
    /// <inheritdoc />
    [Route("[controller]")]
    [ApiController]
    [Produces(HttpContentTypes.ApplicationJson)]
    public class AccountsController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DbContext _dbContext;
        private readonly IApplicationUserService _applicationUserService;

        /// <inheritdoc />
        public AccountsController(
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            DbContext context,
            IApplicationUserService applicationUserService) : base(mapper)
        {
            _userManager = userManager;
            _applicationUserService = applicationUserService;
            _dbContext = context;
        }

        /// <summary>
        /// Gets the user based on the provided <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>The gotten user.</returns>
        [HttpGet("{id}")]
        [SwaggerResponse(200, "The gotten user.", typeof(UserViewModel))]
        [SwaggerResponse(404, "The user was not found.", typeof(NotFoundErrorViewModel))]
        public async Task<IActionResult> GetUser(long id)
        {
            var user = await _applicationUserService.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound(NotFoundErrorViewModel.Create(nameof(User), id));
            }

            return MappedOk<UserViewModel>(user);
        }

        /// <summary>
        /// Updates the user using the data provided in the body.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);

#pragma warning disable 162
            var userId = GetUserIdFromClaims(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _applicationUserService.GetByIdAsync(userId.Value);

            // Update with ViewModel

            await _userManager.UpdateAsync(user);

            return Ok();
#pragma warning restore 162
        }

        /// <summary>
        /// Creates a new user for the provided body.
        /// </summary>
        /// <param name="viewModel">The body containing the data for the new user.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel viewModel)
        {
            var user = new ApplicationUser()
            {
                UserName = viewModel.UserName,
                Email = viewModel.Email
            };

            await _userManager.CreateAsync(user, viewModel.Password);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Deletes the user based on the provided id.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser()
        {
            await Task.Delay(1);
            return StatusCode(StatusCodes.Status501NotImplemented);
        }
    }
}