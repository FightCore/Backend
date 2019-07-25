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
        Task<List<Post>> GetPublicPostList();
    }

    public class PostRepository : EntityRepository<Post>, IPostRepository
    {
        public PostRepository(DbContext context) : base(context)
        {
        }

        public Task<List<Post>> GetPublicPostList()
        {
            return Queryable.Include(post => post.Author)
                .Include(post => post.Game)
                .Where(post => post.IsPrivate == false).ToListAsync();
        }
    }
}
