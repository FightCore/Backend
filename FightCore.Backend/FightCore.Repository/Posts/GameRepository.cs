using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bartdebever.Patterns.Repositories;
using FightCore.Models;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Repositories.Posts
{
    public interface IGameRepository : IRepository<Game, long>
    {
        Task<List<Game>> GetAllGamesAsync();

        Task<Game> GetGameById(long id);

        Task<Game> GetByAbbreviationAsync(string abbreviation);
    }

    public class GameRepository : EntityRepository<Game>, IGameRepository
    {
        public GameRepository(DbContext context) : base(context)
        {
        }

        public Task<List<Game>> GetAllGamesAsync()
        {
            return IncludedQueryable.ToListAsync();
        }

        public Task<Game> GetGameById(long id)
        {
            return IncludedQueryable.FirstOrDefaultAsync(game => game.Id == id);
        }

        public Task<Game> GetByAbbreviationAsync(string abbreviation)
        {
            return IncludedQueryable.FirstOrDefaultAsync(game =>
                game.Abbreviation.ToLower() == abbreviation);
        }

        private IQueryable<Game> IncludedQueryable =>
            Queryable.Include(game => game.Icon);
    }
}
