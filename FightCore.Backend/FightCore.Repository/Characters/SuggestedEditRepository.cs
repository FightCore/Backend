﻿using System;
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
    }
}