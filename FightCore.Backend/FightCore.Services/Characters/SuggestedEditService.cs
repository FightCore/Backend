using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bartdebever.Patterns.Services;
using FightCore.Models;
using FightCore.Models.Characters;
using FightCore.Repositories.Characters;
using FightCore.Repositories.Dtos;

namespace FightCore.Services.Characters
{
    public interface ISuggestedEditService : IService<SuggestedEdit, long, ISuggestedEditRepository>
    {
        Task<List<SuggestedEdit>> GetAllDoneForCharacter(long characterId);

        Task<List<SuggestedEdit>> GetAllOpenForCharacter(long characterId);

        Task<List<SuggestedEdit>> GetOpenEditsForCharacterAndUser(long characterId, long userId);

        Task<List<string>> GetContributorsForEntity(long entityId);

        Task<List<long>> GetPopularCharacterIds();

        Task<List<ContributorDto>> GetTopContributors();
    }

    public class SuggestedEditService : EntityService<SuggestedEdit, ISuggestedEditRepository>, ISuggestedEditService
    {
        public SuggestedEditService(ISuggestedEditRepository repository) : base(repository)
        {
        }

        public Task<List<SuggestedEdit>> GetAllDoneForCharacter(long characterId)
        {
            return Repository.GetAllDoneForCharacter(characterId);
        }

        public Task<List<SuggestedEdit>> GetAllOpenForCharacter(long characterId)
        {
            return Repository.GetAllOpenForCharacter(characterId);
        }

        public Task<List<SuggestedEdit>> GetOpenEditsForCharacterAndUser(long characterId, long userId)
        {
            return Repository.GetEditsForCharacterAndUser(characterId, userId);
        }

        public Task<List<string>> GetContributorsForEntity(long entityId)
        {
            return Repository.GetContributorsForEntity(entityId);
        }

        public Task<List<long>> GetPopularCharacterIds()
        {
            return Repository.GetPopularCharacterId();
        }

        public Task<List<ContributorDto>> GetTopContributors()
        {
            return Repository.GetTopContributors();
        }
    }
}
