using System;
using System.Collections.Generic;
using System.Text;
using Bartdebever.Patterns.Models;
using FightCore.Models.Characters;
using FightCore.Models.Globals;

namespace FightCore.Models
{
    public class Game : BaseEntity
    {
        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public string BannerUrl { get; set; }
        
        public List<Character> Characters { get; set; }
        
        public List<Stage> Stages { get; set; }

        public string Description { get; set; }

        public FightCoreImage Icon { get; set; }
    }
}
