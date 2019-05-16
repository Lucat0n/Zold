using TiledSharp;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zold.Utilities;
using Microsoft.Xna.Framework;



namespace Zold.Screens.Implemented.Map
{

    abstract class Location
    {
       // public List<Rectangle> colisionTiles = new List<Rectangle>();
        // // // TILES
        //TmxMap map;
        TmxMap map2;
        TmxMap currentMap;

        Texture2D tileset;
        int tileWidth;
        int tileHeight;
        int tilesetTilesWide;
        int tilesetTilesHigh;
        GameScreenManager gameScreenManager;

        int screenWdth = 800;
        int screenHeight = 480;

        SpriteBatchSpriteSheet spriteSheet;


        Player player;

        public Location(GameScreenManager gameScreenManager, SpriteBatchSpriteSheet spriteSheet,  Player player)
        {
            this.gameScreenManager = gameScreenManager;
            this.spriteSheet = spriteSheet;
            this.player = player;
            //map = new TmxMap(@"Content/MainRoom.tmx");
           // map2 = new TmxMap(@"Content/mapa2v2.tmx");
            currentMap = getCurrentMap();

            //initMap();
        }

        public abstract TmxMap getCurrentMap();
        public abstract List<int> getLayerNumbers();
        public abstract List<int> getColideLayers();
        public abstract Vector2 getPortal();

        public virtual void initMap(GameScreenManager gameScreenManager, TmxMap currentMap)
        {

            tileset = gameScreenManager.Content.Load<Texture2D>(currentMap.Tilesets[0].Name.ToString());
            tileWidth = currentMap.Tilesets[0].TileWidth;
            tileHeight = currentMap.Tilesets[0].TileHeight;
            tilesetTilesWide = tileset.Width / tileWidth;
            tilesetTilesHigh = tileset.Height / tileHeight;
           // getColideObjects(currentMap, 0);
        }

        public virtual void drawTiles(int layer, TmxMap map, Rectangle bounds)
        {
            for (var i = 0; i < map.Layers[layer].Tiles.Count; i++)
            {
                int gid = map.Layers[layer].Tiles[i].Gid;

                // Empty tile, do nothing
                if (gid == 0) { }

                else
                {
                    int tileFrame = gid - 1;
                    int column = tileFrame % tilesetTilesWide;
                    int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                    float x = (i % map.Width) * map.TileWidth;
                    float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight;

                    Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);
                    spriteSheet.Begin();
                    spriteSheet.Draw(tileset, new Rectangle((int)x + bounds.X, (int)y + bounds.Y, tileWidth, tileHeight), tilesetRec, Color.White);
                    spriteSheet.End();
                }
            }
        }

        public virtual void getColideObjects(int warstwa, TmxMap map, Rectangle bounds, List<Rectangle> colisionTiles)
        {
            colisionTiles.Clear();
            for (var i = 0; i < map.Layers[warstwa].Tiles.Count; i++)
            {
                int gid = map.Layers[warstwa].Tiles[i].Gid;

                if (gid == 0) { }

                else
                {
                    int tileFrame = gid - 1;
                    int column = tileFrame % tilesetTilesWide;
                    int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                    float x = (i % map.Width) * map.TileWidth;
                    float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight;

                    colisionTiles.Add(new Rectangle((int)x + bounds.X, (int)y + bounds.Y, tileWidth, tileHeight));
                }
            }
        }

        public virtual void dontGoOutsideMap()
        {
            int sh;
            int sw;
            if (gameScreenManager.IsFullScreenOn)
            {
                sh = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                sw = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            }
            else
            {
                sh = screenHeight;
                sw = screenWdth;
            }

            if (player.GetPosition().X <= 0)
            {
                MapManager.canMoveLeft = false;
            }

            if (player.GetPosition().Y <= 0)
            {
                MapManager.canMoveUp = false;
            }

            if (player.GetPosition().Y + player.texture.Height > sh)
            {
                MapManager.canMoveDown = false;
            }
            if (player.GetPosition().X + player.texture.Width >= sw)
            {
                MapManager.canMoveRight = false;
            }
        }

        public virtual void checkIfColide(List<Rectangle> colisionTiles)
        {
            MapManager.canMoveRight = true;
            MapManager.canMoveLeft = true;
            MapManager.canMoveDown = true;
            MapManager.canMoveUp = true;

            foreach (Rectangle tile in colisionTiles)
            {
                //  Console.WriteLine("tile: " + tile);
                Rectangle ghost = new Rectangle((int)player.GetPosition().X, (int)player.GetPosition().Y, 32, 48);

                if (ghost.X == tile.X - 32 && ghost.Y == tile.Y)
                {
                    MapManager.canMoveRight = false;
                }

                else if (ghost.X == tile.X + 32 && ghost.Y == tile.Y)
                {
                    MapManager.canMoveLeft = false;
                }

                else if (ghost.Y - 32 == tile.Y && tile.X == ghost.X)
                {
                    MapManager.canMoveUp = false;
                }

                else if (ghost.Y + 32 == tile.Y && tile.X == ghost.X)
                {
                    MapManager.canMoveDown = false;
                }

            }

        }
        }
}
