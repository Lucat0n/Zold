using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Timers;
using Zold.Utilities;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters.Enemies;
using Zold.Screens.Implemented.Combat.Skills;
using Zold.Statistics;
using Zold.Screens.Implemented.Combat.Skills.Implemented;

namespace Zold.Screens.Implemented.Combat.CombatObjects.Characters
{
    class Player : Character
    {
        private Map range;
        private Timer attackTimer;
        private List<Enemy> enemies;
        private SlowingShot skill;

        public Player(Vector2 position, Stats statistics, List<Enemy> enemies, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int width, int height): base(position, statistics, SpriteBatchSpriteSheet, width, height)
        {
            this.enemies = enemies;

            SpriteBatchSpriteSheet.MakeAnimation(3, "Left", 250);
            SpriteBatchSpriteSheet.MakeAnimation(1, "Right", 250);
            
            direction = "Right";

            range = new Map(Vector2.Zero, null, 40, 1);
            skill = new SlowingShot(CombatScreen);
            
            attackTimer = new Timer();
            attackTimer.Interval = 500;
            attackTimer.Elapsed += new ElapsedEventHandler(Attack);
        }

        public void Controls()
        {
            CenterPosition = new Vector2(Position.X + width / 2, Position.Y + height / 2);
            BottomPosition = new Vector2(Position.X, Position.Y + 44);
            CalculateDepth();
            CheckDirection();
            SpriteBatchSpriteSheet.LayerDepth = layerDepth;

            if (attackTimer.Enabled == false)
                action = "Idle";

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (BottomPosition.Y >= CombatScreen.TopMapEdge)
                    UpdatePosition(0, -GetSpeed());
                attackTimer.Enabled = false;
                action = "Moving";
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (BottomPosition.Y <= CombatScreen.BottomMapEdge)
                    UpdatePosition(0, GetSpeed());
                attackTimer.Enabled = false;
                action = "Moving";
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (BottomPosition.X >= CombatScreen.LeflMapEdge)
                    UpdatePosition(-GetSpeed(), 0);
                attackTimer.Enabled = false;
                action = "Moving";
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (BottomPosition.X <= CombatScreen.RightMapEdge)
                    UpdatePosition(GetSpeed(), 0);
                attackTimer.Enabled = false;
                action = "Moving";
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                action = "Attacking";
                attackTimer.Enabled = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                skill.CombatScreen = CombatScreen;
                if (direction == "Right")
                {
                    skill.Use(CenterPosition, 10, CalcDirection(CenterPosition, new Vector2(CenterPosition.X + 1, CenterPosition.Y)));
                }
                else
                    skill.Use(CenterPosition, 10, CalcDirection(CenterPosition, new Vector2(CenterPosition.X - 1, CenterPosition.Y)));
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatchSpriteSheet.Begin();
            DrawHealth(SpriteBatchSpriteSheet, "green");

            if (action == "Moving")
            {
                SpriteBatchSpriteSheet.PlayFullAniamtion(Position, direction, gameTime);
            }
            else
            {
                if (direction == "Right")
                    SpriteBatchSpriteSheet.Draw(Position, 1, 0);
                if (direction == "Left")
                    SpriteBatchSpriteSheet.Draw(Position, 3, 0);
            }

            SpriteBatchSpriteSheet.End();
        }

        private void Block()
        {
            
        }

        public void Attack(object source, ElapsedEventArgs e)
        {
            Vector2 hitbox;

            if (direction == "Right")
                hitbox = CenterPosition;
            else
                hitbox = new Vector2(CenterPosition.X - 40, CenterPosition.Y);

            enemies.ForEach( Enemy => {
                if (Enemy.CheckBoxCollision(hitbox, range))
                    Enemy.Statistics.Health -= Statistics.Damage;
            });
            attackTimer.Enabled = false;
            action = "Idle";
        }
    }
}