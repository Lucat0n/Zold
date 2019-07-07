using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Zold.Buffs;

namespace Zold.Statistics
{
    class Stats
    {
        public HashSet<IBuff> buffSet = new HashSet<IBuff>();

        private int level;
        private int health;
        private int maxHealth;
        private int damage;
        private int speed;
        private int defence;
        private int attack;

        public int Health { get => health; set => health = value; }
        public int Level { get => level; set => level = value; }
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public int Damage { get => damage; set => damage = value; }
        public int Speed { get => speed; set => speed = value; }
        public int Defence { get => defence; set => defence = value; }
        public int Attack { get => attack; set => attack = value; }

        public Stats()
        {
            Level = 1;
            MaxHealth = 800;
            health = MaxHealth;
            Damage = 20;
            Speed = 80;
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
