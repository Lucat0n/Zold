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
    class Combat
    {
        Player player;
        List<Enemy> enemies;
        int mapEdge;

        //temp
        SpriteFont font;

        public Combat(Player player, List<Enemy> enemies, SpriteFont font)
        {
            this.font = font;
            this.player = player;
            this.enemies = enemies;
        }

        // Called every update
        public void Update(GameTime gameTime)
        {
            player.Controlls();

            enemies.ForEach( enemy => {
                enemy.AI(gameTime);
            });
            
        }

        // Called every Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(player.GetTexture(), player.GetPosition());
            spriteBatch.DrawString(font, "HP: " + player.Hp.ToString(), new Vector2(15, 15), Color.Black);
            spriteBatch.DrawString(font, player.Action, new Vector2(player.position.X, player.position.Y - 15), Color.Black);


            enemies.ForEach(enemy => {
               spriteBatch.Draw(enemy.GetTexture(), enemy.GetPosition());

               //spriteBatch.DrawString(font, "Distance: " + enemy.Distance.ToString(), new Vector2(100, 80), Color.Black);
               //spriteBatch.DrawString(font, "Direction: \n x: " + enemy.GetDirection().X.ToString() + " y: " + enemy.GetDirection().Y.ToString(), new Vector2(100, 100), Color.Black);
               
               spriteBatch.DrawString(font, "HP: " + enemy.Hp.ToString(), new Vector2(enemy.position.X, enemy.position.Y - 25), Color.Black);
               spriteBatch.DrawString(font, enemy.Action, new Vector2(enemy.position.X, enemy.position.Y - 15), Color.Black);
           });
        }
    }
}