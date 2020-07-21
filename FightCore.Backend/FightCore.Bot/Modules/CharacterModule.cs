using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Discord.Commands;
using FightCore.Bot.EmbedCreators.Characters;
using FightCore.Bot.Services;
using FightCore.Services.Games;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Bot.Modules
{
    [Group("character")]
    [Alias("c")]
    public class CharacterModule : ModuleBase<SocketCommandContext>
    {
        private readonly ICharacterService _characterService;
        private readonly FrameDataService _frameDataService;

        public CharacterModule(ICharacterService characterService, FrameDataService frameDataService)
        {
            _characterService = characterService;
            _frameDataService = frameDataService;
        }

        [Command]
        public async Task Info(string character)
        {
            using (Context.Channel.EnterTypingState())
            {
                var characterEntity = _frameDataService.GetCharacter(character);

                var fightCoreCharacter = await _characterService.GetWithAllByIdAsync(characterEntity.FightCoreId);

                var embed = new CharacterInfoEmbedCreator().CreateInfoEmbed(fightCoreCharacter);

                await ReplyAsync(string.Empty, embed: embed);
            }
        }

        [Command("move")]
        [Alias("m")]
        public async Task FrameDataTest(string character, [Remainder] string move)
        {
            using (Context.Channel.EnterTypingState())
            {
                var characterEntity = _frameDataService.GetCharacter(character);

                var fightCoreCharacter = await _characterService.GetWithAllByIdAsync(characterEntity.FightCoreId);
                var attack = _frameDataService.GetMove(character, move);

                if (attack == null)
                {
                    await ReplyAsync("Not found");
                    return;
                }

                var embed = new CharacterInfoEmbedCreator().CreateMoveEmbed(characterEntity, attack, fightCoreCharacter);

                await ReplyAsync(string.Empty, embed: embed);
            }
        }

        [Command("moves")]
        public async Task ListMoves([Remainder] string character)
        {
            using (Context.Channel.EnterTypingState())
            {
                var characterEntity = _frameDataService.GetCharacter(character);
                var moves = _frameDataService.GetMoves(characterEntity.NormalizedName);

                var fightCoreCharacter = await _characterService.GetWithAllByIdAsync(characterEntity.FightCoreId);

                var embed = new CharacterInfoEmbedCreator().CreateMoveListEmbed(characterEntity, moves, fightCoreCharacter);
                await ReplyAsync(string.Empty, embed: embed);
            }
        }
    }
}
