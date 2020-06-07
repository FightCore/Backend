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

        Task<List<Character>> GetAllWithGamesAsync();

        Task<Character> GetWithGameByIdAsync(long id, bool enableTracking = true);

        Task<List<Character>> GetCharactersByGameAsync(long gameId);
    }
    
    public class CharacterService : EntityService<Character, ICharacterRepository>, ICharacterService
    {
        public CharacterService(ICharacterRepository repository) : base(repository)
        {
        }

        public void UpdateCharacter(Character trackedCharacter, Character character)
        {
            trackedCharacter.GeneralInformation = character.GeneralInformation;

            // Be sure to set the references to the character correctly.
            foreach (var video in character.Videos)
            {
                video.Character = character;
            }
            foreach (var player in character.NotablePlayers)
            {
                player.Character = character;
            }
            foreach (var website in character.Websites)
            {
                website.Character = character;
            }

            trackedCharacter.Videos = character.Videos;
            trackedCharacter.NotablePlayers = character.NotablePlayers;
            trackedCharacter.Websites = character.Websites;
        }

        public Task<List<Character>> GetAllWithGamesAsync()
        {
            return Repository.GetCharactersWithGames();
        }

        public Task<Character> GetWithGameByIdAsync(long id, bool enableTracking = true)
        {
            return Repository.GetWithFullIncludeByIdAsync(id, enableTracking);
        }

        public Task<List<Character>> GetCharactersByGameAsync(long gameId)
        {
            return Repository.GetCharactersByGameAsync(gameId);
        }
    }
}