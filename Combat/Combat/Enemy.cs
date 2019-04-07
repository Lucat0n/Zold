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
        Timer attackTimer = new System.Timers.Timer();
        public Vector2 position;
        public Vector2 direction;
        Vector2 attackPosition;
        int damage;
        float speed;
        double distance;
        public string action { get; set; }
        double attackStart;
        float attackEnd;


        public Enemy(Player player, Vector2 position)
        {
            this.player = player;
            this.position = position;
            damage = 5;

            action = "Idle";

            attackTimer.Interval = 1000;
            attackTimer.Elapsed += new ElapsedEventHandler(Attack);
        }

        public void AI(GameTime gameTime)
        {
            speed = 60f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            CalcDirection();

            if (attackTimer.Enabled == true)
                action = "Attacking";
            else
                action = "Idle";

            if (distance <= 50 && attackTimer.Enabled == false)
            {
                attackPosition = player.GetPosition();
                attackTimer.Enabled = true;
            }
            else if (distance <= 200 && attackTimer.Enabled == false)
            {
                action = "Moving";
                Move();
            }
        }

        private void Attack(object source, ElapsedEventArgs e)
        {
            if (attackPosition == player.GetPosition())
                player.hp -= damage;
            attackTimer.Enabled = false;
        }

        public void Move()
        {
            position.X += direction.X * speed;
            position.Y += direction.Y * speed;
        }

        private void CalcDirection()
        {
            distance = Vector2.Distance(player.GetPosition(), position);
            direction = new Vector2(player.GetPosition().X - position.X, player.GetPosition().Y - position.Y);
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

        public double GetDistance()
        {
            return distance;
        }
    }
}