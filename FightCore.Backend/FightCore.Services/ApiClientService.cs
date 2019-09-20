using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bartdebever.Patterns.Services;
using FightCore.Models;
using FightCore.Repositories;

namespace FightCore.Services
{
    public interface IApiClientService : IService<ApiClient, long, IApiClientRepository>
    {
        /// <summary>
        /// Gets the <see cref="ApiClient"/> object for the provided <paramref name="apiKey"/>.
        /// </summary>
        /// <param name="apiKey">The provided api key.</param>
        /// <returns>The found client.</returns>
        Task<ApiClient> GetForKeyAsync(string apiKey);
    }

    public class ApiClientService : EntityService<ApiClient, IApiClientRepository>, IApiClientService
    {
        public ApiClientService(IApiClientRepository repository) : base(repository)
        {
        }

        public Task<ApiClient> GetForKeyAsync(string apiKey)
        {
            return Repository.GetForKeyAsync(apiKey);
        }
    }
}
