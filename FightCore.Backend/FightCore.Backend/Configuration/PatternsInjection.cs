using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FightCore.Data;
using FightCore.Models;
using FightCore.Repositories;
using FightCore.Repositories.Fakes.Posts;
using FightCore.Repositories.Games;
using FightCore.Repositories.Posts;
using FightCore.Repositories.Users;
using FightCore.Services;
using FightCore.Services.Encryption;
using FightCore.Services.Games;
using FightCore.Services.Posts;
using FightCore.Services.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FightCore.Backend.Configuration
{
    /// <summary>
    /// Static class to add the patterns services to the dependency injection.
    /// </summary>
    public static class PatternsInjection
    {
        /// <summary>
        /// Adds the required dependencies for patterns and all repositories
        /// and services to the <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The service collection to build off.</param>
        /// <param name="configuration">The configuration to read the variables from.</param>
        /// <returns></returns>
        public static IServiceCollection AddPatterns(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DbContext, ApplicationDbContext>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<ILikeService, LikeService>();
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<IStageService, StageService>();
            services.AddScoped<IApiClientService, ApiClientService>();
            services.AddScoped<ICharacterService, CharacterService>();
            services.AddScoped<IEncryptionService, AesEncryptionService>();
            services.AddScoped<ICachingService, CachingService>();

            var parsingSuccess = bool.TryParse(configuration["UseMocking"], out var mocking);
            if (parsingSuccess && mocking)
            {
                // Add the Mocks
                services.AddScoped<IPostRepository, FakePostRepository>();
                services.AddScoped<IGameRepository, FakeGameRepository>();
                services.AddScoped<ILikeRepository, FakeLikeRepository>();
            }
            else
            {
                // Add the EF Core repositories
                services.AddScoped<IPostRepository, PostRepository>();
                services.AddScoped<IGameRepository, GameRepository>();
                services.AddScoped<ILikeRepository, LikeRepository>();
                services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
                services.AddScoped<IStageRepository, StageRepository>();
                services.AddScoped<ICharacterRepository, CharacterRepository>();
                services.AddScoped<IApiClientRepository, ApiClientRepository>();
            }

            return services;
        }
    }
}
