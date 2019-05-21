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
            int value = (int)pi.GetValue(null);
            value += amount;
            pi.SetValue(character, Convert.ChangeType(value, pi.PropertyType));
            character.buffSet.Remove(this);
        }
    }
}
