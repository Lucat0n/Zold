using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;
using System;

namespace Map
{
    class Map
    {
        
        TmxMap map;
        TmxMap map2;
        TmxMap currentMap;

        Texture2D tileset;

        int tileWidth;
        int tileHeight;
        int tilesetTilesWide;
        int tilesetTilesHigh;

        public Map(Texture2D tileset, TmxMap mapa)
        {
            //loading tiles
            map =mapa;
            this.tileset = tileset;

            tileWidth = map.Tilesets[0].TileWidth;
            tileHeight = map.Tilesets[0].TileHeight;
            tilesetTilesWide = tileset.Width / tileWidth;
            tilesetTilesHigh = tileset.Height / tileHeight;

            //map2 = new TmxMap(@"Content/placeholders/mapa3.tmx");
        }

        public void drawTiles(int layer, TmxMap map, SpriteBatch spriteBatch)
        {
            for (var i = 0; i < map.Layers[layer].Tiles.Count; i++)
            {
                int gid = map.Layers[layer].Tiles[i].Gid;

                // Empty tile, do nothing
                if (gid == 0)
                {

                }
                else
                {
                    int tileFrame = gid - 1;
                    int column = tileFrame % tilesetTilesWide;
                    int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                    float x = (i % map.Width) * map.TileWidth;
                    float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight;

                    Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);
                    //if (layer==1 && tilesetRec.Intersects(playerOne.GetTextureRecta())){
                    //    playerOne.SetPosition(new Vector2(32,32));
                    //}
                    spriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                }
            }
        }

    }
}
