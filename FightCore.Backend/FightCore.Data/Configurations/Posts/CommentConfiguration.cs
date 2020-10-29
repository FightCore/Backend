using FightCore.Models.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FightCore.Data.Configurations.Posts
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(comment => comment.Author)
                .WithMany()
                .HasForeignKey(comment => comment.AuthorId);

            builder.HasOne(comment => comment.Parent)
                .WithMany(comment => comment.Children)
                .HasForeignKey(comment => comment.ParentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(comment => comment.Post)
                .WithMany(post => post.Comments)
                .HasForeignKey(comment => comment.PostId);
        }
    }
}
