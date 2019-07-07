using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Timers;
using Zold.Utilities;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters.Enemies;
using Zold.Screens.Implemented.Combat.Skills;
using Zold.Statistics;
using Zold.Screens.Implemented.Combat.Skills.Implemented;
using Zold.Screens.Implemented.Combat.Utilities;
using System;

namespace Zold.Screens.Implemented.Combat.CombatObjects.Characters
{
    class Player : Character
    {
        private Box range;
        private Timer attackTimer;
        private List<Enemy> enemies;
        private SlowingShot skill;

        public Player(Vector2 position, Stats statistics, List<Enemy> enemies, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int width, int height): base(position, statistics, SpriteBatchSpriteSheet, width, height)
        {
            name = "Player";
            this.enemies = enemies;
            
            direction = "Right_Player";
            
            range = new Box(Vector2.Zero, null, 40, 1);
            skill = new SlowingShot(CombatScreen);
            
            attackTimer = new Timer();
            attackTimer.Interval = 500;
            attackTimer.Elapsed += new ElapsedEventHandler(Attack);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatchSpriteSheet.Begin(transformMatrix: CombatCamera.BindCameraTransformation());
            DrawHealth(SpriteBatchSpriteSheet, "green");

            if (action == "Moving")
            {
                SpriteBatchSpriteSheet.PlayFullAniamtion(Position, direction, gameTime);
            }
            else
            {
                if (direction == "Right_Player")
                    SpriteBatchSpriteSheet.Draw(Position, 1, 0);
                if (direction == "Left_Player")
                    SpriteBatchSpriteSheet.Draw(Position, 3, 0);
            }

            SpriteBatchSpriteSheet.End();
        }

        public override void Update(GameTime gameTime)
        {
            CalculateDepth();
            CheckDirection();
            SetGridPosition();
            SpriteBatchSpriteSheet.LayerDepth = layerDepth;
            Controls();
        }

        public void Controls()
        {
            if (attackTimer.Enabled == false)
                action = "Idle";

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && BottomPosition.Y >= CombatScreen.Map.TopMapEdge && attackTimer.Enabled == false)
            {
                Velocity.Y = -1;
                action = "Moving";
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) && BottomPosition.Y <= CombatScreen.Map.BottomMapEdge && attackTimer.Enabled == false)
            {
                Velocity.Y = 1;
                action = "Moving";
            }
            else
            {
                Velocity.Y = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left) && BottomPosition.X >= CombatScreen.Map.LeflMapEdge && attackTimer.Enabled == false)
            {
                Velocity.X = -1;
                action = "Moving";
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && BottomPosition.X <= CombatScreen.Map.RightMapEdge && attackTimer.Enabled == false)
            {
                Velocity.X = 1;
                action = "Moving";
            }
            else
            {
                Velocity.X = 0;
            }
            UpdatePosition(Velocity * GetSpeed());
            CombatScreen.CheckNodeCollision(this);

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                action = "Attacking";
                attackTimer.Enabled = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                skill.CombatScreen = CombatScreen;
                if (direction == "Right_Player")
                {
                    skill.Use(CenterPosition, 10, CalcDirection(CenterPosition, new Vector2(CenterPosition.X + 1, CenterPosition.Y)));
                }
                else
                    skill.Use(CenterPosition, 10, CalcDirection(CenterPosition, new Vector2(CenterPosition.X - 1, CenterPosition.Y)));
            }
        }

        private void Block()
        {
            
        }

        public void Attack(object source, ElapsedEventArgs e)
        {
            if (direction == "Right_Player")
                range.Position = CenterPosition;
            else
                range.Position = new Vector2(CenterPosition.X - 40, CenterPosition.Y);

            enemies.ForEach( Enemy => {
                if (Enemy.CheckBoxCollision(range))
                    Enemy.Statistics.Health -= Statistics.Damage;
            });
            attackTimer.Enabled = false;
            action = "Idle";
        }
    }
}