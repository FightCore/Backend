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
        Task<List<Post>> GetPublicPosts(long? userId = null);
    }

    public class PostService : EntityService<Post, IPostRepository>, IPostService
    {
        public PostService(IPostRepository repository) : base(repository)
        {
        }

        public Task<List<Post>> GetPublicPosts(long? userId = null)
        {
            return Repository.GetPublicPostList(userId ?? 0);
        }
    }
}
