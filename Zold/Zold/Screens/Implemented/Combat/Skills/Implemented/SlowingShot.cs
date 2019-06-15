using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Zold.Buffs;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters;

namespace Zold.Screens.Implemented.Combat.Skills.Implemented
{
    class SlowingShot : Skill
    {
        public SlowingShot(CombatScreen CombatScreen) : base(CombatScreen)
        {
            CooldownTimer = new Timer();
            CooldownTimer.Interval = 1000;
            CooldownTimer.Elapsed += new ElapsedEventHandler(Ready);
        }

        private void Ready(object sender, ElapsedEventArgs e)
        {
            CooldownTimer.Stop();
        }

        public void Use(Vector2 StartingPosition, int Damage, Vector2 Destination)
        {
            if (CooldownTimer.Enabled == false)
            {
                CombatScreen.MakePlayerProjectile(StartingPosition, Destination, this, Damage, "combat/Textures/arrow", 22, 5);
                CooldownTimer.Start();
            }
        }

        override public void ApplyEffect(Character target)
        {
            CombatScreen.AddBuff(target.Statistics, BuffFactory.CreateInstantBuff(-50, 3));
        }
    }
}
