using System;
using System.IO;
using System.Security.Claims;
using AutoMapper;
using IdentityModel;
using Jil;
using Microsoft.AspNetCore.Mvc;

namespace FightCore.Backend.Controllers
{
    /// <inheritdoc />
    public abstract class BaseApiController : ControllerBase
    {
        /// <summary>
        /// The AutoMapper to be used to map objects to ViewModels.
        /// </summary>
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
                claim.Type.Equals(ClaimTypes.NameIdentifier)
                || claim.Type.Equals(JwtClaimTypes.Subject))?.Value;
            
            if (subject == null)
            {
                return null;
            }

            return Convert.ToInt64(subject);
        }

        /// <summary>
        /// Deserialize the provided <paramref name="json"/> into the type
        /// <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type that should be deserialized to.</typeparam>
        /// <param name="json">The json to deserialize.</param>
        /// <returns>The deserialized object.</returns>
        protected TEntity Deserialize<TEntity>(string json)
        {
            using (var input = new StringReader(json))
            {
                return JSON.Deserialize<TEntity>(input);
            }
        }

        /// <summary>
        /// Serializes the provided <paramref name="entity"/> into a JSON string.
        /// </summary>
        /// <typeparam name="TEntity">The type of <paramref name="entity"/>.</typeparam>
        /// <param name="entity">The entity to be serialized.</param>
        /// <returns>An JSON representation of <paramref name="entity"/>.</returns>
        protected string Serialize<TEntity>(TEntity entity)
        {
            return JSON.Serialize(entity);
        }
    }
}
