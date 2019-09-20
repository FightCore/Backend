using System;
using System.Collections.Generic;
using System.Text;
using Bartdebever.Patterns.Models;

namespace FightCore.Models
{
    public class ApiClient : BaseEntity
    {
        public string ApiKey { get; set; }

        public string Name { get; set; }

        // Following 2 variables are unused.
        public string ContactEmail { get; set; }

        public long RateLimit { get; set; }
    }
}
