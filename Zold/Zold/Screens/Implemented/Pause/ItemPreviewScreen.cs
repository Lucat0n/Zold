using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Zold.Inventory;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Pause
{
    class ItemPreviewScreen : GameScreen
    {
        private Rectangle mainBox;
        private Rectangle imageBox;

        internal ItemPreviewScreen(Item item)
        {

        }
        
        public override void Draw(GameTime gameTime)
        {
            gameScreenManager.SpriteBatch.Begin();
            gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("pause/Textures/secondaryWindow"), mainBox, Color.White);
            gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("screenManager/Textures/blank"), imageBox, Color.White);
            gameScreenManager.SpriteBatch.End();
        }

        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                gameScreenManager.RemoveScreen(this);
            }
        }

        public override void LoadContent()
        {
            mainBox = new Rectangle(gameScreenManager.GraphicsDevice.Viewport.Width / 2 - gameScreenManager.GraphicsDevice.Viewport.Width / 4, gameScreenManager.GraphicsDevice.Viewport.Height / 8, gameScreenManager.GraphicsDevice.Viewport.Width / 2, (int)(gameScreenManager.GraphicsDevice.Viewport.Height * 0.6));
            imageBox = new Rectangle(mainBox.X + mainBox.Height / 20, mainBox.Y + mainBox.Height / 20, mainBox.Height / 2, mainBox.Height / 2);
            //descriptionBox = new Rectangle(gameScreenManager.GraphicsDevice.Viewport.Width / 4, (int)(gameScreenManager.GraphicsDevice.Viewport.Height * 0.8), gameScreenManager.GraphicsDevice.Viewport.Width / 2, gameScreenManager.GraphicsDevice.Viewport.Height / 8);
        }

        public override void UnloadContent()
        {
            //throw new NotImplementedException();
        }
    }
}
