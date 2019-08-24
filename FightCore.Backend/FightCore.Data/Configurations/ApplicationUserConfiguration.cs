using System;
using System.Collections.Generic;
using System.Text;
using FightCore.Configuration.Seeds;
using FightCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FightCore.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasMany(user => user.Likes)
                .WithOne(like => like.User)
                .HasForeignKey(like => like.UserId);
        }
    }
}
