using Bartdebever.Patterns.Models;
using FightCore.Models.Characters;

namespace FightCore.Models.Techniques
{
    public class Technique : BaseEntity
    {
        public ApplicationUser Author { get; set; }

        public long AuthorId { get; set; }

        public Game Game { get; set; }

        public long GameId { get; set; }

        public Character Character { get; set; }

        public long? CharacterId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public bool Verified { get; set; }

        public string Source { get; set; }

        public string ImageUrl { get; set; }
    }
}
