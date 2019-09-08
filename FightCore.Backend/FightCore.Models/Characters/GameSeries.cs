using System;
using System.Collections.Generic;
using System.Text;
using Bartdebever.Patterns.Models;
using FightCore.Models.Globals;

namespace FightCore.Models.Characters
{
    public class GameSeries : BaseEntity
    {
        public string Name { get; set; }

        public FightCoreImage GameIcon { get; set; }
    }
}
