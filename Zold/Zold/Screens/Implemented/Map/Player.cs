using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zold.Screens.Implemented.Map
{
    class Player
    {
        public Texture2D texture { get; set; }
        private Vector2 position;
        private Vector2 centerPosition;
        public float speed { get; private set; }

        public KeyboardState current;
        public KeyboardState previous;

        public int Width { get; set; }
        public int Height { get; set; }

        public Player(Vector2 position, Texture2D texture)
        {
            this.texture = texture;
            this.position = position;

            Width = texture.Width;
            Height = texture.Height;
            speed = 2.3f;
        }

        public void move(int wid, int heigh, bool canMoveLeft, bool canMoveUp, bool canMoveRight, bool canMoveDown)
        {
           
               // previous = current;
                current = Keyboard.GetState();

                if (canMoveRight && current.IsKeyDown(Keys.Right)  && !current.IsKeyDown(Keys.Up) && !current.IsKeyDown(Keys.Down))
                    position.X += speed;

                if (canMoveUp && current.IsKeyDown(Keys.Up) && !current.IsKeyDown(Keys.Left) && !current.IsKeyDown(Keys.Right))
                    position.Y -= speed;

                if (canMoveDown && current.IsKeyDown(Keys.Down) && !current.IsKeyDown(Keys.Left) && !current.IsKeyDown(Keys.Right))
                    position.Y += speed;

                if (canMoveLeft && current.IsKeyDown(Keys.Left) && !current.IsKeyDown(Keys.Up) && !current.IsKeyDown(Keys.Down))
                    position.X -= speed;

                centerPosition = new Vector2(position.X + 16, position.Y + 24);
            
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public void SetPosition(Vector2 value)
        {
            position = value;
        }

        public void SetPosition(float px, float py)
        {
            Vector2 value = new Vector2(px, py);
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