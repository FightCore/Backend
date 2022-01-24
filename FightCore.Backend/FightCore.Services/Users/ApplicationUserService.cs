using System.Threading.Tasks;
using Bartdebever.Patterns.Services;
using FightCore.Models;
using FightCore.Repositories.Users;

namespace FightCore.Services.Users
{
    public interface IApplicationUserService : IService<ApplicationUser, long, IApplicationUserRepository>
    {
	    Task<ApplicationUser> GetUserForFirebaseId(string firebaseId);

        Task<bool> IsUsernameTaken(string username);
    }

    public class ApplicationUserService : BaseService<ApplicationUser, long, IApplicationUserRepository>, IApplicationUserService
    {
        public ApplicationUserService(IApplicationUserRepository repository) : base(repository)
        {
        }

        public Task<ApplicationUser> GetUserForFirebaseId(string firebaseId)
        {
	        return string.IsNullOrWhiteSpace(firebaseId) ? null : Repository.GetUserForFirebaseId(firebaseId);
        }

        public Task<bool> IsUsernameTaken(string username)
        {
            return Repository.IsUsernameTaken(username);
        }
    }
}
