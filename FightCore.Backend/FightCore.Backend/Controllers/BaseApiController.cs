using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace FightCore.Backend.Controllers
{
    /// <inheritdoc />
    public abstract class BaseApiController : ControllerBase
    {
        protected readonly IMapper Mapper;

        /// <inheritdoc />
        protected BaseApiController(IMapper mapper)
        {
            Mapper = mapper;
        }

        /// <summary>
        /// Maps a model to a ViewModel using <see cref="Mapper"/>.
        /// Wraps this in the default Ok(...) result.
        /// </summary>
        /// <typeparam name="TViewModel">
        /// The type of the ViewModel to be mapped to.
        /// </typeparam>
        /// <param name="model">
        /// The model which should be mapped.
        /// </param>
        /// <returns>The mapped <paramref name="model"/> in an 200 result.</returns>
        protected IActionResult MappedOk<TViewModel>(object model)
        {
            var viewModels = this.Mapper.Map<TViewModel>(model);

            return Ok(viewModels);
        }

        /// <summary>
        /// Gets the currently logged in user's Id based on the given <paramref name="claimsPrincipal"/>.
        /// </summary>
        /// <param name="claimsPrincipal">
        /// The claims from the user parsed from the JWT.
        /// </param>
        /// <returns>The id of the user or throws an exception if not parsed.</returns>
        protected long? GetUserIdFromClaims(ClaimsPrincipal claimsPrincipal)
        {
            // Finds the id of the user in it's subject claim.
            // Note that this is stored in ClaimTypes.NameIdentifier.
            // This is note because of Identity Server but rather because of the
            // default way that ASP.NET Core handles JWT (Microsoft standard)
            var subject = claimsPrincipal.FindFirst(claim =>
                claim.Type.Equals(ClaimTypes.NameIdentifier))?.Value;

            if (subject == null)
            {
                return null;
            }

            return Convert.ToInt64(subject);
        }
    }
}
