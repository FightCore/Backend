using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FightCore.FrameData.Models;

namespace FightCore.Backend.ViewModels.FrameData
{
    public class CharacterFrameDataViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// This is the name, normalized to use upper case and removed the special characters.
        /// This is meant to be used for users to search.
        /// </summary>
        public string NormalizedName { get; set; }

        public List<MoveViewModel> Moves { get; set; }

        public CharacterStatisticsViewModel CharacterStatistics { get; set; }
    }
}
