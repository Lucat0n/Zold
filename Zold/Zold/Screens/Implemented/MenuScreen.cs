using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Zold.Screens.Implemented
{
    class MenuScreen : GameScreen
    {
        private enum MenuState
        {
            DrawLogo,
            MoveLogo,
            Main,
            Play,
            LoadGame,
            Options
        }
        private bool isMousePressed = false;
        private Color backButtonColor = Color.White;
        private Color checkBoxColor = Color.White;
        private Color playButtonColor = Color.White;
        private Color optionsButtonColor = Color.White;
        private Color[] colors = { Color.White, Color.White, Color.White, Color.White }; // 1-playButton, 2-optionsButton, 3-backButtonColor
        private int titleY;
        private Rectangle backButtonRectangle;
        private Rectangle checkBoxRectangle;
        private Rectangle playButtonRectangle;
        private Rectangle resolutionButtonRectangle;
        private Rectangle optionsButtonRectangle;
        private Texture2D backButton;
        private Texture2D boxChecked;
        private Texture2D boxUnchecked;
        private Texture2D fscrIcon;
        private Texture2D playButton;
        private Texture2D optionsButton;
        private Texture2D title;
        private MenuState menuState = MenuState.DrawLogo;

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
        }
        #endregion*/

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
                    break;
                case MenuState.Play:
                    gameScreenManager.RemoveScreen(this);
                    gameScreenManager.InsertScreen(new Map.MapManager());
                    break;

            }
            gameScreenManager.SpriteBatch.End();
            if(menuState == MenuState.DrawLogo)
                gameScreenManager.FadeScreen(FadeAlpha);
        }

        public override void Update(GameTime gameTime)
        {
            switch (menuState)
            {
                case MenuState.DrawLogo:
                    if (UpdateFade(gameTime, FadeInTime))
                    {
                        ScreenState = ScreenState.Active;
                        menuState = MenuState.MoveLogo;
                    }
                    break;
                case MenuState.MoveLogo:
                    if (titleY > gameScreenManager.GraphicsDevice.Viewport.Width / 20)
                        titleY--;
                    else
                        menuState = MenuState.Main;
                    break;
                case MenuState.Main:
                    HandleInput(gameScreenManager.MouseState, gameScreenManager.Cursor, gameScreenManager.KeyboardState);
                    playButtonRectangle = new Rectangle(gameScreenManager.GraphicsDevice.Viewport.Width / 2 - gameScreenManager.GraphicsDevice.Viewport.Width / 8, gameScreenManager.GraphicsDevice.Viewport.Width / 2 - gameScreenManager.GraphicsDevice.Viewport.Width / 4, gameScreenManager.GraphicsDevice.Viewport.Width / 4, gameScreenManager.GraphicsDevice.Viewport.Width / 10);
                    optionsButtonRectangle = new Rectangle(gameScreenManager.GraphicsDevice.Viewport.Width / 2 - gameScreenManager.GraphicsDevice.Viewport.Width / 8, gameScreenManager.GraphicsDevice.Viewport.Width / 2 - gameScreenManager.GraphicsDevice.Viewport.Width / 8, gameScreenManager.GraphicsDevice.Viewport.Width / 4, gameScreenManager.GraphicsDevice.Viewport.Width / 10);
                    break;
                case MenuState.Options:
                    HandleInput(gameScreenManager.MouseState, gameScreenManager.Cursor, gameScreenManager.KeyboardState);
                    checkBoxRectangle = new Rectangle(gameScreenManager.GraphicsDevice.Viewport.Width / 2 + gameScreenManager.GraphicsDevice.Viewport.Width / 8, gameScreenManager.GraphicsDevice.Viewport.Width / 2 - gameScreenManager.GraphicsDevice.Viewport.Width / 4, gameScreenManager.GraphicsDevice.Viewport.Width / 12, gameScreenManager.GraphicsDevice.Viewport.Width / 12);
                    resolutionButtonRectangle = new Rectangle(gameScreenManager.GraphicsDevice.Viewport.Width / 2 - gameScreenManager.GraphicsDevice.Viewport.Width / 16, gameScreenManager.GraphicsDevice.Viewport.Width / 2 - gameScreenManager.GraphicsDevice.Viewport.Width / 4, gameScreenManager.GraphicsDevice.Viewport.Width / 8, gameScreenManager.GraphicsDevice.Viewport.Width / 8);
                    backButtonRectangle = new Rectangle(gameScreenManager.GraphicsDevice.Viewport.Width / 2 - gameScreenManager.GraphicsDevice.Viewport.Width / 4, gameScreenManager.GraphicsDevice.Viewport.Width / 2 - gameScreenManager.GraphicsDevice.Viewport.Width / 4, gameScreenManager.GraphicsDevice.Viewport.Width / 12, gameScreenManager.GraphicsDevice.Viewport.Width / 12);
                    break;
            }
        }

        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState)
        {
            ButtonsEvents(mouseState, mousePos);
        }

        public override void LoadContent()
        {
            titleY = this.gameScreenManager.GraphicsDevice.Viewport.Height / 3;
            title = gameScreenManager.Content.Load<Texture2D>("placeholders/zold");
            backButton = gameScreenManager.Content.Load<Texture2D>("placeholders/backButton");
            boxChecked = gameScreenManager.Content.Load<Texture2D>("placeholders/boxChecked");
            boxUnchecked = gameScreenManager.Content.Load<Texture2D>("placeholders/boxUnchecked");
            fscrIcon = gameScreenManager.Content.Load<Texture2D>("placeholders/fscrIcon");
            playButton = gameScreenManager.Content.Load<Texture2D>("placeholders/playButton");
            optionsButton = gameScreenManager.Content.Load<Texture2D>("placeholders/optionsButton");

        }

        public override void UnloadContent()
        {
            title.Dispose();
            playButton.Dispose();
            optionsButton.Dispose();
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
                    /*if (playButtonRectangle.Intersects(Cursor))
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
                            this.menuState = MenuState.Play;
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
                            this.menuState = MenuState.Options;
                        }
                    }
                    else
                    {
                        playButtonColor = Color.White;
                        optionsButtonColor = Color.White;
                    }*/
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
                    break;
            }

        }
    }
}
