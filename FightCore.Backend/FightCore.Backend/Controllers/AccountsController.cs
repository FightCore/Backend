using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.ViewModels.Errors;
using FightCore.Backend.ViewModels.Posts;
using FightCore.Backend.ViewModels.User;
using FightCore.Models;
using FightCore.Services.Posts;
using FightCore.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace FightCore.Backend.Controllers
{
    /// <inheritdoc />
    [Route("[controller]")]
    [ApiController]
    [Produces(HttpContentTypes.ApplicationJson)]
    public class AccountsController : BaseApiController
    {
	    private readonly DbContext _dbContext;
        private readonly IApplicationUserService _applicationUserService;
        private readonly IPostService _postService;
        private readonly IProcessingService _processingService;

        /// <inheritdoc />
        public AccountsController(
            IMapper mapper,
            DbContext context,
            IApplicationUserService applicationUserService,
            IPostService postService,
            IProcessingService processingService) : base(mapper)
        {
	        _applicationUserService = applicationUserService;
            _dbContext = context;
            _postService = postService;
            _processingService = processingService;
        }

        /// <summary>
        /// Gets the user based on the provided <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>The gotten user.</returns>
        [HttpGet("{id}")]
        [SwaggerResponse(200, "The gotten user.", typeof(UserViewModel))]
        [SwaggerResponse(404, "The user was not found.", typeof(NotFoundErrorViewModel))]
        public async Task<IActionResult> GetUser(long id)
        {
            var user = await _applicationUserService.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound(NotFoundErrorViewModel.Create(nameof(User), id));
            }

            return MappedOk<UserViewModel>(user);
        }

        /// <summary>
        /// Gets all posts for the provided user <paramref name="id"/>.
        /// If the authorized user is the provided user, private and unapproved
        /// posts are also gathered.
        /// </summary>
        /// <param name="id">The user id to get the posts for.</param>
        /// <returns>A list of posts.</returns>
        [HttpGet("{id}/posts")]
        public async Task<IActionResult> GetPostsFromUser(int id)
        {
            var userId = GetUserId();

            var isUser = id == userId;

            var posts = await _postService.GetForUserIdAsync(id, isUser);

            posts = _processingService.ProcessPosts(posts, GetUserId());

            return MappedOk<List<PostViewModel>>(posts);
        }
    }
}