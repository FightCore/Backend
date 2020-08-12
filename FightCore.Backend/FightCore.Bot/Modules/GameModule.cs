using System.Threading.Tasks;
using Discord.Commands;
using FightCore.Bot.Configuration;
using FightCore.Bot.EmbedCreators.Game;
using FightCore.Services;
using Microsoft.Extensions.Options;

namespace FightCore.Bot.Modules
{
    [Group("game")]
    public class GameModule : ModuleBase<SocketCommandContext>
    {
        private readonly IGameService _gameService;
        private readonly bool _isEnabled;

        public GameModule(IGameService gameService, IOptions<ModuleSettings> moduleSettings)
        {
            _gameService = gameService;
            _isEnabled = moduleSettings.Value.Games;
        }

        [Command()]
        public async Task Info([Remainder]string abbreviation)
        {
            if (!_isEnabled)
            {
                return;
            }

            var game = await _gameService.GetByAbbreviationAsync(abbreviation);

            if (game == null)
            {
                await ReplyAsync("Not found");
                return;
            }

            var embed = new GameEmbedCreator(null).CreateGameEmbed(game);

            await ReplyAsync(string.Empty, embed: embed);
        }
    }
}
