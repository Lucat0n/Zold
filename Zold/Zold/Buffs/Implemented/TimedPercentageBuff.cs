using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zold.Buffs.Implemented
{
    class TimedPercentageBuff : TimedBuff
    {
        private bool isTriggered = false;
        private float amount;
        private int originalAmount;

        public float Amount { get => amount; set => amount = value; }

        public override void Start()
        {
            if (!isTriggered)
            {
                originalAmount = (int)pi.GetValue(statistics);
                int targetAmount = (int)((float)pi.GetValue(statistics) * Amount);
                pi.SetValue(statistics, targetAmount);
                isTriggered = true;
            }
            if (--ticksToLive <= 0)
            {
                pi.SetValue(statistics, originalAmount);
                statistics.buffSet.Remove(this);
            }
        }
    }
}
