using System;
using System.Collections.Generic;
using System.Text;
using FightCore.Models.Characters;
using FightCore.Models.Globals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FightCore.Data.Configurations
{
    public class CharacterVideoConfiguration : IEntityTypeConfiguration<VideoResource>
    {
        public void Configure(EntityTypeBuilder<VideoResource> builder)
        {
            builder.HasOne(video => video.Character)
                .WithMany();
        }
    }
}
