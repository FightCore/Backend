using System;
using System.Collections.Generic;
using System.Text;
using Bartdebever.Patterns.Services;
using FightCore.Models.Characters;
using FightCore.Repositories.Characters;

namespace FightCore.Services.Characters
{
    public interface IWebsiteResourceService : IService<WebsiteResource, long, IWebsiteResourceRepository>
    {
    }

    public class WebsiteResourceService : EntityService<WebsiteResource, IWebsiteResourceRepository>, IWebsiteResourceService
    {
        public WebsiteResourceService(IWebsiteResourceRepository repository) : base(repository)
        {
        }
    }
}
