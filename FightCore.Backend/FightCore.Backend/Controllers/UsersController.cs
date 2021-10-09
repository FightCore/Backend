using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.ViewModels.User;
using FightCore.Data;
using FightCore.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Backend.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class UsersController : BaseApiController
	{
		private readonly IApplicationUserService _applicationUserService;
		private readonly DbContext _dbContext;

		public UsersController(
			IMapper mapper,
			IApplicationUserService applicationUserService, DbContext dbContext) : base(mapper)
		{
			_applicationUserService = applicationUserService;
			_dbContext = dbContext;
		}

		[HttpGet("me")]
		[Authorize]
		public async Task<IActionResult> GetCurrentUser()
		{
			var user = GetUser();

			if (user == null)
			{
				return Unauthorized();
			}

			return Ok(Mapper.Map<UserViewModel>(user));
		}

		[HttpPut("me")]
		[Authorize]
		public async Task<IActionResult> UpdateUser(UpdateUserViewModel userViewModel)
		{
			var user = GetUser();

			if (user == null)
			{
				return Unauthorized();
			}

			user.Username = userViewModel.Username;
			_applicationUserService.Update(user);
			await _dbContext.SaveChangesAsync();

			return Ok(Mapper.Map<UserViewModel>(user));
		}
	}
}
