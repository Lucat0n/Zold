using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combat
{
    class Enemy
    {
        Player player;
        Texture2D texture;
        public Vector2 position, direction;
        int speed = 1;
        double distance;

        public Enemy(Player player, Vector2 position)
        {
            this.player = player;
            this.position = position;
        }

        public void move()
        {
            distance = Vector2.Distance(player.getPosition(), position);
            direction = new Vector2(player.getPosition().X - position.X, player.getPosition().Y - position.Y);
            direction.Normalize();

            if (distance <= 100)
            {
                position.X += direction.X * speed;
                position.Y += direction.Y * speed;
            }
        }

        public void setTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        public Texture2D getTexture()
        {
            return texture;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public Vector2 getDirection()
        {
            return direction;
        }

        public double getDistance()
        {
            return distance;
        }
    }
}
