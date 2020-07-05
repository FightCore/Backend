using System;
using System.Linq;
using FightCore.Models.Characters;
using FightCore.Models.Enums;
using FightCore.Models.Globals;
using FightCore.Services.Games;
using Newtonsoft.Json;

namespace FightCore.Services.Characters
{
    public interface IEditFacadeService
    {
        void ApproveEdit(SuggestedEdit edit, Character character);
    }

    public class EditFacadeService : IEditFacadeService
    {
        private readonly ICharacterService _characterService;

        public EditFacadeService(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        public void ApproveEdit(SuggestedEdit edit, Character character)
        {
            switch (edit.Editable)
            {
                case Editables.GeneralInformations:
                    edit.Original = character.GeneralInformation;
                    character.GeneralInformation = edit.Target;
                    break;
                case Editables.Website:
                    switch (edit.EditType)
                    {
                        case EditType.Add:
                            var website = JsonConvert.DeserializeObject<WebsiteResource>(edit.Target);
                            website.CharacterId = character.Id;
                            character.Websites.Add(website);
                            break;
                        case EditType.Edit:
                            break;
                        case EditType.Remove:
                            var websiteId = JsonConvert.DeserializeObject<WebsiteResource>(edit.Target).Id;
                            character.Websites.Remove(character.Websites.FirstOrDefault(websiteResource =>
                                websiteResource.Id == websiteId));
                            break;
                    }

                    break;
                case Editables.Video:
                    switch (edit.EditType)
                    {
                        case EditType.Add:
                            var videoResource = JsonConvert.DeserializeObject<VideoResource>(edit.Target);
                            videoResource.Character = character;
                            character.Videos.Add(videoResource);
                            break;
                        case EditType.Edit:
                            break;
                        case EditType.Remove:
                            var websiteId = JsonConvert.DeserializeObject<VideoResource>(edit.Target).Id;
                            character.Videos.Remove(character.Videos.FirstOrDefault(websiteResource => websiteResource.Id == websiteId));
                            break;
                    }

                    break;
                case Editables.Player:
                    switch (edit.EditType)
                    {
                        case EditType.Add:
                            var player = JsonConvert.DeserializeObject<NotablePlayer>(edit.Target);
                            player.Character = character;
                            character.NotablePlayers.Add(player);
                            break;
                        case EditType.Edit:
                            break;
                        case EditType.Remove:
                            var playerId = JsonConvert.DeserializeObject<NotablePlayer>(edit.Target).Id;
                            character.NotablePlayers.Remove(character.NotablePlayers.FirstOrDefault(notablePlayer => notablePlayer.Id == playerId));
                            break;
                    }

                    break;
            }
        }
    }
}
