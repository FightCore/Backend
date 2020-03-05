using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using FightCore.Bot.EmbedCreators.Game;
using FightCore.Services;

namespace FightCore.Bot.Modules
{
    [Group("game")]
    public class GameModule : ModuleBase<SocketCommandContext>
    {
        private readonly IGameService _gameService;

        public GameModule(IGameService gameService)
        {
            _gameService = gameService;
        }

        [Command()]
        public async Task Info([Remainder]string abbreviation)
        {
            var game = await _gameService.GetByAbbreviationAsync(abbreviation);

            if (game == null)
            {
                await ReplyAsync("Not found");
                return;
            }

            var embed = new GameEmbedCreator().CreateGameEmbed(game);

            await ReplyAsync(string.Empty, embed: embed);
        }
    }
}
