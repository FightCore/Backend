using System;
using System.Collections.Generic;
using System.Text;
using Bartdebever.Patterns.Models;

namespace FightCore.Models.Globals
{
    public class FightCoreImage : BaseEntity
    {
        public string Url { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
