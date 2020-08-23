using FightCore.Models;

namespace FightCore.Repositories.Dtos
{
    public class ContributorDto
    {
        public ApplicationUser User { get; set; }

        public long UserId { get; set; }

        public int Edits { get; set; }
    }
}
