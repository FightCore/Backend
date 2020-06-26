using System;
using System.Collections.Generic;
using System.Text;
using FightCore.Models.Characters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FightCore.Data.Configurations
{
    public class SuggestedEditConfiguration : IEntityTypeConfiguration<SuggestedEdit>
    {
        public void Configure(EntityTypeBuilder<SuggestedEdit> builder)
        {
            builder.HasOne(edit => edit.ApprovedByUser)
                .WithMany()
                .HasForeignKey(edit => edit.ApprovedByUserId);

            builder.HasOne(edit => edit.User)
                .WithMany()
                .HasForeignKey(edit => edit.UserId);
        }
    }
}
