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
        Texture2D texture;
        Vector2 position;
        int speed = 2;

        public Player(Vector2 position)
        {
            this.position = position;
        }

        public void move(string direction)
        {
            switch (direction)
            {
                case "up":
                    position.Y -= speed;
                    break;
                case "down":
                    position.Y += speed;
                    break;
                case "left":
                    position.X -= speed;
                    break;
                case "right":
                    position.X += speed;
                    break;

            }
        }

        public void setTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        public void setPosition(Vector2 position)
        {
            this.position = position;
        }

        public Texture2D getTexture()
        {
            return texture;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public int getSpeed()
        {
            return speed;
        }
    }
}
