using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using Zold.Utilities;

namespace Zold
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ContentLoader contentLoader;

        Texture2D scott;
        //public static int qwe = 90;
        Texture2D cyberpunk;
        Texture2D poww;

        Texture2D ralf;
        Texture2D wallace;

        Texture2D splash;

        Texture2D zold;
        Texture2D playButton;
        Texture2D optionsButton;
        Texture2D creditsButton;
        Texture2D blank;

        Vector2 pos;

        float splashAlpha = 0.0f;
        float splashDelay = 5;

        float zoldAlpha = 0.0f;
        float zoldDelay = 4;

        int zoldY;

        Color wht = Color.White;
        Color whtC = Color.White;

        Color bacgrund = Color.Black;
        Color bacgrundAfterHit = Color.Red;

        int moveSpeed = 3;

        Color kolorPow = Color.White * 0;
        Color kolorPow2 = Color.White * 0;

        Color playButtonColor = Color.White;
        Color optionsButtonColor = Color.White;

        public enum gameState { Splash, Menu, Game, Pause, Credits, Combat }

        private gameState state;

        // // wymiary i pos policji 
        int wallacePosX = 400;
        int wallacePosY = 300;
        static int wallaceWidth = 80;
        static int wallaceHeight = 120;

        // // wymiary i pos budynku 2
        int ralfX = 440;
        int ralfY = 40;
        static int ralfWidth = wallaceWidth + 50;
        static int ralfHeight = wallaceHeight + 80;

        bool forest;
        bool city;
        bool cyber;
        bool isPaused = false;
        bool isEscPressed = false;

        MouseState mouseState;
        Rectangle Cursor;
        Rectangle backButtonRectangle;
        Rectangle optionsButtonRectangle;
        Rectangle playButtonRectangle;
        Rectangle resolutionButtonRectangle;
        Rectangle checkBoxRectangle;
        Rectangle quitButtonRectangle;

        Texture2D quitButton;
        //Texture2D creditsButton;
        Texture2D backButton;
        Texture2D boxChecked;
        Texture2D boxUnchecked;
        Texture2D fscrIcon;

        Color backButtonColor = Color.White;
        Color quitButtonColor = Color.White;
        Color checkBoxColor = Color.White; //DO ZMIANY JEŚLI MA BYĆ WIĘCEJ CHECKBOXÓW


        bool isFullScreenOn = false;
        bool isMenuLoaded = false;
        bool isMousePressed = false;
        bool inOptions = false;


        // here is dope music
        Song menuMusic;
        Song gameplayMusic;
        Song currentSong;
        SoundEffect bgMusic;
        SoundEffect combatMusic;

        bool songStart = false;
        bool combatStart = false;

        //player
        Map.Player playerOne;
        Map.Enemy enemy;

        //Combat - temp
        Combat.Combat Combat;
        Combat.Player combatPlayer;
        Combat.Enemy skeleton;
        Combat.Enemy rat;
        List<Combat.Enemy> enemies;
        SpriteFont font;
        Texture2D combatPlayerTex;
        Texture2D skeletonTex;
        Texture2D ratTex;
        Texture2D line;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            contentLoader = new ContentLoader(this);
            base.Initialize();
            
            state = gameState.Menu;

            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void LoadContent()
        {
            contentLoader.LoadLocation("placeholders");
            // Create a new SpriteBatch, which can be used to draw textures.
            /*spriteBatch = new SpriteBatch(GraphicsDevice);
            //game elements
            poww = Content.Load<Texture2D>("placeholders/citybackgrund");
            cyberpunk = Content.Load<Texture2D>("placeholders/cyber2");
            scott = Content.Load<Texture2D>("placeholders/sct");
            wallace = Content.Load<Texture2D>("placeholders/police");
            ralf = Content.Load<Texture2D>("placeholders/ralf");
            splash = Content.Load<Texture2D>("placeholders/rzprod");
            zold = Content.Load<Texture2D>("placeholders/zold");
            playButton = Content.Load<Texture2D>("placeholders/playButton");
            optionsButton = Content.Load<Texture2D>("placeholders/optionsButton");
            quitButton = Content.Load<Texture2D>("placeholders/quitButton");
            blank = Content.Load<Texture2D>("placeholders/blank");
            backButton = Content.Load<Texture2D>("placeholders/backButton");
            boxChecked = Content.Load<Texture2D>("placeholders/boxChecked");
            boxUnchecked = Content.Load<Texture2D>("placeholders/boxUnchecked");
            fscrIcon = Content.Load<Texture2D>("placeholders/fscrIcon");


            // loading music
            menuMusic = Content.Load<Song>(@"placeholders/doskozzza");
            gameplayMusic = Content.Load<Song>("placeholders/lufa");
            bgMusic = Content.Load<SoundEffect>("placeholders/menu-music");
            combatMusic = Content.Load<SoundEffect>("placeholders/kombat");

            // Combat content
            font = Content.Load<SpriteFont>("placeholders/font");
            combatPlayerTex = Content.Load<Texture2D>("placeholders/main");
            skeletonTex = Content.Load<Texture2D>("placeholders/skeleton");
            ratTex = Content.Load<Texture2D>("placeholders/rat");
            line = Content.Load<Texture2D>("placeholders/line");
            

            currentSong = menuMusic;

            MediaPlayer.Play(currentSong);

            pos = new Vector2(10, 10);

            forest = true;
            city = false;
            cyber = false;

            //makin playa
            playerOne = new Map.Player(pos);
            playerOne.SetTexture(scott);
            
            enemy = new Map.Enemy(playerOne, new Vector2(400, 300));
            enemy.SetTexture(Content.Load<Texture2D>("placeholders/dosko-sm"));
            // TODO: use this.Content to load your game content here

            // Combat
            enemies = new List<Combat.Enemy>();

            combatPlayer = new Combat.Player(new Vector2(0, 200), 100, enemies);
            combatPlayer.SetTexture(combatPlayerTex);
            
            skeleton = new Combat.Mob(combatPlayer, new Vector2(300, 300));
            rat = new Combat.Charger(combatPlayer, new Vector2(300, 400));
            skeleton.SetTexture(skeletonTex);
            rat.SetTexture(ratTex);

            enemies.Add(skeleton);
            enemies.Add(rat);

            Combat = new Combat.Combat(combatPlayer, enemies, font, line);*/

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Debug.WriteLine(contentLoader.Assets.Keys.Count);
            //Debug.WriteLine(Path.GetFileName("../../../../Content/placeholders"));
            //MediaPlayer.Play(menuMusic);

            /*switch (state)
            {
                case gameState.Splash:
                    //state = gameState.Game;
                    break;
                case gameState.Menu:
                    UpdateMenu(gameTime);
                    // MediaPlayer.Play(menuMusic);
                    break;
                case gameState.Pause:
                    break;
                case gameState.Credits:
                    break;
                case gameState.Game:
                    UpdateMainGame(gameTime);
                    break;
                case gameState.Combat:
                    Combat.Update(gameTime);
                    break;
            }


            base.Update(gameTime);*/
        }

        protected void UpdateMenu(GameTime gameTime)
        {
            bacgrund = Color.Black;
            playButtonRectangle = new Rectangle(GraphicsDevice.Viewport.Width / 2 - GraphicsDevice.Viewport.Width / 8, GraphicsDevice.Viewport.Width / 2 - GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Width / 10);
            optionsButtonRectangle = new Rectangle(GraphicsDevice.Viewport.Width / 2 - GraphicsDevice.Viewport.Width / 8, GraphicsDevice.Viewport.Width / 2 - GraphicsDevice.Viewport.Width / 8, GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Width / 10);
            if (isMenuLoaded)
            {
                UpdateCursorPosition();
                ButtonsEvents();
            }
            if (inOptions)
            {
                checkBoxRectangle = new Rectangle(GraphicsDevice.Viewport.Width / 2 + GraphicsDevice.Viewport.Width / 8, GraphicsDevice.Viewport.Width / 2 - GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Width / 12, GraphicsDevice.Viewport.Width / 12);
                resolutionButtonRectangle = new Rectangle(GraphicsDevice.Viewport.Width / 2 - GraphicsDevice.Viewport.Width / 16, GraphicsDevice.Viewport.Width / 2 - GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Width / 8, GraphicsDevice.Viewport.Width / 8);
                backButtonRectangle = new Rectangle(GraphicsDevice.Viewport.Width / 2 - GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Width / 2 - GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Width / 12, GraphicsDevice.Viewport.Width / 12);

            }
        }

        protected void UpdateMainGame(GameTime gameTime)
        {
            if (!songStart)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(gameplayMusic);
                songStart = true;
            }

            KeyboardEvents();

            if (!isPaused)
            {
                playerOne.move(scott.Width, scott.Height);
                bacgrund = Color.Green;
                ManageLocations();
                if (cyber)
                {
                    enemy.AI(gameTime);
                }
            }
            else
            {
                PauseButtonsEvents();
                UpdateCursorPosition();
                quitButtonRectangle = new Rectangle(GraphicsDevice.Viewport.Width / 2 - GraphicsDevice.Viewport.Width / 8, GraphicsDevice.Viewport.Width / 2 - GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Width / 10);

            }
        }

        protected override void Draw(GameTime gameTime)
        {
            /*GraphicsDevice.Clear(bacgrund);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            switch (state)
            {
                case gameState.Splash:
                    DrawSplash(gameTime);
                    break;
                case gameState.Menu:
                    DrawMenu(gameTime);
                    break;
                case gameState.Pause:
                    break;
                case gameState.Credits:
                    break;
                case gameState.Game:
                    DrawMainGame(gameTime);
                    break;
                case gameState.Combat:
                    Combat.Draw(spriteBatch);
                    break;
            }


            spriteBatch.End();

            base.Draw(gameTime);*/
        }

        protected void DrawSplash(GameTime gameTime)
        {
            splashDelay -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (splashDelay > 0.0f) splashAlpha += (float)gameTime.ElapsedGameTime.TotalSeconds / 2.5f;
            else if (splashDelay > -5.0f) splashAlpha -= (float)gameTime.ElapsedGameTime.TotalSeconds / 2.5f;
            else state = gameState.Menu;
            spriteBatch.Draw(splash, new Vector2(GraphicsDevice.Viewport.Width / 2 - splash.Width / 2, GraphicsDevice.Viewport.Height / 2 - splash.Height / 2), Color.White * splashAlpha);
        }

        protected void DrawMenu(GameTime gameTime)
        {
            if (!inOptions)
            {
                zoldAlpha += (float)gameTime.ElapsedGameTime.TotalSeconds / 2.5f;
                zoldDelay -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (zoldDelay > 0.0f)
                {
                    zoldY = GraphicsDevice.Viewport.Height / 3;
                }
                else if (zoldY > GraphicsDevice.Viewport.Width / 20)
                {
                    isMenuLoaded = false;
                    zoldY--;
                }
                else
                {
                    isMenuLoaded = true;
                    spriteBatch.Draw(playButton, playButtonRectangle, playButtonColor);
                    spriteBatch.Draw(optionsButton, optionsButtonRectangle, optionsButtonColor);
                }
                //spriteBatch.Draw(zold, new Vector2(GraphicsDevice.Viewport.Width / 2 - zold.Width / 2, GraphicsDevice.Viewport.Height / 2 - zold.Height / 2), Color.White * zoldAlpha);
                spriteBatch.Draw(zold, new Rectangle(GraphicsDevice.Viewport.Width / 4, zoldY, GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Width / 6), Color.White * zoldAlpha);
            }
            else
            {
                spriteBatch.Draw(isFullScreenOn ? boxChecked : boxUnchecked, checkBoxRectangle, checkBoxColor);
                spriteBatch.Draw(fscrIcon, resolutionButtonRectangle, Color.White);
                spriteBatch.Draw(backButton, backButtonRectangle, backButtonColor);
            }

        }

        protected void DrawMainGame(GameTime gameTime)
        {
            spriteBatch.Draw(poww, new Rectangle(0, 0, 802, 580), kolorPow);
            spriteBatch.Draw(cyberpunk, new Rectangle(0, 0, 802, 580), kolorPow2);

            //postac 
            spriteBatch.Draw(playerOne.GetTexture(), playerOne.GetPosition(), Color.White);

            ///budunek policji
            spriteBatch.Draw(wallace, new Rectangle(wallacePosX, wallacePosY, wallaceWidth + 50, wallaceHeight + 20), wht);

            //wiezowiec 2
            spriteBatch.Draw(ralf, new Rectangle(ralfX, ralfY, ralfWidth, ralfHeight), kolorPow);

            if (cyber)
            {
                // przeciwnik - DOSKOZZZA
                spriteBatch.Draw(enemy.GetTexture(), enemy.GetPosition(), Color.White);
            }
            if (isPaused)
            {
                spriteBatch.Draw(blank, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White * 0.5f);
                spriteBatch.Draw(quitButton, quitButtonRectangle, quitButtonColor);

            }
        }

        private void UpdateCursorPosition()
        {
            /* Update Cursor position by Mouse */
            mouseState = Mouse.GetState();
            Cursor.X = mouseState.X;
            Cursor.Y = mouseState.Y;
        }

        private void ButtonsEvents()
        {
            if (!inOptions)
            {
                if (playButtonRectangle.Intersects(Cursor))
                {
                    playButtonColor = Color.LightGray;
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        playButtonColor = Color.Gray;
                        isMousePressed = true;

                    }
                    else if (mouseState.LeftButton == ButtonState.Released && isMousePressed)
                    {
                        isMousePressed = false;
                        this.state = gameState.Game;
                    }

                }
                else if (optionsButtonRectangle.Intersects(Cursor))
                {
                    optionsButtonColor = Color.LightGray;
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        optionsButtonColor = Color.Gray;
                        isMousePressed = true;

                    }
                    else if (mouseState.LeftButton == ButtonState.Released && isMousePressed)
                    {
                        isMousePressed = false;
                        inOptions = true;
                    }
                }
                else
                {
                    playButtonColor = Color.White;
                    optionsButtonColor = Color.White;
                }
            }
            else
            {
                if (backButtonRectangle.Intersects(Cursor))
                {
                    backButtonColor = Color.LightGray;
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        backButtonColor = Color.Gray;
                        isMousePressed = true;

                    }
                    else if (mouseState.LeftButton == ButtonState.Released && isMousePressed)
                    {
                        isMousePressed = false;
                        inOptions = false;
                        graphics.ApplyChanges();
                    }

                }
                else if (checkBoxRectangle.Intersects(Cursor))
                {
                    checkBoxColor = Color.LightGray;
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        checkBoxColor = Color.Gray;
                        isMousePressed = true;

                    }
                    else if (mouseState.LeftButton == ButtonState.Released && isMousePressed)
                    {
                        isMousePressed = false;
                        isFullScreenOn = !isFullScreenOn;
                        if (isFullScreenOn)
                        {
                            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                        }
                        else
                        {
                            graphics.PreferredBackBufferWidth = 800;
                            graphics.PreferredBackBufferHeight = 480;
                        }
                    }
                }
                else
                {
                    backButtonColor = Color.White;
                    checkBoxColor = Color.White;
                }
            }
        }

        private void PauseButtonsEvents()
        {
            if (quitButtonRectangle.Intersects(Cursor))
            {
                quitButtonColor = Color.LightGray;
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    isMousePressed = true;
                    quitButtonColor = Color.DarkGray;
                }
                else if (mouseState.LeftButton == ButtonState.Released && isMousePressed)
                {
                    quitButtonColor = Color.Gray;
                    zoldAlpha = 0.0f;
                    zoldDelay = 4;
                    isMenuLoaded = false;
                    isMousePressed = false;
                    isPaused = false;
                    this.state = gameState.Menu;
                }
            }
            else
            {
                quitButtonColor = Color.White;
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
                if ((playerOne.GetPosition().X + scott.Width >= wallacePosX && playerOne.GetPosition().X < wallacePosX + wallaceWidth) && (playerOne.GetPosition().Y + scott.Height >= wallacePosY && playerOne.GetPosition().Y < wallacePosY + wallaceHeight))
                {
                    bacgrund = bacgrundAfterHit;
                    kolorPow = Color.White;
                    kolorPow2 = Color.White * 0;
                    wht = Color.Wheat * 0;

                    playerOne.SetPosition(15, 290);

                    forest = false;
                    city = true;
                    cyber = false;
                }

                if (playerOne.GetPosition().X < 0)
                {
                    kolorPow2 = Color.White;
                    kolorPow = Color.White * 0;
                    wht = Color.White * 0;

                    playerOne.SetPosition(615, playerOne.GetPosition().Y);

                    forest = false;
                    city = false;
                    cyber = true;
                }
            }

            if (city)
            {
                if ((playerOne.GetPosition().X + scott.Width >= ralfX && playerOne.GetPosition().X < ralfX + ralfWidth) && (playerOne.GetPosition().Y + scott.Height >= ralfY && playerOne.GetPosition().Y < ralfY + ralfHeight))
                {
                    // bacgrund = Color.Green;
                    kolorPow2 = Color.White;
                    kolorPow = Color.White * 0;
                    wht = Color.White * 0;

                    pos.X = 100;
                    pos.Y = 320;

                    playerOne.SetPosition(100, 320);

                    forest = false;
                    city = false;
                    cyber = true;


                }

                if (playerOne.GetPosition().X < 0)   /// do lasu
                {
                    kolorPow = Color.Wheat * 0;
                    bacgrund = Color.Green;
                    kolorPow2 = Color.White * 0;
                    wht = Color.White;

                    playerOne.SetPosition(615, playerOne.GetPosition().Y);

                    forest = true;
                    city = false;
                    cyber = false;
                }
            }

            if (cyber)
            {
                if (playerOne.GetPosition().X + scott.Width >= enemy.GetPosition().X
                    && playerOne.GetPosition().Y + scott.Width >= enemy.GetPosition().Y)
                {
                    state = gameState.Combat;
                    if (songStart)
                    {
                        MediaPlayer.Stop();
                        combatMusic.Play();
                        songStart = false;
                    }
                }
            }

            if (playerOne.GetPosition().X + scott.Width >= 800)    /// do lasu
            {
                bacgrund = Color.Green;
                kolorPow2 = Color.White * 0;
                wht = Color.White;
                pos.X = 15;
                //pos.Y = 2;
                playerOne.SetPosition(15, playerOne.GetPosition().Y);

                forest = true;
                city = false;
                cyber = false;
            }

            if (playerOne.GetPosition().X < 0)   /// do miasta
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
}




