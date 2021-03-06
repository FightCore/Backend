using Bartdebever.Patterns.Models;

namespace FightCore.Models.Characters
{
    public class Stage : BaseEntity
    {
        public string Name { get; set; }
        
        public Game Game { get; set; }
        
        public long GameId { get; set; }
    }
}