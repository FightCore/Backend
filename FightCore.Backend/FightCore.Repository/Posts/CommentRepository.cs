using System.Linq;
using System.Threading.Tasks;
using Bartdebever.Patterns.Repositories;
using FightCore.Models.Posts;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Repositories.Posts
{
    public interface ICommentRepository : IRepository<Comment, long>
    {
        Task<Comment> GetCommentById(long commentId);
    }

    public class CommentRepository : EntityRepository<Comment>, ICommentRepository
    {
        public CommentRepository(DbContext context) : base(context)
        {
        }

        public Task<Comment> GetCommentById(long commentId)
        {
            return Queryable
                .Include(comment => comment.Author)
                .Include(comment => comment.Parent)
                .Include(comment => comment.Children)
                .FirstOrDefaultAsync(comment => comment.Id == commentId);
        }
    }
}
