using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord;
using FightCore.Bot.Configuration;
using FightCore.Bot.EmbedCreators.Base;
using FightCore.Bot.Helpers;
using FightCore.Bot.Models.FrameData;
using FightCore.MeleeFrameData;
using FightCore.Models.Characters;
using Microsoft.Extensions.Options;

namespace FightCore.Bot.EmbedCreators.Characters
{
    public class CharacterInfoEmbedCreator : BaseEmbedCreator
    {
        private readonly EmbedSettings _embedSettings;

        public CharacterInfoEmbedCreator(IOptions<EmbedSettings> embedSettings)
        {
            _embedSettings = embedSettings.Value;
        }

        public Embed CreateInfoEmbed(Character character, Misc misc)
        {
            var embedBuilder = new EmbedBuilder {Title = character.Name};

            if (_embedSettings.FightCoreInfo)
            {
                embedBuilder.AddField("General information", ShortenString(character.GeneralInformation, 250));

                if (character.NotablePlayers.Any())
                {
                    embedBuilder.AddField("Notable players",
                        string.Join(", ", character.NotablePlayers.Take(6).Select(player => player.Name))
                    );
                }
            }

            if (character.StockIcon != null)
                embedBuilder.WithThumbnailUrl(character.StockIcon.Url);

            if (character.CharacterImage != null)
                embedBuilder.WithImageUrl(character.CharacterImage.Url);

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"**Weight:** {misc.Weight}");
            stringBuilder.AppendLine($"**Gravity:** {misc.Gravity}");
            stringBuilder.AppendLine($"**Walk speed:** {misc.WalkSpeed}");
            stringBuilder.AppendLine($"**Run speed:** {misc.RunSpeed}");
            stringBuilder.AppendLine($"**Wave dash length (rank):** {misc.WaveDashLengthRank}");
            stringBuilder.AppendLine($"**PLA Intangibility Frames:** {misc.PLAIntangibilityFrames}");
            stringBuilder.AppendLine($"**Can wall jump:** {misc.CanWallJump}");
            stringBuilder.AppendLine($"**Jump squad:** {misc.JumpSquat}");
            embedBuilder.AddField("Frame data", stringBuilder.ToString());

            embedBuilder.WithUrl($"https://www.fightcore.gg/character/{character.Id}");
            embedBuilder = AddFooter(embedBuilder);
            return embedBuilder.Build();
        }

        public Embed CreateMoveEmbed(WrapperCharacter character, NormalizedEntity move, Character fightCoreCharacter)
        {
            return move switch
            {
                Attack attack => CreateAttackEmbed(character, attack, fightCoreCharacter),
                Dodge dodge => CreateDodgeEmbed(character, dodge, fightCoreCharacter),
                Grab grab => CreateGrabEmbed(character, grab, fightCoreCharacter),
                Throw @throw => CreateThrowEmbed(character, @throw, fightCoreCharacter),
                _ => throw new NotImplementedException()
            };
        }

        public Embed CreateMoveListEmbed(WrapperCharacter character, List<NormalizedEntity> moves, Character fightCoreCharacter)
        {
            var embedBuilder = new EmbedBuilder();
            embedBuilder.WithUrl($"http://meleeframedata.com/{character.NormalizedName}")
                .WithThumbnailUrl(fightCoreCharacter.StockIcon.Url.Replace(" ", "+"));
            embedBuilder.Title = $"{character.Name} - Moves";

            embedBuilder.AddField("Moves", ShortenField(
                string.Join(", ", moves.Select(move => move.Name))))
                .AddField("Help", "To check out a move use:\n`-character move {CHARACTER NAME} {MOVE NAME}`\n" +
                                          "For example: `-character move Fox u-smash`");

            embedBuilder = AddFooter(embedBuilder);
            return embedBuilder.Build();
        }


        private Embed CreateAttackEmbed(WrapperCharacter character, Attack move, Character fightCoreCharacter)
        {
            var embedBuilder = CreateDefaultFrameDataEmbed(character, move, fightCoreCharacter);

            var frameDataBuilder = new StringBuilder();
            AddIfPossible("Total frames", move.Total, frameDataBuilder);
            AddIfPossible("Hit start", move.Start, frameDataBuilder);
            AddIfPossible("Hit end", move.End, frameDataBuilder);
            AddIfPossible("Shield stun", move.Stun, frameDataBuilder);
            AddIfPossible("Percent", move.Percent, frameDataBuilder);
            AddIfPossible("Percent (weak hit)", move.PercentWeak, frameDataBuilder);
            AddIfPossible("IASA", move.Iasa, frameDataBuilder);
            AddIfPossible("Auto cancel start", move.AutoCancelStart, frameDataBuilder);
            AddIfPossible("Auto cancel end", move.AutoCancelEnd, frameDataBuilder);
            AddIfPossible("Land lag", move.LandingLag, frameDataBuilder);
            AddIfPossible("L-Canceled", move.LCanceledLandingLag, frameDataBuilder);

            embedBuilder.AddField("Frame Data", frameDataBuilder.ToString(), true);

            if (!string.IsNullOrWhiteSpace(move.Notes))
            {
                embedBuilder.AddField("Notes", move.Notes, true);
            }

            embedBuilder = AddMeleeFrameDataInfo(embedBuilder);

            return embedBuilder.Build();
        }

