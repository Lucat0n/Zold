using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters;

namespace Zold.Buffs
{
    interface IBuff
    {

        string TargetStat { get; set; }
        Character Character { get; set; }

        void Start();

    }
}
