using Microsoft.Xna.Framework;
using System.Timers;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters;

namespace Zold.Screens.Implemented.Combat.Skills
{
    class ShootEnemy : Skill
    {
        public Vector2 Destination;
        public Vector2 StartPosition;
        private int damage;

        public ShootEnemy (CombatScreen CombatScreen, int damage) : base (CombatScreen)
        {
            this.damage = damage;

            CooldownTimer = new Timer();
            CooldownTimer.Interval = 1000;
            CooldownTimer.Elapsed += new ElapsedEventHandler(Ready);
            CooldownTimer.Enabled = false;
        }

        public override void ApplyEffect(Character target)
        {
            throw new System.NotImplementedException();
        }

        public void Use()
        {
            CooldownTimer.Start();
        }

        private void Ready(object source, ElapsedEventArgs e)
        {
            CombatScreen.MakeEnemyProjectile(StartPosition, Destination, null, damage, "combat/Textures/arrow", 22, 5);
            CooldownTimer.Interval = 2000;
            CooldownTimer.Stop();
        }
    }
}
