using System;
using System.Collections.Generic;
using System.Text;
using Bartdebever.Patterns.Models;

namespace FightCore.Models.Globals
{
    public class InformationSource : BaseEntity
    {
        public string Url { get; set; }

        public string DisplayName { get; set; }
    }
}
