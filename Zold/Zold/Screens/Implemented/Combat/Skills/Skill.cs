using Microsoft.Xna.Framework;
using System.Timers;

namespace Zold.Screens.Implemented.Combat.Skills
{
    class Skill
    {
        public Timer cooldownTimer;
        public Vector2 Destination;
        public CombatScreen CombatScreen;
        public Vector2 StartPosition;

        public Skill (CombatScreen combatScreen)
        {
            this.CombatScreen = combatScreen;

            cooldownTimer = new Timer();
            cooldownTimer.Interval = 2000;
            cooldownTimer.Elapsed += new ElapsedEventHandler(Ready);
        }

        public void Use(string owner)
        {
            if (cooldownTimer.Enabled == true)
                return;
            else
            {
                if(owner == "Player")
                {
                    CombatScreen.MakePlayerProjectile(StartPosition, "combat/Textures/arrow", Destination, 22, 5);
                }
                else
                {
                    CombatScreen.MakeEnemyProjectile(StartPosition, "combat/Textures/arrow", Destination, 22, 5);
                }
            }
            cooldownTimer.Enabled = true;
        }

        private void Ready(object source, ElapsedEventArgs e)
        {
            cooldownTimer.Enabled = false;
        }
    }
}
