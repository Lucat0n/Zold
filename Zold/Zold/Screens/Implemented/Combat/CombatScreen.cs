using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using Zold.Utilities;
using Zold.Screens.Implemented.Combat.CombatObjects;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters.Enemies;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters;
using Zold.Screens.Implemented.Combat.Utilities;
using System.Threading;
using Zold.Buffs;
using System.Threading.Tasks;
using TiledSharp;
using System;
using Zold.Statistics;

namespace Zold.Screens.Implemented.Combat
{
    class CombatScreen : GameScreen
    {
        public Player player;
        public List<Enemy> enemies;
        public List<CombatObject> objects;
        public Timer timer;
        public Utilities.Map Map;

        private bool isEscPressed = false;

        //DEBUG
        Texture2D pixel;

        public CombatScreen(Player player, List<Enemy> enemies)
        {
            this.player = player;
            this.enemies = enemies;
            
            objects = new List<CombatObject>();

            IsTransparent = false;

            timer = new Timer(e => { OnTimerTick(); }, null, 0, 500);
        }

        public override void Update(GameTime gameTime)
        {
            CheckProjectileCollisions();
            objects = objects.OrderBy(item => item.BottomPosition.Y).ToList();

            objects.ForEach(obj => {
                obj.BaseSpeed = gameScreenManager.baseSpeed;
                obj.Update(gameTime);
            });

            var enemiesToDelete = enemies.Where(x => x.Statistics.Health <= 0).ToArray();
            foreach (Enemy enemy in enemiesToDelete)
            {
                enemies.Remove(enemy);
                objects.Remove(enemy);
            }

            if (enemies.Count == 0)
            {
                gameScreenManager.RemoveScreen(this);
            }
            else if (player.Statistics.Health <= 0)
            {
                gameScreenManager.RemoveScreen(this);
            }
        }

        public override void LoadContent()
        {
            objects.Add(player);
            objects.AddRange(enemies);

            Map = new Utilities.Map(gameScreenManager, this);

            foreach (CombatObject obj in objects)
            {
                if(obj is Character)
                {
                    obj.CombatScreen = this;
                    obj.Map = new Projectile(new Vector2(Map.LeflMapEdge, Map.TopMapEdge), 0, null, Vector2.Zero, Map.RightMapEdge, Map.BottomMapEdge - Map.TopMapEdge);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            // Sorting mode FrontToBack - layerDepth 1.0f = front, 0 = back
            gameScreenManager.GraphicsDevice.Clear(Color.CornflowerBlue);
            gameScreenManager.SpriteBatch.Begin(SpriteSortMode.FrontToBack);

            // DEBUG - grid
            pixel = new Texture2D(GameScreenManager.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });
            for (int x = 0; x < 40; x++)
                for (int y = 0; y < 16; y++)
                {
                    Map.Nodes[x + "_" + y].DrawBorder(pixel, GameScreenManager.GraphicsDevice, gameScreenManager.SpriteBatch);
                }
            // END OF DEBUG

            Map.DrawTiles(0);
            objects.ForEach(obj => {
                obj.Draw(gameTime);
            });
            /*  DEBUG
            enemies.ForEach(enemy =>
            {
                gameScreenManager.SpriteBatch.DrawString(Assets.Instance.Get("combat/Fonts/dialog"), "HP: " + enemy.Statistics.Health.ToString(), new Vector2(enemy.Position.X, enemy.Position.Y - 35), Color.Black);
                gameScreenManager.SpriteBatch.DrawString(Assets.Instance.Get("combat/Fonts/dialog"), "Action: " + enemy.action, new Vector2(enemy.Position.X, enemy.Position.Y - 25), Color.Black);
            });

            gameScreenManager.SpriteBatch.DrawString(Assets.Instance.Get("combat/Fonts/dialog"), "HP: " + player.Statistics.Health, new Vector2(player.Position.X, player.Position.Y - 35), Color.Black);
            gameScreenManager.SpriteBatch.DrawString(Assets.Instance.Get("combat/Fonts/dialog"), "Y: " + player.Position.Y, new Vector2(player.Position.X, player.Position.Y - 25), Color.Black);
            gameScreenManager.SpriteBatch.DrawString(Assets.Instance.Get("combat/Fonts/dialog"), "X: " + player.Position.X, new Vector2(player.Position.X, player.Position.Y - 15), Color.Black);
            */

            gameScreenManager.SpriteBatch.End();
        }

        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState)
        {
            player.Controls();

            if(keyboardState.IsKeyDown(Keys.F6) && !isEscPressed)
            {
                isEscPressed = true;
                AddBuff(player.Statistics, BuffFactory.CreateTimedBuff(-10, 0));
            }else if (keyboardState.IsKeyUp(Keys.F6))
            {
                isEscPressed = false;
            }
        }

        public override void UnloadContent()
        {
           // timer.Change(Timeout.Infinite, Timeout.Infinite);
           // timer.Dispose();
        }

        public void MakeEnemyProjectile(Vector2 position, int dmg, string texture, Vector2 destination, int width, int height)
        {
            Projectile projectile = new Projectile(position, dmg, new SpriteBatchSpriteSheet(gameScreenManager.GraphicsDevice, Assets.Instance.Get(texture), 2, 1, width, height), destination, width, height);
            projectile.Targets.Add(player);
            objects.Add(projectile);
        }

        public void MakePlayerProjectile(Vector2 position, int dmg, string texture, Vector2 destination, int width, int height)
        {
            Projectile projectile = new Projectile(position, dmg, new SpriteBatchSpriteSheet(gameScreenManager.GraphicsDevice, Assets.Instance.Get(texture), 2, 1, width, height), destination, width, height);
            projectile.Targets.AddRange(enemies);
            objects.Add(projectile);
        }

        public void AddObstacle(Obstacle obstacle)
        {
            objects.Add(obstacle);
        }

        private void OnTimerTick()
        {
            Parallel.ForEach(objects, obj =>
            {
                if (obj is Character)
                    obj.Statistics.UpdateBuffs();
            });
        }

        public void AddBuff(Stats s, IBuff buff)
        {
            buff.Statistics = s;
            s.buffSet.Add(buff);
            s.buffSet.TryGetValue(buff, out IBuff temp);
            temp.Start();
        }

        private void CheckProjectileCollisions()
        {
            Character toDelete = null;
            Projectile projectile = null;
            objects.ForEach(obj =>
            {
                if (obj is Projectile)
                {
                    projectile = (Projectile)obj;
                    projectile.Targets.ForEach(target =>
                    {
                        if (target.CheckBoxCollision(projectile.Position, target))
                        {
                            toDelete = target;
                            target.Statistics.Health -= projectile.Statistics.Damage;
                        }
                    });
                    projectile.Targets.Remove(toDelete);
                }
            });
        }
    }
}