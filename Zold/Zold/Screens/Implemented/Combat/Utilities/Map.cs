using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TiledSharp;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.Utilities
{
    class Map
    {
        GameScreenManager gameScreenManager;

        private TmxMap currentMap;
        private SpriteBatchSpriteSheet mapSprite;
        private Texture2D tileset;
        private int tileWidth;
        private int tileHeight;
        private int tilesetTilesWide;
        private int tilesetTilesHigh;

        public int TopMapEdge;
        public int BottomMapEdge;
        public int RightMapEdge;
        public int LeflMapEdge;

        public Map(GameScreenManager gameScreenManager)
        {
            this.gameScreenManager = gameScreenManager;

            currentMap = new TmxMap("Content/graphic/combat/combat_city.tmx");
            mapSprite = new SpriteBatchSpriteSheet(gameScreenManager.GraphicsDevice, null, 0, 0, 0, 0);

            InitMap(currentMap);
        }

        public virtual void InitMap(TmxMap currentMap)
        {
            tileset = gameScreenManager.Content.Load<Texture2D>("graphic\\combat\\" + currentMap.Tilesets[0].Name.ToString());
            tileWidth = currentMap.Tilesets[0].TileWidth;
            tileHeight = currentMap.Tilesets[0].TileHeight;
            tilesetTilesWide = tileset.Width / tileWidth;
            tilesetTilesHigh = tileset.Height / tileHeight;

            TopMapEdge = int.Parse(currentMap.Layers[1].Properties["Height"]) * tileHeight;
            BottomMapEdge = tileset.Height;
            RightMapEdge = tileset.Width;
            LeflMapEdge = 0;
        }

        public virtual void DrawTiles(int layer)
        {
            for (var i = 0; i < currentMap.Layers[layer].Tiles.Count; i++)
            {
                int gid = currentMap.Layers[layer].Tiles[i].Gid;

                if (gid != 0)
                {
                    int tileFrame = gid - 1;
                    int column = tileFrame % tilesetTilesWide;
                    int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);
                    float x = (i % currentMap.Width) * currentMap.TileWidth;
                    float y = (float)Math.Floor(i / (double)currentMap.Width) * currentMap.TileHeight;

                    Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);
                    mapSprite.Begin();
                    mapSprite.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                    mapSprite.End();
                }
            }
        }
    }
}
