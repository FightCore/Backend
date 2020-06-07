using Bartdebever.Patterns.Services;
using FightCore.Models.Characters;
using FightCore.Repositories.Characters;

namespace FightCore.Services.Characters
{
    public interface ICharacterVideoService : IService<CharacterVideo, long, ICharacterVideoRepository>
    {

    }
    public class CharacterVideoService : EntityService<CharacterVideo, ICharacterVideoRepository>, ICharacterVideoService
    {
        public CharacterVideoService(ICharacterVideoRepository repository) : base(repository)
        {
        }
    }
}
