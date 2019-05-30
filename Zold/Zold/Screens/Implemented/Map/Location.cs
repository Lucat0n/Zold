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


using System.IO;
using System.IO.Compression;


namespace Zold.Screens.Implemented.Map
{

    abstract class Location
    {
        // public List<Rectangle> colisionTiles = new List<Rectangle>();
        // // // TILES
        //TmxMap map;

        private const uint FlippedHorizontallyFlag = 0x80000000;
        private const uint FlippedVerticallyFlag = 0x40000000;
        private const uint FlippedDiagonallyFlag = 0x20000000;

        internal const byte HorizontalFlipDrawFlag = 1;
        internal const byte VerticalFlipDrawFlag = 2;
        internal const byte DiagonallyFlipDrawFlag = 4;
        public byte[] FlipAndRotate;


        public static List<TmxMap> LisatofLocations = new List<TmxMap>();
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
        Effect effect;


        Player player;

        public Location(GameScreenManager gameScreenManager, SpriteBatchSpriteSheet spriteSheet, Player player)
        {
            this.gameScreenManager = gameScreenManager;
            this.spriteSheet = spriteSheet;
            this.player = player;
            currentMap = getCurrentMap();
        }

        public abstract TmxMap getCurrentMap();
        public abstract List<int> getLayerNumbers();
        public abstract List<int> getColideLayers();
        public abstract Vector2 getPortal();
        public abstract Vector2 playersNewPosition();
        public abstract List<Npc> GetCharacters();
        public abstract Npc GetCharacter(int i);
        public abstract List<Enemy> GetEnemies();
        public abstract Enemy Getenemy(int i);

        public virtual void initMap(GameScreenManager gameScreenManager, TmxMap currentMap)
        {

            tileset = gameScreenManager.Content.Load<Texture2D>(currentMap.Tilesets[0].Name.ToString());
            tileWidth = currentMap.Tilesets[0].TileWidth;
            tileHeight = currentMap.Tilesets[0].TileHeight;
            tilesetTilesWide = tileset.Width / tileWidth;
            tilesetTilesHigh = tileset.Height / tileHeight;
            // getColideObjects(currentMap, 0);
        }

        //public void checkMapChuje(int layer, TmxMap map)
        //{
        //    Stream stream = new MemoryStream(buffer, false);


        //    using (stream)
        //    using (var br = new BinaryReader(stream))

        //        for (var i = 0; i < map.Layers[layer].Tiles.Count; i++)
        //        {
        //            uint tileData = br.ReadUInt32();

        //            // The data contain flip information as well as the tileset index
        //            byte flipAndRotateFlags = 0;
        //            if ((tileData & FlippedHorizontallyFlag) != 0)
        //            {
        //                flipAndRotateFlags |= HorizontalFlipDrawFlag;
        //            }
        //            if ((tileData & FlippedVerticallyFlag) != 0)
        //            {
        //                flipAndRotateFlags |= VerticalFlipDrawFlag;
        //            }
        //            if ((tileData & FlippedDiagonallyFlag) != 0)
        //            {
        //                flipAndRotateFlags |= DiagonallyFlipDrawFlag;
        //            }
        //            FlipAndRotate[i] = flipAndRotateFlags;


        //        }

        //}

