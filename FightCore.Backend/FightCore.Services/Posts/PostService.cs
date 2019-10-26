using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bartdebever.Patterns.Services;
using FightCore.Models.Posts;
using FightCore.Repositories.Posts;
using FightCore.Services.Encryption;

namespace FightCore.Services.Posts
{
    public interface IPostService : IService<Post, long, IPostRepository>
    {
        Task<List<Post>> GetPublicPostsAsync(long? userId = null);

        Task<Post> GetPublicByIdAsync(long id, long userId);

        Task<List<Post>> GetForUserIdAsync(long userId, bool getPrivate);

        Task<List<Post>> GetForCharacterIdAsync(long characterId);

        Task<List<Post>> GetLatestPosts();

        Task<List<Post>> GetPostsByGameId(long gameId);
    }

    public class PostService : EntityService<Post, IPostRepository>, IPostService
    {
        private readonly IEncryptionService _encryptionService;
        private readonly IProcessingService _processingService;

        public PostService(
            IPostRepository repository,
            IEncryptionService encryptionService,
            IProcessingService processingService) : base(repository)
        {
            _encryptionService = encryptionService;
            _processingService = processingService;
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

        public override Post Add(Post entity)
        {
            entity = EncryptPost(entity);

            return base.Add(entity);
        }

        public override async Task<Post> AddAsync(Post entity)
        {
            entity = await EncryptPostAsync(entity);

            return await base.AddAsync(entity);
        }

        public override Post Update(Post entity)
        {
            entity = EncryptPost(entity);

            return base.Update(entity);
        }

        private Post EncryptPost(Post post)
        {
            post = _processingService.ProcessPost(post);
            if (string.IsNullOrWhiteSpace(post.Iv))
            {
                post.Iv = _encryptionService.GetIV();
            }

            post.Body = _encryptionService.Encrypt(post.Body, post.Iv);

            return post;
        }

        private async Task<Post> EncryptPostAsync(Post post)
        {
            post = await _processingService.ProcessPostAsync(post);
            if (string.IsNullOrWhiteSpace(post.Iv))
            {
                post.Iv = _encryptionService.GetIV();
            }
            
            post.Body = _encryptionService.Encrypt(post.Body, post.Iv);

            return post;
        }
    }
}
