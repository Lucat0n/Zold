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
        private String targetStat;
        private Timer timer;

        /// <summary>
        /// Inicjalizuje czas trwania jako 5 sekund z pięcioma interwałami
        /// </summary>
        internal TimedBuff(String targetStat, int delta)
        {
            this.targetStat = targetStat;
            this.delta = delta;
            duration = 5;
            intervalsAmount = 5;
        }

        /// <summary>
        /// Inicjalizuje ilość interwałów równą czasowi trwania buffa
        /// </summary>
        internal TimedBuff(String targetStat, int delta, int duration)
        {
            this.targetStat = targetStat;
            this.delta = delta;
            this.duration = duration;
            intervalsAmount = (byte)duration;
        }

        internal TimedBuff(String targetStat, int delta, int duration, byte intervalsAmount)
        {
            this.targetStat = targetStat;
            this.delta = delta;
            this.duration = duration;
            this.intervalsAmount = intervalsAmount;
        }

        public string TargetStat { get => targetStat; set => targetStat = value; }
        internal Character Character { get => character; set => character = value; }

        public void Start()
        {
            float temp = duration / intervalsAmount;
            timer = new Timer((e) => { ModifyStats(); }, null, 0, (int)(temp * 1000));
        }

        private void ModifyStats()
        {
            Type type = typeof(Character);
            PropertyInfo pi = type.GetProperty(targetStat);
            int value = (int)pi.GetValue(character);
            value += delta;
            pi.SetValue(Character, Convert.ChangeType(value, pi.PropertyType), null);
        }
    }
}
