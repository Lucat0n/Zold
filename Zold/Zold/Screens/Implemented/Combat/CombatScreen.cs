using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat
{
    class CombatScreen : GameScreen
    {
        Player player;
        List<Enemy> enemies;
        SpriteFont font;
        SpriteBatchSpriteSheet SpriteBatchSpriteSheet;

        public CombatScreen(Player player, List<Enemy> enemies)
        {
            font = Assets.Instance.Get("placeholders/Fonts/dialog");
            this.player = player;
            this.enemies = enemies;
            IsTransparent = false;
        }

        public override void Update(GameTime gameTime)
        {
            player.Controlls();

            enemies.ForEach(enemy =>
            {
                enemy.AI(gameTime);
            });

        }

        public override void LoadContent()
        {
            SpriteBatchSpriteSheet = new SpriteBatchSpriteSheet(gameScreenManager.GraphicsDevice, Assets.Instance.Get("placeholders/Textures/main"), 4, 3, 32, 48);
            SpriteBatchSpriteSheet.MakeAnimation(3, "Left", 250);
            SpriteBatchSpriteSheet.MakeAnimation(1, "Right", 250);
        }

        public override void Draw(GameTime gameTime)
        {
            gameScreenManager.GraphicsDevice.Clear(Color.CornflowerBlue);
            gameScreenManager.SpriteBatch.Begin();
            SpriteBatchSpriteSheet.Begin();

            if (player.Action == "Moving")
            {
                SpriteBatchSpriteSheet.PlayFullAniamtion(player.GetPosition(), player.Direction, gameTime);
            }
            //else if (player.Action == "Idle")
            else
            {
                if (player.Direction == "Right")
                    SpriteBatchSpriteSheet.Draw(player.GetPosition(), 1, 0);
                if (player.Direction == "Left")
                    SpriteBatchSpriteSheet.Draw(player.GetPosition(), 3, 0);
            }

            SpriteBatchSpriteSheet.End();

            gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("placeholders/Textures/line"), new Vector2(0, 150));

            //gameScreenManager.SpriteBatch.Draw(player.GetTexture(), player.GetPosition());
            gameScreenManager.SpriteBatch.DrawString(font, "HP: " + player.Hp.ToString(), new Vector2(15, 15), Color.Black);
            gameScreenManager.SpriteBatch.DrawString(font, "Y: " + player.position.Y.ToString(), new Vector2(player.position.X, player.position.Y - 25), Color.Black);
            gameScreenManager.SpriteBatch.DrawString(font, player.Action, new Vector2(player.position.X, player.position.Y - 15), Color.Black);


            enemies.ForEach(enemy =>
            {
                gameScreenManager.SpriteBatch.Draw(enemy.GetTexture(), enemy.GetPosition());

                //spriteBatch.DrawString(font, "Distance: " + enemy.Distance.ToString(), new Vector2(100, 80), Color.Black);
                //spriteBatch.DrawString(font, "Direction: \n x: " + enemy.GetDirection().X.ToString() + " y: " + enemy.GetDirection().Y.ToString(), new Vector2(100, 100), Color.Black);

                gameScreenManager.SpriteBatch.DrawString(font, "bot.Y: " + enemy.bottomPosition.Y.ToString(), new Vector2(enemy.position.X, enemy.position.Y - 35), Color.Black);
                gameScreenManager.SpriteBatch.DrawString(font, "HP: " + enemy.Hp.ToString(), new Vector2(enemy.position.X, enemy.position.Y - 25), Color.Black);
                gameScreenManager.SpriteBatch.DrawString(font, enemy.Action, new Vector2(enemy.position.X, enemy.position.Y - 15), Color.Black);
            });

            gameScreenManager.SpriteBatch.End();
        }

        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState) { }
        public override void UnloadContent() { }
    }
}