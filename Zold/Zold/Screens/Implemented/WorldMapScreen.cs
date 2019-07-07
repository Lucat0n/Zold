using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TiledSharp;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Map
{
    class WorldMapScreen : GameScreen
    {
        List<Enemy> enemies;

        GameScreenManager gameScreenManager;
        SpriteBatchSpriteSheet spriteSheet;
        Texture2D tileset;
        int layer = 0;
        int tileWidth;
        int tileHeight;
        int tilesetTilesWide;
        int tilesetTilesHigh;
        Player player;

        Zold.Screens.Camera cameraPlayer;

        TmxMap currentMap;
        public WorldMapScreen(GameScreenManager gameScreenManager, SpriteBatchSpriteSheet spriteSheet, Player player)
        {
            currentMap = new TmxMap(@"Content/graphic/locations/World/MiastoMapa.tmx");
            this.player = player;
            this.gameScreenManager = gameScreenManager;
            this.spriteSheet = spriteSheet;
            this.IsTransparent = false;
        }

        public override void Draw(GameTime gameTime)
        {
            long tileFrame;

            for (var i = 0; i < currentMap.Layers[layer].Tiles.Count; i++)
            {
                long gid = currentMap.Layers[layer].Tiles[i].Gid;
                bool flipH = currentMap.Layers[layer].Tiles[i].HorizontalFlip;
                bool flipV = currentMap.Layers[layer].Tiles[i].VerticalFlip;
                bool flipD = currentMap.Layers[layer].Tiles[i].DiagonalFlip;

                // Empty tile, do nothing
                if (gid == 0) { }

                else
                {
                    tileFrame = gid - 1;

                    int column = (int)tileFrame % tilesetTilesWide;
                    int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                    float x = (i % currentMap.Width) * currentMap.TileWidth;
                    float y = (float)Math.Floor(i / (double)currentMap.Width) * currentMap.TileHeight;

                    Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);
                    spriteSheet.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, transformMatrix: cameraPlayer.Transform());
                    spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White * 0);
                    spriteSheet.End();
                }
            }
        }

        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                gameScreenManager.RemoveScreen(this);
        }

        public override void LoadContent()
        {
            tileset = gameScreenManager.Content.Load<Texture2D>(currentMap.Tilesets[0].Name.ToString());
            tileWidth = currentMap.Tilesets[0].TileWidth;
            tileHeight = currentMap.Tilesets[0].TileHeight;
            tilesetTilesWide = tileset.Width / tileWidth;
            tilesetTilesHigh = tileset.Height / tileHeight;
            cameraPlayer = new Screens.Camera(player.GetPosition());
        }

        public override void UnloadContent()
        {
            //throw new NotImplementedException();
        }
    }
}
