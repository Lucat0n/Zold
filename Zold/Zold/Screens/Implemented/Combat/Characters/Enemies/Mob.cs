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
            attackTimer = new Timer();
            attackTimer.Interval = 1000;
            attackTimer.Elapsed += new ElapsedEventHandler(Attack);
        }

        public override void AI(GameTime gameTime)
        {
            CalculateDepth();
            Speed = 60f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            playerDirection = CalcDirection(player.GetCenterPosition(), position);

            if (attackTimer.Enabled == true)
                Action = "Attacking";
            else
                Action = "Idle";

            if (Distance <= 50 && attackTimer.Enabled == false)
            {
                attackPosition = player.GetCenterPosition();
                attackTimer.Enabled = true;
            }
            else if (Distance <= 400 && attackTimer.Enabled == false)
            {
                Action = "Moving";
                Move();
            }
        }

        public override void Move()
        {
            position.X += playerDirection.X * Speed;
            position.Y += playerDirection.Y * Speed;
        }

        public override void Animation()
        {
            SpriteBatchSpriteSheet.Begin();
            SpriteBatchSpriteSheet.Draw(position, 1, 0);
            SpriteBatchSpriteSheet.End();
        }

        private void Attack(object source, ElapsedEventArgs e)
        {
            if (player.CheckPointCollision(attackPosition))
                player.Hp -= Damage;
            attackTimer.Enabled = false;
        }
    }
}
