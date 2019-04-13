using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Combat
{
    /// <summary>
    /// This is the main class of Combat.
    /// </summary>
    public class Combat
    {
        Player player;
        Enemy enemy;
        List<Enemy> enemies;

        //temp
        SpriteFont font;
        Texture2D playerTex;
        Texture2D enemyTex;

        public Combat(Texture2D playerTex, Texture2D enemyTex, SpriteFont font)
        {
            //temp
            this.font = font;
            this.playerTex = playerTex;
            this.enemyTex = enemyTex;

            Initialize();
            LoadContent();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        void Initialize()
        {
            enemies = new List<Enemy>();
            player = new Player(new Vector2(0, 0), 100, enemies);
            enemy = new Enemy(player, new Vector2(300, 300));
            enemies.Add(enemy);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        void LoadContent()
        {
            player.SetTexture(playerTex);
            enemy.SetTexture(enemyTex);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            player.Controlls();
            enemy.AI(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(enemy.getTexture(), enemyPos);
            spriteBatch.Draw(enemy.GetTexture(), enemy.GetPosition());
            spriteBatch.Draw(player.GetTexture(), player.GetPosition());


            spriteBatch.DrawString(font, "Distance: " + enemy.Distance.ToString(), new Vector2(100, 80), Color.Black);
            spriteBatch.DrawString(font, "Direction: \n x: " + enemy.GetDirection().X.ToString() + " y: " + enemy.GetDirection().Y.ToString(), new Vector2(100, 100), Color.Black);
            spriteBatch.DrawString(font, "HP: " + player.Hp.ToString(), new Vector2(15, 15), Color.Black);
            spriteBatch.DrawString(font, "HP: " + enemy.Hp.ToString(), new Vector2(700, 15), Color.Black);
            spriteBatch.DrawString(font, player.Action, new Vector2(player.position.X, player.position.Y - 15), Color.Black);
            spriteBatch.DrawString(font, enemy.Action, new Vector2(enemy.position.X, enemy.position.Y - 15), Color.Black);
        }
    }
}