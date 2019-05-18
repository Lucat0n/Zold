﻿using System;
using System.Diagnostics;
using System.Reflection;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters;

namespace Zold.Buffs
{
    class InstantBuff : IBuff
    {
        private int amount;
        private string targetStat;
        private Character character;

        internal int Amount { get => amount; set => amount = value; }
        public string TargetStat { get => targetStat; set => targetStat = value; }
        internal Character Character { get => character; set => character = value; }

        public void Start()
        {
            Type type = typeof(Character);
            PropertyInfo pi = type.GetProperty(targetStat);
            int value = (int)pi.GetValue(null);
            value += amount;
            pi.SetValue(character, Convert.ChangeType(value, pi.PropertyType), null);
        }
    }
}