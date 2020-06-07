using FightCore.Models.Characters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FightCore.Data.Configurations
{
    public class CharacterConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.HasOne(character => character.Game)
                .WithMany(game => game.Characters)
                .HasForeignKey(character => character.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasMany(character => character.Contributors)
                .WithOne(contributor => contributor.Character)
                .HasForeignKey(contributor => contributor.CharacterId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasMany(character => character.NotablePlayers)
                .WithOne(notablePlayer => notablePlayer.Character)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(character => character.StockIcon)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(character => character.CharacterImage)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasMany(character => character.Videos)
                .WithOne(video => video.Character)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasMany(character => character.Websites)
                .WithOne(website => website.Character)
                .HasForeignKey(website => website.CharacterId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}