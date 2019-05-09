using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Zold.Utilities;

namespace Zold.Screens.Implemented
{
    class MenuScreen : GameScreen
    {
        #region variables
        private enum MenuState
        {
            DrawLogo,
            MoveLogo,
            Main,
            Play,
            LoadGame,
            Options
        }
        private bool countDownComplete = false;
        private bool[] isCountDownActive = { false, false };
        private bool isMousePressed = false;
        private byte masterVolume;
        private Color backButtonColor = Color.White;
        private Color checkBoxColor = Color.White;
        private Color leftArrowColor = Color.White;
        private Color playButtonColor = Color.White;
        private Color rightArrowColor = Color.White;
        private Color optionsButtonColor = Color.White;
        private Color[] colors = { Color.White, Color.White, Color.White, Color.White }; // 1-playButton, 2-optionsButton, 3-backButtonColor
        private int titleY;
        private Rectangle backButtonRectangle;
        private Rectangle checkBoxRectangle;
        private Rectangle leftArrowButtonRectangle;
        private Rectangle playButtonRectangle;
        private Rectangle resolutionButtonRectangle;
        private Rectangle rightArrowButtonRectangle;
        private Rectangle optionsButtonRectangle;
        private SpriteFont font;
        private Texture2D backButton;
        private Texture2D boxChecked;
        private Texture2D boxUnchecked;
        private Texture2D fscrIcon;
        private Texture2D leftArrow;
        private Texture2D playButton;
        private Texture2D rightArrow;
        private Texture2D optionsButton;
        private Texture2D title;
        private TimeSpan buttonBlock;

        private MenuState menuState = MenuState.DrawLogo;
        //music
        private SoundEffect bgMusic;
        private SoundEffectInstance bg;
        private bool songStart = false;

        /*#region properties
        public Color PlayButtonColor
        {
            get { return playButtonColor; }
            set { playButtonColor = value; }
        }
        public Color OptionsButtonColor
        {
            get { return optionsButtonColor; }
            set { optionsButtonColor = value; }
        }*/
        #endregion

        public MenuScreen()
        {
            IsTransparent = false;
            FadeInTime = new TimeSpan(0, 0, 3);
        }

        public override void Draw(GameTime gameTime)
        {
            gameScreenManager.GraphicsDevice.Clear(Color.Black);
            gameScreenManager.SpriteBatch.Begin();
            switch (menuState)
            {
                case MenuState.DrawLogo:
                    gameScreenManager.SpriteBatch.Draw(title, new Rectangle(gameScreenManager.GraphicsDevice.Viewport.Width / 4, titleY, gameScreenManager.GraphicsDevice.Viewport.Width / 2, gameScreenManager.GraphicsDevice.Viewport.Width / 6), Color.White);
                    break;
                case MenuState.MoveLogo:
                    gameScreenManager.SpriteBatch.Draw(title, new Rectangle(gameScreenManager.GraphicsDevice.Viewport.Width / 4, titleY, gameScreenManager.GraphicsDevice.Viewport.Width / 2, gameScreenManager.GraphicsDevice.Viewport.Width / 6), Color.White);
                    break;
                case MenuState.Main:
                    gameScreenManager.SpriteBatch.Draw(title, new Rectangle(gameScreenManager.GraphicsDevice.Viewport.Width / 4, titleY, gameScreenManager.GraphicsDevice.Viewport.Width / 2, gameScreenManager.GraphicsDevice.Viewport.Width / 6), Color.White);
                    gameScreenManager.SpriteBatch.Draw(playButton, playButtonRectangle, colors[0]);
                    gameScreenManager.SpriteBatch.Draw(optionsButton, optionsButtonRectangle, colors[1]);
                    break;
                case MenuState.Options:
                    gameScreenManager.SpriteBatch.Draw(gameScreenManager.IsFullScreenOn ? boxChecked : boxUnchecked, checkBoxRectangle, checkBoxColor);
                    gameScreenManager.SpriteBatch.Draw(fscrIcon, resolutionButtonRectangle, Color.White);
                    gameScreenManager.SpriteBatch.Draw(backButton, backButtonRectangle, backButtonColor);
                    gameScreenManager.SpriteBatch.Draw(leftArrow, leftArrowButtonRectangle, leftArrowColor);
                    gameScreenManager.SpriteBatch.Draw(rightArrow, rightArrowButtonRectangle, rightArrowColor);
                    gameScreenManager.SpriteBatch.DrawString(font, masterVolume.ToString(), new Vector2(gameScreenManager.GraphicsDevice.Viewport.Width / 2 - masterVolume.ToString().Length * font.LineSpacing/2.5f, gameScreenManager.GraphicsDevice.Viewport.Height/2), Color.White, 0, Vector2.Zero, new Vector2(gameScreenManager.GraphicsDevice.Viewport.Height / 200, gameScreenManager.GraphicsDevice.Viewport.Height / 200), SpriteEffects.None, 1f); ;
                    break;
               

            }
            gameScreenManager.SpriteBatch.End();
            if(menuState == MenuState.DrawLogo)
                gameScreenManager.FadeScreen(FadeAlpha);
        }

