﻿using Microsoft.Xna.Framework;
using System.Timers;
using Zold.Statistics;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.CombatObjects.Characters.Enemies
{
    class Charger : Enemy
    {
        private Timer prepareTimer;
        private Timer cooldownTimer;
        private float chargeSpeed;
        private Vector2 chargePosition;
        private Vector2 chargeDirection;
        private float chargeRange;
        private float chargeCheck;
        private bool charge;
        private bool hit;

        public Charger(Player player, Stats statistics, Vector2 position, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int width, int height) : base(player, statistics, position, SpriteBatchSpriteSheet, width, height)
        {
            name = "Charger";

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
            chargeSpeed = BaseSpeed * 350;
            playerDirection = CalcDirection(BottomPosition, new Vector2(player.BottomPosition.X, player.BottomPosition.Y));
            Distance = Vector2.Distance(new Vector2(player.BottomPosition.X, player.BottomPosition.Y - Height), BottomPosition);

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
                chargePosition = new Vector2(player.BottomPosition.X, player.BottomPosition.Y);
                chargeDirection = CalcDirection(BottomPosition, chargePosition);
                prepareTimer.Enabled = true;
            }
            else if (prepareTimer.Enabled == false)
            {
                action = "Moving";
                Move(GetNextNodeDirection());
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
            else if (action == "Charging")
            {
                if (direction == "Right_Charger")
                    SpriteBatchSpriteSheet.Draw(Position, 4, 0);
                if (direction == "Left_Charger")
                    SpriteBatchSpriteSheet.Draw(Position, 4, 1);
            }
            else if (action == "Preparing")
            {
                if (direction == "Right_Charger")
                    SpriteBatchSpriteSheet.Draw(Position, 0, 0);
                if (direction == "Left_Charger")
                    SpriteBatchSpriteSheet.Draw(Position, 1, 0);
            }
            else if (action == "Idle")
            {
                if (direction == "Right_Charger")
                    SpriteBatchSpriteSheet.Draw(Position, 0, 0);
                if (direction == "Left_Charger")
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
            UpdatePosition(chargeDirection * chargeSpeed);
            chargeCheck += chargeSpeed;

            if (player.CheckBoxCollision(this) && !hit)
            {
                player.Statistics.Health -= Statistics.Damage;
                hit = true;
            }

            if (!Map.CheckPointCollision(BottomPosition))
            {
                charge = false;
                cooldownTimer.Enabled = true;
            }
        }
    }
}