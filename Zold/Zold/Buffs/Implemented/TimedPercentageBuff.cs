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
                originalAmount = (int)pi.GetValue(character);
                int targetAmount = (int)((float)pi.GetValue(character) * Amount);
                pi.SetValue(character, targetAmount);
                isTriggered = true;
            }
            if (--ticksToLive <= 0)
            {
                pi.SetValue(character, originalAmount);
                character.buffSet.Remove(this);
            }
        }
    }
}
