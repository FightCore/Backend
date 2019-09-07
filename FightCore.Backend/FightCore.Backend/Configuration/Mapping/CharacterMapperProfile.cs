using AutoMapper;
using FightCore.Backend.ViewModels.Characters;
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
        }
    }
}