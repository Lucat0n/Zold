using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Zold.Screens.Implemented.Combat
{
    class Charger : Enemy
    {
        private Timer prepareTimer;
        private Timer cooldownTimer;
        public float chargeSpeed { get; set; }
        private Vector2 chargePosition;
        private Vector2 chargeDirection;
        private float chargeRange;
        private float chargeCheck;
        private bool charge;

        public Charger(Player player, Vector2 position) : base(player, position)
        {
            charge = false;
            Height = 20;

            cooldownTimer = new Timer();
            cooldownTimer.Interval = 1000;
            cooldownTimer.Elapsed += new ElapsedEventHandler(Ready);

            prepareTimer = new Timer();
            prepareTimer.Interval = 1000;
            prepareTimer.Elapsed += new ElapsedEventHandler(Prepare);
        }

        public override void AI(GameTime gameTime)
        {
            bottomPosition = new Vector2(position.X, position.Y + Height);
            Speed = 60f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            chargeSpeed = Speed * 5;
            playerDirection = CalcDirection(new Vector2(player.bottomPosition.X, player.bottomPosition.Y - Height), position);

            if (bottomPosition.Y < player.mapEdge)
                position.Y = player.mapEdge - Height;

            if (prepareTimer.Enabled == true)
                Action = "Preparing";
            else
                Action = "Idle";

            if (charge)
            {
                Action = "Charging";
                Charge();
            }
            else if (Distance <= 200 && prepareTimer.Enabled == false && cooldownTimer.Enabled == false)
            {
                chargePosition = new Vector2(player.bottomPosition.X, player.bottomPosition.Y - Height);
                chargeDirection = CalcDirection(chargePosition, position);
                chargeCheck = 0;
                chargeRange = Vector2.Distance(chargePosition, position) + 50;
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
            if (bottomPosition.Y >= player.mapEdge)
                position.Y += playerDirection.Y * Speed;
        }

        private void Prepare(object source, ElapsedEventArgs e)
        {
            charge = true;
            prepareTimer.Enabled = false;
        }

        private void Ready(object source, ElapsedEventArgs e)
        {
            cooldownTimer.Enabled = false;
        }

        private void Charge()
        {
            position.X += chargeDirection.X * chargeSpeed;
            position.Y += chargeDirection.Y * chargeSpeed;
            chargeCheck += chargeSpeed;

            if ((chargeCheck > chargeRange) || (bottomPosition.Y + 1 < player.mapEdge))
            {
                charge = false;
                cooldownTimer.Enabled = true;
            }
        }
    }
}
