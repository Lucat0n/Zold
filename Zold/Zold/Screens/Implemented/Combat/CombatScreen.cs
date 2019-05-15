using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using Zold.Utilities;
using Zold.Screens.Implemented.Combat.CombatObject;
using Zold.Screens.Implemented.Combat.CombatObject.Characters.Enemies;
using Zold.Screens.Implemented.Combat.CombatObject.Characters;

namespace Zold.Screens.Implemented.Combat
{
    class CombatScreen : GameScreen
    {
        Player player;
        List<Enemy> enemies;
        List<Character> objectsToRender;
        public List<Projectile> Projectiles;
        string combatState;

        public CombatScreen(Player player, List<Enemy> enemies)
        {
            this.player = player;
            this.enemies = enemies;
            
            Projectiles = new List<Projectile>();
            objectsToRender = new List<Character>();
            objectsToRender.Add(player);
            objectsToRender.AddRange(enemies);

            foreach (Character character in objectsToRender)
            {
                character.CombatScreen = this;
            } 

            combatState = "";
            IsTransparent = false;
        }

        public override void Update(GameTime gameTime)
        {
            objectsToRender = objectsToRender.OrderBy(item => item.Position.Y).ToList();

            enemies.ForEach(enemy =>
            {
                enemy.AI(gameTime);
            });

            Projectiles.ForEach(projectile =>
            {
                projectile.Move(gameTime);
            });

            var enemiesToDelete = enemies.Where(x => x.Hp <= 0).ToArray();
            foreach (Enemy enemy in enemiesToDelete)
            {
                enemies.Remove(enemy);
                objectsToRender.Remove(enemy);
            }

            if (enemies.Count == 0)
            {
                combatState = "Wygrana";
                gameScreenManager.RemoveScreen(this);
                gameScreenManager.InsertScreen(new Map.MapManager());
            }
            else if (player.Hp <= 0)
            {
                combatState = "Przegrana";
                gameScreenManager.RemoveScreen(this);
                gameScreenManager.InsertScreen(new Map.MapManager());
            }
        }

        public override void LoadContent()
        {
            gameScreenManager.ContentLoader.LoadLocation("combat");
        }

        public override void Draw(GameTime gameTime)
        {
            // Sorting mode FrontToBack - layerDepth 1.0f = front, 0 = back
            gameScreenManager.GraphicsDevice.Clear(Color.CornflowerBlue);
            gameScreenManager.SpriteBatch.Begin(SpriteSortMode.FrontToBack);

            objectsToRender.ForEach(item =>
            {
                gameScreenManager.SpriteBatch.DrawString(Assets.Instance.Get("combat/Fonts/dialog"), "HP: " + item.Hp.ToString(), new Vector2(item.Position.X, item.Position.Y - 35), Color.Black);
                item.Animation(gameTime);
            });

            Projectiles.ForEach(projectile =>
            {
                projectile.Animation(gameTime);
            });
            
            gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("combat/Textures/line"), new Vector2(0, 150), null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);

            gameScreenManager.SpriteBatch.End();
        }

        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState)
        {
            player.Controls();
        }

        public override void UnloadContent()
        {
            gameScreenManager.ContentLoader.UnloadLocation("combat");
        }

        public Projectile MakeProjectile(Vector2 position, string texture, Vector2 destination, int width, int height)
        {
            return new Projectile(position, new SpriteBatchSpriteSheet(gameScreenManager.GraphicsDevice, Assets.Instance.Get(texture), 2, 1, width, height), destination, width, height);
        }
    }
}