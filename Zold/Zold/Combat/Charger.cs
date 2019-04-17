using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Combat
{
    class Charger : Enemy
    {
        private Timer prepareTimer;
        public float chargeSpeed { get; set; }
        private Vector2 chargePosition;
        private Vector2 chargeDirection;
        private bool charge;

        public Charger(Player player, Vector2 position) : base(player, position)
        {
            charge = false;

            prepareTimer = new Timer();
            prepareTimer.Interval = 1000;
            prepareTimer.Elapsed += new ElapsedEventHandler(Prepare);
        }

        public override void AI(GameTime gameTime)
        {
            Speed = 60f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            chargeSpeed = Speed * 5;
            CalcDirection();

            if (prepareTimer.Enabled == true)
                Action = "Preparing";
            else
                Action = "Idle";

            if (charge)
            {
                Action = "Charging";
                Charge();
            }
            else if (Distance <= 200 && prepareTimer.Enabled == false)
            {
                chargePosition = player.GetCenterPosition();
                CalcChargeDirection();
                prepareTimer.Enabled = true;
            }
            else if (Distance <= 400 && prepareTimer.Enabled == false)
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

        private void Prepare(object source, ElapsedEventArgs e)
        {
            charge = true;
            prepareTimer.Enabled = false;
        }

        private void Charge()
        {
            position.X += chargeDirection.X * chargeSpeed;
            position.Y += chargeDirection.Y * chargeSpeed;

            if (Math.Round(position.X, 0) == Math.Round(chargePosition.X, 0))
            //if (position.Equals(chargePosition))
            {
                charge = false;
            }
        }

        public void CalcChargeDirection()
        {
            chargeDirection = new Vector2(chargePosition.X - position.X, chargePosition.Y - position.Y);
            chargeDirection.Normalize();
        }

    }
}
