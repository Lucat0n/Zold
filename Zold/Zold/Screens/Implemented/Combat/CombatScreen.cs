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
using Microsoft.Xna.Framework.Audio;
using Zold.Screens.Implemented.Combat.Skills;

namespace Zold.Screens.Implemented.Combat
{
    class CombatScreen : GameScreen
    {
        public Player player;
        public List<Enemy> enemies;
        public List<CombatObject> objects;
        public List<CombatObject> objectsToAdd;
        public List<CombatObject> objectsToRemove;
        public Timer timer;
        public Utilities.Map Map;

        bool songStart = false;

        private SoundEffect bgMusic;
        private SoundEffect combatMusic;
        private SoundEffectInstance bg;

        private bool isEscPressed = false;

        //DEBUG
        Texture2D pixel;

        public CombatScreen(Player player, List<Enemy> enemies)
        {
            this.player = player;
            this.enemies = enemies;
            
            objects = new List<CombatObject>();
            objectsToAdd = new List<CombatObject>();
            objectsToRemove = new List<CombatObject>();

            IsTransparent = false;

            timer = new Timer(e => { OnTimerTick(); }, null, 0, 500);

            

        }

        public override void Update(GameTime gameTime)
        {
            CheckProjectileCollisions();
            objects = objects.OrderBy(item => item.BottomPosition.Y).ToList();

            AddAndRemoveObjects();

            if (!songStart)
            {
                bg.Play();
                songStart = true;

            }
            foreach (CombatObject obj in objects)
            {
                obj.BaseSpeed = gameScreenManager.baseSpeed;
                obj.Update(gameTime);
            }

            var enemiesToDelete = enemies.Where(x => x.Statistics.Health <= 0).ToArray();
            foreach (Enemy enemy in enemiesToDelete)
            {
                enemies.Remove(enemy);
                objects.Remove(enemy);
            }

            if (enemies.Count == 0 || player.Statistics.Health <= 0)
            {

                bg.Stop();
                bg = bgMusic.CreateInstance();
                bg.Play();

                gameScreenManager.RemoveScreen(this);
            }

            CombatCamera.Follow(player.Position);
        }

        public override void LoadContent()
        {
            objects.Add(player);
            objects.AddRange(enemies);
            gameScreenManager.ContentLoader.LoadLocation("placeholders");

            bgMusic = Assets.Instance.Get("placeholders/Music/explo");
            combatMusic = Assets.Instance.Get("placeholders/Music/comba");
            bg = combatMusic.CreateInstance();


            Map = new Utilities.Map(gameScreenManager, this);

            foreach (CombatObject obj in objects)
            {
                if(obj is Character)
                {
                    obj.CombatScreen = this;
                    obj.Map = new Box(new Vector2(Map.LeflMapEdge, Map.TopMapEdge), null, Map.RightMapEdge, Map.BottomMapEdge - Map.TopMapEdge);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            // Sorting mode FrontToBack - layerDepth 1.0f = front, 0 = back
            gameScreenManager.GraphicsDevice.Clear(Color.Black);
            gameScreenManager.SpriteBatch.Begin(SpriteSortMode.FrontToBack,transformMatrix:CombatCamera.BindCameraTransformation());
            /*
            // DEBUG - grid
            pixel = new Texture2D(GameScreenManager.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });
            for (int x = 0; x < 40; x++)
                for (int y = 0; y < 16; y++)
                {
                    Map.Nodes[new RoyT.AStar.Position(x,y)].DrawBorder(pixel, GameScreenManager.GraphicsDevice, gameScreenManager.SpriteBatch);
                }
            // END OF DEBUG
            */
            Map.DrawTiles(0);
            objects.ForEach(obj => {
                obj.Draw(gameTime);
            });

            gameScreenManager.SpriteBatch.End();
        }

        private void AddAndRemoveObjects()
        {
            objects.AddRange(objectsToAdd);
            objectsToAdd.Clear();

            foreach (CombatObject obj in objectsToRemove)
            {
                objects.Remove(obj);
            }
            objectsToRemove.Clear();
        }

        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState)
        {
            player.Controls();

            if(keyboardState.IsKeyDown(Keys.F6) && !isEscPressed)
            {
                isEscPressed = true;
                AddBuff(player.Statistics, BuffFactory.CreateTimedBuff(3, 0.2f, 10));
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

        public void MakeEnemyProjectile(Vector2 position, Vector2 destination, Skill skill, int dmg, string texture, int width, int height)
        {
            Projectile projectile = new Projectile(position, destination, skill, dmg, new SpriteBatchSpriteSheet(gameScreenManager.GraphicsDevice, Assets.Instance.Get(texture), 2, 1, width, height), width, height);
            projectile.Targets.Add(player);
            objectsToAdd.Add(projectile);
        }

        public void MakePlayerProjectile(Vector2 position, Vector2 destination, Skill skill, int dmg, string texture, int width, int height)
        {
            Projectile projectile = new Projectile(position, destination, skill, dmg, new SpriteBatchSpriteSheet(gameScreenManager.GraphicsDevice, Assets.Instance.Get(texture), 2, 1, width, height), width, height);
            projectile.Targets.AddRange(enemies);
            objectsToAdd.Add(projectile);
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
                        if (target.CheckBoxCollision(projectile))
                        {
                            toDelete = target;
                            target.Statistics.Health -= projectile.Statistics.Damage;
                            if (projectile.Skill != null)
                                projectile.Skill.ApplyEffect(target);
                        }
                    });
                    projectile.Targets.Remove(toDelete);
                }
            });
        }

        public bool CheckLineOfSight(Ray ray, float distance)
        {
            foreach (Node node in Map.CollisionNodes)
            {
                if(node.HitBox.Intersects(ray) == null || distance < node.HitBox.Intersects(ray))
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckNodeCollision(CombatObject obj)
        {
            bool collision = false;
            Map.CollisionNodes.ForEach(node =>
            {
                if ((node.PosX < obj.BottomPosition.X) && (node.PosX + node.Width > obj.BottomPosition.X) &&
                (node.PosY < obj.BottomPosition.Y) && (node.PosY + node.Height > obj.BottomPosition.Y))
                {
                    collision = true;
                    if (obj is Player)
                    {
                        Vector2 newPos = obj.Position;

                        if (obj.Velocity.X > 0) // object came from the right
                            newPos.X = node.PosX - obj.Width / 2;
                        else if (obj.Velocity.X < 0) // object came from the left
                            newPos.X = node.PosX + node.Width - obj.Width / 2;
                        if (obj.Velocity.Y > 0) // object came from the top
                            newPos.Y = node.PosY - obj.Height;
                        else if (obj.Velocity.Y < 0) // object came from the bottom
                            newPos.Y = node.PosY + node.Height - obj.Height;

                        obj.SetPosition(newPos);
                    }
                    else if (obj is Projectile)
                    {
                        objectsToRemove.Add(obj);
                    }
                    else if (obj is Charger)
                    {
                        (obj as Charger).StopCharge();
                    }
                }
            });
            return collision;
        }
    }
}