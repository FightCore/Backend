using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.ViewModels.Posts;
using FightCore.Models.Posts;

namespace FightCore.Backend.Configuration.Mapping
{
    /// <summary>
    /// Creates a mapping between the post models and view models.
    /// </summary>
    public class PostMapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostMapperProfile"/> object.
        /// </summary>
        public PostMapperProfile()
        {
            CreateMap<Post, PostViewModel>()
                .ForMember(viewModel => viewModel.Game,
                    options => options.MapFrom(post => post.Game.Abbreviation))
                .ForMember(viewModel => viewModel.Likes,
                    options => options.MapFrom(post => post.Likes.Count))
                .ForMember(viewModel => viewModel.BannerUrl,
                    options => options.MapFrom(post => post.Game.BannerUrl))
                .ForMember(viewModel => viewModel.GameId,
                    options => options.MapFrom(post => post.Game.Id));

            CreateMap<CreatePostViewModel, Post>();
        }
    }
}
