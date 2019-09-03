using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bartdebever.Patterns.Repositories;
using FightCore.Models.Characters;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Repositories.Games
{
    public interface ICharacterRepository : IRepository<Character, long>
    {
        Task<List<Character>> GetCharactersWithGames();
    }
    
    public class CharacterRepository : EntityRepository<Character>, ICharacterRepository
    {
        public CharacterRepository(DbContext context) : base(context)
        {
        }

        public Task<List<Character>> GetCharactersWithGames()
        {
            return Queryable.Include(character => character.Game).ToListAsync();
        }
    }
}