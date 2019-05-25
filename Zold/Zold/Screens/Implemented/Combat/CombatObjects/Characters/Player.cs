using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Timers;
using Zold.Utilities;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters.Enemies;
using Zold.Screens.Implemented.Combat.Skills;

namespace Zold.Screens.Implemented.Combat.CombatObjects.Characters
{
    class Player : Character
    {
        public Vector2 CenterPosition;
        private Projectile range;
        private Timer attackTimer;
        private List<Enemy> enemies;
        private Skill skill;

        public Player(Vector2 position, int hp, List<Enemy> enemies, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int width, int height): base(position, SpriteBatchSpriteSheet, width, height)
        {
            this.Hp = hp;
            this.enemies = enemies;

            SpriteBatchSpriteSheet.MakeAnimation(3, "Left", 250);
            SpriteBatchSpriteSheet.MakeAnimation(1, "Right", 250);
            
            direction = "Right";
            speed = 2;
            Damage = 20;

            range = new Projectile(Vector2.Zero, null, Vector2.Zero, 40, 1);
            CenterPosition = new Vector2(position.X + this.width / 2, position.Y + this.height/2);
            skill = new Skill(CombatScreen);
            
            attackTimer = new Timer();
            attackTimer.Interval = 500;
            attackTimer.Elapsed += new ElapsedEventHandler(Attack);
        }

        public void Controls()
        {
            CenterPosition = new Vector2(Position.X + this.width / 2, Position.Y + this.height / 2);
            BottomPosition = new Vector2(Position.X, Position.Y + 44);
            CalculateDepth();
            CheckDirection();
            SpriteBatchSpriteSheet.LayerDepth = layerDepth;

            if (attackTimer.Enabled == false)
                action = "Idle";

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (BottomPosition.Y >= topMapEdge)
                    Position.Y -= speed;
                attackTimer.Enabled = false;
                action = "Moving";
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                Position.Y += speed;
                attackTimer.Enabled = false;
                action = "Moving";
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Position.X -= speed;
                attackTimer.Enabled = false;
                action = "Moving";
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Position.X += speed;
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
                if (direction == "Right")
                {
                    skill.Destination = CalcDirection(new Vector2(CenterPosition.X + 1, CenterPosition.Y), CenterPosition);
                }
                else
                    skill.Destination = CalcDirection(new Vector2(CenterPosition.X - 1, CenterPosition.Y), CenterPosition);
                skill.CombatScreen = CombatScreen;
                skill.StartPosition = CenterPosition;
                skill.Use("Player");
            }
        }

        public override void Animation(GameTime gameTime)
        {
            SpriteBatchSpriteSheet.Begin();

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
                    Enemy.Hp -= Damage;
            });
            attackTimer.Enabled = false;
            action = "Idle";
        }

        public bool CheckPointCollision(Vector2 point)
        {
            if ((Position.X < point.X) && (Position.X + width > point.X) &&
                (Position.Y < point.Y) && (Position.Y + height > point.Y))
                return true;
            return false;
        }
    }
}