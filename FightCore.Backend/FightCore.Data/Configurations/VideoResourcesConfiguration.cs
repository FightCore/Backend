using System;
using System.Collections.Generic;
using System.Text;
using FightCore.Models.Characters;
using FightCore.Models.Globals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FightCore.Data.Configurations
{
    public class CharacterVideoConfiguration : IEntityTypeConfiguration<CharacterVideo>
    {
        public void Configure(EntityTypeBuilder<CharacterVideo> builder)
        {
            builder.HasOne(video => video.Video)
                .WithMany();
        }
    }
}
