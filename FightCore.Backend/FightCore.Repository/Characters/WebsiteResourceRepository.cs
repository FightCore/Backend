using Bartdebever.Patterns.Repositories;
using FightCore.Models.Characters;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Repositories.Characters
{
    public interface IWebsiteResourceRepository : IRepository<WebsiteResource, long>
    {

    }

    public class WebsiteResourceRepository  : EntityRepository<WebsiteResource>, IWebsiteResourceRepository
    {
        public WebsiteResourceRepository(DbContext context) : base(context)
        {
        }
    }
}
