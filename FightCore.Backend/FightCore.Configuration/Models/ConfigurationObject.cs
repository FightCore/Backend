using System;
using System.Collections.Generic;
using System.Text;

namespace FightCore.Configuration.Models
{
    public class ConfigurationObject
    {
        public EncryptionConfiguration Encryption { get; set; }

        public CachingConfiguration Caching { get; set; }
    }
}
