using FightCore.Backend.ViewModels.User;

namespace FightCore.Backend.ViewModels.Edits
{
    public class TopContributorDto
    {
        public UserViewModel User { get; set; }

        public int Edits { get; set; }
    }
}
