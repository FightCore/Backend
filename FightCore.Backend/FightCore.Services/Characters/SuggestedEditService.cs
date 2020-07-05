using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bartdebever.Patterns.Services;
using FightCore.Models.Characters;
using FightCore.Repositories.Characters;

namespace FightCore.Services.Characters
{
    public interface ISuggestedEditService : IService<SuggestedEdit, long, ISuggestedEditRepository>
    {
        Task<List<SuggestedEdit>> GetAllForCharacter(long characterId);

        Task<List<SuggestedEdit>> GetEditsForCharacterAndUser(long characterId, long userId);
    }

    public class SuggestedEditService : EntityService<SuggestedEdit, ISuggestedEditRepository>, ISuggestedEditService
    {
        public SuggestedEditService(ISuggestedEditRepository repository) : base(repository)
        {
        }

        public Task<List<SuggestedEdit>> GetAllForCharacter(long characterId)
        {
            return Repository.GetAllForCharacter(characterId);
        }

        public Task<List<SuggestedEdit>> GetEditsForCharacterAndUser(long characterId, long userId)
        {
            return Repository.GetEditsForCharacterAndUser(characterId, userId);
        }
    }
}
