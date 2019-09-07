using Bartdebever.Patterns.Models;
using FightCore.Models.Enums;

namespace FightCore.Models.Characters
{
    public class Contributor : BaseEntity
    {
        public ApplicationUser User { get; set; }
        
        public long UserId { get; set; }
        
        public Character Character { get; set; }
        
        public long CharacterId { get; set; }
        
        public ContributorType ContributorType { get; set; }
    }
}