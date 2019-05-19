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
        private Type type;
        private PropertyInfo pi;

        public string TargetStat { get => targetStat; set => targetStat = value; }
        public int Delta { get => delta; set => delta = value; }
        public int Duration { get => duration; set => duration = value; }
        public byte IntervalsAmount { get => intervalsAmount; set => intervalsAmount = value; }
        internal Character Character { get => character; set => character = value; }

        public void Start()
        {
            type = typeof(Character);
            pi = type.GetProperty(targetStat);
            float temp = Duration / IntervalsAmount;
            timer = new Timer((e) => { ModifyStats(); }, null, 0, (int)(temp * 1000));
        }

        private void ModifyStats()
        {
            pi.SetValue(character, (int)pi.GetValue(character) + delta, null);
        }
    }
}
