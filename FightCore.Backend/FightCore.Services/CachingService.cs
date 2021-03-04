using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FightCore.Configuration;
using Microsoft.Extensions.Caching.Distributed;

namespace FightCore.Services
{
    public interface ICachingService
    {
        /// <summary>
        /// Adds the <paramref name="value"/> to the caching solution based
        /// on the provided <paramref name="key"/>.
        /// Only works if the caching service is enabled in the settings.
        /// </summary>
        /// <param name="key">The key to be added under.</param>
        /// <param name="value">The value to be set for the key.</param>
        /// <returns>An awaitable task.</returns>
        Task AddAsync(string key, string value);

        /// <summary>
        /// Gets the string from the caching solution based on the <paramref name="key"/>.
        /// Only works if the caching service is enabled in the settings.
        /// </summary>
        /// <param name="key">The key to search for.</param>
        /// <returns>The found value or null.</returns>
        Task<string> GetAsync(string key);

        /// <summary>
        /// Removes an item by it's key from cache.
        /// Only works if the caching service is enabled in the settings.
        /// </summary>
        /// <param name="key">The key that should be removed.</param>
        /// <returns>An awaitable task.</returns>
        Task RemoveAsync(string key);
    }

    public class CachingService : ICachingService
    {
        private readonly IDistributedCache _distributedCache;

        // The tasks should only be executed when the caching service is enabled.
        private readonly bool _enabled;

        /// <summary>
        /// Initializes a new instance of the <see cref="CachingService"/> class.
        /// Reads the configuration if caching is enabled.
        /// </summary>
        /// <param name="distributedCache">
        /// The caching solution to be used.
        /// </param>
        public CachingService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            _enabled = ConfigurationBuilder.Configuration.Caching.Enabled;
        }

        /// <inheritdoc />
        public Task AddAsync(string key, string value)
        {
            return _enabled ? _distributedCache.SetStringAsync(key, value) : Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task<string> GetAsync(string key)
        {
            return _enabled ? _distributedCache.GetStringAsync(key) : Task.FromResult((string)null);
        }

        /// <inheritdoc />
        public Task RemoveAsync(string key)
        {
            return _enabled ? _distributedCache.RemoveAsync(key): Task.CompletedTask;
        }
    }
}
