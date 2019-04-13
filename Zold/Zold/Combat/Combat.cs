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
        }

        void Initialize()
        {
            enemies = new List<Enemy>();
            player = new Player(new Vector2(0, 0), 100, enemies);
            enemy = new Enemy(player, new Vector2(300, 300));
            enemies.Add(enemy);

            player.SetTexture(playerTex);
            enemy.SetTexture(enemyTex);
        }

        // Called every update
        public void Update(GameTime gameTime)
        {
            player.Controlls();
            enemy.AI(gameTime);
        }

        // Called every Draw
        public void Draw(SpriteBatch spriteBatch)
        {
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