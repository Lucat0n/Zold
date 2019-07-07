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

        Player player;

        Effect opacity;
        float currentOp;

        List<Rectangle> opacityTiles;

        public static bool opac;

        public bool postproc;

        RenderTarget2D opacTarget, mainTarget2;

        Effect op2;


        public Location(GameScreenManager gameScreenManager, SpriteBatchSpriteSheet spriteSheet, Player player, bool postproc)
        {
            this.gameScreenManager = gameScreenManager;
            this.spriteSheet = spriteSheet;
            this.player = player;
            this.postproc = postproc;
            currentMap = getCurrentMap();

            //ListofPlaces.Add(new Locations.TheRoom(gameScreenManager, spriteSheet, player,false));
            //  ListofPlaces.Add(new Locations.Dormitory(gameScreenManager, spriteSheet, player,true));

            opacity = Assets.Instance.Get("placeholders/shaders/opacityShader");
            op2 = Assets.Instance.Get("placeholders/shaders/otherOpacity");
            opacityTiles = new List<Rectangle>();
            currentOp = 0.55f;
        }

        public abstract string getLocQuest();
        public abstract TmxMap getCurrentMap();
        public abstract List<int> getLayerNumbers();
        public abstract List<int> getColideLayers();
        public abstract List<Vector2> getPortals();
        public abstract List<Vector2> playersNewPositions();
        public abstract List<Npc> GetCharacters();
        public abstract Npc GetCharacter(int i);
        public abstract List<Enemy> GetEnemies();
        public abstract Enemy Getenemy(int i);
        public abstract List<Location> ListofNextPlaces();
        public abstract List<int> offsets();
        public abstract string getName();

        public virtual void initMap(GameScreenManager gameScreenManager, TmxMap currentMap)
        {

            tileset = gameScreenManager.Content.Load<Texture2D>(currentMap.Tilesets[0].Name.ToString());
            tileWidth = currentMap.Tilesets[0].TileWidth;
            tileHeight = currentMap.Tilesets[0].TileHeight;
            tilesetTilesWide = tileset.Width / tileWidth;
            tilesetTilesHigh = tileset.Height / tileHeight;

            var pp = gameScreenManager.GraphicsDevice.PresentationParameters;
            opacTarget = new RenderTarget2D(gameScreenManager.GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);
            mainTarget2 = new RenderTarget2D(gameScreenManager.GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);

            // getColideObjects(currentMap, 0);
        }

        public List<Location> ListofPlaces()
        {
            List<Location> ListofLocs = new List<Location>();
            ListofLocs.Add(new Locations.TheRoom(gameScreenManager, spriteSheet, player, false));
            ListofLocs.Add(new Locations.Dormitory(gameScreenManager, spriteSheet, player, true));
            ListofLocs.Add(new Locations.DormitoryFirstFloor(gameScreenManager, spriteSheet, player, false));
            return ListofLocs;
        }


        //TODO: wywalić tego matrixa z parametrów
        public virtual void drawTiles(int layer, TmxMap map, Matrix cameraTransformation)
        {
            long tileFrame;

            Rectangle rect;
            Rectangle ghost = new Rectangle((int)player.GetPosition().X, (int)player.GetPosition().Y, 32, 48);

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




                    //Texture2D lightMask = Assets.Instance.Get("placeholders/Textures/lightmask3");
                    //gameScreenManager.GraphicsDevice.SetRenderTarget(opacTarget);
                    //gameScreenManager.GraphicsDevice.Clear(Color.TransparentBlack);
                    //spriteSheet.Begin(SpriteSortMode.Immediate, BlendState.Additive, transformMatrix: cameraTransformation);
                    //spriteSheet.Draw(lightMask, new Rectangle((int)player.GetPosition().X - 100, (int)player.GetPosition().Y - 100, lightMask.Width - 15, lightMask.Height - 15), Color.White);
                    //spriteSheet.End();

                    //gameScreenManager.GraphicsDevice.SetRenderTarget(mainTarget2);


                    Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);
                  //  gameScreenManager.GraphicsDevice.SetRenderTarget(null);
                    spriteSheet.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, transformMatrix: cameraTransformation);

                    if (xx == "Sciany" || xx =="Podloga" || xx == "Meble")
                    {

                        if (layer == 6 && flipV && flipH && flipD)
                        {
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White * 0);
                            //spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(180 * Math.PI / 180), new Vector2(32, 32), SpriteEffects.None, 1);
                        }
                        else if (layer == 6 && flipV && !flipH && !flipD)
                        {
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White * 0);
                            //  spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(180 * Math.PI / 180), new Vector2(32, 32), SpriteEffects.None, 1);
                        }
                        else if (layer == 6 && flipV && !flipH && flipD)
                        {
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(270 * Math.PI / 180), new Vector2(32, 0), SpriteEffects.None, 1);
                            //    spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(180 * Math.PI / 180), new Vector2(32, 32), SpriteEffects.None, 1);
                        }
                        else if (layer == 6 && flipV && flipH && !flipD)
                        {
                            // spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(180 * Math.PI / 180), new Vector2(32, 32), SpriteEffects.None, 1);
                        }
                        else if (layer == 6 && !flipV && flipH && !flipD)
                        {
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(270 * Math.PI / 180), new Vector2(32, 0), SpriteEffects.None, 1);
                            //  spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(180 * Math.PI / 180), new Vector2(32, 32), SpriteEffects.None, 1);
                        }
                        else if (layer == 6 && !flipV && !flipH && flipD)
                        {
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                            //spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(180 * Math.PI / 180), new Vector2(32, 32), SpriteEffects.None, 1);
                        }
                        else if (layer == 6 && !flipV && flipH && flipD)
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
                            rect = new Rectangle((int)x, (int)y, tileWidth, tileHeight);

                            if (ghost.Intersects(rect) && layer == 6)
                            {
                                opacity.Parameters["param1"].SetValue(0.7f);
                                opacity.CurrentTechnique.Passes[0].Apply();
                                spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);

                                //spriteSheet.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, transformMatrix: cameraTransformation);

                            }
                            else
                            {
                                opac = false;
                                opacity.Parameters["param1"].SetValue(1f);
                                opacity.CurrentTechnique.Passes[0].Apply();
                                spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                            }
                        }
                    }

                    if (xx == "sciany2" || xx == "podloga2" || xx == "meble2" )
                    {

                        //spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);

                        if (layer == 4 && flipV && flipH && flipD)
                        {
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                            //spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(180 * Math.PI / 180), new Vector2(32, 32), SpriteEffects.None, 1);
                        }
                        else if (layer == 4 && flipV && !flipH && !flipD)
                        {
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
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
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(180 * Math.PI / 180), new Vector2(32, 32), SpriteEffects.None, 1);
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

                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                        }

                        else
                        {
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                        }
                    }


                    if (xx == "sciany3" || xx == "podloga3" || xx == "meble3")
                    {

                        //spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);

                        if (layer == 4 && flipV && flipH && flipD)
                        {
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                            //spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(270 * Math.PI / 180), new Vector2(32, 0), SpriteEffects.None, 1);
                        }
                        else if (layer == 4 && flipV && !flipH && !flipD)
                        {
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
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
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(180 * Math.PI / 180), new Vector2(32, 32), SpriteEffects.None, 1);
                        }
                        else if (layer == 4 && !flipV && flipH && !flipD)
                        {
                           // spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                              spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White, (float)(270 * Math.PI / 180), new Vector2(32, 0), SpriteEffects.None, 1);
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

                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                        }

                        else
                        {
                            spriteSheet.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                        }
                    }



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

        public virtual void checkIfColideWithNpc(Npc npc)
        {
            Rectangle ghost = new Rectangle((int)player.GetPosition().X, (int)player.GetPosition().Y, 32, 48);
            Rectangle ghostNpc = new Rectangle((int)npc.GetPosition().X, (int)npc.GetPosition().Y, 32, 64);

            if (ghost.X == ghostNpc.X - 32 && ghost.Y + 32 == ghostNpc.Y)
            {
                MapManager.canMoveRight = false;
            }

            else if (ghost.X == ghostNpc.X + 32 && ghost.Y + 32 == ghostNpc.Y)
            {
                MapManager.canMoveLeft = false;
            }

            else if (ghost.Y == ghostNpc.Y && ghostNpc.X == ghost.X)
            {
                MapManager.canMoveUp = false;

            }

            else if (ghost.Y + 32 * 2 == ghostNpc.Y && ghostNpc.X == ghost.X)
            {
                MapManager.canMoveDown = false;
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
