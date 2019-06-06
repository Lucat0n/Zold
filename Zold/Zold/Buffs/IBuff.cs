using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters;
using Zold.Statistics;

namespace Zold.Buffs
{
    interface IBuff
    {

        string TargetStat { get; set; }
        Stats Statistics{ get; set; }

        void Start();

    }
}
