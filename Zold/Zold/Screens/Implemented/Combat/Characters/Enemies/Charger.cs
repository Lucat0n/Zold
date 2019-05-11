﻿using Microsoft.Xna.Framework;
using System.Timers;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.Characters.Enemies
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
        private bool hit;

        public Charger(Player player, Vector2 position, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int width, int height) : base(player, position, SpriteBatchSpriteSheet, width, height)
        {
            SpriteBatchSpriteSheet.MakeAnimation(0, "Right", 250);
            SpriteBatchSpriteSheet.MakeAnimation(1, "Left", 250);
            SpriteBatchSpriteSheet.MakeAnimation(3, "Death_Right", 250);
            SpriteBatchSpriteSheet.MakeAnimation(4, "Death_Left", 250);

            charge = false;

            cooldownTimer = new Timer();
            cooldownTimer.Interval = 2000;
            cooldownTimer.Elapsed += new ElapsedEventHandler(Ready);

            prepareTimer = new Timer();
            prepareTimer.Interval = 1000;
            prepareTimer.Elapsed += new ElapsedEventHandler(Prepare);
        }

        public override void AI(GameTime gameTime)
        {
            CalculateDepth();
            CheckDirection();
            speed = 60f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            BottomPosition = new Vector2(Position.X, Position.Y + height);
            chargeSpeed = speed * 10;
            playerDirection = CalcDirection(new Vector2(player.BottomPosition.X, player.BottomPosition.Y - height), Position);

            if (BottomPosition.Y < mapEdge)
                Position.Y = mapEdge - height;

            if (prepareTimer.Enabled == true)
                action = "Preparing";
            else
                action = "Idle";

            if (charge)
            {
                action = "Charging";
                Charge();
            }
            else if (Distance <= 200 && prepareTimer.Enabled == false && cooldownTimer.Enabled == false)
            {
                chargePosition = new Vector2(player.BottomPosition.X, player.BottomPosition.Y - height);
                chargeDirection = CalcDirection(chargePosition, Position);
                chargeCheck = 0;
                chargeRange = Vector2.Distance(chargePosition, Position) + 200;
                prepareTimer.Enabled = true;
            }
            else if (Distance <= 400 && prepareTimer.Enabled == false)
            {
                action = "Moving";
                Move();
            }
        }

        public override void Move()
        {
            Position.X += playerDirection.X * speed;
            if (BottomPosition.Y >= mapEdge)
                Position.Y += playerDirection.Y * speed;
        }

        public override void Animation(GameTime gameTime)
        {
            SpriteBatchSpriteSheet.Begin();

            if (action == "Moving")
            {
                SpriteBatchSpriteSheet.PlayFullAniamtion(Position, direction, gameTime);
            }
            else if (action == "Charging")
            {
                if (direction == "Right")
                    SpriteBatchSpriteSheet.Draw(Position, 4, 0);
                if (direction == "Left")
                    SpriteBatchSpriteSheet.Draw(Position, 4, 1);
            }
            else if (action == "Preparing")
            {
                if (direction == "Right")
                    SpriteBatchSpriteSheet.Draw(Position, 0, 0);
                if (direction == "Left")
                    SpriteBatchSpriteSheet.Draw(Position, 1, 0);
            }
            else if (action == "Idle")
            {
                if (direction == "Right")
                    SpriteBatchSpriteSheet.Draw(Position, 0, 0);
                if (direction == "Left")
                    SpriteBatchSpriteSheet.Draw(Position, 1, 0);
            }

            SpriteBatchSpriteSheet.End();
        }

        private void Prepare(object source, ElapsedEventArgs e)
        {
            hit = false;
            charge = true;
            prepareTimer.Enabled = false;
        }

        private void Ready(object source, ElapsedEventArgs e)
        {
            cooldownTimer.Enabled = false;
        }

        private void Charge()
        {
            Position.X += chargeDirection.X * chargeSpeed;
            Position.Y += chargeDirection.Y * chargeSpeed;
            chargeCheck += chargeSpeed;

            if (player.CheckBoxCollision(Position, height, width) && !hit)
            {
                player.Hp -= 10;
                hit = true;
            }

            if ((chargeCheck > chargeRange) || (BottomPosition.Y + 1 < mapEdge))
            {
                charge = false;
                cooldownTimer.Enabled = true;
            }
        }
    }
}