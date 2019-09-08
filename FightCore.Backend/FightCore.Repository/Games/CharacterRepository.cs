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
        
        Task<Character> GetWithGameByIdAsync(long id);
    }
    
    public class CharacterRepository : EntityRepository<Character>, ICharacterRepository
    {
        public CharacterRepository(DbContext context) : base(context)
        {
        }

        public Task<List<Character>> GetCharactersWithGames()
        {
            return Queryable.Include(character => character.Game)
                .Include(character => character.StockIcon)
                .Include(character => character.Series)
                .ThenInclude(gameSeries => gameSeries.GameIcon)
                .ToListAsync();
        }

        public Task<Character> GetWithGameByIdAsync(long id)
        {
            return Queryable.Include(character => character.Game)
                .Include(character => character.StockIcon)
                .Include(character => character.CharacterImage)
                .Include(character => character.NotablePlayers)
                .Include(character => character.Contributors)
                .Include(character => character.Videos)
                .ThenInclude(characterVideo => characterVideo.Video)
                .Include(character => character.Series)
                .ThenInclude(gameSeries => gameSeries.GameIcon)
                .FirstOrDefaultAsync(character => character.Id == id);
        }
    }
}