        //TODO: wywalić tego matrixa z parametrów
        public virtual void drawTiles(int layer, TmxMap map, Matrix cameraTransformation)
        {
            long tileFrame;
            Effect effect = Assets.Instance.Get("placeholders/shaders/testShader");

            for (var i = 0; i < map.Layers[layer].Tiles.Count; i++)
            {
                long gid = map.Layers[layer].Tiles[i].Gid;
                bool flipH = map.Layers[layer].Tiles[i].HorizontalFlip;
                bool flipV = map.Layers[layer].Tiles[i].VerticalFlip;
                bool flipD = map.Layers[layer].Tiles[i].DiagonalFlip;
                string xx = map.Layers[layer].Name;

                //bool flipD = map.Layers[layer].Tiles[i].;

                // Empty tile, do nothing
                if (gid == 0) { }

                else
                {
                    //FlipAndRotate[i] = flipAndRotateFlags;
                    //byte flipAndRotate = FlipAndRotate[i];
                    //SpriteEffects flipEffect = SpriteEffects.None;
                    //float rotation = 0f;
                    //Console.WriteLine("----------------------- ");
                    if (layer == 4 && (flipV || flipH || flipD))
                    {
                        

                        
                        Console.WriteLine("----------------------- ");



                        if (flipH)
                        {

                        }
                    }
                    if (layer == 4 && (!flipD && !flipV && !flipH))
                    {
                        
                        tileFrame = gid - 1;
                    }
                    else
                    {
                        tileFrame = gid - 1;
                    }

                    int column = (int)tileFrame % tilesetTilesWide;
                    int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                    float x = (i % map.Width) * map.TileWidth;
                    float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight;





                    //if ((flipAndRotate & HorizontalFlipDrawFlag) != 0)
                    //{
                    //    flipEffect |= SpriteEffects.FlipHorizontally;
                    //}
                    //if ((flipAndRotate & VerticalFlipDrawFlag) != 0)
                    //{
                    //    flipEffect |= SpriteEffects.FlipVertically;
                    //}
                    //if ((flipAndRotate & DiagonallyFlipDrawFlag) != 0)
                    //{
                    //    if ((flipAndRotate & HorizontalFlipDrawFlag) != 0 &&
                    //         (flipAndRotate & VerticalFlipDrawFlag) != 0)
                    //    {
                    //        rotation = (float)(Math.PI / 2);
                    //        flipEffect ^= SpriteEffects.FlipVertically;
                    //    }
                    //    else if ((flipAndRotate & HorizontalFlipDrawFlag) != 0)
                    //    {
                    //        rotation = (float)-(Math.PI / 2);
                    //        flipEffect ^= SpriteEffects.FlipVertically;
                    //    }
                    //    else if ((flipAndRotate & VerticalFlipDrawFlag) != 0)
                    //    {
                    //        rotation = (float)(Math.PI / 2);
                    //        flipEffect ^= SpriteEffects.FlipHorizontally;
                    //    }
                    //    else
                    //    {
                    //        rotation = -(float)(Math.PI / 2);
                    //        flipEffect ^= SpriteEffects.FlipHorizontally;
                    //    }
                    //}





                    Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);
                    spriteSheet.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, transformMatrix: cameraTransformation);


                    if (xx == "Sciany" || xx =="Podloga" || xx == "Meble")
                    {

                        if (layer == 5 && flipV && flipH && flipD)
                        {
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White * 0);
                            //spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(180 * Math.PI / 180), new Vector2(32, 32), SpriteEffects.None, 1);
                        }
                        else if (layer == 5 && flipV && !flipH && !flipD)
                        {
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White * 0);
                            //  spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(180 * Math.PI / 180), new Vector2(32, 32), SpriteEffects.None, 1);
                        }
                        else if (layer == 5 && flipV && !flipH && flipD)
                        {
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(270 * Math.PI / 180), new Vector2(32, 0), SpriteEffects.None, 1);
                            //    spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(180 * Math.PI / 180), new Vector2(32, 32), SpriteEffects.None, 1);
                        }
                        else if (layer == 5 && flipV && flipH && !flipD)
                        {
                            // spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(180 * Math.PI / 180), new Vector2(32, 32), SpriteEffects.None, 1);
                        }
                        else if (layer == 5 && !flipV && flipH && !flipD)
                        {
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(270 * Math.PI / 180), new Vector2(32, 0), SpriteEffects.None, 1);
                            //  spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(180 * Math.PI / 180), new Vector2(32, 32), SpriteEffects.None, 1);
                        }
                        else if (layer == 5 && !flipV && !flipH && flipD)
                        {
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                            //spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(180 * Math.PI / 180), new Vector2(32, 32), SpriteEffects.None, 1);
                        }
                        else if (layer == 5 && !flipV && flipH && flipD)
                        {
                            //spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(90 * Math.PI / 180), new Vector2(0, 32), SpriteEffects.None, 1);
                        }
                        else if (layer == 3)
                        {
                            // spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(270 * Math.PI / 180), new Vector2(32, 0), SpriteEffects.None, 1);
                        }

                        else
                        {
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                        }
                    }

                    if (xx == "sciany2" || xx == "podloga2" || xx == "meble2")
                    {

                        //spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);

                        if (layer == 4 && flipV && flipH && flipD)
                        {
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White * 0);
                            //spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(180 * Math.PI / 180), new Vector2(32, 32), SpriteEffects.None, 1);
                        }
                        else if (layer == 4 && flipV && !flipH && !flipD)
                        {
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White * 0);
                            //  spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(180 * Math.PI / 180), new Vector2(32, 32), SpriteEffects.None, 1);
                        }
                        else if (layer == 4 && flipV && !flipH && flipD)
                        {
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(270 * Math.PI / 180), new Vector2(32, 0), SpriteEffects.None, 1);
                            //    spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(180 * Math.PI / 180), new Vector2(32, 32), SpriteEffects.None, 1);
                        }
                        else if (layer == 4 && flipV && flipH && !flipD)
                        {
                            // spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White*0, (float)(180 * Math.PI / 180), new Vector2(32, 32), SpriteEffects.None, 1);
                        }
                        else if (layer == 4 && !flipV && flipH && !flipD)
                        {
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White );
                            //  spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(180 * Math.PI / 180), new Vector2(32, 32), SpriteEffects.None, 1);
                        }
                        else if (layer == 4 && !flipV && !flipH && flipD)
                        {
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                            //spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(180 * Math.PI / 180), new Vector2(32, 32), SpriteEffects.None, 1);
                        }
                        else if (layer == 4 && !flipV && flipH && flipD)
                        {
                            //spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(90 * Math.PI / 180), new Vector2(0, 32), SpriteEffects.None, 1);
                        }
                        else if (layer == 3)
                        {
                            Console.WriteLine("Horizontal flip: " + flipH);
                        Console.WriteLine("verticaal flip: " + flipV);
                        Console.WriteLine("diagonal flip: " + flipD);
                        Console.WriteLine("gid: " + xx);
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                        }

                        else
                        {
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                        }
                    }

                    // spriteSheet.Draw(tileset, new Vector2(0,0), new Rectangle((int)x , (int)y , tileWidth, tileHeight), Color.White, (float)Math.PI, new Vector2(0,0), 1, SpriteEffects.None, 0);

                    spriteSheet.End();
                }
            }
        }

        public virtual void getColideObjects(int warstwa, TmxMap map, List<Rectangle> colisionTiles)
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

                    colisionTiles.Add(new Rectangle((int)x, (int)y, tileWidth, tileHeight));
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

                if (ghost.X == tile.X - 32 && ghost.Y + 32 == tile.Y)
                {
                    MapManager.canMoveRight = false;
                }

                else if (ghost.X == tile.X + 32 && ghost.Y + 32 == tile.Y)
                {
                    MapManager.canMoveLeft = false;
                }

                else if (ghost.Y == tile.Y && tile.X == ghost.X)
                {
                    MapManager.canMoveUp = false;

                }

                else if (ghost.Y + 32 * 2 == tile.Y && tile.X == ghost.X)
                {
                    MapManager.canMoveDown = false;
                }

            }

        }
    }
}
