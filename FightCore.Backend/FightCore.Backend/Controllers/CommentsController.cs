using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.ViewModels.Posts;
using FightCore.Data;
using FightCore.Models.Posts;
using FightCore.Services.Posts;
using FightCore.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Backend.Controllers
{
    [Route("posts/{postId:long}/comments")]
    [ApiController]
    public class CommentsController : BaseApiController
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly IApplicationUserService _applicationUserService;
        private readonly DbContext _dbContext;

        public CommentsController(
            IPostService postService,
            ICommentService commentService,
            IApplicationUserService applicationUserService,
            DbContext context,
            IMapper mapper) : base(mapper)
        {
            _postService = postService;
            _commentService = commentService;
            _dbContext = context;
            _applicationUserService = applicationUserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCommentsForPost()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostComment(long postId, CreateCommentViewModel commentViewModel)
        {
            var post = await _postService.GetByIdAsync(postId);

            if (post == null)
            {
                return NotFound();
            }

            var userId = GetUserId();

            if (!userId.HasValue)
            {
                return Unauthorized();
            }

            var comment = new Comment
            {
                AuthorId = userId.Value,
                Content = commentViewModel.Content,
                PostId = post.Id,
                PostedAt = DateTime.Now
            };

            _commentService.Add(comment);
            await _dbContext.SaveChangesAsync();

            var user = await _applicationUserService.GetByIdAsync(userId.Value);
            comment.Author = user;

            return MappedOk<CommentViewModel>(comment);
        }

        [HttpDelete("{commentId}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(long postId, long commentId)
        {
            var userId = GetUserId();

            if (!userId.HasValue)
            {
                return Unauthorized();
            }

            var comment = await _commentService.GetCommentById(commentId);
            if (comment.AuthorId != userId.Value)
            {
                return Unauthorized();
            }

            if (comment.Children.Any())
            {
                comment.Content = string.Empty;
                comment.AuthorId = 1;
            }
            else
            {
                _commentService.Remove(comment);
            }

            await _dbContext.SaveChangesAsync();
            return Accepted();
        }
    }
}
