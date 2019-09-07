using System.Collections.Generic;
using Bartdebever.Patterns.Models;

namespace FightCore.Models.Characters
{
    public class Character : BaseEntity
    {
        public string Name { get; set; }
        
        public Game Game { get; set; }
        
        public long GameId { get; set; }
        
        public string GeneralInformation { get; set; }
        
        public List<NotablePlayer> NotablePlayers { get; set; }
        
        public List<Contributor> Contributors { get; set; }
    }
}