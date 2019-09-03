using System.Collections.Generic;
using System.Threading.Tasks;
using Bartdebever.Patterns.Services;
using FightCore.Models.Characters;
using FightCore.Repositories.Games;

namespace FightCore.Services.Games
{
    public interface ICharacterService : IService<Character, long, ICharacterRepository>
    {
        void UpdateCharacter(Character trackedCharacter, Character character);

        Task<List<Character>> GetCharactersWithGames();
    }
    
    public class CharacterService : EntityService<Character, ICharacterRepository>, ICharacterService
    {
        public CharacterService(ICharacterRepository repository) : base(repository)
        {
        }

        public void UpdateCharacter(Character trackedCharacter, Character character)
        {
            trackedCharacter.Name = character.Name;
        }

        public Task<List<Character>> GetCharactersWithGames()
        {
            return Repository.GetCharactersWithGames();
        }
    }
}