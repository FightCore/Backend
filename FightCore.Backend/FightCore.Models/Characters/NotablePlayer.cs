using Bartdebever.Patterns.Models;

namespace FightCore.Models.Characters
{
    public class NotablePlayer : BaseEntity
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public Character Character { get; set; }
    }
}