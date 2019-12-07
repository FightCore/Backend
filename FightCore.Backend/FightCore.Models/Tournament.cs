using System;
using System.Collections.Generic;
using System.Text;

namespace FightCore.Models
{
    public class Tournament
    {
        public string Name { get; set; }

        public string VenueAddress { get; set; }

        public string VenueName { get; set; }

        public IEnumerable<string> Games { get; set; }

        public DateTime StartAt { get; set; }

        public string Source { get; set; }

        public string Url { get; set; }
    }
}
