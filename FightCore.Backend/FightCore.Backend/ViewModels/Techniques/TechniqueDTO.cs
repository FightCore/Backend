using FightCore.Backend.ViewModels.FrameData;
using FightCore.Backend.ViewModels.User;

namespace FightCore.Backend.ViewModels.Techniques
{
    public class TechniqueDTO
    {
        public UserViewModel Author { get; set; }

        public long AuthorId { get; set; }

        public GameViewModel Game { get; set; }

        public long GameId { get; set; }

        public BasicCharacterViewModel Character { get; set; }

        public long? CharacterId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public bool Verified { get; set; }

        public string Source { get; set; }

        public string ImageUrl { get; set; }
    }
}
