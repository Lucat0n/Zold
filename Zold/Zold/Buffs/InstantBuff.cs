using System;
using System.Diagnostics;
using System.Reflection;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters;

namespace Zold.Buffs
{
    abstract class InstantBuff : IBuff
    {
        protected string targetStat;
        protected Character character;
        protected Type type;
        protected PropertyInfo pi;

        public string TargetStat { get => targetStat; set => targetStat = value; }
        public Character Character { get => character; set => character = value; }

        public virtual void Init()
        {
            type = typeof(Character);
            pi = type.GetProperty(targetStat);
        }

        public abstract void Start();

    }
}
