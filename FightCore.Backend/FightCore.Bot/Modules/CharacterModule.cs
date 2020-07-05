using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using FightCore.Bot.BotModels.FrameData;
using FightCore.Bot.EmbedCreators.Characters;
using FightCore.Bot.Models.FrameData;
using FightCore.Services.Games;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;

namespace FightCore.Bot.Modules
{
    [Group("character")]
    public class CharacterModule : ModuleBase<SocketCommandContext>
    {
        private readonly ICharacterService _characterService;
        private readonly List<WrapperCharacter> _moves;

        public CharacterModule(ICharacterService characterService)
        {
            _characterService = characterService;
            _moves = JsonConvert.DeserializeObject<List<WrapperCharacter>>(File.ReadAllText("Data/Moves.json"));
        }

        [Command]
        public async Task Info(string character)
        {
            var characterEntity = _moves.FirstOrDefault(wrapperCharacter =>
                wrapperCharacter.Name.Equals(character, StringComparison.InvariantCultureIgnoreCase));

            var fightCoreCharacter = await _characterService.GetWithAllByIdAsync(characterEntity.FightCoreId);

            var embed = new CharacterInfoEmbedCreator().CreateInfoEmbed(fightCoreCharacter);

            await ReplyAsync(string.Empty, embed: embed);
        }

        [Command("move")]
        public async Task FrameDataTest(string character, [Remainder] string move)
        {
            var characterEntity = _moves.FirstOrDefault(wrapperCharacter =>
                wrapperCharacter.Name.Equals(character, StringComparison.InvariantCultureIgnoreCase));

            var fightCoreCharacter = await _characterService.GetWithAllByIdAsync(characterEntity.FightCoreId);

            var moveEntity = characterEntity.Moves.FirstOrDefault(storedMove =>
                storedMove.Name.Equals(move, StringComparison.InvariantCultureIgnoreCase));
            var embed = new CharacterInfoEmbedCreator().CreateMoveEmbed(characterEntity, moveEntity,
                fightCoreCharacter);
            await ReplyAsync(string.Empty, embed: embed);
        }

        [Command("moves")]
        public async Task ListMoves(string character)
        {
            var characterEntity = _moves.FirstOrDefault(wrapperCharacter =>
                wrapperCharacter.Name.Equals(character, StringComparison.InvariantCultureIgnoreCase));

            var fightCoreCharacter = await _characterService.GetWithAllByIdAsync(characterEntity.FightCoreId);

            var embed = new CharacterInfoEmbedCreator().CreateMoveListEmbed(characterEntity, fightCoreCharacter);
            await ReplyAsync(string.Empty, embed: embed);
        }
    }
}