        private Embed CreateDodgeEmbed(WrapperCharacter character, Dodge dodge, Character fightCoreCharacter)
        {
            var embedBuilder = CreateDefaultFrameDataEmbed(character, dodge, fightCoreCharacter);
            var frameDataBuilder = new StringBuilder();
            AddIfPossible("Start", dodge.Start, frameDataBuilder);
            AddIfPossible("Invulnerable ends", dodge.EndInvulnerable, frameDataBuilder);
            AddIfPossible("Total", dodge.Total, frameDataBuilder);

            embedBuilder.AddField("Frame data", frameDataBuilder.ToString());

            embedBuilder = AddMeleeFrameDataInfo(embedBuilder);
            return embedBuilder.Build();
        }

        private Embed CreateGrabEmbed(WrapperCharacter character, Grab grab, Character fightCoreCharacter)
        {
            var embedBuilder = CreateDefaultFrameDataEmbed(character, grab, fightCoreCharacter);


            var frameDataBuilder = new StringBuilder();
            AddIfPossible("Start", grab.Start, frameDataBuilder);
            AddIfPossible("Total", grab.Total, frameDataBuilder);
            embedBuilder.AddField("Frame data", frameDataBuilder.ToString());

            if (!string.IsNullOrWhiteSpace(grab.Notes))
            {
                embedBuilder.AddField("Notes", grab.Notes, true);
            }

            embedBuilder = AddMeleeFrameDataInfo(embedBuilder);
            return embedBuilder.Build();
        }

        private Embed CreateThrowEmbed(WrapperCharacter character, Throw throwMove, Character fightCoreCharacter)
        {
            var embedBuilder = CreateDefaultFrameDataEmbed(character, throwMove, fightCoreCharacter);


            var frameDataBuilder = new StringBuilder();
            AddIfPossible("Start", throwMove.Start, frameDataBuilder);
            AddIfPossible("End", throwMove.End, frameDataBuilder);
            AddIfPossible("Total", throwMove.Total, frameDataBuilder);
            AddIfPossible("Percent", throwMove.Percent, frameDataBuilder);
            embedBuilder.AddField("Frame data", frameDataBuilder.ToString());

            if (!string.IsNullOrWhiteSpace(throwMove.Notes))
            {
                embedBuilder.AddField("Notes", throwMove.Notes, true);
            }

            embedBuilder = AddMeleeFrameDataInfo(embedBuilder);
            return embedBuilder.Build();
        }

        private EmbedBuilder CreateDefaultFrameDataEmbed(WrapperCharacter character, NormalizedEntity move,
            Character fightCoreCharacter)
        {
            var characterName = SearchHelper.Normalize(move.Character);
            var moveName = SearchHelper.Normalize(move.NormalizedType);
            var embedBuilder = new EmbedBuilder()
                .WithUrl($"http://meleeframedata.com/{move.Character}")
                .WithThumbnailUrl(fightCoreCharacter.StockIcon.Url.Replace(" ", "+"))
                .WithImageUrl($"https://i.fightcore.gg/melee/moves/{characterName}/{moveName}.gif");

            embedBuilder.Title = $"{character.Name} - {move.Name}";
            return AddFooter(embedBuilder);
        }

        private EmbedBuilder AddFooter(EmbedBuilder builder)
        {
            switch (_embedSettings.FooterType)
            {
                case FooterType.FightCore:
                    builder = AddFightCoreFooter(builder);
                    break;
                case FooterType.MeleeOnline:
                    builder.WithFooter("Melee Online Frame Data", "https://cdn.discordapp.com/icons/724998978113896508/a_a765306c32c21eca27349539154983a9.webp?size=128")
                        .WithColor(Color.Green)
                        .WithCurrentTimestamp();
                    break;
                case FooterType.DutchNetplay:
                    builder.WithFooter("Dutch Melee Discord", "https://cdn.discordapp.com/icons/283580261520769026/df9cab8218c661ebd2ad0c4550969504.webp?size=128")
                        .WithColor(Color.Red)
                        .WithCurrentTimestamp();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return builder;
        }

        private static EmbedBuilder AddMeleeFrameDataInfo(EmbedBuilder builder)
        {
            return builder.AddField("Melee Frame Data",
                "All of this data is provided by http://meleeframedata.com.");
        }

        private static void AddIfPossible(string key, int? value, StringBuilder stringBuilder)
        {
            if (!value.HasValue || value <= 0)
            {
                return;
            }

            stringBuilder.Append($"**{key}:** {value}\n");
        }
    }
}
