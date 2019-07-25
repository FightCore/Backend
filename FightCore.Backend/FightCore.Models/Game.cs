using System;
using System.Collections.Generic;
using System.Text;
using Bartdebever.Patterns.Models;

namespace FightCore.Models
{
    public class Game : BaseEntity
    {
        public string Name { get; set; }

        public string Abbreviation { get; set; }
    }
}
