using AutoMapper;
using FightCore.Backend.ViewModels.Characters;
using FightCore.Backend.ViewModels.Globals;
using FightCore.Models.Characters;
using FightCore.Models.Globals;

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

            CreateMap<NotablePlayer, NotablePlayerViewModel>().ReverseMap();

            CreateMap<Contributor, ContributorViewModel>().ReverseMap();

            CreateMap<Character, GetCharacterListViewModel>().ReverseMap();

            CreateMap<VideoResource, VideoViewModel>()
                .ForMember(viewModel => viewModel.YoutubeId,
                    options => options.MapFrom(characterVideo =>
                        characterVideo.YoutubeId))
                .ForMember(viewModel => viewModel.Name,
                    options => options.MapFrom(characterVideo =>
                        characterVideo.Name))
                .ForMember(viewModel => viewModel.Description,
                    options => options.MapFrom(characterVideo =>
                        characterVideo.Description))
                .ForMember(viewModel => viewModel.Id,
                    options => options.MapFrom(characterVideo =>
                        characterVideo.Id)).ReverseMap();

            CreateMap<InformationSource, InformationSourceViewModel>().ReverseMap();

            CreateMap<GameSeries, GameSeriesViewModel>().ReverseMap();

            CreateMap<WebsiteResource, WebsiteViewModel>().ReverseMap();

            CreateMap<UpdateCharacterViewModel, Character>();
        }
    }
}