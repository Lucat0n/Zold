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
        private enum PauseState
        {
            equipment,
            items,
            perks,
            map,
            options,
            main
        }
        private PauseState pauseState = PauseState.main;
        private bool isDownPressed = false;
        private bool isEnterPressed = false;
        private bool isEscPressed = false;
        private bool isUpPressed = false;
        private Rectangle cursorPos;
        private Rectangle mainWindow;
        private Rectangle secondaryWindow;
        private readonly SpriteFont font;
        private readonly String[] mainOptions = new String[]{"Rzeczy", "Itemki", "Zdolnosci", "Mapa", "Opcje"};
        private readonly String[] options = new String[]{"Pelny ekran", "Glosnosc muzyki", "Glosnosc efektow", "Wyjscie"};
        private TimeSpan cooldown;
        private SByte index = 0;
        private SByte optionsIndex = 0;
        #endregion

        public PauseScreen()
        {
            IsTransparent = true;
            this.cooldown = new TimeSpan(0, 0, 0, 500);
            font = Assets.Instance.Get("placeholders/Fonts/dialog");
        }
        public override void Draw(GameTime gameTime)
        {
            //gameScreenManager.SpriteBatch.Begin();
            gameScreenManager.SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap, null, null, null, null);
            gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("pause/Textures/mainWindow"), mainWindow, Color.White);
            for(int i=0; i<mainOptions.Count(); i++)
                gameScreenManager.SpriteBatch.DrawString(font, mainOptions[i], new Vector2(50 + (mainWindow.Width / 2.75f), 50 + mainWindow.Height/15 + (mainWindow.Height / 6) * i), Color.White, 0, Vector2.Zero, new Vector2(mainWindow.Height/110, mainWindow.Height / 110), SpriteEffects.None, 1f);
            //gameScreenManager.SpriteBatch.DrawString(font, mainOptions[i], new Vector2(50 + (int)(mainWindow.Width / 2.5), 50 + mainWindow.Height / 10 + (mainWindow.Height / 6) * i), Color.White);
            gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("pause/Textures/cursor"), cursorPos, Color.White);
            switch (pauseState)
            {
                case (PauseState.equipment):
                    gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("pause/Textures/secondaryWindow"), secondaryWindow, Color.White);
                    break;
                case (PauseState.items):
                    gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("pause/Textures/secondaryWindow"), secondaryWindow, Color.White);
                    break;
                case (PauseState.perks):
                    gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("pause/Textures/secondaryWindow"), secondaryWindow, Color.White);
                    break;
                case (PauseState.map):
                    gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("pause/Textures/secondaryWindow"), secondaryWindow, Color.White);
                    break;
                case (PauseState.options):
                    gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("pause/Textures/secondaryWindow"), secondaryWindow, Color.White);
                    for (int i = 0; i < options.Count(); i++)
                        gameScreenManager.SpriteBatch.DrawString(font, options[i], new Vector2(secondaryWindow.X + (secondaryWindow.Width / 6), 50 + secondaryWindow.Height / 18 + (secondaryWindow.Height / 4) * i), Color.White, 0, Vector2.Zero, new Vector2(mainWindow.Height / 90f, mainWindow.Height / 90f), SpriteEffects.None, 1f);
                    gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("pause/Textures/cursor"), cursorPos, Color.White);
                    break;
            }
            gameScreenManager.SpriteBatch.End();
        }

        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState)
        {
            switch (pauseState)
            {
                case (PauseState.main):
                    if (keyboardState.IsKeyDown(Keys.Escape) && this.cooldown <= TimeSpan.Zero && !isEscPressed)
                    {
                        gameScreenManager.RemoveScreen(this);
                        Assets.Instance.Remove("pause");
                    }
                    else if (keyboardState.IsKeyUp(Keys.Escape))
                        isEscPressed = false;
                    if (keyboardState.IsKeyDown(Keys.Down) && !isDownPressed)
                    {
                        if (++index > 4)
                            index = 0;
                        isDownPressed = true;
                    }
                    else if (keyboardState.IsKeyUp(Keys.Down))
                        isDownPressed = false;
                    if (keyboardState.IsKeyDown(Keys.Up) && !isUpPressed)
                    {
                        if (--index < 0)
                            index = 4;
                        isUpPressed = true;
                    }
                    else if (keyboardState.IsKeyUp(Keys.Up))
                        isUpPressed = false;
                    if (keyboardState.IsKeyDown(Keys.Enter) && !isEnterPressed)
                    {
                        pauseState = (PauseState)index;
                        isEnterPressed = true;
                    }
                    else if (keyboardState.IsKeyUp(Keys.Enter))
                        isEnterPressed = false;
                    break;
                case (PauseState.equipment):
                    if (keyboardState.IsKeyDown(Keys.Escape))
                    {
                        isEscPressed = true;
                        pauseState = PauseState.main;
                    }
                    break;
                case (PauseState.items):
                    if (keyboardState.IsKeyDown(Keys.Escape))
                    {
                        isEscPressed = true;
                        pauseState = PauseState.main;
                    }
                    break;
                case (PauseState.perks):
                    if (keyboardState.IsKeyDown(Keys.Escape))
                    {
                        isEscPressed = true;
                        pauseState = PauseState.main;
                    }
                    break;
                case (PauseState.map):
                    if (keyboardState.IsKeyDown(Keys.Escape))
                    {
                        isEscPressed = true;
                        pauseState = PauseState.main;
                    }
                    break;
                case (PauseState.options):
                    if (keyboardState.IsKeyDown(Keys.Escape))
                    {
                        isEscPressed = true;
                        pauseState = PauseState.main;
                    }
                    if (keyboardState.IsKeyDown(Keys.Down) && !isDownPressed)
                    {
                        if (++optionsIndex > 3)
                            optionsIndex = 0;
                        isDownPressed = true;
                    }
                    else if (keyboardState.IsKeyUp(Keys.Down))
                        isDownPressed = false;
                    if (keyboardState.IsKeyDown(Keys.Up) && !isUpPressed)
                    {
                        if (--optionsIndex < 0)
                            optionsIndex = 3;
                        isUpPressed = true;
                    }
                    else if (keyboardState.IsKeyUp(Keys.Up))
                        isUpPressed = false;
                    if (keyboardState.IsKeyDown(Keys.Enter) && !isEnterPressed)
                    {
                        switch (optionsIndex)
                        {
                            case (3):
                                gameScreenManager.RemoveScreen(this);
                                gameScreenManager.GoToMenu();
                                break;
                        }
                        isEnterPressed = true;
                    }
                    else if (keyboardState.IsKeyUp(Keys.Enter))
                        isEnterPressed = false;
                    break;

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
            switch (pauseState)
            {
                case (PauseState.main):
                    cursorPos = new Rectangle(50 + mainWindow.Width / 4, 50 + mainWindow.Height / 9 + (mainWindow.Height / 6) * index, mainWindow.Width / 12, mainWindow.Width / 12);
                    break;
                case (PauseState.equipment):
                    secondaryWindow = new Rectangle(80 + mainWindow.Width, 50, gameScreenManager.GraphicsDevice.Viewport.Width / 2, gameScreenManager.GraphicsDevice.Viewport.Height / 2);
                    break;
                case (PauseState.items):
                    secondaryWindow = new Rectangle(80 + mainWindow.Width, 50, gameScreenManager.GraphicsDevice.Viewport.Width / 2, gameScreenManager.GraphicsDevice.Viewport.Height / 2);
                    break;
                case (PauseState.perks):
                    secondaryWindow = new Rectangle(80 + mainWindow.Width, 50, gameScreenManager.GraphicsDevice.Viewport.Width / 2, gameScreenManager.GraphicsDevice.Viewport.Height / 2);
                    break;
                case (PauseState.map):
                    secondaryWindow = new Rectangle(80 + mainWindow.Width, 50, gameScreenManager.GraphicsDevice.Viewport.Width / 2, gameScreenManager.GraphicsDevice.Viewport.Height / 2);
                    break;
                case (PauseState.options):
                    cursorPos = new Rectangle(secondaryWindow.X + (secondaryWindow.Width / 10), 50 + (int)(secondaryWindow.Height / 8f) + (secondaryWindow.Height / 4) * optionsIndex, mainWindow.Width / 12, mainWindow.Width / 12);
                    secondaryWindow = new Rectangle(80 + mainWindow.Width, 50, gameScreenManager.GraphicsDevice.Viewport.Width / 2, gameScreenManager.GraphicsDevice.Viewport.Height / 2);
                    break;
            }
                
        }
    }
}
