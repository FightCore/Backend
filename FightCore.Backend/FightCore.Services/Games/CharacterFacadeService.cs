using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FightCore.Models.Characters;
using FightCore.Services.Characters;

namespace FightCore.Services.Games
{
    public interface ICharacterFacadeService
    {
        void UpdateCharacter(Character oldCharacter, Character newCharacter);
    }

    public class CharacterFacadeService : ICharacterFacadeService
    {
        public CharacterFacadeService(
            INotablePlayerService notablePlayerService,
            IWebsiteResourceService websiteResourceService,
            ICharacterVideoService characterVideoService)
        {
            _notablePlayerService = notablePlayerService;
            _characterVideoService = characterVideoService;
            _websiteResourceService = websiteResourceService;
        }

        private readonly INotablePlayerService _notablePlayerService;
        private readonly ICharacterVideoService _characterVideoService;
        private readonly IWebsiteResourceService _websiteResourceService;

        public void UpdateCharacter(Character oldCharacter, Character newCharacter)
        {
            var removedPlayers = oldCharacter.NotablePlayers.Where(notablePlayer =>
                    newCharacter.NotablePlayers.Any(newNotablePlayer => notablePlayer.Id != newNotablePlayer.Id)).ToList();
            foreach (var player in removedPlayers)
            {
                _notablePlayerService.Remove(player);
            }

            foreach (var notablePlayer in newCharacter.NotablePlayers)
            {
                notablePlayer.Character = oldCharacter;
            }

            oldCharacter.NotablePlayers = newCharacter.NotablePlayers;

            var removedVideos = oldCharacter.Videos.Where(notablePlayer =>
                newCharacter.Videos.Any(newNotablePlayer => notablePlayer.Id != newNotablePlayer.Id)).ToList();
            foreach (var video in removedVideos)
            {
                _characterVideoService.Remove(video);
            }

            foreach (var video in newCharacter.Videos)
            {
                video.Character = oldCharacter;
            }

            oldCharacter.Videos = newCharacter.Videos;

            var removedWebsite = oldCharacter.Websites.Where(oldWebsite =>
                newCharacter.Websites.Any(website => oldWebsite.Id != website.Id)).ToList();
            foreach (var website in removedWebsite)
            {
                _websiteResourceService.Remove(website);
            }

            foreach (var website in newCharacter.Websites)
            {
                website.Character = oldCharacter;
            }

            oldCharacter.Websites = newCharacter.Websites;

        }
    }
}