        public override void Update(GameTime gameTime)
        {
            if (!songStart)
            {
                bg = bgMusic.CreateInstance();
                bg.Volume = gameScreenManager.MasterVolume;
                bg.Play();
                songStart = true;
            }

            switch (menuState)
            {
                case MenuState.DrawLogo:
                    SkipIntro();
                    if (UpdateFade(gameTime, FadeInTime))
                    {
                        ScreenState = ScreenState.Active;
                        menuState = MenuState.MoveLogo;
                    }
                    break;
                case MenuState.MoveLogo:
                    SkipIntro();
                    if (titleY > gameScreenManager.GraphicsDevice.Viewport.Width / 20)
                        titleY--;
                    else
                        menuState = MenuState.Main;
                    break;
                case MenuState.Main:
                    titleY = gameScreenManager.GraphicsDevice.Viewport.Width / 20;
                    HandleInput(gameScreenManager.MouseState, gameScreenManager.Cursor, gameScreenManager.KeyboardState);
                    playButtonRectangle = new Rectangle(gameScreenManager.GraphicsDevice.Viewport.Width / 2 - gameScreenManager.GraphicsDevice.Viewport.Width / 8, gameScreenManager.GraphicsDevice.Viewport.Height / 2 - gameScreenManager.GraphicsDevice.Viewport.Height/16, gameScreenManager.GraphicsDevice.Viewport.Width / 4, gameScreenManager.GraphicsDevice.Viewport.Width / 10);
                    optionsButtonRectangle = new Rectangle(gameScreenManager.GraphicsDevice.Viewport.Width / 2 - gameScreenManager.GraphicsDevice.Viewport.Width / 8, gameScreenManager.GraphicsDevice.Viewport.Height / 2 + gameScreenManager.GraphicsDevice.Viewport.Height / 6, gameScreenManager.GraphicsDevice.Viewport.Width / 4, gameScreenManager.GraphicsDevice.Viewport.Width / 10);
                    break;
                case MenuState.Options:
                    HandleInput(gameScreenManager.MouseState, gameScreenManager.Cursor, gameScreenManager.KeyboardState);
                    checkBoxRectangle = new Rectangle(gameScreenManager.GraphicsDevice.Viewport.Width / 2 + gameScreenManager.GraphicsDevice.Viewport.Width / 8, gameScreenManager.GraphicsDevice.Viewport.Height / 2 - gameScreenManager.GraphicsDevice.Viewport.Height / 4, gameScreenManager.GraphicsDevice.Viewport.Width / 16, gameScreenManager.GraphicsDevice.Viewport.Width / 16);
                    resolutionButtonRectangle = new Rectangle(gameScreenManager.GraphicsDevice.Viewport.Width / 2 - gameScreenManager.GraphicsDevice.Viewport.Width / 24, gameScreenManager.GraphicsDevice.Viewport.Height / 2 - gameScreenManager.GraphicsDevice.Viewport.Height / 4, gameScreenManager.GraphicsDevice.Viewport.Width / 12, gameScreenManager.GraphicsDevice.Viewport.Width / 12);
                    backButtonRectangle = new Rectangle(gameScreenManager.GraphicsDevice.Viewport.Width / 8, gameScreenManager.GraphicsDevice.Viewport.Width / 4, gameScreenManager.GraphicsDevice.Viewport.Width / 12, gameScreenManager.GraphicsDevice.Viewport.Width / 12);
                    leftArrowButtonRectangle = new Rectangle(7 * gameScreenManager.GraphicsDevice.Viewport.Width / 16 - gameScreenManager.GraphicsDevice.Viewport.Width / 24, gameScreenManager.GraphicsDevice.Viewport.Height / 2, gameScreenManager.GraphicsDevice.Viewport.Width / 24, gameScreenManager.GraphicsDevice.Viewport.Height / 12);
                    rightArrowButtonRectangle = new Rectangle(gameScreenManager.GraphicsDevice.Viewport.Width / 2 + gameScreenManager.GraphicsDevice.Viewport.Width / 16, gameScreenManager.GraphicsDevice.Viewport.Height / 2, gameScreenManager.GraphicsDevice.Viewport.Width / 24, gameScreenManager.GraphicsDevice.Viewport.Height / 12);
                    if (isCountDownActive.Contains(true)) CountDown(gameTime);
                    break;
                case MenuState.Play:
                    bg.Dispose();
                    gameScreenManager.RemoveScreen(this);
                    gameScreenManager.InsertScreen(new Map.MapManager());
                    break;
            }
        }

        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState)
        {
            ButtonsEvents(mouseState, mousePos);
        }

