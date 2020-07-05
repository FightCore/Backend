using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord;
using FightCore.Bot.BotModels.FrameData;
using FightCore.Bot.EmbedCreators.Base;
using FightCore.Bot.Models.FrameData;
using FightCore.Models.Characters;

namespace FightCore.Bot.EmbedCreators.Characters
{
    public class CharacterInfoEmbedCreator : BaseEmbedCreator
    {
        public Embed CreateInfoEmbed(Character character)
        {
            var embedBuilder = new EmbedBuilder();

            embedBuilder.Title = character.Name;
            embedBuilder.AddField("General information",ShortenString(character.GeneralInformation, 300));

            if (character.NotablePlayers.Any())
            {
                embedBuilder.AddField("Notable players",
                    string.Join(", ", character.NotablePlayers.Take(6).Select(player => player.Name))
                );
            }

            if (character.StockIcon != null)
                embedBuilder.WithThumbnailUrl(character.StockIcon.Url);

            if (character.CharacterImage != null)
                embedBuilder.WithImageUrl(character.CharacterImage.Url);

            embedBuilder.WithUrl($"https://www.fightcore.gg/character/{character.Id}");
            embedBuilder = AddFightCoreFooter(embedBuilder);
            return embedBuilder.Build();
        }

        public Embed CreateMoveEmbed(WrapperCharacter character, Move move, Character fightCoreCharacter)
        {
            var embedBuilder = new EmbedBuilder();
            embedBuilder.WithUrl(character.Source);
            embedBuilder.WithThumbnailUrl(fightCoreCharacter.StockIcon.Url);
            embedBuilder.WithImageUrl(
                move.GifURL);
            embedBuilder.Title = $"{character.Name} - {move.Name}";

            var descriptionBuilder = new StringBuilder();
            AddString("Frames", move.TotalFrames, descriptionBuilder);
            AddString("Hit", move.HitFrames, descriptionBuilder);
            AddString("IASA", move.IASA, descriptionBuilder);
            AddString("Auto cancel", move.AutoCancel, descriptionBuilder);
            AddString("Land lag", move.LandLag, descriptionBuilder);
            AddString("L-Canceled", move.LCanceled, descriptionBuilder);

            foreach (var keyValuePair in move.Extra)
            {
                AddString(keyValuePair.Key, keyValuePair.Value, descriptionBuilder);
            }

            embedBuilder.Description = descriptionBuilder.ToString();
            embedBuilder = AddFightCoreFooter(embedBuilder);
            return embedBuilder.Build();
        }

        public Embed CreateMoveListEmbed(WrapperCharacter character, Character fightCoreCharacter)
        {
            var embedBuilder = new EmbedBuilder();
            embedBuilder.WithUrl(character.Source);
            embedBuilder.WithThumbnailUrl(fightCoreCharacter.StockIcon.Url);
            embedBuilder.Title = $"{character.Name} - Moves";

            embedBuilder.AddField("Moves", ShortenField(
                string.Join(", ", character.Moves.Select(move => move.Name))));
            embedBuilder.AddField("Help", "To check out a move use:\n`-character move {CHARACTER NAME} {MOVE NAME}`\n" +
                                          "For example: `-character move Fox u-smash`");


            embedBuilder = AddFightCoreFooter(embedBuilder);
            return embedBuilder.Build();
        }

        private StringBuilder AddString(string key, string value, StringBuilder stringBuilder)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return stringBuilder;
            }

            stringBuilder.Append($"**{key}:** {value}\n");

            return stringBuilder;
        }
    }
}
