using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Zold.Screens.Implemented.Map
{
    class Enemy
    {
        Player player;
        Texture2D texture;
        public Vector2 position;
        public Vector2 direction;
        public float Speed { get; private set; }
        public double Distance { get; private set; }

        public Enemy(Player player, Vector2 position, Texture2D texture)
        {
            this.player = player;
            this.position = position;
            this.texture = texture;
        }

        public Enemy(Player player, Vector2 position)
        {
            this.player = player;
            this.position = position;
        }

        public void AI(GameTime gameTime)
        {
            Speed = 60f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            CalcDirection();

            if (Distance <= 200)
            {
                Move();
            }
        }

        public void Move()
        {
            position.X += direction.X * Speed;
            position.Y += direction.Y * Speed;
        }

        private void CalcDirection()
        {
            Distance = Vector2.Distance(player.GetCenterPosition(), position);
            direction = new Vector2(player.GetCenterPosition().X - position.X, player.GetCenterPosition().Y - position.Y);
            direction.Normalize();
        }

        public void SetTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        public Texture2D GetTexture()
        {
            return texture;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public Vector2 GetDirection()
        {
            return direction;
        }
    }
}