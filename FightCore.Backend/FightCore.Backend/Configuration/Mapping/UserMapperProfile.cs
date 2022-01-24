using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using FightCore.Backend.ViewModels.User;
using FightCore.Models;

namespace FightCore.Backend.Configuration.Mapping
{
    /// <summary>
    /// Creates a mapping between the users models and view models.
    /// </summary>
    public class UserMapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserMapperProfile"/> object.
        /// </summary>
        public UserMapperProfile()
        {
	        CreateMap<ApplicationUser, UserViewModel>()
		        .ForMember(viewModel => viewModel.Name,
			        options => options.MapFrom(user => user.Username));
        }
    }
}
