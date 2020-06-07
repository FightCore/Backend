using FightCore.Data;
using FightCore.KuroganeHammer.Services;
using FightCore.Repositories;
using FightCore.Repositories.Characters;
using FightCore.Repositories.Games;
using FightCore.Repositories.Posts;
using FightCore.Repositories.Users;
using FightCore.Services;
using FightCore.Services.Characters;
using FightCore.Services.Encryption;
using FightCore.Services.Games;
using FightCore.Services.Posts;
using FightCore.Services.Users;
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
            services.AddScoped<DbContext, ApplicationDbContext>()
                .AddScoped<IPostService, PostService>()
                .AddScoped<IGameService, GameService>()
                .AddScoped<ILikeService, LikeService>()
                .AddScoped<IApplicationUserService, ApplicationUserService>()
                .AddScoped<IStageService, StageService>()
                .AddScoped<IApiClientService, ApiClientService>()
                .AddScoped<ICharacterService, CharacterService>()
                .AddScoped<IEncryptionService, AesEncryptionService>()
                .AddScoped<ICachingService, CachingService>()
                .AddScoped<IKuroganeHammerService, KuroganeHammerService>()
                .AddScoped<ITournamentService, TournamentService>()
                .AddScoped<INotablePlayerService, NotablePlayerService>()
                .AddScoped<ICharacterFacadeService, CharacterFacadeService>()
                .AddScoped<IWebsiteResourceService, WebsiteResourceService>()
                .AddScoped<ICharacterVideoService, CharacterVideoService>();

            // Add the EF Core repositories
            services.AddScoped<IPostRepository, PostRepository>()
                .AddScoped<IGameRepository, GameRepository>()
                .AddScoped<ILikeRepository, LikeRepository>()
                .AddScoped<IApplicationUserRepository, ApplicationUserRepository>()
                .AddScoped<IStageRepository, StageRepository>()
                .AddScoped<ICharacterRepository, CharacterRepository>()
                .AddScoped<IApiClientRepository, ApiClientRepository>()
                .AddScoped<ITournamentRepository, TournamentRepository>()
                .AddScoped<IProcessingService, ProcessingService>()
                .AddScoped<INotablePlayerRepository, NotablePlayerRepository>()
                .AddScoped<IWebsiteResourceRepository, WebsiteResourceRepository>()
                .AddScoped<ICharacterVideoRepository, CharacterVideoRepository>();

            return services;
        }
    }
}
