using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        public void move()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                position.Y -= speed;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                position.Y += speed;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                position.X -= speed;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                position.X += speed;
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