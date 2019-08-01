using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bartdebever.Patterns.Repositories;
using FightCore.Models.Posts;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Repositories.Posts
{
    public interface IPostRepository : IRepository<Post, long>
    {
        Task<List<Post>> GetPublicPostListAsync(long userId);

        Task<Post> GetPublicByIdAsync(long id, long userId);
    }

    public class PostRepository : EntityRepository<Post>, IPostRepository
    {
        public PostRepository(DbContext context) : base(context)
        {
        }

        public Task<List<Post>> GetPublicPostListAsync(long userId)
        {
            return IncludedQueryable
                .Where(post => post.IsPrivate == false || post .AuthorId == userId)
                .ToListAsync();
        }

        public Task<Post> GetPublicByIdAsync(long id, long userId)
        {
            return IncludedQueryable
                .FirstOrDefaultAsync(
                    post => post.Id == id
                            && (post.IsPrivate == false
                                || post.AuthorId == userId));
        }

        private IQueryable<Post> IncludedQueryable => Queryable
            .Include(post => post.Author)
            .Include(post => post.Game)
            .Include(post => post.Likes);
    }
}
