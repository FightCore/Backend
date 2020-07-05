using System.ComponentModel.DataAnnotations;
using Bartdebever.Patterns.Models;

namespace FightCore.Models.Characters
{
    public class NotablePlayer : BaseEntity
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

        [MaxLength(2)]
        [MinLength(2)]
        public string Country { get; set; }
        
        public Character Character { get; set; }
    }
}