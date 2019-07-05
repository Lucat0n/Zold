using Microsoft.Xna.Framework;
using System.Timers;
using Zold.Statistics;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.CombatObjects.Characters.Enemies
{
    class Mob : Enemy
    {
        private Timer attackTimer;

        public Mob(Player player, Stats statistics, Vector2 position, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int width, int height) : base(player, statistics, position, SpriteBatchSpriteSheet, width, height)
        {
            name = "Mob";
            attackTimer = new Timer();
            attackTimer.Interval = 1000;
            attackTimer.Elapsed += new ElapsedEventHandler(Attack);
        }

        public override void AI(GameTime gameTime)
        {
            CalculateDepth();
            CheckDirection();
            Distance = Vector2.Distance(new Vector2(player.BottomPosition.X, player.BottomPosition.Y), BottomPosition);

            if (attackTimer.Enabled == true)
                action = "Attacking";
            else
                action = "Idle";

            if (Distance <= 50 && attackTimer.Enabled == false)
            {
                attackPosition = player.CenterPosition;
                attackTimer.Enabled = true;
            }
            else if (attackTimer.Enabled == false)
            {
                action = "Moving";
                Move(player.BottomPosition);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatchSpriteSheet.Begin();
            DrawHealth(SpriteBatchSpriteSheet, "red");

            if (action == "Moving")
            {
                SpriteBatchSpriteSheet.PlayFullAniamtion(Position, direction, gameTime);
            }
            else
            {
                if (direction == "Right_Mob")
                    SpriteBatchSpriteSheet.Draw(Position, 1, 0);
                if (direction == "Left_Mob")
                    SpriteBatchSpriteSheet.Draw(Position, 3, 0);
            }

            SpriteBatchSpriteSheet.End();
        }

        private void Attack(object source, ElapsedEventArgs e)
        {
            if (player.CheckPointCollision(attackPosition))
                player.Statistics.Health -= Statistics.Damage;
            attackTimer.Enabled = false;
        }
    }
}
