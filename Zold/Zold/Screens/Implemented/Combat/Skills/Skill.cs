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

        public void Use()
        {
            if (cooldownTimer.Enabled == true)
                return;
            else
            {
                CombatScreen.MakeProjectile(StartPosition, "combat/Textures/arrow", Destination, 22, 5);
            }
            cooldownTimer.Enabled = true;
        }

        private void Ready(object source, ElapsedEventArgs e)
        {
            cooldownTimer.Enabled = false;
        }
    }
}
