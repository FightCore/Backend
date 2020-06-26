using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using FightCore.Bot.EmbedCreators.Base;

namespace FightCore.Bot.EmbedCreators.Game
{
    public class GameEmbedCreator : BaseEmbedCreator
    {
        public Embed CreateGameEmbed(FightCore.Models.Game game)
        {
            var embedBuilder = new EmbedBuilder();
            embedBuilder.WithTitle(game.Name);
            embedBuilder.WithDescription(ShortenDescription(game.Description));

            if (game.Icon != null)
                embedBuilder.WithThumbnailUrl(game.Icon.Url);

            embedBuilder.WithImageUrl(game.BannerUrl);
            embedBuilder.WithUrl($"https://www.fightcore.gg/game/{game.Id}");

            return embedBuilder.Build();
        }
    }
}
