using AutoMapper;
using FightCore.Backend.ViewModels.Characters.Edits;
using FightCore.Backend.ViewModels.Edits;
using FightCore.Models.Characters;
using FightCore.Repositories.Dtos;

namespace FightCore.Backend.Configuration.Mapping
{
    public class EditMapperProfile : Profile
    {
        public EditMapperProfile()
        {
            CreateMap<SuggestedEdit, SuggestedEditViewModel>()
                .ForMember(editDto => editDto.UserName, options => options.MapFrom(edit => edit.User.UserName));

            CreateMap<ContributorDto, TopContributorDto>();
        }
    }
}
