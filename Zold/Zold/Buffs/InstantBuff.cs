using System;
using System.Diagnostics;
using System.Reflection;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters;
using Zold.Statistics;

namespace Zold.Buffs
{
    abstract class InstantBuff : IBuff
    {
        protected string targetStat;
        protected Stats statistics;
        protected Type type;
        protected PropertyInfo pi;

        public string TargetStat { get => targetStat; set => targetStat = value; }
        public Stats Statistics { get => statistics; set => statistics = value; }

        public virtual void Init()
        {
            type = typeof(Stats);
            pi = type.GetProperty(targetStat);
        }

        public abstract void Start();

    }
}
