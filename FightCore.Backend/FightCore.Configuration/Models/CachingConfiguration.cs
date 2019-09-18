using System;
using System.Collections.Generic;
using System.Text;

namespace FightCore.Configuration.Models
{
    public class CachingConfiguration
    {
        /// <summary>
        /// Defines if the caching solution should be used.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// The hostname of the server where the Redis cache is hosted.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// The instance to be used for FightCore's caching.
        /// </summary>
        public string Instance { get; set; }
    }
}
