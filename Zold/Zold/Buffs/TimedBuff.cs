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
        private int amount;
        private int ticksToLive;
        private byte ticksPerUpdate;
        private string targetStat;
        private Type type;
        private PropertyInfo pi;

        public string TargetStat { get => targetStat; set => targetStat = value; }
        public int Amount { get => amount; set => amount = value; }
        public int TicksToLive { get => ticksToLive; set => ticksToLive = value; }
        public byte TicksPerUpdate { get => ticksPerUpdate; set => ticksPerUpdate = value; }
        public Character Character { get => character; set => character = value; }

        public void Init()
        {
            type = typeof(Character);
            pi = type.GetProperty(targetStat);
        }

        public void Start()
        {
            if (ticksToLive%ticksPerUpdate == 0)
                pi.SetValue(character, (int)pi.GetValue(character) + amount);
            if (--ticksToLive <= 0)
                character.buffSet.Remove(this);
            Debug.WriteLine("ticki: " + ticksToLive + " hp: " + character.Hp);
        }

    }
}
