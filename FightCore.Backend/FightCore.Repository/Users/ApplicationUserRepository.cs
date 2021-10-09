using System.Threading.Tasks;
using Bartdebever.Patterns.Repositories;
using FightCore.Models;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Repositories.Users
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser, long>
    {
	    Task<ApplicationUser> GetUserForFirebaseId(string firebaseId);
    }

    public class ApplicationUserRepository : BaseRepository<ApplicationUser, long>, IApplicationUserRepository
    {
        public ApplicationUserRepository(DbContext context) : base(context)
        {
        }

        public Task<ApplicationUser> GetUserForFirebaseId(string firebaseId)
        {
	        return Queryable.FirstOrDefaultAsync(user => user.FirebaseUserId == firebaseId);
        }
    }
}
