using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combat
{
    class Player
    {
        public Texture2D texture { get; set; }
        public Vector2 position { get; set; }
        public int hp { get; set; }
        public int speed { get; private set; }

        public Player(Vector2 position, int hp)
            {
                this.position = position;
                this.hp = hp;
                speed = 2;
            }

        public void move(string direction)
        {
            switch (direction)
            {
                case "up":
                    // position.Y -= speed;
                    position = new Vector2(position.X, position.Y - speed);
                    break;
                case "down":
                    // position.Y += speed;
                    position = new Vector2(position.X, position.Y + speed);
                    break;
                case "left":
                    // position.X -= speed;
                    position = new Vector2(position.X - speed, position.Y);
                    break;
                case "right":
                    // position.X += speed;
                    position = new Vector2(position.X + speed, position.Y);
                    break;

            }
        }
    }
}
