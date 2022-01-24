using FightCore.Data.Configurations;
using FightCore.Data.Configurations.Posts;
using FightCore.Models;
using FightCore.Models.Characters;
using FightCore.Models.Posts;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext() : base()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
	        builder.ApplyConfiguration(new PostConfiguration());
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new SuggestedEditConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Game> Game { get; set; }

        public DbSet<ApiClient> ApiClients { get; set; }

        public DbSet<WebsiteResource> WebsiteResources { get; set; }

        public DbSet<Comment> Comments { get; set; }

    }
}
