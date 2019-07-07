using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zold.Buffs.Implemented
{
    /// <summary>
    /// Zwiększa Hp o ilość % maksymalnego HP lub inne statystyki o % aktualnej wartości.
    /// </summary>
    class InstantPercentageBuff : InstantBuff
    {
        private float amount;
        protected PropertyInfo pi2;

        public float Amount { get => amount; set => amount = value; }

        public override void Init()
        {
            base.Init();
            if(TargetStat.Equals("Hp"))
                pi2 = type.GetProperty("Max" + targetStat);
        }

        public override void Start()
        {
            int value;
            if (pi2 != null)
                value = (int)pi2.GetValue(statistics);
            else
                value = (int)pi.GetValue(statistics);
            value = (int)(value * amount);
            pi.SetValue(statistics, Convert.ChangeType(value, pi.PropertyType));
            statistics.buffSet.Remove(this);
        }
    }
}
