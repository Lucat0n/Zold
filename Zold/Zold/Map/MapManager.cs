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

namespace Map
{
    class MapManager : Zold.Screens.GameScreen
    {

        public List<string> powiedzonka = new List<string>();

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
        SpriteFont font;
        SpriteFont dialog;

        // //postacie
        Texture2D pietrek;
        Vector2 pos;

        Texture2D adven;
        int advenPosX = 256;
        int advenPosY = 64;

        //ramka na tekst
        Texture2D dotekstu;
        Texture2D dymek;
        //tla (nie uzywane)
        Texture2D cyberpunk;
        Texture2D poww;

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

        Map.Player player;
        Map.Enemy enemy;

        //bools
        bool songStart = false;
        bool displayed = false;
        bool forest;
        bool city;
        bool cyber;
        bool isPaused = false;
        bool isEscPressed = false;
        bool disp = false; // is message displayed?

        

        public MapManager()
        {

            //map2 = new TmxMap(@"Content/placeholders/mapa3.tmx");
        }

        public override void LoadContent()
        {
            powiedzonka.Add("Witaj zielona magnetyczna gwiazdo");
            powiedzonka.Add("A ty tu czego?");
            powiedzonka.Add("Nie widzisz, ze jestem zajety");
            powiedzonka.Add("Elo");
            powiedzonka.Add("Tez kiedys bylem jak ty, ale sie jeblem i przestalem");
            powiedzonka.Add("Ruchasz sie?");
            //poww = gameScreenManager.Content.Load<Texture2D>("placeholders/citybackgrund");
           // cyberpunk = gameScreenManager.Content.Load<Texture2D>("placeholders/cyber2");

            //postacie
            pietrek = gameScreenManager.Content.Load<Texture2D>("placeholders/main");
            adven = gameScreenManager.Content.Load<Texture2D>("placeholders/Adven");
            //budynki
            budynek1 = gameScreenManager.Content.Load<Texture2D>("placeholders/police");
            policja = gameScreenManager.Content.Load<Texture2D>("placeholders/ralf");

            //do tekstu
            dotekstu = gameScreenManager.Content.Load<Texture2D>("placeholders/dotekstu");
            dymek = gameScreenManager.Content.Load<Texture2D>("placeholders/dymek");
  
            // loading music
            menuMusic = gameScreenManager.Content.Load<Song>(@"placeholders/doskozzza");
            gameplayMusic = gameScreenManager.Content.Load<Song>("placeholders/lufa");
            bgMusic = gameScreenManager.Content.Load<SoundEffect>("placeholders/menu-music");
            combatMusic = gameScreenManager.Content.Load<SoundEffect>("placeholders/kombat");

            currentSong = gameplayMusic;

            //loading fonts
            font = gameScreenManager.Content.Load<SpriteFont>("placeholders/font");
            dialog = gameScreenManager.Content.Load<SpriteFont>("placeholders/dialog");

            map = new TmxMap(@"Content/placeholders/mapa2.tmx");
            map2 = new TmxMap(@"Content/placeholders/mapa3.tmx");
            currentMap = map;

            tileset = gameScreenManager.Content.Load<Texture2D>(map.Tilesets[0].Name.ToString());
            tileWidth = map.Tilesets[0].TileWidth;
            tileHeight = map.Tilesets[0].TileHeight;
            tilesetTilesWide = tileset.Width / tileWidth;
            tilesetTilesHigh = tileset.Height / tileHeight;

            currentSong = gameplayMusic;
            //MediaPlayer.Play(currentSong);

            forest = true;
            city = false;
            cyber = false;

            pos = new Vector2(10, 10);
            player = new Map.Player(pos);
            player.SetTexture(pietrek);

            enemy = new Map.Enemy(player, new Vector2(400, 300));
            enemy.SetTexture(gameScreenManager.Content.Load<Texture2D>("placeholders/dosko-sm"));

        }

        public override void UnloadContent()
        {
            throw new NotImplementedException();
        }


