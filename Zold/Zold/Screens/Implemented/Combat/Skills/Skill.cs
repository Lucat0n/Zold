using Microsoft.Xna.Framework;
using System.Timers;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters;

namespace Zold.Screens.Implemented.Combat.Skills
{
    abstract class Skill
    {
        public Timer CooldownTimer;
        public CombatScreen CombatScreen;

        public Skill(CombatScreen CombatScreen)
        {
            this.CombatScreen = CombatScreen;
        }

        abstract public void ApplyEffect(Character target);
    }
}
