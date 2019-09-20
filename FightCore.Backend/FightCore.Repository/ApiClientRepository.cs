using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bartdebever.Patterns.Repositories;
using FightCore.Models;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Repositories
{
    public interface IApiClientRepository : IRepository<ApiClient, long>
    {
        Task<ApiClient> GetForKeyAsync(string apiKey);
    }
    public class ApiClientRepository : EntityRepository<ApiClient>, IApiClientRepository
    {
        public ApiClientRepository(DbContext context) : base(context)
        {
        }

        public Task<ApiClient> GetForKeyAsync(string apiKey)
        {
            return Queryable.FirstOrDefaultAsync(apiClient => apiClient.ApiKey == apiKey);
        }
    }
}
