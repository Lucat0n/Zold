using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using TiledSharp;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Map
{
    class Camera
    {
        GameScreenManager gameScreenManager;
        Location location;

        public Camera(GameScreenManager gameScreenManager, Location location)
        {
            this.gameScreenManager = gameScreenManager;
            this.location = location;
        }

        public void moveCamera(int lewy, int gorny, int prawy, int dolny, GameTime gameTime, Player player, TmxMap currentMap, Rectangle bounds, List<Rectangle> colisionTiles)
        {
            int mapWidth = currentMap.Width * 32;
            int mapHeight = currentMap.Height * 32;

            int sh;
            int sw;
            if (gameScreenManager.IsFullScreenOn)
            {
                sh = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                sw = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            }
            else
            {
                sh = MapManager.screenHeight;
                sw = MapManager.screenWdth;
            }

            KeyboardState keyState = Keyboard.GetState();

            int scrollx = 0, scrolly = 0;

            //right border
            if (player.GetPosition().X > prawy && keyState.IsKeyDown(Keys.Right) && bounds.X - sw > -1 * mapWidth)
            {
               // canMoveRight = false;

                scrollx = -16;
                // m_animPercent = 0;

                // player.SetPosition(player.GetPosition() - new Vector2(8, 0));
                player.SetPosition(player.GetPosition().X - 32, player.GetPosition().Y);
                // player.move(player.Width, player.Height, canMoveLeft, canMoveUp, canMoveRight, canMoveDown, gameTime);
            }

            if (player.GetPosition().X < lewy && keyState.IsKeyDown(Keys.Left) && bounds.X < 0)
            {

                // location.getColideObjects(0, currentMap, bounds);
                scrollx = 16;
                player.SetPosition(player.GetPosition().X + 32, player.GetPosition().Y);
            }

            if (player.GetPosition().Y > dolny && keyState.IsKeyDown(Keys.Down) && bounds.Y - sh > -1 * mapHeight)
            {
                //   canMoveRight = false;
                //location.getColideObjects(0, currentMap, bounds);
                scrolly = -16;
                player.SetPosition(player.GetPosition().X, player.GetPosition().Y - 32);
            }

            if (player.GetPosition().Y < gorny && keyState.IsKeyDown(Keys.Up) && bounds.Y < 0)
            {
                //   canMoveRight = false;
                // location.getColideObjects(0, currentMap, bounds);
                scrolly = 16;
                player.SetPosition(player.GetPosition().X, player.GetPosition().Y + 32);
            }
            location.getColideObjects(0, currentMap, bounds, colisionTiles);

            MapManager.bounds.X += scrollx;
            MapManager.bounds.Y += scrolly;
        }

        public Matrix Transform { get; set; }

        public void Follow(Player player)
        {
            var position = Matrix.CreateTranslation(
                -player.GetPosition().X - (player.texture.Width / 2),
                -player.GetPosition().Y - (player.texture.Height / 2),
                0
                );

            var offset = Matrix.CreateTranslation(
                256,
                128,
                0
                );

            Transform = position * offset;
        }

    }
}
