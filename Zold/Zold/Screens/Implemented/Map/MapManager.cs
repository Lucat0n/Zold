﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;
using System;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Map
{
    class MapManager : GameScreen
    {

        public List<string> powiedzonka = new List<string>();
        public List<Rectangle> colisionTiles = new List<Rectangle>();

        //colors 
        Color kolorPow = Color.White * 0;
        Color kolorPow2 = Color.White * 0;
        Color wht = Color.White;
        Color whtC = Color.White;
        Color bacgrund = Color.Black;
        Color bacgrundAfterHit = Color.Red;

        // // // TILES
        TmxMap map;
        TmxMap map2;
        TmxMap currentMap;

        Texture2D tileset;

        int tileWidth;
        int tileHeight;
        int tilesetTilesWide;
        int tilesetTilesHigh;

        // here is dope music
        Song menuMusic;
        Song gameplayMusic;
        Song currentSong;
        SoundEffect bgMusic;
        SoundEffect combatMusic;

        //spritefonts
        SpriteFont dialog;

        // //postacie
        Vector2 pos;

        Texture2D adven;
        int advenPosX = 256;
        int advenPosY = 64;

        //ramka na tekst
        Texture2D dotekstu;
        Texture2D dymek;
        //tla (nie uzywane)
        Texture2D location3punk;
        Texture2D poww;

        //Combat
        Zold.Screens.Implemented.Combat.CombatScreen Combat;
        Zold.Screens.Implemented.Combat.Player combatPlayer;
        Zold.Screens.Implemented.Combat.Enemy skeleton;
        Zold.Screens.Implemented.Combat.Enemy rat;
        List<Zold.Screens.Implemented.Combat.Enemy> enemies;
        Texture2D skeletonTex;
        Texture2D ratTex;
        Texture2D line;

        //budynki
        Texture2D policja;
        int policjaPosX = 400;
        int policjaPosY = 300;
        static int policjaWidth = 80;
        static int policjaHeight = 120;

        Texture2D budynek1;
        int ralfX = 440;
        int ralfY = 40;
        static int ralfWidth = policjaWidth + 50;
        static int ralfHeight = policjaHeight + 80;

        Player player;
        Enemy enemy;

        //bools
        bool songStart = false;
        bool displayed = false;
        bool location1;
        bool location2;
        bool location3;
        bool isPaused = false;
        bool isEscPressed = false;
        bool disp = false; // is message displayed?
        bool drawed = false;
        bool canMoveLeft;
        bool canMoveUp;
        bool canMoveRight;
        bool canMoveDown;

        int screenWdth = 800;
        int screenHeight = 480;

        Rectangle bounds; //camera bounds 

        public MapManager()
        {

            //map2 = new TmxMap(@"Content/placeholders/mapa3.tmx");
        }

        public override void LoadContent()
        {
            canMoveDown = true;
            canMoveLeft = true;
            canMoveRight = true;
            canMoveUp = true;

            powiedzonka.Add("Witaj zielona magnetyczna gwiazdo");
            powiedzonka.Add("A ty tu czego?");
            powiedzonka.Add("Nie widzisz, ze jestem zajety");
            powiedzonka.Add("Elo");
            powiedzonka.Add("Tez kiedys bylem jak ty, ale sie jeblem i przestalem");
            powiedzonka.Add("Ruchasz sie?");
            //poww = gameScreenManager.Content.Load<Texture2D>("placeholders/location2backgrund");
            // location3punk = gameScreenManager.Content.Load<Texture2D>("placeholders/location32");

            // loading music
            bgMusic = Assets.Instance.Get("placeholders/Music/menu-music");
            combatMusic = Assets.Instance.Get("placeholders/Music/kombat");

            currentSong = gameplayMusic;

            //loading fonts
            dialog = Assets.Instance.Get("placeholders/Fonts/dialog");

            map = new TmxMap(@"Content/mapa2v2.tmx");
            map2 = new TmxMap(@"Content/mapa2v2.tmx");
            currentMap = map;

            tileset = gameScreenManager.Content.Load<Texture2D>(map.Tilesets[0].Name.ToString());
            tileWidth = map.Tilesets[0].TileWidth;
            tileHeight = map.Tilesets[0].TileHeight;
            tilesetTilesWide = tileset.Width / tileWidth;
            tilesetTilesHigh = tileset.Height / tileHeight;

            currentSong = gameplayMusic;
            //MediaPlayer.Play(currentSong);

            location1 = true;
            location2 = false;
            location3 = false;

            pos = new Vector2(10, 10);
            player = new Map.Player(pos, Assets.Instance.Get("placeholders/Textures/main"), 2.7f);

            enemy = new Map.Enemy(player, new Vector2(400, 300));
            enemy.SetTexture(Assets.Instance.Get("placeholders/Textures/rat"));

            // Combat

            enemies = new List<Zold.Screens.Implemented.Combat.Enemy>();
            combatPlayer = new Zold.Screens.Implemented.Combat.Player(new Vector2(0, 200), 100, enemies);
            combatPlayer.SetTexture(Assets.Instance.Get("placeholders/Textures/main"));

            skeleton = new Zold.Screens.Implemented.Combat.Mob(combatPlayer, new Vector2(300, 300));
            rat = new Zold.Screens.Implemented.Combat.Charger(combatPlayer, new Vector2(300, 400));
            skeleton.SetTexture(Assets.Instance.Get("placeholders/Textures/skeleton"));
            rat.SetTexture(Assets.Instance.Get("placeholders/Textures/rat"));
            enemies.Add(skeleton);
            enemies.Add(rat);
            Combat = new Combat.CombatScreen(combatPlayer, enemies);

            //camera
            bounds = new Rectangle(0, 0, 0, 0);
            getColideObjects(map);
            getColideObjects(map2);

            Console.WriteLine("szerokosc mapy 1: " + map.Width);
            Console.WriteLine("wysookosc mapy 1: " + map.Height);


        }

        public override void UnloadContent()
        {
            throw new NotImplementedException();
        }


        public override void Draw(GameTime gameTime)
        {
            gameScreenManager.GraphicsDevice.Clear(Color.Black);
            gameScreenManager.SpriteBatch.Begin();

           

            drawTiles(1, currentMap);
            drawTiles(0, currentMap);

            gameScreenManager.SpriteBatch.DrawString(dialog, "X: "+player.GetPosition().X.ToString(), new Vector2(10, 10), Color.White);
            gameScreenManager.SpriteBatch.DrawString(dialog, "Y: "+player.GetPosition().Y.ToString(), new Vector2(10, 40), Color.White);
            gameScreenManager.SpriteBatch.DrawString(dialog, "boundsX: "+bounds.X.ToString(), new Vector2(10, 70), Color.White);
            gameScreenManager.SpriteBatch.DrawString(dialog, "boundsY: "+bounds.Y.ToString(), new Vector2(10, 110), Color.White);

            //gameScreenManager.SpriteBatch.Draw(poww, new Rectangle(0, 0, 802, 580), kolorPow);
            // gameScreenManager.SpriteBatch.Draw(location3punk, new Rectangle(0, 0, 802, 580), kolorPow2);
            gameScreenManager.SpriteBatch.Draw(player.texture, player.GetPosition(), Color.White);

            ///budunek policji
            gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("placeholders/Textures/police"), new Rectangle(policjaPosX + bounds.X, policjaPosY + bounds.Y, policjaWidth + 50, policjaHeight + 20), wht);
            //wiezowiec 2
            gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("placeholders/Textures/ralf"), new Rectangle(ralfX + bounds.X, ralfY + bounds.Y, ralfWidth, ralfHeight), kolorPow);

            //postacie
            if (location1)
            {
                adven = Assets.Instance.Get("placeholders/Textures/Adven");
                gameScreenManager.SpriteBatch.Draw(adven, new Rectangle(advenPosX + bounds.X, advenPosY + bounds.Y, adven.Width, adven.Height), Color.White);

                displayDialog(player, Assets.Instance.Get("placeholders/Textures/Adven"), advenPosX + bounds.X, advenPosY + bounds.Y);

            }

            if (location3)
            {
                currentMap = map2;
                // przeciwnik - DOSKOZZZA
                gameScreenManager.SpriteBatch.Draw(enemy.GetTexture(), enemy.GetPosition(), Color.White);
            }
            if (isPaused)
            {
                //gameScreenManager.SpriteBatch.Draw(blank, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White * 0.5f);
                // gameScreenManager.SpriteBatch.Draw(quitButton, quitButtonRectangle, quitButtonColor);

            }
            gameScreenManager.SpriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            moveCamera(128,128,650,352);
            if (!songStart)
            {
                MediaPlayer.Play(currentSong);
                songStart = true;
            }
            
            KeyboardEvents();
            checkIfColide();
            dontGoOutsideMap();
            if (!isPaused)
            {
                player.move(player.Width, player.Height, canMoveLeft,canMoveUp, canMoveRight, canMoveDown);
                bacgrund = Color.Green;
                ManageLocations();
                if (location3)
                {
                    enemy.AI(gameTime);
                }
            }
        }


        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState) { }

        public void drawTiles(int layer, TmxMap map)
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
                    gameScreenManager.SpriteBatch.Draw(tileset, new Rectangle((int)x + bounds.X, (int)y + bounds.Y, tileWidth, tileHeight), tilesetRec, Color.White);
                }
            }
        }

        public void getColideObjects(TmxMap map)
        {
            colisionTiles.Clear();
            for (var i = 0; i < map.Layers[0].Tiles.Count; i++)
            {
                int gid = map.Layers[0].Tiles[i].Gid;

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

        void dontGoOutsideMap()
        {

            if(player.GetPosition().X <= 0)
            {
                canMoveLeft = false;
            }

            if (player.GetPosition().Y <= 0)
            {
                canMoveUp = false;
            }

            if (player.GetPosition().Y +player.texture.Height > screenHeight && bounds.Y < -800)
            {
                canMoveDown = false;
            }
            if (player.GetPosition().X + player.texture.Width >= screenWdth && bounds.X < -1440)
            {
                canMoveRight = false;
            }

        }

            void checkIfColide()
        {
            canMoveRight = true;
            canMoveLeft = true;
            canMoveDown = true;
            canMoveUp = true;

            foreach (Rectangle tile in colisionTiles)
            {
              //  Console.WriteLine("tile: " + tile);
                Rectangle ghost = new Rectangle((int)player.GetPosition().X, (int)player.GetPosition().Y, player.texture.Width, player.texture.Height);

                if (ghost.Intersects(tile))
                {
                    if(ghost.X <= tile.X)
                    {
                        canMoveRight = false;
                    }


                    if (ghost.X > tile.X)
                    {
                        canMoveLeft = false;
                    }

                    if(ghost.Y > tile.Y)
                    {
                        canMoveUp = false;
                    }

                    if(ghost.Y <= tile.Y)
                    {
                        canMoveDown = false;
                    }

                }

            }
        }

        void moveCamera(int lewy, int gorny, int prawy, int dolny)
        {
            int mapWidth = map.Width*32;
            int mapHeight = map.Height*32;

            
            KeyboardState keyState = Keyboard.GetState();

            int scrollx = 0, scrolly = 0;

            //  if (keyState.IsKeyDown(Keys.Left))
            //   scrollx = cumSpeed;

            //right border
            if (player.GetPosition().X > prawy && keyState.IsKeyDown(Keys.Right) && bounds.X - screenWdth> -1*mapWidth)
            {
                //   canMoveRight = false;
                getColideObjects(map);
                 scrollx = -7;
                player.SetPosition(player.GetPosition().X - 7, player.GetPosition().Y);                   
            }

            if (player.GetPosition().X < lewy && keyState.IsKeyDown(Keys.Left) && bounds.X <0)
            {
                
                getColideObjects(map);
                scrollx = +7;
                player.SetPosition(player.GetPosition().X + 7, player.GetPosition().Y);
            }

            if (player.GetPosition().Y > dolny && keyState.IsKeyDown(Keys.Down) && bounds.Y - screenHeight > -1 * mapHeight)
            {
                //   canMoveRight = false;
                getColideObjects(map);
                scrolly = -7;
                player.SetPosition(player.GetPosition().X, player.GetPosition().Y -7);
            }

            if (player.GetPosition().Y < gorny && keyState.IsKeyDown(Keys.Up) && bounds.Y < 0)
            {
                //   canMoveRight = false;
                getColideObjects(map);
                scrolly = +7;
                player.SetPosition(player.GetPosition().X, player.GetPosition().Y +7);
            }
            //to 700 to ustawiona na sztywno prawa krawedz mapy

            //    if (keyState.IsKeyDown(Keys.Up))
            //       scrolly = cumSpeed;
            //   if (keyState.IsKeyDown(Keys.Down))
            //       scrolly = -cumSpeed;

            bounds.X += scrollx;
            bounds.Y += scrolly;
        }

        private void KeyboardEvents()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                if (!isEscPressed)
                {
                    isPaused = !isPaused;
                    isEscPressed = true;
                }
            }
            else isEscPressed = false;
        }



        void ManageLocations()
        {
            if (location1)
            {
                if ((player.GetPosition().X + player.Width >= policjaPosX + bounds.X && player.GetPosition().X < policjaPosX + bounds.X + policjaWidth) && (player.GetPosition().Y + player.Height >= policjaPosY + bounds.Y && player.GetPosition().Y < policjaPosY + bounds.Y+ policjaHeight))
                {
                    bacgrund = bacgrundAfterHit;
                    kolorPow = Color.White;
                    kolorPow2 = Color.White * 0;
                    wht = Color.Wheat * 0;

                    player.SetPosition(15, 290);

                    location1 = false;
                    location2 = true;
                    location3 = false;
                }

                //if (player.GetPosition().X < 0)
                //{
                //    kolorPow2 = Color.White;
                //    kolorPow = Color.White * 0;
                //    wht = Color.White * 0;

                //    player.SetPosition(615, player.GetPosition().Y);

                //    location1 = false;
                //    location2 = false;
                //    location3 = true;
                //}
            }

            if (location2)
            {
                if ((player.GetPosition().X + player.Width >= ralfX && player.GetPosition().X < ralfX + ralfWidth) && (player.GetPosition().Y + player.Height >= ralfY && player.GetPosition().Y < ralfY + ralfHeight))
                {
                    // bacgrund = Color.Green;
                    kolorPow2 = Color.White;
                    kolorPow = Color.White * 0;
                    wht = Color.White * 0;

                    pos.X = 100;
                    pos.Y = 320;

                    player.SetPosition(100, 320);

                    location1 = false;
                    location2 = false;
                    location3 = true;


                }

                //if (player.GetPosition().X < 0)   /// do lasu
                //{
                //    kolorPow = Color.Wheat * 0;
                //    bacgrund = Color.Green;
                //    kolorPow2 = Color.White * 0;
                //    wht = Color.White;

                //    player.SetPosition(615, player.GetPosition().Y);

                //    location1 = true;
                //    location2 = false;
                //    location3 = false;
                //}
            }

            if (location3)
            {
                if (player.GetPosition().X + player.Width >= enemy.GetPosition().X
                    && player.GetPosition().Y + player.Width >= enemy.GetPosition().Y)
                {
                    //CHANGE STATE TO COMBAT HEEREEEE

                    gameScreenManager.RemoveScreen(this);
                    gameScreenManager.InsertScreen(Combat);

                    //if (songStart)
                    //{
                    //    MediaPlayer.Stop();
                    //    combatMusic.Play();
                    //    songStart = false;
                    //}
                }


                if (player.GetPosition().X + player.Width >= 800)    /// do lasu
                {
                    bacgrund = Color.Green;
                    kolorPow2 = Color.White * 0;
                    wht = Color.White;
                    pos.X = 15;
                    //pos.Y = 2;
                    player.SetPosition(15, player.GetPosition().Y);

                    location1 = true;
                    location2 = false;
                    location3 = false;
                }

                if (player.GetPosition().X < 0)   /// do miasta
                {
                    bacgrund = bacgrundAfterHit;
                    kolorPow = Color.White;
                    kolorPow2 = Color.Wheat * 0;
                    wht = Color.Wheat * 0;

                    pos.X = 650;
                    //   pos.Y = 290;



                    location1 = false;
                    location2 = true;
                    location3 = false;
                }
            }
        }



        public void displayDialog(Player playerOne, Texture2D npcet, int posx, int posy)
        {

            int index = 2;
            if (playerOne.GetPosition().X >= posx - 50 && playerOne.GetPosition().X < posx + npcet.Width + 20
                && playerOne.GetPosition().Y >= posy && playerOne.GetPosition().Y < posy + npcet.Height + 30)

            {
                Rectangle tlo = new Rectangle(100, 420, 500, 50);

                if (!disp)
                {
                    dymek = Assets.Instance.Get("placeholders/Textures/dymek");
                    gameScreenManager.SpriteBatch.Draw(dymek, new Rectangle(advenPosX - 12 + bounds.X, advenPosY-14 + bounds.Y, dymek.Width*2, dymek.Height*2), Color.White);
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && !disp)
                        disp = true;
                }
                // else if (Keyboard.GetState().IsKeyDown(Keys.Space) && disp)
                //     disp = false;

                if (disp)
                {
                    //var random = new Random();
                    //index = random.Next(powiedzonka.Count);

                    gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("placeholders/Textures/dotekstu"), tlo, Color.White);
                    gameScreenManager.SpriteBatch.DrawString(dialog, powiedzonka[index], new Vector2(145, 425), Color.White);

                }
            }
        }

    }
}