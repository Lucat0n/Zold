using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Zold.Screens.Implemented
{
    class SplashScreen : GameScreen
    {
        Texture2D splash;
        TimeSpan LifeTime = new TimeSpan(0, 0, 3);

        public SplashScreen()
        {
            FadeInTime = new TimeSpan(0, 0, 5);
            FadeOutTime = new TimeSpan(0, 0, 5);
            IsTransparent = false;
        }

        public override void Draw(GameTime gameTime)
        {

            gameScreenManager.SpriteBatch.Begin();
            gameScreenManager.SpriteBatch.Draw(splash, new Vector2(gameScreenManager.GraphicsDevice.Viewport.Width / 2 - splash.Width / 2,
                gameScreenManager.GraphicsDevice.Viewport.Height / 2 - splash.Height / 2),
                Color.White);
            gameScreenManager.SpriteBatch.End();
            gameScreenManager.FadeScreen(FadeAlpha);
        }

        public override void Update(GameTime gameTime)
        {
            switch (ScreenState)
            {
                case ScreenState.FadeIn:
                    if (UpdateFade(gameTime, FadeInTime))
                        this.ScreenState = ScreenState.Active;
                    
                    break;
                case ScreenState.Active:
                    LifeTime -= new TimeSpan((long)gameTime.ElapsedGameTime.TotalMilliseconds * 10000);
                    if (LifeTime <= TimeSpan.Zero)
                        this.ScreenState = ScreenState.FadeOut;
                    break;
                case ScreenState.FadeOut:
                    if (UpdateFade(gameTime, FadeOutTime))
                    {
                        gameScreenManager.RemoveScreen(this);
                        gameScreenManager.InsertScreen(new MenuScreen());
                    }
                    break;
            }
        }

        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState) { }

        public override void LoadContent()
        {
            splash = gameScreenManager.Content.Load<Texture2D>("placeholders/rzprod");
        }

        public override void UnloadContent()
        {
            splash.Dispose();
        }
    }
}
