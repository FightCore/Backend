using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.ViewModels.User;
using FightCore.Models;

namespace FightCore.Backend.Configuration.Mapping
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(viewModel => viewModel.Name,
                    options => options.MapFrom(user => user.UserName));
        }
    }
}
