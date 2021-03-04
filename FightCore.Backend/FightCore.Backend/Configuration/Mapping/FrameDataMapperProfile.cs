using AutoMapper;
using FightCore.Backend.ViewModels.FrameData;
using FightCore.FrameData.Models;

namespace FightCore.Backend.Configuration.Mapping
{
    public class FrameDataMapperProfile : Profile
    {
        public FrameDataMapperProfile()
        {
            CreateMap<Character, CharacterFrameDataViewModel>();
            CreateMap<Move, MoveViewModel>();
            CreateMap<Move, ExtendedMoveViewModel>();
            CreateMap<Hitbox, HitboxViewModel>();
            CreateMap<CharacterStatistics, CharacterStatisticsViewModel>();
            CreateMap<Character, BasicCharacterViewModel>();
            CreateMap<CharacterMiscInfo, CharacterInfoDto>();
        }
    }
}
