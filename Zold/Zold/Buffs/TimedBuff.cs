using System;
using System.Reflection;
using Zold.Statistics;

namespace Zold.Buffs
{
    abstract class TimedBuff : IBuff
    {
        protected Stats statistics;
        protected int ticksToLive;
        protected byte ticksPerUpdate;
        private string targetStat;
        private Type type;
        protected PropertyInfo pi;

        public string TargetStat { get => targetStat; set => targetStat = value; }
        public int TicksToLive { get => ticksToLive; set => ticksToLive = value; }
        public byte TicksPerUpdate { get => ticksPerUpdate; set => ticksPerUpdate = value; }
        public Stats Statistics { get => statistics; set => statistics = value; }

        public virtual void Init()
        {
            type = typeof(Stats);
            pi = type.GetProperty("Health");
        }

        public abstract void Start();

    }
}
