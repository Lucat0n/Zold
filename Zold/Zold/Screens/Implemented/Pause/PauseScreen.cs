using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Zold.Screens.Implemented.Map;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Pause
{
    class PauseScreen : GameScreen
    {
        #region vars
        private bool isDownPressed = false;
        private bool isUpPressed = false;
        private Rectangle cursorPos;
        private Rectangle mainWindow;
        private readonly SpriteFont font;
        private readonly String[] options = new String[]{"Rzeczy", "Itemki", "Zdolnosci", "Mapa", "Opcje"};
        private TimeSpan cooldown;
        private SByte index = 0;
        #endregion

        public PauseScreen()
        {
            IsTransparent = true;
            this.cooldown = new TimeSpan(0, 0, 0, 500);
            font = Assets.Instance.Get("placeholders/Fonts/dialog");
        }
        public override void Draw(GameTime gameTime)
        {
            gameScreenManager.SpriteBatch.Begin();
            gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("pause/Textures/mainWindow"), mainWindow, Color.White);
            for(int i=0; i<options.Count(); i++)
                gameScreenManager.SpriteBatch.DrawString(font, options[i], new Vector2(50 + (int)(mainWindow.Width / 2.5), 50 + mainWindow.Height / 10 + (mainWindow.Height / 6) * i), Color.White);
            gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("pause/Textures/cursor"), cursorPos, Color.White);
            gameScreenManager.SpriteBatch.End();
        }

        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Escape) && this.cooldown <= TimeSpan.Zero)
            {
                gameScreenManager.RemoveScreen(this);
                Assets.Instance.Remove("pause");
            }
            if (keyboardState.IsKeyDown(Keys.Down) && !isDownPressed)
            {
                if(++index>4)
                    index=0;
                isDownPressed = true;
            } else if (keyboardState.IsKeyUp(Keys.Down))
                isDownPressed = false;
            if (keyboardState.IsKeyDown(Keys.Up) && !isUpPressed)
            {
                if (--index<0)
                    index = 4;
                isUpPressed = true;
            }
            else if (keyboardState.IsKeyUp(Keys.Up))
                isUpPressed = false;
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
            cursorPos = new Rectangle(50 + mainWindow.Width / 4, 50 + mainWindow.Height / 7 + (mainWindow.Height/6) * index, mainWindow.Width/12 , mainWindow.Width/12);
        }
    }
}
