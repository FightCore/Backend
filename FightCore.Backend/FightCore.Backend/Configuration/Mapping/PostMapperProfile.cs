using System;
using System.Linq;
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
                .ForMember(viewModel => viewModel.Likes,
                    options => options.MapFrom(post => post.Likes.Count))
                .ForMember(viewModel => viewModel.GameId,
                    options => options.MapFrom(post => post.Game.Id))
                .ForMember(viewModel => viewModel.Tags, options =>
                    options.MapFrom(post => post.Tags.Split(",", StringSplitOptions.None).ToList()));
            CreateMap<CreatePostViewModel, Post>()
                .ForMember(post => post.Tags, options => options.MapFrom(viewModel =>
                    string.Join(',', viewModel.Tags)));
        }
    }
}
