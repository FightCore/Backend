using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.ViewModels.User;
using FightCore.Models;
using FightCore.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DbContext _dbContext;
        private readonly IApplicationUserService _applicationUserService;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(long id)
        {
            var user = await _applicationUserService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return MappedOk<UserViewModel>(user);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);

            var userId = GetUserIdFromClaims(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _applicationUserService.GetByIdAsync(userId.Value);

            // Update with ViewModel

            await _userManager.UpdateAsync(user);

            return Ok();
        }

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

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }
    }
}