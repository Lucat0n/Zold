using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Zold.Screens.Implemented.Map;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Pause
{
    class PauseScreen : GameScreen
    {
        private TimeSpan cooldown;
        Rectangle mainWindow;

        public PauseScreen()
        {
            IsTransparent = true;
            this.cooldown = new TimeSpan(0, 0, 0, 500);
        }
        public override void Draw(GameTime gameTime)
        {
            gameScreenManager.SpriteBatch.Begin();
            gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("pause/Textures/mainWindow"), mainWindow, Color.White);
            gameScreenManager.SpriteBatch.End();
        }

        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Escape) && this.cooldown <= TimeSpan.Zero)
            {
                gameScreenManager.RemoveScreen(this);
                Assets.Instance.Remove("pause");
            }
        }

        public override void LoadContent()
        {
            gameScreenManager.LoadAssets("pause");
        }

        public override void UnloadContent()
        {
            Assets.Instance.Remove("pause");
        }

        public override void Update(GameTime gameTime)
        {
            if (this.cooldown>TimeSpan.Zero)
                this.cooldown -= new TimeSpan(0, 0, 0, gameTime.ElapsedGameTime.Milliseconds);

            mainWindow = new Rectangle(50, 50, gameScreenManager.GraphicsDevice.Viewport.Height / 3, gameScreenManager.GraphicsDevice.Viewport.Height / 3);
        }
    }
}
