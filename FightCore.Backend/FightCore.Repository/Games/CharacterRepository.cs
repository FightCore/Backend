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
        Task<List<Character>> GetCharactersByGameAsync(long gameId);

        Task<List<Character>> GetCharactersWithGames();
        
        Task<Character> GetWithFullIncludeByIdAsync(long id, bool enableTracking = true);
    }
    
    public class CharacterRepository : EntityRepository<Character>, ICharacterRepository
    {
        public CharacterRepository(DbContext context) : base(context)
        {
        }

        public Task<List<Character>> GetCharactersWithGames()
        {
            return MinimalInclude.ToListAsync();
        }

        public Task<Character> GetWithFullIncludeByIdAsync(long id, bool enableTracking = true)
        {
            var queryable = FullInclude;
            if (!enableTracking)
            {
                queryable = queryable.AsNoTracking();
            }

            return queryable.FirstOrDefaultAsync(character => character.Id == id);
        }

        public Task<List<Character>> GetCharactersByGameAsync(long gameId)
        {
            return MinimalInclude.Where(character => character.GameId == gameId).ToListAsync();
        }

        private IQueryable<Character> MinimalInclude =>
            Queryable.Include(character => character.Game)
                .Include(character => character.StockIcon)
                .Include(character => character.Series)
                .ThenInclude(gameSeries => gameSeries.GameIcon);

        private IQueryable<Character> FullInclude =>
            Queryable.Include(character => character.Game)
                .Include(character => character.StockIcon)
                .Include(character => character.InformationSources)
                .Include(character => character.CharacterImage)
                .Include(character => character.NotablePlayers)
                .Include(character => character.Contributors)
                .ThenInclude(contributor => contributor.User)
                .Include(character => character.Videos)
                .Include(character => character.Series)
                .ThenInclude(gameSeries => gameSeries.GameIcon)
                .Include(character => character.Websites);
    }
}