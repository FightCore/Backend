using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bartdebever.Patterns.Repositories;
using FightCore.Models;
using FightCore.Models.Characters;
using FightCore.Repositories.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Repositories.Characters
{
    public interface ISuggestedEditRepository : IRepository<SuggestedEdit, long>
    {
        Task<List<SuggestedEdit>> GetAllDoneForCharacter(long characterId);

        Task<List<SuggestedEdit>> GetAllOpenForCharacter(long characterId);

        Task<List<SuggestedEdit>> GetEditsForCharacterAndUser(long characterId, long userId);

        Task<List<string>> GetContributorsForEntity(long entityId);

        Task<List<long>> GetPopularCharacterId();

        Task<List<ContributorDto>> GetTopContributors();
    }

    public class SuggestedEditRepository : EntityRepository<SuggestedEdit>, ISuggestedEditRepository
    {
        public SuggestedEditRepository(DbContext context) : base(context)
        {
        }

        public Task<List<SuggestedEdit>> GetAllDoneForCharacter(long characterId)
        {
            return Queryable
                .Include(edit => edit.User)
                .Where(edit => edit.EntityId == characterId && edit.ApprovedByUserId.HasValue)
                .ToListAsync();
        }

        public Task<List<SuggestedEdit>> GetAllOpenForCharacter(long characterId)
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
                .Where(edit => edit.EntityId == entityId && edit.ApprovedByUserId.HasValue)
                .Select(edit => edit.User.Username)
                .Distinct()
                .ToListAsync();
        }

        public Task<List<long>> GetPopularCharacterId()
        {
            return Queryable
                .Select(edit => edit.EntityId)
                .GroupBy(entityId => entityId)
                .OrderByDescending(entityIds => entityIds.Count())
                .Select(grouping => grouping.Key)
                .Take(3)
                .ToListAsync();
        }

        public Task<List<ContributorDto>> GetTopContributors()
        {
            return Queryable.Where(edit => edit.ApprovedByUserId.HasValue)
                .Include(edit => edit.User)
                .GroupBy(edit => edit.UserId)
                .OrderByDescending(grouping => grouping.Count())
                .Select(grouping => new ContributorDto()
                {
                    UserId = grouping.Key,
                    Edits = grouping.Count()
                })
                .Take(5)
                .ToListAsync();
        }
    }
}
