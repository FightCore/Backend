using Bartdebever.Patterns.Repositories;
using FightCore.Models.Characters;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Repositories.Games
{
    public interface IStageRepository : IRepository<Stage, long>
    {
    }
    
    public class StageRepository : EntityRepository<Stage>, IStageRepository
    {
        public StageRepository(DbContext context) : base(context)
        {
        }
    }
}