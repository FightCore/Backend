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

        Task<List<Post>> GetForUserIdAsync(long userId, bool getPrivate);

        Task<List<Post>> GetForCharacterIdAsync(long characterId);

        Task<List<Post>> GetLatestPosts();

        Task<List<Post>> GetFeaturedPosts();

        Task<List<Post>> GetPostsByGameId(long gameId);
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

        public Task<List<Post>> GetForUserIdAsync(long userId, bool getPrivate)
        {
            return IncludedQueryable
                .Where(post =>
                    post.AuthorId == userId
                    && getPrivate
                    ? getPrivate
                    : post.IsPrivate == false)
                .ToListAsync();
        }

        public Task<List<Post>> GetForCharacterIdAsync(long characterId)
        {
            return IncludedQueryable.Where(post =>
                post.IsPrivate == false
                && post.CharacterId.HasValue
                && post.CharacterId == characterId)
                .ToListAsync();
        }

        public Task<List<Post>> GetLatestPosts()
        {
            return IncludedQueryable
                .Where(post => !post.IsPrivate)
                .OrderByDescending(post =>
                post.Id).Take(3).ToListAsync();
        }

        public Task<List<Post>> GetFeaturedPosts()
        {
            return IncludedQueryable
                .Where(post => post.Featured)
                .OrderByDescending(post => post.Id)
                .Take(3)
                .ToListAsync();
        }

        public Task<List<Post>> GetPostsByGameId(long gameId)
        {
            return IncludedQueryable
                .Where(post => post.GameId == gameId && !post.IsPrivate)
                .ToListAsync();
        }

        private IQueryable<Post> IncludedQueryable => Queryable
            .Include(post => post.Author)
            .Include(post => post.Game)
            .Include(post => post.Character)
            .ThenInclude(character => character.StockIcon)
            .Include(post => post.Likes)
            .Include(post => post.Comments)
            .AsSplitQuery();
    }
}
