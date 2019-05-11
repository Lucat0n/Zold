using Microsoft.Xna.Framework;
using System.Timers;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.Characters.Enemies
{
    class Mob : Enemy
    {
        private Timer attackTimer;

        public Mob(Player player, Vector2 position, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int width, int height) : base(player, position, SpriteBatchSpriteSheet, width, height)
        {
            SpriteBatchSpriteSheet.MakeAnimation(3, "Left", 250);
            SpriteBatchSpriteSheet.MakeAnimation(1, "Right", 250);

            attackTimer = new Timer();
            attackTimer.Interval = 1000;
            attackTimer.Elapsed += new ElapsedEventHandler(Attack);
        }

        public override void AI(GameTime gameTime)
        {
            CalculateDepth();
            speed = 60f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            playerDirection = CalcDirection(player.GetCenterPosition(), Position);

            if (attackTimer.Enabled == true)
                action = "Attacking";
            else
                action = "Idle";

            if (Distance <= 50 && attackTimer.Enabled == false)
            {
                attackPosition = player.GetCenterPosition();
                attackTimer.Enabled = true;
            }
            else if (Distance <= 400 && attackTimer.Enabled == false)
            {
                action = "Moving";
                Move();
            }
        }

        public override void Move()
        {
            Position.X += playerDirection.X * speed;
            Position.Y += playerDirection.Y * speed;
        }

        public override void Animation()
        {
            SpriteBatchSpriteSheet.Begin();
            SpriteBatchSpriteSheet.Draw(Position, 1, 0);
            SpriteBatchSpriteSheet.End();
        }

        private void Attack(object source, ElapsedEventArgs e)
        {
            if (player.CheckPointCollision(attackPosition))
                player.hp -= damage;
            attackTimer.Enabled = false;
        }
    }
}
