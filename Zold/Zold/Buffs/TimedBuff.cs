using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zold.Buffs
{
    class TimedBuff : IBuff
    {
        private int duration;
        private int delta;
        private byte intervalsAmount;

        /// <summary>
        /// Inicjalizuje czas trwania jako 5 sekund z pięcioma interwałami
        /// </summary>
        internal TimedBuff(String targetStat, int delta)
        {
            this.delta = delta;
            duration = 5;
            intervalsAmount = 5;
        }

        /// <summary>
        /// Inicjalizuje ilość interwałów równą czasowi trwania buffa
        /// </summary>
        internal TimedBuff(String targetStat, int delta, int duration)
        {
            this.delta = delta;
            this.duration = duration;
            intervalsAmount = (byte)duration;
        }

        internal TimedBuff(String targetStat, int delta, int duration, byte intervalsAmount)
        {
            this.delta = delta;
            this.duration = duration;
            this.intervalsAmount = intervalsAmount;
        }

        public string TargetStat { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}
