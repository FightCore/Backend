using System;
using System.Collections.Generic;
using System.Text;
using FightCore.Configuration.Models;
using Microsoft.Extensions.Configuration;

namespace FightCore.Configuration
{
    public static class ConfigurationBuilder
    {
        public static ConfigurationObject Configuration { get; set; }

        public static void Build(IConfiguration configuration)
        {
            Configuration = new ConfigurationObject()
            {
                Encryption = new EncryptionConfiguration()
                {
                    Algorithm = "AES",
                    Key = configuration["Encryption:Key"]
                }
            };
        }
    }
}
