using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bartdebever.Patterns.Services;
using FightCore.Models.Posts;
using FightCore.Repositories.Posts;

namespace FightCore.Services.Posts
{
    public interface IPostService : IService<Post, long, IPostRepository>
    {
        Task<List<Post>> GetPublicPostsAsync(long? userId = null);

        Task<Post> GetPublicByIdAsync(long id, long userId);
    }

    public class PostService : EntityService<Post, IPostRepository>, IPostService
    {
        public PostService(IPostRepository repository) : base(repository)
        {
        }

        public Task<List<Post>> GetPublicPostsAsync(long? userId = null)
        {
            return Repository.GetPublicPostListAsync(userId ?? 0);
        }

        public Task<Post> GetPublicByIdAsync(long id, long userId)
        {
            return Repository.GetPublicByIdAsync(id, userId);
        }
    }
}
