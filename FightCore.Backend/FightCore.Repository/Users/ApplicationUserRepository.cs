using System;
using System.Collections.Generic;
using System.Text;
using Bartdebever.Patterns.Repositories;
using FightCore.Models;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Repositories.Users
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser, long>
    {
    }

    public class ApplicationUserRepository : BaseRepository<ApplicationUser, long>, IApplicationUserRepository
    {
        public ApplicationUserRepository(DbContext context) : base(context)
        {
        }
    }
}
