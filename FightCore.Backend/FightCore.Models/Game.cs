using System;
using System.Collections.Generic;
using System.Text;
using Bartdebever.Patterns.Models;
using FightCore.Models.Characters;

namespace FightCore.Models
{
    public class Game : BaseEntity
    {
        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public string BannerUrl { get; set; }
        
        public List<Character> Characters { get; set; }
        
        public List<Stage> Stages { get; set; }
    }
}
