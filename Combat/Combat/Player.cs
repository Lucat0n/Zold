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
        private Texture2D texture;
        private Vector2 position;
        public int hp { get; set; }
        public int speed { get; private set; }

        public Player(Vector2 position, int hp)
        {
            this.SetPosition(position);
            this.hp = hp;
            speed = 2;
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

        public Texture2D GetTexture()
        {
            return texture;
        }

        public void SetTexture(Texture2D value)
        {
            texture = value;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public void SetPosition(Vector2 value)
        {
            position = value;
        }
    }
}