        public override void LoadContent()
        {
            gameScreenManager.LoadAssets("menu");
            titleY = this.gameScreenManager.GraphicsDevice.Viewport.Height / 3;
            title = Assets.Instance.Get("menu/Textures/zold");
            backButton = Assets.Instance.Get("menu/Textures/backButton");
            boxChecked = Assets.Instance.Get("menu/Textures/boxChecked");
            boxUnchecked = Assets.Instance.Get("menu/Textures/boxUnchecked");
            fscrIcon = Assets.Instance.Get("menu/Textures/fscrIcon");
            leftArrow = Assets.Instance.Get("menu/Textures/leftArrow");
            playButton = Assets.Instance.Get("menu/Textures/playButton");
            rightArrow = Assets.Instance.Get("menu/Textures/rightArrow");
            optionsButton = Assets.Instance.Get("menu/Textures/optionsButton");
            font = Assets.Instance.Get("menu/Fonts/dialog");
            bgMusic = Assets.Instance.Get("menu/Music/menu-music");
            bg = bgMusic.CreateInstance();
            buttonBlock = new TimeSpan(0, 0, 0, 0, 750);
            masterVolume = (byte)(gameScreenManager.MasterVolume * 100);
        }

        public override void UnloadContent()
        {
            title.Dispose();
            backButton.Dispose();
            boxChecked.Dispose();
            boxUnchecked.Dispose();
            fscrIcon.Dispose();
            playButton.Dispose();
            optionsButton.Dispose();
            bg.Dispose();
            bgMusic.Dispose();
            Assets.Instance.Remove("menu");
        }

        private void CheckInteraction(int id, Rectangle buttonRectangle, Rectangle cursor, MenuState targetState, MouseState mouseState)
        {
            if (buttonRectangle.Intersects(cursor))
            {
                colors[id] = Color.LightGray;
                if(mouseState.LeftButton == ButtonState.Pressed)
                {
                    colors[id] = Color.Gray;
                    isMousePressed = true;
                }
                else if(mouseState.LeftButton == ButtonState.Released && isMousePressed)
                {
                    isMousePressed = false;
                    this.menuState = targetState;
                }
            }
            else
            {
                colors[id] = Color.White;
            }
        }

