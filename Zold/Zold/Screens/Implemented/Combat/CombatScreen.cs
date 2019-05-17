using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using Zold.Utilities;
using Zold.Screens.Implemented.Combat.CombatObjects;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters.Enemies;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters;

namespace Zold.Screens.Implemented.Combat
{
    class CombatScreen : GameScreen
    {
        Player player;
        List<Enemy> enemies;
        List<CombatObject> objectsToRender;
        List<Projectile> projectiles;
        string combatState;

        public CombatScreen(Player player, List<Enemy> enemies)
        {
            this.player = player;
            this.enemies = enemies;
            
            projectiles = new List<Projectile>();
            objectsToRender = new List<CombatObject>();
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

            projectiles.ForEach(projectile =>
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
            }
            else if (player.Hp <= 0)
            {
                combatState = "Przegrana";
                gameScreenManager.RemoveScreen(this);
            }
        }

        public override void LoadContent()
        {
        }

        public override void Draw(GameTime gameTime)
        {
            // Sorting mode FrontToBack - layerDepth 1.0f = front, 0 = back
            gameScreenManager.GraphicsDevice.Clear(Color.CornflowerBlue);
            gameScreenManager.SpriteBatch.Begin(SpriteSortMode.FrontToBack);

            objectsToRender.ForEach(item =>
            {
                item.Animation(gameTime);
            });

            enemies.ForEach(enemy =>
            {
                gameScreenManager.SpriteBatch.DrawString(Assets.Instance.Get("combat/Fonts/dialog"), "HP: " + enemy.Hp.ToString(), new Vector2(enemy.Position.X, enemy.Position.Y - 35), Color.Black);
                gameScreenManager.SpriteBatch.DrawString(Assets.Instance.Get("combat/Fonts/dialog"), "Action: " + enemy.action, new Vector2(enemy.Position.X, enemy.Position.Y - 25), Color.Black);
            });

            gameScreenManager.SpriteBatch.DrawString(Assets.Instance.Get("combat/Fonts/dialog"), "HP: " + player.Hp, new Vector2(player.Position.X, player.Position.Y - 15), Color.Black);
            gameScreenManager.SpriteBatch.DrawString(Assets.Instance.Get("combat/Fonts/dialog"), "Y: " + player.Position.Y, new Vector2(player.Position.X, player.Position.Y - 25), Color.Black);
            gameScreenManager.SpriteBatch.DrawString(Assets.Instance.Get("combat/Fonts/dialog"), "X: " + player.Position.X, new Vector2(player.Position.X, player.Position.Y - 15), Color.Black);

            gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("combat/Textures/line"), new Vector2(0, 150), null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("combat/Textures/line"), new Vector2(0, 450), null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);

            gameScreenManager.SpriteBatch.End();
        }

        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState)
        {
            player.Controls();
        }

        public override void UnloadContent()
        {
        }

        public void MakeProjectile(Vector2 position, string texture, Vector2 destination, int width, int height)
        {
            projectiles.Add(new Projectile(position, new SpriteBatchSpriteSheet(gameScreenManager.GraphicsDevice, Assets.Instance.Get(texture), 2, 1, width, height), destination, width, height));
            objectsToRender.AddRange(projectiles);
        }
    }
}