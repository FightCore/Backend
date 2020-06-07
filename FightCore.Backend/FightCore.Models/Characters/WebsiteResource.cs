using Bartdebever.Patterns.Models;

namespace FightCore.Models.Characters
{
    public class WebsiteResource : BaseEntity
    {
        public Character Character { get; set; }

        public long CharacterId { get; set; }

        public string WebsiteUrl { get; set; }

        public string Title { get; set; }
    }
}
