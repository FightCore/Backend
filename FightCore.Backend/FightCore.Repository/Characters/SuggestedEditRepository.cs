using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bartdebever.Patterns.Repositories;
using FightCore.Models.Characters;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Repositories.Characters
{
    public interface ISuggestedEditRepository : IRepository<SuggestedEdit, long>
    {
        Task<List<SuggestedEdit>> GetAllForCharacter(long characterId);

        Task<List<SuggestedEdit>> GetEditsForCharacterAndUser(long characterId, long userId);

        Task<List<string>> GetContributorsForEntity(long entityId);

        Task<List<long>> GetPopularCharacterId();
    }

    public class SuggestedEditRepository : EntityRepository<SuggestedEdit>, ISuggestedEditRepository
    {
        public SuggestedEditRepository(DbContext context) : base(context)
        {
        }

        public Task<List<SuggestedEdit>> GetAllForCharacter(long characterId)
        {
            return Queryable
                .Include(edit => edit.User)
                .Where(edit => edit.EntityId == characterId && edit.ApprovedByUserId == null)
                .ToListAsync();
        }

        public Task<List<SuggestedEdit>> GetEditsForCharacterAndUser(long characterId, long userId)
        {
            return Queryable
                .Include(edit => edit.User)
                .Where(edit => edit.EntityId == characterId && edit.ApprovedByUserId == null
                    && edit.UserId == userId)
                .ToListAsync();
        }

        public Task<List<string>> GetContributorsForEntity(long entityId)
        {
            return Queryable.Include(edit => edit.User)
                .Where(edit => edit.EntityId == entityId)
                .Select(edit => edit.User.UserName)
                .Distinct()
                .ToListAsync();
        }

        public Task<List<long>> GetPopularCharacterId()
        {
            return Queryable.GroupBy(edit => edit.EntityId)
                .OrderByDescending(entityIds => entityIds.Count())
                .Take(5)
                .Select(key => key.Key)
                .ToListAsync();
        }
    }
}
