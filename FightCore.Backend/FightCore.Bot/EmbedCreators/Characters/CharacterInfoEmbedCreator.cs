using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using FightCore.Bot.EmbedCreators.Base;
using FightCore.Models.Characters;

namespace FightCore.Bot.EmbedCreators.Characters
{
    public class CharacterInfoEmbedCreator : BaseEmbedCreator
    {
        public Embed CreateInfoEmbed(Character character)
        {
            var embedBuilder = new EmbedBuilder();

            embedBuilder.Title = character.Name;
            embedBuilder.WithDescription(ShortenDescription(character.GeneralInformation));
            if (character.StockIcon != null)
                embedBuilder.WithThumbnailUrl(character.StockIcon.Url);

            if (character.CharacterImage != null)
                embedBuilder.WithImageUrl(character.CharacterImage.Url);

            embedBuilder.WithUrl($"https://www.fightcore.gg/character/{character.Id}");

            return embedBuilder.Build();
        }
    }
}
