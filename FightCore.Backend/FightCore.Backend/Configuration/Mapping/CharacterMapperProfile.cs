using AutoMapper;
using FightCore.Backend.ViewModels.Characters;
using FightCore.Backend.ViewModels.Globals;
using FightCore.Models.Characters;

namespace FightCore.Backend.Configuration.Mapping
{
    /// <summary>
    /// Profile to map the character objects to their respective view models.
    /// </summary>
    public class CharacterMapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterMapperProfile"/>.
        /// </summary>
        public CharacterMapperProfile()
        {
            CreateMap<Character, GetCharacterViewModel>();

            CreateMap<NotablePlayer, NotablePlayerViewModel>();

            CreateMap<Contributor, ContributorViewModel>();
                //.ForMember(viewModel => viewModel.User,
                //    options => options.MapFrom(model => model.User));

            CreateMap<Character, GetCharacterListViewModel>();

            CreateMap<CharacterVideo, VideoViewModel>()
                .ForMember(viewModel => viewModel.YoutubeId,
                    options => options.MapFrom(characterVideo =>
                        characterVideo.Video.YoutubeId))
                .ForMember(viewModel => viewModel.Name,
                    options => options.MapFrom(characterVideo =>
                        characterVideo.Video.Name))
                .ForMember(viewModel => viewModel.Description,
                    options => options.MapFrom(characterVideo =>
                        characterVideo.Video.Description))
                .ForMember(viewModel => viewModel.Id,
                    options => options.MapFrom(characterVideo =>
                        characterVideo.Id));


            CreateMap<GameSeries, GameSeriesViewModel>();
        }
    }
}