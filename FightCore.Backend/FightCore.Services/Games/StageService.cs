using Bartdebever.Patterns.Services;
using FightCore.Models.Characters;
using FightCore.Repositories.Games;

namespace FightCore.Services.Games
{
    public interface IStageService : IService<Stage, long, IStageRepository>
    {
    }
    
    public class StageService : EntityService<Stage, IStageRepository>, IStageService
    {
        public StageService(IStageRepository repository) : base(repository)
        {
        }
    }
}