        public override void Draw(GameTime gameTime)
        {
            gameScreenManager.GraphicsDevice.Clear(Color.Black);
            gameScreenManager.SpriteBatch.Begin();
            drawTiles(0, currentMap);

            //gameScreenManager.SpriteBatch.Draw(poww, new Rectangle(0, 0, 802, 580), kolorPow);
            // gameScreenManager.SpriteBatch.Draw(cyberpunk, new Rectangle(0, 0, 802, 580), kolorPow2);
            gameScreenManager.SpriteBatch.Draw(player.GetTexture(), player.GetPosition(), Color.White);

            ///budunek policji
            gameScreenManager.SpriteBatch.Draw(budynek1, new Rectangle(policjaPosX, policjaPosY, policjaWidth + 50, policjaHeight + 20), wht);
            //wiezowiec 2
            gameScreenManager.SpriteBatch.Draw(policja, new Rectangle(ralfX, ralfY, ralfWidth, ralfHeight), kolorPow);

            //postacie
            if (forest)
            {
                gameScreenManager.SpriteBatch.Draw(adven, new Rectangle(advenPosX, advenPosY, adven.Width, adven.Height), Color.White);

                displayDialog(player, adven, advenPosX, advenPosY);
                
            }

            if (cyber)
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
            if (!songStart)
            {
                MediaPlayer.Play(currentSong);
                songStart = true;
            }

            KeyboardEvents();

            if (!isPaused)
            {
                player.move(pietrek.Width, pietrek.Height);
                bacgrund = Color.Green;
                ManageLocations();
                if (cyber)
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
                    //if (layer==1 && tilesetRec.Intersects(player.GetTextureRecta())){
                    //    player.SetPosition(new Vector2(32,32));
                    //}
                    gameScreenManager.SpriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                }
            }
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
            if (forest)
            {
                if ((player.GetPosition().X + pietrek.Width >= policjaPosX && player.GetPosition().X < policjaPosX + policjaWidth) && (player.GetPosition().Y + pietrek.Height >= policjaPosY && player.GetPosition().Y < policjaPosY + policjaHeight))
                {
                    bacgrund = bacgrundAfterHit;
                    kolorPow = Color.White;
                    kolorPow2 = Color.White * 0;
                    wht = Color.Wheat * 0;

                    player.SetPosition(15, 290);

                    forest = false;
                    city = true;
                    cyber = false;
                }

                if (player.GetPosition().X < 0)
                {
                    kolorPow2 = Color.White;
                    kolorPow = Color.White * 0;
                    wht = Color.White * 0;

                    player.SetPosition(615, player.GetPosition().Y);

                    forest = false;
                    city = false;
                    cyber = true;
                }
            }

            if (city)
            {
                if ((player.GetPosition().X + pietrek.Width >= ralfX && player.GetPosition().X < ralfX + ralfWidth) && (player.GetPosition().Y + pietrek.Height >= ralfY && player.GetPosition().Y < ralfY + ralfHeight))
                {
                    // bacgrund = Color.Green;
                    kolorPow2 = Color.White;
                    kolorPow = Color.White * 0;
                    wht = Color.White * 0;

                    pos.X = 100;
                    pos.Y = 320;

                    player.SetPosition(100, 320);

                    forest = false;
                    city = false;
                    cyber = true;


                }

                if (player.GetPosition().X < 0)   /// do lasu
                {
                    kolorPow = Color.Wheat * 0;
                    bacgrund = Color.Green;
                    kolorPow2 = Color.White * 0;
                    wht = Color.White;

                    player.SetPosition(615, player.GetPosition().Y);

                    forest = true;
                    city = false;
                    cyber = false;
                }
            }

            if (cyber)
            {
                if (player.GetPosition().X + pietrek.Width >= enemy.GetPosition().X
                    && player.GetPosition().Y + pietrek.Width >= enemy.GetPosition().Y)
                {
                    //CHANGE STATE TO COMBAT HEEREEEE

                    //if (songStart)
                    //{
                    //    MediaPlayer.Stop();
                    //    combatMusic.Play();
                    //    songStart = false;
                    //}
                }


                if (player.GetPosition().X + pietrek.Width >= 800)    /// do lasu
                {
                    bacgrund = Color.Green;
                    kolorPow2 = Color.White * 0;
                    wht = Color.White;
                    pos.X = 15;
                    //pos.Y = 2;
                    player.SetPosition(15, player.GetPosition().Y);

                    forest = true;
                    city = false;
                    cyber = false;
                }

                if (player.GetPosition().X < 0)   /// do miasta
                {
                    bacgrund = bacgrundAfterHit;
                    kolorPow = Color.White;
                    kolorPow2 = Color.Wheat * 0;
                    wht = Color.Wheat * 0;

                    pos.X = 650;
                    //   pos.Y = 290;



                    forest = false;
                    city = true;
                    cyber = false;
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
                    

                    gameScreenManager.SpriteBatch.Draw(dymek, new Rectangle(advenPosX - 5, advenPosY, dymek.Width, dymek.Height), Color.White);
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && !disp)
                        disp = true;
                }
               // else if (Keyboard.GetState().IsKeyDown(Keys.Space) && disp)
               //     disp = false;

                if (disp)
                {
                    //var random = new Random();
                    //index = random.Next(powiedzonka.Count);

                    gameScreenManager.SpriteBatch.Draw(dotekstu, tlo, Color.White);
                    gameScreenManager.SpriteBatch.DrawString(dialog, powiedzonka[index], new Vector2(145, 425), Color.White);

                }
            }
        }

    }
}
