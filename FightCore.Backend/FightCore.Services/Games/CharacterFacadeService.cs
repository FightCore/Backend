using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FightCore.Models.Characters;
using FightCore.Models.Enums;
using FightCore.Services.Characters;

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
            ICharacterVideoService characterVideoService,
            ISuggestedEditService suggestedEditService)
        {
            _notablePlayerService = notablePlayerService;
            _characterVideoService = characterVideoService;
            _websiteResourceService = websiteResourceService;
            _suggestedEditService = suggestedEditService;
        }

        private readonly INotablePlayerService _notablePlayerService;
        private readonly ICharacterVideoService _characterVideoService;
        private readonly IWebsiteResourceService _websiteResourceService;
        private readonly ISuggestedEditService _suggestedEditService;

        public void UpdateCharacter(Character oldCharacter, Character newCharacter, long userId)
        {
            if (oldCharacter.GeneralInformation != newCharacter.GeneralInformation)
            {
                _suggestedEditService.Add(new SuggestedEdit()
                {
                    UserId = userId,
                    Editable = Editables.GeneralInformations,
                    Original = oldCharacter.GeneralInformation,
                    Target = newCharacter.GeneralInformation,
                    EntityId = oldCharacter.Id
                });
            }
        }
    }
}
