using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightCore.Backend.ViewModels.FrameData
{
    public class HitboxViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long Damage { get; set; }

        public long Angle { get; set; }

        public long KnockbackGrowth { get; set; }

        public long SetKnockback { get; set; }

        public long BaseKnockback { get; set; }

        public string Effect { get; set; }

        public int HitlagAttacker { get; set; }

        public int HitlagDefender { get; set; }

        public int Shieldstun { get; set; }
    }
}
