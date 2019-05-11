using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using Zold.Utilities;
using Zold.Screens.Implemented.Combat.Characters;
using Zold.Screens.Implemented.Combat.Characters.Enemies;
using System;

namespace Zold.Screens.Implemented.Combat
{
    class CombatScreen : GameScreen
    {
        Player player;
        List<Enemy> enemies;
        List<Character> objectsToRender;
        string combatState;

        public CombatScreen(Player player, List<Enemy> enemies)
        {
            this.player = player;
            this.enemies = enemies;

            objectsToRender = new List<Character>();
            objectsToRender.Add(player);
            objectsToRender.AddRange(enemies);
 

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

            objectsToRender.ForEach(item => {
                gameScreenManager.SpriteBatch.DrawString(Assets.Instance.Get("combat/Fonts/dialog"), "HP: " + item.Hp.ToString(), new Vector2(item.Position.X, item.Position.Y - 35), Color.Black);
                item.Animation(gameTime);
            }) ;
            
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
    }
}