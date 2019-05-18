using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters;

namespace Zold.Buffs
{
    class TimedBuff : IBuff
    {
        private Character character;
        private int duration;
        private int delta;
        private byte intervalsAmount;
        private string targetStat;
        private Timer timer;

        public string TargetStat { get => targetStat; set => targetStat = value; }
        public int Delta { get => delta; set => delta = value; }
        public int Duration { get => duration; set => duration = value; }
        public byte IntervalsAmount { get => intervalsAmount; set => intervalsAmount = value; }
        internal Character Character { get => character; set => character = value; }

        public void Start()
        {
            float temp = Duration / IntervalsAmount;
            timer = new Timer((e) => { ModifyStats(); }, null, 0, (int)(temp * 1000));
        }

        private void ModifyStats()
        {
            Type type = typeof(Character);
            PropertyInfo pi = type.GetProperty(targetStat);
            int value = (int)pi.GetValue(character);
            value += Delta;
            pi.SetValue(character, Convert.ChangeType(value, pi.PropertyType), null);
        }
    }
}
