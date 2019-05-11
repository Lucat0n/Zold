using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
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
        SoundEffect currentSong;
        SoundEffect bgMusic;

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

        //Combat
        Combat.CombatScreen Combat;
        Combat.Characters.Player combatPlayer;
        Combat.Characters.Enemies.Enemy skeleton;
        Combat.Characters.Enemies.Enemy rat;
        List<Combat.Characters.Enemies.Enemy> enemies;

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
        bool wasPaused = false;
        bool songStart = false;
        bool pressed = false;
        bool location1;
        bool location2;
        bool location3;
        bool isPaused = false;
        private bool isEscPressed = false;
        bool disp = false; // is message displayed?
        bool drawed = false;
        bool canMoveLeft;
        bool canMoveUp;
        bool canMoveRight;
        bool canMoveDown;

        int screenWdth = 800;
        int screenHeight = 480;

        Rectangle bounds; //camera bounds 

        SpriteBatchSpriteSheet animManager;
        SpriteBatchSpriteSheet spriteSheet;
        SpriteBatchSpriteSheet spriteSheetHP;

        //measures
        int playerWidth = 32;
        int playerHeight = 48;

        TimeSpan PauseCooldown;

        int hp;

        public MapManager(){}

        #region init
        public override void LoadContent()
        {
            gameScreenManager.ContentLoader.LoadLocation("placeholders");

            canMoveDown = true;
            canMoveLeft = true;
            canMoveRight = true;
            canMoveUp = true;

            hp = 100;

            PauseCooldown = new TimeSpan(0, 0, 0, 500);

            powiedzonka.Add("Witaj zielona magnetyczna gwiazdo");
            powiedzonka.Add("A ty tu czego?");
            powiedzonka.Add("Zbieram zlom, nie widzisz bulwa");
            powiedzonka.Add("Jestem zajety");
            powiedzonka.Add("Odejdz");
            powiedzonka.Add("Do samochodu i do widzenia");
            powiedzonka.Add("Ile razy sie zesrales? ");
            powiedzonka.Add("Niech zyje wolny zold, precz z komuniom");
            powiedzonka.Add("Elo");
            powiedzonka.Add("Tez kiedys bylem jak ty, ale sie jeblem i przestalem");
            powiedzonka.Add("Ruchasz sie?");
  
            // loading music
            //combatMusic = Assets.Instance.Get("placeholders/Music/kombat");

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

            location1 = true;
            location2 = false;
            location3 = false;

            pos = new Vector2(10, 10);

            spriteSheet = new SpriteBatchSpriteSheet(gameScreenManager.GraphicsDevice, Assets.Instance.Get("placeholders/Textures/main"), 4, 3, playerWidth, playerHeight);
            player = new Map.Player(pos, Assets.Instance.Get("placeholders/Textures/main"), 2.7f, spriteSheet, hp);

            //hpbar 
            spriteSheetHP = new SpriteBatchSpriteSheet(gameScreenManager.GraphicsDevice, Assets.Instance.Get("placeholders/Textures/hpbars"), 101, 1, 250,32);
           // hpbar = new Map.Player(pos, Assets.Instance.Get("placeholders/Textures/main"), 2.7f, spriteSheet);
            //player = new Player(pos, Assets.Instance.Get("placeholders/Textures/main"));

            enemy = new Enemy(player, new Vector2(400, 300));
            enemy.SetTexture(Assets.Instance.Get("placeholders/Textures/rat"));

            // Combat

            enemies = new List<Combat.Characters.Enemies.Enemy>();

            combatPlayer = new Combat.Characters.Player(new Vector2(0, 200), 100, enemies, new SpriteBatchSpriteSheet(gameScreenManager.GraphicsDevice, Assets.Instance.Get("placeholders/Textures/main"), 4, 3, playerWidth, playerHeight), 32 ,48);

            skeleton = new Combat.Characters.Enemies.Mob(combatPlayer, new Vector2(300, 300), Assets.Instance.Get("placeholders/Textures/skeleton"), 32, 48);
            rat = new Combat.Characters.Enemies.Charger(combatPlayer, new Vector2(300, 400), Assets.Instance.Get("placeholders/Textures/rat"), 44, 20);
            enemies.Add(skeleton);
            enemies.Add(rat);
            Combat = new Combat.CombatScreen(combatPlayer, enemies);

            //camera
            bounds = new Rectangle(0, 0, 0, 0);
            getColideObjects(map, 0);
            getColideObjects(map2,0);

        }

        public override void UnloadContent()
        {
            Assets.Instance.Remove("placeholders");
        }
        #endregion

        #region drawupdate
        public override void Draw(GameTime gameTime)
        {
            gameScreenManager.GraphicsDevice.Clear(Color.Black);
            gameScreenManager.SpriteBatch.Begin();
            
            drawTiles(1, currentMap);
            drawTiles(0, currentMap);

            player.Animation(gameTime);

            gameScreenManager.SpriteBatch.DrawString(dialog, "X: "+player.GetPosition().X.ToString(), new Vector2(10, 10), Color.White);
            gameScreenManager.SpriteBatch.DrawString(dialog, "Y: "+player.GetPosition().Y.ToString(), new Vector2(10, 40), Color.White);
            gameScreenManager.SpriteBatch.DrawString(dialog, "boundsX: "+bounds.X.ToString(), new Vector2(10, 70), Color.White);
            gameScreenManager.SpriteBatch.DrawString(dialog, "boundsY: "+bounds.Y.ToString(), new Vector2(10, 110), Color.White);
            
           // gameScreenManager.SpriteBatch.Draw(player.texture, player.GetPosition(), Color.White);

            ///budunek policji
            gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("placeholders/Textures/police"), new Rectangle(policjaPosX + bounds.X, policjaPosY + bounds.Y, policjaWidth + 50, policjaHeight + 20), wht);

            //hpbar
            if (gameScreenManager.IsFullScreenOn)
            {
                int posY= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 300;
                player.AnimateHealth(gameTime, spriteSheetHP, posY);
            }
            else
            {
                //gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("placeholders/Textures/hpbar"), new Rectangle(550, 16, 250, 32), wht);
                player.AnimateHealth(gameTime, spriteSheetHP,550);
            }

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
                // przeciwnik
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
            
            if (gameScreenManager.IsFullScreenOn)
            {
                moveCamera(256, 256, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 256, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 256);
            }
            else
            {
                moveCamera(128, 128, 650, 352); /// trub okienkowy
            }

            //if (!songStart)
            //{
            //    MediaPlayer.Play(currentSong);
            //    songStart = true;
            //}
            
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
            else
            {
                gameScreenManager.InsertScreen(new Pause.PauseScreen());
                isPaused = false;
            }
        }

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
                    spriteSheet.Begin();
                    spriteSheet.Draw(tileset, new Rectangle((int)x + bounds.X, (int)y + bounds.Y, tileWidth, tileHeight), tilesetRec, Color.White);
                    spriteSheet.End();
                }
            }
        }

        public void getColideObjects(TmxMap map, int warstwa)
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

        void dontGoOutsideMap()
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
                canMoveLeft = false;
            }

            if (player.GetPosition().Y <= 0)
            {
                canMoveUp = false;
            }

            if (player.GetPosition().Y +playerHeight >sh)
            {
                canMoveDown = false;
            }
            if (player.GetPosition().X + playerWidth >= sw )
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
                Rectangle ghost = new Rectangle((int)player.GetPosition().X, (int)player.GetPosition().Y, 32, 48);

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


            KeyboardState keyState = Keyboard.GetState();

            int scrollx = 0, scrolly = 0;

            //right border
            if (player.GetPosition().X > prawy && keyState.IsKeyDown(Keys.Right) && bounds.X - sw> -1*mapWidth)
            {
                //   canMoveRight = false;
                getColideObjects(map,0);
                 scrollx = -7;
                player.SetPosition(player.GetPosition().X - 7, player.GetPosition().Y);                   
            }

            if (player.GetPosition().X < lewy && keyState.IsKeyDown(Keys.Left) && bounds.X <0)
            {
                
                getColideObjects(map,0);
                scrollx = +7;
                player.SetPosition(player.GetPosition().X + 7, player.GetPosition().Y);
            }

            if (player.GetPosition().Y > dolny && keyState.IsKeyDown(Keys.Down) && bounds.Y - sh > -1 * mapHeight)
            {
                //   canMoveRight = false;
                getColideObjects(map,0);
                scrolly = -7;
                player.SetPosition(player.GetPosition().X, player.GetPosition().Y -7);
            }

            if (player.GetPosition().Y < gorny && keyState.IsKeyDown(Keys.Up) && bounds.Y < 0)
            {
                //   canMoveRight = false;
                getColideObjects(map,0);
                scrolly = +7;
                player.SetPosition(player.GetPosition().X, player.GetPosition().Y +7);
            }

            bounds.X += scrollx;
            bounds.Y += scrolly;
        }

        #endregion

        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState)
        {
            if(keyboardState.IsKeyDown(Keys.Escape) && !pressed) 
            {
                pressed = true;
                isPaused = !isPaused;
                //Debug.WriteLine(isPaused ? "paused" : "unpaused");
            }else if (keyboardState.IsKeyUp(Keys.Escape) && pressed)
            {
                pressed = false;
            }
        }

        #region managelocations
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
            }

            if (location3)
            {
                if (player.GetPosition().X + player.Width >= enemy.GetPosition().X
                    && player.GetPosition().Y + player.Width >= enemy.GetPosition().Y)
                {
                    //CHANGE STATE TO COMBAT
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
  
                    location1 = false;
                    location2 = true;
                    location3 = false;
                }
            }
        }
        #endregion
        
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

        /*private void CalculatePause(GameTime gameTime)
        {
            this.PauseCooldown -= new TimeSpan(0,0,0,gameTime.ElapsedGameTime.Milliseconds);
            if (PauseCooldown <= TimeSpan.Zero)
            {
                Debug.WriteLine("Halo salut");
                wasPaused = false;
            }
        }*/

    }
}