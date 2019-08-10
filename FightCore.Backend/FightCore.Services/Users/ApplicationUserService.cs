using System;
using System.Collections.Generic;
using System.Text;
using Bartdebever.Patterns.Services;
using FightCore.Models;
using FightCore.Repositories.Users;

namespace FightCore.Services.Users
{
    public interface IApplicationUserService : IService<ApplicationUser, long, IApplicationUserRepository>
    {
    }

    public class ApplicationUserService : BaseService<ApplicationUser, long, IApplicationUserRepository>, IApplicationUserService
    {
        public ApplicationUserService(IApplicationUserRepository repository) : base(repository)
        {
        }
    }
}
