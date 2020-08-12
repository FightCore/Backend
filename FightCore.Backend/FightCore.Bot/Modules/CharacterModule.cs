using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.Commands;
using FightCore.Bot.Configuration;
using FightCore.Bot.EmbedCreators;
using FightCore.Bot.EmbedCreators.Characters;
using FightCore.Bot.Services;
using FightCore.Services.Games;
using Microsoft.Extensions.Options;

namespace FightCore.Bot.Modules
{
    [Group("character")]
    [Alias("c")]
    public class CharacterModule : ModuleBase<SocketCommandContext>
    {
        private readonly ICharacterService _characterService;
        private readonly FrameDataService _frameDataService;
        private readonly CharacterInfoEmbedCreator _characterInfoEmbedCreator;
        private readonly NotFoundEmbedCreator _notFoundEmbedCreator;
        private readonly bool _isEnabled;

        public CharacterModule(
            ICharacterService characterService,
            FrameDataService frameDataService,
            CharacterInfoEmbedCreator characterInfoEmbedCreator,
            NotFoundEmbedCreator notFoundEmbedCreator,
            IOptions<ModuleSettings> moduleSettings)
        {
            _characterService = characterService;
            _frameDataService = frameDataService;
            _characterInfoEmbedCreator = characterInfoEmbedCreator;
            _notFoundEmbedCreator = notFoundEmbedCreator;
            _isEnabled = moduleSettings.Value.Moves;
        }

        [Command]
        public async Task Info(string character)
        {
            if (!_isEnabled)
            {
                return;
            }

            using (Context.Channel.EnterTypingState())
            {
                var characterEntity = _frameDataService.GetCharacter(character);
                if (characterEntity == null)
                {
                    var notFoundEmbed = _notFoundEmbedCreator.Create(new Dictionary<string, string>()
                        {{"Character", character}});
                    await ReplyAsync("", embed: notFoundEmbed);
                    return;
                }

                var fightCoreCharacter = await _characterService.GetWithAllByIdAsync(characterEntity.FightCoreId);
                var misc = _frameDataService.GetMiscForCharacter(characterEntity.NormalizedName);
                var embed = _characterInfoEmbedCreator.CreateInfoEmbed(fightCoreCharacter, misc);

                await ReplyAsync(string.Empty, embed: embed);
            }
        }

        [Command("move")]
        [Alias("m")]
        public async Task FrameDataTest(string character, [Remainder] string move)
        {
            if (!_isEnabled)
            {
                return;
            }

            using (Context.Channel.EnterTypingState())
            {
                var characterEntity = _frameDataService.GetCharacter(character);

                if (characterEntity == null)
                {
                    var notFoundEmbed = _notFoundEmbedCreator.Create(new Dictionary<string, string>()
                        {{"Character", character}});
                    await ReplyAsync("", embed: notFoundEmbed);
                    return;
                }

                var fightCoreCharacter = await _characterService.GetWithAllByIdAsync(characterEntity.FightCoreId);
                var attack = _frameDataService.GetMove(character, move);

                if (attack == null)
                {
                    var notFoundEmbed = _notFoundEmbedCreator.Create(new Dictionary<string, string>()
                        {{"Character", character}, {"Move", move}});
                    await ReplyAsync("", embed: notFoundEmbed);
                    return;
                }

                var embed = _characterInfoEmbedCreator.CreateMoveEmbed(characterEntity, attack, fightCoreCharacter);

                await ReplyAsync(string.Empty, embed: embed);
            }
        }

        [Command("moves")]
        public async Task ListMoves([Remainder] string character)
        {
            if (!_isEnabled)
            {
                return;
            }

            using (Context.Channel.EnterTypingState())
            {
                var characterEntity = _frameDataService.GetCharacter(character);

                if (characterEntity == null)
                {
                    var notFoundEmbed = _notFoundEmbedCreator.Create(new Dictionary<string, string>()
                        {{"Character", character}});
                    await ReplyAsync("", embed: notFoundEmbed);
                    return;
                }

                var moves = _frameDataService.GetMoves(characterEntity.NormalizedName);

                var fightCoreCharacter = await _characterService.GetWithAllByIdAsync(characterEntity.FightCoreId);

                var embed = _characterInfoEmbedCreator.CreateMoveListEmbed(characterEntity, moves, fightCoreCharacter);
                await ReplyAsync(string.Empty, embed: embed);
            }
        }
    }
}
