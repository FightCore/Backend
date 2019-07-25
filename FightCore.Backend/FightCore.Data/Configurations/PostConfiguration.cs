using System;
using System.Collections.Generic;
using System.Text;
using FightCore.Models.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FightCore.Data.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasOne(post => post.Author)
                .WithMany().HasForeignKey(post => post.AuthorId);

            builder.HasOne(post => post.Game)
                .WithMany().HasForeignKey(post => post.GameId);
        }
    }
}
