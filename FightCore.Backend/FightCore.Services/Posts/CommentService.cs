using System.Threading.Tasks;
using Bartdebever.Patterns.Services;
using FightCore.Models.Posts;
using FightCore.Repositories.Posts;

namespace FightCore.Services.Posts
{
    public interface ICommentService : IService<Comment, long, ICommentRepository>
    {
        Task<Comment> GetCommentById(long commentId);
    }
    public class CommentService : EntityService<Comment, ICommentRepository>, ICommentService
    {
        public CommentService(ICommentRepository repository) : base(repository)
        {
        }

        public Task<Comment> GetCommentById(long commentId)
        {
            return Repository.GetCommentById(commentId);
        }
    }
}
