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
        public Player player;
        public Texture2D texture;
        public Vector2 position;
        public Vector2 direction;
        public Vector2 attackPosition;
        public int Damage { get; set; }
        public int Hp { get; set; }
        public float Speed { get; set; }
        public double Distance { get; set; }
        public string Action { get; set; }
        public double AttackStart { get; set; }
        public float AttackEnd { get; set; }


        public Enemy(Player player, Vector2 position)
        {
            this.player = player;
            this.position = position;
            Damage = 5;
            Hp = 50;

            Action = "Idle";
        }

        public virtual void AI(GameTime gameTime)
        {
        }

        public virtual void Move()
        {
        }

        public void CalcDirection()
        {
            Distance = Vector2.Distance(player.GetCenterPosition(), position);
            direction = new Vector2(player.GetCenterPosition().X - position.X, player.GetCenterPosition().Y - position.Y);
            direction.Normalize();
        }

        public bool CheckPointCollision(Vector2 point)
        {
            if ((position.X < point.X) && (position.X + 32 > point.X) &&
                (position.Y < point.Y) && (position.Y + 48 > point.Y))
                return true;
            return false;
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