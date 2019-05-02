using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Timers;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat
{
    class Player
    {
        private const int WIDTH = 32;
        private const int HEIGHT = 48;
        public Vector2 position;
        private Vector2 centerPosition;
        private Timer attackTimer;
        private SpriteBatchSpriteSheet SpriteBatchSpriteSheet;
        private List<Enemy> enemies;
        public Vector2 bottomPosition { get; set; }
        public int mapEdge { get; set; }
        public int Hp { get; set; }
        public string Action { get; private set; }
        public string Direction { get; private set; }
        public int Speed { get; private set; }

        public Player(Vector2 position, int Hp, List<Enemy> enemies, SpriteBatchSpriteSheet SpriteBatchSpriteSheet)
        {
            this.position = position;
            this.Hp = Hp;
            this.enemies = enemies;

            this.SpriteBatchSpriteSheet = SpriteBatchSpriteSheet;
            SpriteBatchSpriteSheet.MakeAnimation(3, "Left", 250);
            SpriteBatchSpriteSheet.MakeAnimation(1, "Right", 250);

            mapEdge = 150;
            Action = "";
            Speed = 2;

            centerPosition = new Vector2(position.X + WIDTH/2, position.Y + HEIGHT/2);

            attackTimer = new Timer();
            attackTimer.Interval = 500;
            attackTimer.Elapsed += new ElapsedEventHandler(Attack);
        }

        public void Controls()
        {
            centerPosition = new Vector2(position.X + 16, position.Y + 24);
            bottomPosition = new Vector2(position.X, position.Y + 44);

            if (attackTimer.Enabled == false)
                Action = "Idle";

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (bottomPosition.Y >= mapEdge)
                    position.Y -= Speed;
                attackTimer.Enabled = false;
                Action = "Moving";
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                position.Y += Speed;
                attackTimer.Enabled = false;
                Action = "Moving";
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                position.X -= Speed;
                attackTimer.Enabled = false;
                Action = "Moving";
                Direction = "Left";
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                position.X += Speed;
                attackTimer.Enabled = false;
                Action = "Moving";
                Direction = "Right";
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Action = "Attacking";
                attackTimer.Enabled = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                Block();
        }

        public void Animation(GameTime gameTime)
        {
            SpriteBatchSpriteSheet.Begin();

            if (Action == "Moving")
            {
                SpriteBatchSpriteSheet.PlayFullAniamtion(GetPosition(), Direction, gameTime);
            }
            //else if (Action == "Idle")
            else
            {
                if (Direction == "Right")
                    SpriteBatchSpriteSheet.Draw(GetPosition(), 1, 0);
                if (Direction == "Left")
                    SpriteBatchSpriteSheet.Draw(GetPosition(), 3, 0);
            }

            SpriteBatchSpriteSheet.End();
        }

        private void Block()
        {
            
        }

        public void Attack(object source, ElapsedEventArgs e)
        {
            Vector2 hitbox;

            if (Direction == "Right")
                hitbox = centerPosition;
            else
                hitbox = new Vector2(centerPosition.X - 40, centerPosition.Y);

            enemies.ForEach( Enemy => {
                if (Enemy.CheckBoxCollision(hitbox, 1, 40))
                    Enemy.Hp -= 5;
            });
            attackTimer.Enabled = false;
            Action = "Idle";
        }

        public bool CheckPointCollision(Vector2 point)
        {
            if ((position.X < point.X) && (position.X + WIDTH > point.X) &&
                (position.Y < point.Y) && (position.Y + HEIGHT > point.Y))
                return true;
            return false;
        }

        public bool CheckBoxCollision(Vector2 point, int height, int width)
        {
            if (position.X < point.X + width &&
                position.X + WIDTH > point.X &&
                position.Y < point.Y + height &&
                position.Y + HEIGHT > point.Y)
                return true;
            return false;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public void SetPosition(Vector2 value)
        {
            position = value;
        }

        public Vector2 GetCenterPosition()
        {
            return centerPosition;
        }

        public void SetCenterPosition(Vector2 value)
        {
            centerPosition = value;
        }
    }
}