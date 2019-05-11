using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using Zold.Utilities;
using Zold.Screens.Implemented.Combat.Characters;
using Zold.Screens.Implemented.Combat.Characters.Enemies;

namespace Zold.Screens.Implemented.Combat
{
    class CombatScreen : GameScreen
    {
        Player player;
        List<Enemy> enemies;
        SpriteFont font;
        string combatState;

        public CombatScreen(Player player, List<Enemy> enemies)
        {
            font = Assets.Instance.Get("combat/Fonts/dialog");
            this.player = player;
            this.enemies = enemies;
            combatState = "";
            IsTransparent = false;
        }

        public override void Update(GameTime gameTime)
        {
            enemies.ForEach(enemy =>
            {
                enemy.AI(gameTime);
            });

            var enemiesToDelete = enemies.Where(x => x.Hp <= 0).ToArray();
            foreach (Enemy enemy in enemiesToDelete)
            {
                enemies.Remove(enemy);
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

            player.Animation(gameTime);
            
            gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("combat/Textures/line"), new Vector2(0, 150), null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);

            #region debug-text
            // Debug text
            gameScreenManager.SpriteBatch.DrawString(font, combatState, new Vector2(400, 15), Color.Black);
            gameScreenManager.SpriteBatch.DrawString(font, "HP: " + player.Hp.ToString(), new Vector2(15, 15), Color.Black);
            gameScreenManager.SpriteBatch.DrawString(font, "Y: " + player.position.Y.ToString(), new Vector2(player.position.X, player.position.Y - 25), Color.Black);
            gameScreenManager.SpriteBatch.DrawString(font, "Depth: " + player.SpriteBatchSpriteSheet.LayerDepth.ToString(), new Vector2(player.position.X, player.position.Y - 35), Color.Black);
            gameScreenManager.SpriteBatch.DrawString(font, player.Action, new Vector2(player.position.X, player.position.Y - 15), Color.Black);
            #endregion

            enemies.ForEach(enemy =>
            {
                enemy.Animation();

                #region debug-text
                // Debug text
                gameScreenManager.SpriteBatch.DrawString(font, "HP: " + enemy.Hp.ToString(), new Vector2(enemy.position.X, enemy.position.Y - 35), Color.Black);
                gameScreenManager.SpriteBatch.DrawString(font, "Depth: " + enemy.LayerDepth.ToString(), new Vector2(enemy.position.X, enemy.position.Y - 25), Color.Black);
                gameScreenManager.SpriteBatch.DrawString(font, enemy.Action, new Vector2(enemy.position.X, enemy.position.Y - 15), Color.Black);
                #endregion
            });

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
    }
}