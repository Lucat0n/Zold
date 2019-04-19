using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Zold.Screens.Implemented
{
    class SplashScreen : GameScreen
    {
        Texture2D splash;

        public SplashScreen()
        {
            FadeInTime = new TimeSpan(0, 0, 5);
            FadeOutTime = new TimeSpan(0, 0, 5);
        }

        public override void Draw(GameTime gameTime)
        {
            gameScreenManager.SpriteBatch.Draw(splash, new Vector2(gameScreenManager.GraphicsDevice.Viewport.Width / 2 - splash.Width / 2, gameScreenManager.GraphicsDevice.Viewport.Height / 2 - splash.Height / 2), Color.White * 0.5f);
        }

        public override void HandleInput(MouseState mouseState, Point mousePos, KeyboardState keyboardState)
        {
            throw new NotImplementedException();
        }

        public override void LoadContent()
        {
            splash = gameScreenManager.Content.Load<Texture2D>("placeholders/rzprod");
        }

        public override void UnloadContent()
        {
            throw new NotImplementedException();
        }
    }
}
