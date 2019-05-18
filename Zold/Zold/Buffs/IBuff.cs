using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zold.Buffs
{
    interface IBuff
    {

        string TargetStat { get; set; }

        void Start();

    }
}
