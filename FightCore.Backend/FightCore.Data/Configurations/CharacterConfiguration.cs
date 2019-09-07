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
                .HasForeignKey(character => character.GameId);

            builder.HasMany(character => character.Contributors)
                .WithOne(contributor => contributor.Character)
                .HasForeignKey(contributor => contributor.CharacterId);

            builder.HasMany(character => character.NotablePlayers)
                .WithOne(notablePlayer => notablePlayer.Character);
        }
    }
}