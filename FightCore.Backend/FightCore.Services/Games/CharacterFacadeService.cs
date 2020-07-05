using System.Linq;
using FightCore.Models.Characters;
using FightCore.Models.Enums;
using FightCore.Services.Characters;
using Newtonsoft.Json;

namespace FightCore.Services.Games
{
    public interface ICharacterFacadeService
    {
        void UpdateCharacter(Character oldCharacter, Character newCharacter, long userId);
    }

    public class CharacterFacadeService : ICharacterFacadeService
    {
        public CharacterFacadeService(
            INotablePlayerService notablePlayerService,
            IWebsiteResourceService websiteResourceService,
            ISuggestedEditService suggestedEditService)
        {
            _suggestedEditService = suggestedEditService;
        }

        private readonly ISuggestedEditService _suggestedEditService;

        public void UpdateCharacter(Character oldCharacter, Character newCharacter, long userId)
        {
            if (oldCharacter.GeneralInformation != newCharacter.GeneralInformation)
            {
                _suggestedEditService.Add(new SuggestedEdit()
                {
                    UserId = userId,
                    Editable = Editables.GeneralInformations,
                    EditType = EditType.Edit,
                    Original = oldCharacter.GeneralInformation,
                    Target = newCharacter.GeneralInformation,
                    EntityId = oldCharacter.Id
                });
            }

            foreach (var website in newCharacter.Websites.Where(website => website.Id == 0))
            {
                _suggestedEditService.Add(new SuggestedEdit()
                {
                    UserId = userId,
                    Editable = Editables.Website,
                    EditType = EditType.Add,
                    Target = JsonConvert.SerializeObject(website),
                    EntityId = oldCharacter.Id
                });
            }

            foreach (var removedWebsite in oldCharacter.Websites.Where(website => newCharacter.Websites.All(websiteResource => websiteResource.Id != website.Id)))
            {
                var storedCharacter = removedWebsite.Character;
                removedWebsite.Character = null;
                _suggestedEditService.Add(new SuggestedEdit()
                {
                    UserId = userId,
                    Editable = Editables.Website,
                    EditType = EditType.Remove,
                    Target = JsonConvert.SerializeObject(removedWebsite),
                    EntityId = oldCharacter.Id
                });
                removedWebsite.Character = storedCharacter;
            }

            foreach (var video in newCharacter.Videos.Where(videoResource => videoResource.Id == 0))
            {
                _suggestedEditService.Add(new SuggestedEdit()
                {
                    UserId = userId,
                    Editable = Editables.Video,
                    EditType = EditType.Add,
                    Target = JsonConvert.SerializeObject(video),
                    EntityId = oldCharacter.Id
                });
            }

            foreach (var removedVideo in oldCharacter.Videos.Where(video => newCharacter.Videos.All(videoResource => videoResource.Id != video.Id)))
            {
                var storedCharacter = removedVideo.Character;
                removedVideo.Character = null;
                _suggestedEditService.Add(new SuggestedEdit()
                {
                    UserId = userId,
                    Editable = Editables.Video,
                    EditType = EditType.Remove,
                    Target = JsonConvert.SerializeObject(removedVideo),
                    EntityId = oldCharacter.Id
                });
                removedVideo.Character = storedCharacter;
            }

            foreach (var notablePlayers in newCharacter.NotablePlayers.Where(notablePlayer => notablePlayer.Id == 0))
            {
                _suggestedEditService.Add(new SuggestedEdit()
                {
                    UserId = userId,
                    Editable = Editables.Player,
                    EditType = EditType.Add,
                    Target = JsonConvert.SerializeObject(notablePlayers),
                    EntityId = oldCharacter.Id
                });
            }

            foreach (var removedPlayer in oldCharacter.NotablePlayers.Where(notablePlayer => newCharacter.NotablePlayers.All(player => player.Id != notablePlayer.Id)))
            {
                var storedCharacter = removedPlayer.Character;
                removedPlayer.Character = null;
                _suggestedEditService.Add(new SuggestedEdit()
                {
                    UserId = userId,
                    Editable = Editables.Player,
                    EditType = EditType.Remove,
                    Target = JsonConvert.SerializeObject(removedPlayer),
                    EntityId = oldCharacter.Id
                });
                removedPlayer.Character = storedCharacter;
            }
        }
    }
}
