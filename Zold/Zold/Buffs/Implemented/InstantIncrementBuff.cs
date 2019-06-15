using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zold.Buffs.Implemented
{
    class InstantIncrementBuff : InstantBuff
    {
        private int amount;

        public int Amount { get => amount; set => amount = value; }

        public override void Start()
        {
            int value = (int)pi.GetValue(statistics);
            value += amount;
            pi.SetValue(statistics, Convert.ChangeType(value, pi.PropertyType));
            statistics.buffSet.Remove(this);
        }
    }
}
