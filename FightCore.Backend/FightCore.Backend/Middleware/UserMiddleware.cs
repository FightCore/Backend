using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using FightCore.Data;
using FightCore.Models;
using FightCore.Services;
using FightCore.Services.Users;
using Jil;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FightCore.Backend.Middleware
{

	public class UserMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<UserMiddleware> _logger;

		public const string UserKey = "User";

		public UserMiddleware(RequestDelegate next, ILogger<UserMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context, IApplicationUserService userService,
			ApplicationDbContext dbContext)
		{
			// Get the user id from the claims using the user_id, this is the Firebase user id.
			var userId = context.User?.Claims?.FirstOrDefault(claim => claim.Type == "user_id")?.Value;

			// Check if it is null, if it is, just continue execution.
			if (string.IsNullOrWhiteSpace(userId))
			{
				await _next(context);
				return;
			}

			// Get the user from the database.
			var user = await userService.GetUserForFirebaseId(userId);

			// If the user is not found, create a new user.
			if (user == null)
			{
				user = new ApplicationUser()
				{
					Username = "TempNewUser",
					FirebaseUserId = userId
				};

				// Use a global try catch to ensure the request goes through. An exception that might happen here could be concurrency.
				// If a user does 2 requests, it can be possible that EF Core tries to save the first user, succeeds
				// tries to save the second user as its not in the context yet, try to create it again and fail to do so.
				// The exception should be logged but the request shouldn't fail.
				try
				{
					dbContext.Add(user);
					await dbContext.SaveChangesAsync();
					_logger.LogInformation("New user with id {userId} and username {username} was created", user.Id,
						user.Username);
				}
				catch (Exception exception)
				{
					_logger.LogWarning(exception, "Failed to create a new user");
					user = null;
				}
			}

			if (user != null && !context.Items.ContainsKey(UserKey))
			{
				context.Items.Add(UserKey, user);
			}

			// Continue executing.
			await _next(context);
		}
	}

	public static class UserMiddlewareExtension
	{
		public static IApplicationBuilder UseUserMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<UserMiddleware>();
		}
	}
}

