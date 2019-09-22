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
    }

    public class PostService : EntityService<Post, IPostRepository>, IPostService
    {
        private readonly IEncryptionService _encryptionService;

        public PostService(
            IPostRepository repository,
            IEncryptionService encryptionService) : base(repository)
        {
            _encryptionService = encryptionService;
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

        public override Post Add(Post entity)
        {
            entity = EncryptPost(entity);

            return base.Add(entity);
        }

        public override Task<Post> AddAsync(Post entity)
        {
            entity = EncryptPost(entity);

            return base.AddAsync(entity);
        }

        public override Post Update(Post entity)
        {
            entity = EncryptPost(entity);

            return base.Update(entity);
        }

        private Post EncryptPost(Post post)
        {
            if (string.IsNullOrWhiteSpace(post.Iv))
            {
                post.Iv = _encryptionService.GetIV();
            }
            
            post.Body = _encryptionService.Encrypt(post.Body, post.Iv);

            return post;
        }
    }
}
