using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.ViewModels.Characters.Edits;
using FightCore.Models.Characters;

namespace FightCore.Backend.Configuration.Mapping
{
    public class EditMapperProfile : Profile
    {
        public EditMapperProfile()
        {
            CreateMap<SuggestedEdit, SuggestedEditViewModel>()
                .ForMember(editDto => editDto.UserName, options => options.MapFrom(edit => edit.User.UserName));
        }
    }
}
