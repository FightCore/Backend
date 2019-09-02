using Bartdebever.Patterns.Services;
using FightCore.Models.Characters;
using FightCore.Repositories.Games;

namespace FightCore.Services.Games
{
    public interface ICharacterService : IService<Character, long, ICharacterRepository>
    {
    }
    
    public class CharacterService : EntityService<Character, ICharacterRepository>, ICharacterService
    {
        public CharacterService(ICharacterRepository repository) : base(repository)
        {
        }
    }
}