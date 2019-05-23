using Newtonsoft.Json;

namespace Zold.Statistics
{
    class Stats
    {
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
            Speed = 2;
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
    }
}
