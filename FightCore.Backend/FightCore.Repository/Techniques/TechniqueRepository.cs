using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bartdebever.Patterns.Repositories;
using FightCore.Models.Techniques;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Repositories.Techniques
{
    public interface ITechniqueRepository : IRepository<Technique, long>
    {
        Task<List<Technique>> GetTechniquesForGame(long gameId);
    }

    public class TechniqueRepository : EntityRepository<Technique>, ITechniqueRepository
    {
        public TechniqueRepository(DbContext context) : base(context)
        {
        }

        public Task<List<Technique>> GetTechniquesForGame(long gameId)
        {
            return Queryable.Where(technique => technique.GameId == gameId).ToListAsync();
        }
    }
}
