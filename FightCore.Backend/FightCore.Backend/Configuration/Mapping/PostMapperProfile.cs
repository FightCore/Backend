using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.ViewModels.Posts;
using FightCore.Models.Posts;

namespace FightCore.Backend.Configuration.Mapping
{
    public class PostMapperProfile : Profile
    {
        public PostMapperProfile()
        {
            CreateMap<Post, PostViewModel>()
                .ForMember(viewModel => viewModel.Author, 
                    options => options.MapFrom(post => post.Author.UserName));
        }
    }
}