        private void ButtonsEvents(MouseState mouseState, Rectangle Cursor)
        {
            switch (menuState)
            {
                case MenuState.Main:
                    CheckInteraction(0, playButtonRectangle, Cursor, MenuState.Play, mouseState);
                    CheckInteraction(1, optionsButtonRectangle, Cursor, MenuState.Options, mouseState);
                    break;
                case MenuState.Options:
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
                            this.menuState = MenuState.Main;
                            gameScreenManager.Graphics.ApplyChanges();
                        }

                    }
                    else
                    {
                        backButtonColor = Color.White;
                    }
                    if (checkBoxRectangle.Intersects(Cursor))
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
                            gameScreenManager.IsFullScreenOn = !gameScreenManager.IsFullScreenOn;
                            
                            if (gameScreenManager.IsFullScreenOn)
                            {
                                gameScreenManager.Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                                gameScreenManager.Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                            }
                            else
                            {
                                gameScreenManager.Graphics.PreferredBackBufferWidth = 800;
                                gameScreenManager.Graphics.PreferredBackBufferHeight = 480;
                            }
                        }
                    }
                    else
                    {
                        checkBoxColor = Color.White;
                    }
                    if (leftArrowButtonRectangle.Intersects(Cursor))
                    {
                        leftArrowColor = Color.LightGray;
                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            if (!isCountDownActive[0])
                            {
                                isCountDownActive[0] = true;
                                if (masterVolume > byte.MinValue)
                                    masterVolume--;
                            }
                            leftArrowColor = Color.Gray;
                            if (countDownComplete && masterVolume > byte.MinValue)
                                masterVolume--;
                            isMousePressed = true;

                        }
                        else if (mouseState.LeftButton == ButtonState.Released && isMousePressed)
                        {
                            isMousePressed = false;
                            if (isCountDownActive[0])
                            {
                                isCountDownActive[0] = false;
                                countDownComplete = false;
                                buttonBlock = new TimeSpan(0, 0, 0, 0, 750);
                            }
                        }
                        bg.Volume = gameScreenManager.MasterVolume = 0.01f * masterVolume;
                    }
                    else if(!leftArrowButtonRectangle.Intersects(Cursor))
                        leftArrowColor = Color.White;
                    if (rightArrowButtonRectangle.Intersects(Cursor))
                    {
                        rightArrowColor = Color.LightGray;
                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            if (!isCountDownActive[1])
                            {
                                isCountDownActive[1] = true;
                                if (masterVolume < 100)
                                    masterVolume++;
                            }
                            
                            rightArrowColor = Color.Gray;
                            if (countDownComplete && masterVolume < 100)
                                masterVolume++;
                            isMousePressed = true;

                        }
                        else if (mouseState.LeftButton == ButtonState.Released && isMousePressed)
                        {
                            isMousePressed = false;
                            if (isCountDownActive[1])
                            {
                                isCountDownActive[1] = false;
                                countDownComplete = false;
                                buttonBlock = new TimeSpan(0, 0, 0, 0, 750);
                            }

                        }
                        bg.Volume = gameScreenManager.MasterVolume = 0.01f * masterVolume;
                    }
                    else if(!rightArrowButtonRectangle.Intersects(Cursor))
                        rightArrowColor = Color.White;
                    break;
            }

        }

        private void CountDown(GameTime gameTime)
        {
            buttonBlock -= new TimeSpan(0,0,0,0,gameTime.ElapsedGameTime.Milliseconds);
            if (buttonBlock <= TimeSpan.Zero)
                countDownComplete = true;
            else
                countDownComplete = false;
        }

        private void SkipIntro()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && (menuState == MenuState.DrawLogo || menuState == MenuState.MoveLogo))
            {
                titleY = gameScreenManager.GraphicsDevice.Viewport.Width / 20;
                menuState = MenuState.Main;
            }
        }
    }
}
