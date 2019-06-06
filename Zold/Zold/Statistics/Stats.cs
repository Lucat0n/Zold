using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Zold.Buffs;

namespace Zold.Statistics
{
    class Stats
    {
        public HashSet<IBuff> buffSet = new HashSet<IBuff>();

        public int Level;
        public int Health;
        public int MaxHealth;
        public int Damage;
        public int Speed;
        public int Defence;
        public int Attack;

        public Stats()
        {
            Level = 1;
            MaxHealth = 80;
            Health = MaxHealth;
            Damage = 20;
            Speed = 120;
            Defence = 5;
            Attack = 5;
        }

        public Stats(int Level, int MaxHealth, int Damage, int Speed, int Defence, int Attack)
        {
            this.Level = Level;
            this.MaxHealth = MaxHealth;
            this.Damage = Damage;
            this.Speed = Speed;
            this.Defence = Defence;
            this.Attack = Attack;

            Health = MaxHealth;
        }

        public void UpdateBuffs()
        {
            IBuff[] buffs = buffSet.ToArray();
            foreach (IBuff buff in buffs)
                buff.Start();
        }
    }
}
