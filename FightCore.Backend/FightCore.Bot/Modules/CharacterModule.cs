using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using FightCore.Bot.EmbedCreators.Characters;
using FightCore.Services.Games;

namespace FightCore.Bot.Modules
{
    [Group("character")]
    public class CharacterModule : ModuleBase<SocketCommandContext>
    {
        private readonly ICharacterService _characterService;

        public CharacterModule(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [Command]
        public async Task Info(long characterId)
        {
            var character = await _characterService.GetWithGameByIdAsync(characterId);

            var embed = new CharacterInfoEmbedCreator().CreateInfoEmbed(character);

            await ReplyAsync(string.Empty, embed: embed);
        }
    }
}
