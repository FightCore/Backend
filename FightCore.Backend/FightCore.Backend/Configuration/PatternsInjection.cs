using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FightCore.Data;
using FightCore.Repositories.Fakes.Posts;
using FightCore.Repositories.Posts;
using FightCore.Services;
using FightCore.Services.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FightCore.Backend.Configuration
{
    public static class PatternsInjection
    {
        public static IServiceCollection AddPatterns(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DbContext, ApplicationDbContext>();

            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IGameService, GameService>();

            var parsingSuccess = bool.TryParse(configuration["UseMocking"], out var mocking);
            if (parsingSuccess && mocking)
            {
                // Add the Mocks
                services.AddScoped<IPostRepository, FakePostRepository>();
                services.AddScoped<IGameRepository, FakeGameRepository>();
            }
            else
            {
                // Add the EF Core repositories
                services.AddScoped<IPostRepository, PostRepository>();
                services.AddScoped<IGameRepository, GameRepository>();
            }

            return services;
        }
    }
}
