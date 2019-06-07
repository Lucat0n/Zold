using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using Zold.Utilities;
using Zold.Screens.Implemented.Combat.CombatObjects;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters.Enemies;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters;
using System.Threading;
using Zold.Buffs;
using System.Threading.Tasks;
using TiledSharp;
using System;
using Zold.Statistics;
using Zold.Screens.Implemented.Combat.Skills;

namespace Zold.Screens.Implemented.Combat
{
    class CombatScreen : GameScreen
    {
        public Player player;
        public List<Enemy> enemies;
        public List<Character> charactersToRender;
        public List<Projectile> projectiles;
        public Timer timer;
        public int TopMapEdge;
        public int BottomMapEdge;
        public int RightMapEdge;
        public int LeflMapEdge;

        private bool isEscPressed = false;

        private TmxMap currentMap;
        private SpriteBatchSpriteSheet mapSprite;
        private Texture2D tileset;
        private int tileWidth;
        private int tileHeight;
        private int tilesetTilesWide;
        private int tilesetTilesHigh;

        public CombatScreen(Player player, List<Enemy> enemies)
        {
            this.player = player;
            this.enemies = enemies;

            projectiles = new List<Projectile>();
            charactersToRender = new List<Character>();

            IsTransparent = false;

            timer = new Timer(e => { OnTimerTick(); }, null, 0, 500);
        }

        public override void Update(GameTime gameTime)
        {
            CheckProjectileCollisions();
            charactersToRender = charactersToRender.OrderBy(item => item.Position.Y).ToList();

            charactersToRender.ForEach(character => character.BaseSpeed = gameScreenManager.baseSpeed);

            enemies.ForEach(enemy =>
            {
                enemy.AI(gameTime);
            });

            projectiles.ForEach(projectile =>
            {
                projectile.BaseSpeed = gameScreenManager.baseSpeed;
                projectile.Move(gameTime);
            });

            var enemiesToDelete = enemies.Where(x => x.Statistics.Health <= 0).ToArray();
            foreach (Enemy enemy in enemiesToDelete)
            {
                enemies.Remove(enemy);
                charactersToRender.Remove(enemy);
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
            currentMap = new TmxMap("Content/graphic/combat/combat_city.tmx");
            mapSprite = new SpriteBatchSpriteSheet(gameScreenManager.GraphicsDevice, null, 0, 0, 0, 0);
            
            charactersToRender.Add(player);
            charactersToRender.AddRange(enemies);

            InitMap(currentMap);

            foreach (Character character in charactersToRender)
            {
                character.CombatScreen = this;
                character.Map = new CombatObjects.Map(new Vector2(LeflMapEdge, TopMapEdge), null, RightMapEdge, BottomMapEdge - TopMapEdge);
            }
        }

        public override void Draw(GameTime gameTime)
        {

            // Sorting mode FrontToBack - layerDepth 1.0f = front, 0 = back
            gameScreenManager.GraphicsDevice.Clear(Color.CornflowerBlue);
            gameScreenManager.SpriteBatch.Begin(SpriteSortMode.FrontToBack);

            DrawTiles(0, currentMap);
            charactersToRender.ForEach(item =>
            {
                item.Draw(gameTime);
            });
            projectiles.ForEach(projectile =>
            {
                projectile.Draw(gameTime);
            });

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

        public void MakeEnemyProjectile(Vector2 position, Vector2 destination, Skill skill, int dmg, string texture, int width, int height)
        {
            Projectile projectile = new Projectile(position, destination, skill, dmg, new SpriteBatchSpriteSheet(gameScreenManager.GraphicsDevice, Assets.Instance.Get(texture), 2, 1, width, height), width, height);
            projectile.Targets.Add(player);
            projectiles.Add(projectile);
        }

        public void MakePlayerProjectile(Vector2 position, Vector2 destination, Skill skill, int dmg, string texture, int width, int height)
        {
            Projectile projectile = new Projectile(position, destination, skill, dmg, new SpriteBatchSpriteSheet(gameScreenManager.GraphicsDevice, Assets.Instance.Get(texture), 2, 1, width, height), width, height);
            projectile.Targets.AddRange(enemies);
            projectiles.Add(projectile);
        }

        private void OnTimerTick()
        {
            Parallel.ForEach(charactersToRender, character =>
            {
                character.Statistics.UpdateBuffs();
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
            projectiles.ForEach(projectile =>
            {
                projectile.Targets.ForEach(target =>
                {
                    if (target.CheckBoxCollision(projectile.Position, target))
                    {
                        toDelete = target;
                        target.Statistics.Health -= projectile.Statistics.Damage;
                        if (projectile.Skill != null)
                            projectile.Skill.ApplyEffect(target);
                    }
                });
                projectile.Targets.Remove(toDelete);
            });
        }

        public virtual void InitMap(TmxMap currentMap)
        {
            tileset = gameScreenManager.Content.Load<Texture2D>("graphic\\combat\\" + currentMap.Tilesets[0].Name.ToString());
            tileWidth = currentMap.Tilesets[0].TileWidth;
            tileHeight = currentMap.Tilesets[0].TileHeight;
            tilesetTilesWide = tileset.Width / tileWidth;
            tilesetTilesHigh = tileset.Height / tileHeight;

            TopMapEdge = int.Parse(currentMap.Layers[1].Properties["Height"]) * tileHeight;
            BottomMapEdge = tileset.Height;
            RightMapEdge = tileset.Width;
            LeflMapEdge = 0;
        }

        public virtual void DrawTiles(int layer, TmxMap map)
        {
            for (var i = 0; i < map.Layers[layer].Tiles.Count; i++)
            {
                int gid = map.Layers[layer].Tiles[i].Gid;

                if (gid != 0)
                {
                    int tileFrame = gid - 1;
                    int column = tileFrame % tilesetTilesWide;
                    int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);
                    float x = (i % map.Width) * map.TileWidth;
                    float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight;

                    Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);
                    mapSprite.Begin();
                    mapSprite.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                    mapSprite.End();
                }
            }
        }
    }
}