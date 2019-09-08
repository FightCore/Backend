using System;
using System.Collections.Generic;
using System.Text;
using Bartdebever.Patterns.Models;

namespace FightCore.Models.Globals
{
    public class VideoResource : BaseEntity
    {
        public string YoutubeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
