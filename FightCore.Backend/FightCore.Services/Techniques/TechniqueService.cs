using System.Collections.Generic;
using System.Threading.Tasks;
using Bartdebever.Patterns.Services;
using FightCore.Models.Techniques;
using FightCore.Repositories.Techniques;

namespace FightCore.Services.Techniques
{

    public interface ITechniqueService : IService<Technique, long, ITechniqueRepository>
    {
        Task<List<Technique>> GetTechniquesForGame(long gameId);
    }

    public class TechniqueService : EntityService<Technique, ITechniqueRepository>, ITechniqueService
    {
        public TechniqueService(ITechniqueRepository repository) : base(repository)
        {
        }

        public Task<List<Technique>> GetTechniquesForGame(long gameId)
        {
            return Repository.GetTechniquesForGame(gameId);
        }
    }
}
