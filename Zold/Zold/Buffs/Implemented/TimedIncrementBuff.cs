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
                pi.SetValue(character, (int)pi.GetValue(character) + amount);
            if (--ticksToLive <= 0)
                character.buffSet.Remove(this);
        }
    }
}
