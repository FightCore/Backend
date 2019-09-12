using System.Collections.Generic;
using System.Threading.Tasks;
using Bartdebever.Patterns.Repositories;
using FightCore.Models.Characters;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Repositories.Games
{
    public interface IStageRepository : IRepository<Stage, long>
    {
        Task<List<Stage>> GetAllWithGame();

        Task<Stage> GetWithGame(long id);
    }
    
    public class StageRepository : EntityRepository<Stage>, IStageRepository
    {
        public StageRepository(DbContext context) : base(context)
        {
        }

        public Task<List<Stage>> GetAllWithGame()
        {
            return Queryable
                .Include(stage => stage.Game)
                .ToListAsync();
        }

        public Task<Stage> GetWithGame(long id)
        {
            return Queryable
                .Include(stage => stage.Game)
                .FirstOrDefaultAsync(stage => stage.Id == id);
        }
    }
}