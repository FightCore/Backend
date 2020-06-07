using Bartdebever.Patterns.Repositories;
using FightCore.Models.Characters;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Repositories.Characters
{
    public interface ICharacterVideoRepository : IRepository<CharacterVideo, long>
    {

    }
    public class CharacterVideoRepository : EntityRepository<CharacterVideo>, ICharacterVideoRepository
    {
        public CharacterVideoRepository(DbContext context) : base(context)
        {
        }
    }
}
