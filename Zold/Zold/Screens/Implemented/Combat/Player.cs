﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Zold.Screens.Implemented.Combat
{
    class Player
    {
        private Texture2D texture;
        public Vector2 position;
        private Vector2 centerPosition;
        private Vector2 attackPosition;
        Timer attackTimer;
        private List<Enemy> enemies;
        public Vector2 bottomPosition { get; set; }
        public int mapEdge { get; set; }
        public int Hp { get; set; }
        public string Action { get; private set; }
        public string Direction { get; private set; }
        public int Speed { get; private set; }

        public Player(Vector2 position, int Hp, List<Enemy> enemies)
        {
            this.position = position;
            this.Hp = Hp;
            this.enemies = enemies;

            mapEdge = 150;
            Action = "";
            Speed = 2;

            centerPosition = new Vector2(position.X + 16, position.Y + 24);

            attackTimer = new Timer();
            attackTimer.Interval = 500;
            attackTimer.Elapsed += new ElapsedEventHandler(Attack);
        }

        public void Controlls()
        {
            centerPosition = new Vector2(position.X + 16, position.Y + 24);
            bottomPosition = new Vector2(position.X, position.Y + 44);

            if (attackTimer.Enabled == false)
                Action = "Idle";

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (bottomPosition.Y >= mapEdge)
                    position.Y -= Speed;
                attackTimer.Enabled = false;
                Action = "Moving";
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                position.Y += Speed;
                attackTimer.Enabled = false;
                Action = "Moving";
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                position.X -= Speed;
                attackPosition = new Vector2(centerPosition.X - 50, centerPosition.Y);
                attackTimer.Enabled = false;
                Action = "Moving";
                Direction = "Left";
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                position.X += Speed;
                attackPosition = new Vector2(centerPosition.X + 50, centerPosition.Y);
                attackTimer.Enabled = false;
                Action = "Moving";
                Direction = "Right";
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Action = "Attacking";
                attackTimer.Enabled = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                Block();
        }

        private void Block()
        {
            
        }

        public void Attack(object source, ElapsedEventArgs e)
        {
            enemies.ForEach( Enemy => {
                if (Enemy.CheckPointCollision(attackPosition))
                    Enemy.Hp -= 5;
            });
            attackTimer.Enabled = false;
            Action = "Idle";
        }

        public bool CheckPointCollision(Vector2 point)
        {
            if ((position.X < point.X) && (position.X + 32 > point.X) &&
                (position.Y < point.Y) && (position.Y + 48 > point.Y))
                return true;
            return false;
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