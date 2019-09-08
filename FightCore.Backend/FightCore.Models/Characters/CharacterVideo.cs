using System;
using System.Collections.Generic;
using System.Text;
using Bartdebever.Patterns.Models;
using FightCore.Models.Globals;

namespace FightCore.Models.Characters
{
    /// <summary>
    /// Defines if the character is featured in a video.
    /// Simple Many to Many table.
    /// </summary>
    public class CharacterVideo : BaseEntity
    {
        public VideoResource Video { get; set; }

        public Character Character { get; set; }
    }
}
