using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Timers;

namespace Zold.Screens.Implemented.Combat
{
    class Mob : Enemy
    {
        private Timer attackTimer;

        public Mob(Player player, Vector2 position, Texture2D texture) : base(player, position, texture)
        {
            Height = 48;
            Width = 32;

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

        private void Attack(object source, ElapsedEventArgs e)
        {
            if (player.CheckPointCollision(attackPosition))
                player.Hp -= Damage;
            attackTimer.Enabled = false;
        }
    }
}
