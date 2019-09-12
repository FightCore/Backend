using System.Collections.Generic;
using System.Threading.Tasks;
using Bartdebever.Patterns.Services;
using FightCore.Models.Characters;
using FightCore.Repositories.Games;

namespace FightCore.Services.Games
{
    public interface IStageService : IService<Stage, long, IStageRepository>
    {
        Task<List<Stage>> GetAllWithGame();

        Task<Stage> GetWithGame(long id);
    }
    
    public class StageService : EntityService<Stage, IStageRepository>, IStageService
    {
        public StageService(IStageRepository repository) : base(repository)
        {
        }

        public Task<List<Stage>> GetAllWithGame()
        {
            return Repository.GetAllWithGame();
        }

        public Task<Stage> GetWithGame(long id)
        {
            return Repository.GetWithGame(id);
        }
    }
}