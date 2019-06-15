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
            this.enemies = enemies;

            SpriteBatchSpriteSheet.MakeAnimation(3, "Left", 250);
            SpriteBatchSpriteSheet.MakeAnimation(1, "Right", 250);
            
            direction = "Right";

            HitBox = new Rectangle((int)position.X, (int)position.Y, width, height);
            range = new Box(Vector2.Zero, null, 40, 1);
            skill = new SlowingShot(CombatScreen);
            
            attackTimer = new Timer();
            attackTimer.Interval = 500;
            attackTimer.Elapsed += new ElapsedEventHandler(Attack);
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

        public override void Update(GameTime gameTime)
        {
            CalculateDepth();
            CheckDirection();
            SpriteBatchSpriteSheet.LayerDepth = layerDepth;
            Controls();
        }

        public void Controls()
        {
            Node collisionNode = CombatScreen.CheckNodeCollision(this);

            if (attackTimer.Enabled == false)
                action = "Idle";

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (BottomPosition.Y >= CombatScreen.Map.TopMapEdge && collisionNode == null)
                {
                    UpdatePosition(0, -GetSpeed());
                }

                attackTimer.Enabled = false;
                action = "Moving";
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (BottomPosition.Y <= CombatScreen.Map.BottomMapEdge && collisionNode == null)
                {
                    UpdatePosition(0, GetSpeed());
                }

                attackTimer.Enabled = false;
                action = "Moving";
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (BottomPosition.X >= CombatScreen.Map.LeflMapEdge && collisionNode == null)
                {
                    UpdatePosition(-GetSpeed(), 0);
                }

                attackTimer.Enabled = false;
                action = "Moving";
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (BottomPosition.X <= CombatScreen.Map.RightMapEdge && collisionNode == null)
                {
                    UpdatePosition(GetSpeed(), 0);
                }

                attackTimer.Enabled = false;
                action = "Moving";
            }
            collisionNode = null;

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

        private void Block()
        {
            
        }

        public void Attack(object source, ElapsedEventArgs e)
        {
            if (direction == "Right")
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