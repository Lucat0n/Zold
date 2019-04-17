using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TiledSharp;


namespace Map
{
    class Mapa
    {
        TmxMap map;
        TmxMap map2;

        Texture2D tileset;

        int tileWidth;
        int tileHeight;
        int tilesetTilesWide;
        int tilesetTilesHigh;

        public Mapa(TmxMap currentMap, Texture2D tileset)
        {
            this.map = currentMap;
            this.tileset = tileset;
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

        //void displayDialog()
        //{
        //    ///need to set 250 i 310 as parameters
        //    if (playerOne.GetPosition().X >= 250 && playerOne.GetPosition().X < 310 && forest)
        //    {
        //        Rectangle tlo = new Rectangle(100, 420, 500, 50);
        //        spriteBatch.Draw(dotekstu, tlo, Color.White);

        //        spriteBatch.DrawString(font, "Witaj zielona magnetyczna gwiazdo!", new Vector2(145, 425), Color.White);
        //    }
        //}
    }
}
