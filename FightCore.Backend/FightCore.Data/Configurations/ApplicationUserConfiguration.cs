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

            builder.HasMany(user => user.Posts)
                .WithOne(post => post.Author)
                .HasForeignKey(post => post.AuthorId);

            builder.HasMany(user => user.Contributors)
                .WithOne(contributor => contributor.User)
                .HasForeignKey(contributor => contributor.UserId);

            builder.Property(user => user.FirebaseUserId)
	            .IsRequired();

            builder.HasAlternateKey(user => user.FirebaseUserId);
        }
    }
}
