using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zold.Buffs.Implemented
{
    class TimedIncrementBuff : TimedBuff
    {
        private int amount;

        public int Amount { get => amount; set => amount = value; }

        public override void Start()
        {
            if (ticksToLive % ticksPerUpdate == 0)
                pi.SetValue(statistics, (int)pi.GetValue(statistics) + amount);
            if (--ticksToLive <= 0)
                statistics.buffSet.Remove(this);
        }
    }
}
