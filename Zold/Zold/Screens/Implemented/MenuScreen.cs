using System;
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
        private Color playButtonColor = Color.White;
        private Color optionsButtonColor = Color.White;
        private int titleY;
        private Rectangle playButtonRectangle;
        private Rectangle optionsButtonRectangle;
        private Texture2D playButton;
        private Texture2D optionsButton;
        private Texture2D title;
        private MenuState menuState = MenuState.DrawLogo;

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
                    gameScreenManager.SpriteBatch.Draw(playButton, playButtonRectangle, playButtonColor);
                    gameScreenManager.SpriteBatch.Draw(optionsButton, optionsButtonRectangle, optionsButtonColor);
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
            playButton = gameScreenManager.Content.Load<Texture2D>("placeholders/playButton");
            optionsButton = gameScreenManager.Content.Load<Texture2D>("placeholders/optionsButton");

        }

        public override void UnloadContent()
        {
            title.Dispose();
            playButton.Dispose();
            optionsButton.Dispose();
        }

        private void ButtonsEvents(MouseState mouseState, Rectangle Cursor)
        {
            switch (menuState)
            {
                case MenuState.Main:
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
                    }
                    break;
                case MenuState.Options:

                    break;
            }

        }
    }
}
