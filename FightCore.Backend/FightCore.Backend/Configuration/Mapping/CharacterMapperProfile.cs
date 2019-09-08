using AutoMapper;
using FightCore.Backend.ViewModels.Characters;
using FightCore.Backend.ViewModels.Globals;
using FightCore.Models.Characters;

namespace FightCore.Backend.Configuration.Mapping
{
    public class CharacterMapperProfile : Profile
    {
        public CharacterMapperProfile()
        {
            CreateMap<Character, GetCharacterViewModel>();

            CreateMap<NotablePlayer, NotablePlayerViewModel>();

            CreateMap<Contributor, ContributorViewModel>();

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
                        characterVideo.Video.Description));

            CreateMap<GameSeries, GameSeriesViewModel>();
        }
    }
}