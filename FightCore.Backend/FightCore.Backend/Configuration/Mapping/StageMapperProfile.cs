using AutoMapper;
using FightCore.Backend.ViewModels.Stages;
using FightCore.Models.Characters;

namespace FightCore.Backend.Configuration.Mapping
{
    /// <summary>
    /// Creates a mapping between the stage models and view models.
    /// </summary>
    public class StageMapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StageMapperProfile"/> object.
        /// </summary>
        public StageMapperProfile()
        {
            CreateMap<Stage, StageViewModel>();
        }
    }
}
