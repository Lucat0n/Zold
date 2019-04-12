using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Combat
{
    class Enemy
    {
        Player player;
        Texture2D texture;
        Timer attackTimer;
        public Vector2 position;
        public Vector2 direction;
        Vector2 attackPosition;
        public int Damage { get; private set; }
        public int Hp { get; set; }
        public float Speed { get; private set; }
        public double Distance { get; private set; }
        public string Action { get; private set; }
        public double AttackStart { get; private set; }
        public float AttackEnd { get; private set; }


        public Enemy(Player player, Vector2 position)
        {
            this.player = player;
            this.position = position;
            Damage = 5;
            Hp = 50;

            Action = "Idle";

            attackTimer = new Timer();
            attackTimer.Interval = 1000;
            attackTimer.Elapsed += new ElapsedEventHandler(Attack);
        }

        public void AI(GameTime gameTime)
        {
            Speed = 60f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            CalcDirection();

            if (attackTimer.Enabled == true)
                Action = "Attacking";
            else
                Action = "Idle";

            if (Distance <= 50 && attackTimer.Enabled == false)
            {
                attackPosition = player.GetCenterPosition();
                attackTimer.Enabled = true;
            }
            else if (Distance <= 200 && attackTimer.Enabled == false)
            {
                Action = "Moving";
                Move();
            }
        }

        private void Attack(object source, ElapsedEventArgs e)
        {
            if (player.CheckPointCollision(attackPosition))
                player.Hp -= Damage;
            attackTimer.Enabled = false;
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