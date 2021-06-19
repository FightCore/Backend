using System.Collections.Generic;
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

        Task<List<Post>> GetForUserIdAsync(long userId, bool getPrivate);

        Task<List<Post>> GetForCharacterIdAsync(long characterId);

        Task<List<Post>> GetLatestPosts();

        Task<List<Post>> GetFeaturedPosts();

        Task<List<Post>> GetPostsByGameId(long gameId);
    }

    public class PostService : EntityService<Post, IPostRepository>, IPostService
    {
        public PostService(
            IPostRepository repository) : base(repository)
        {
        }

        public Task<List<Post>> GetFeaturedPosts()
        {
            return Repository.GetFeaturedPosts();
        }

        public Task<List<Post>> GetPostsByGameId(long gameId)
        {
            return Repository.GetPostsByGameId(gameId);
        }

        public Task<List<Post>> GetPublicPostsAsync(long? userId = null)
        {
            return Repository.GetPublicPostListAsync(userId ?? 0);
        }

        public Task<Post> GetPublicByIdAsync(long id, long userId)
        {
            return Repository.GetPublicByIdAsync(id, userId);
        }

        public Task<List<Post>> GetForUserIdAsync(long userId, bool getPrivate)
        {
            return Repository.GetForUserIdAsync(userId, getPrivate);
        }

        public Task<List<Post>> GetForCharacterIdAsync(long characterId)
        {
            return Repository.GetForCharacterIdAsync(characterId);
        }

        public Task<List<Post>> GetLatestPosts()
        {
            return Repository.GetLatestPosts();
        }
    }
}
