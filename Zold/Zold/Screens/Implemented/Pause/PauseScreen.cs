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
            quests,
            map,
            options,
            main
        }
        private PauseState pauseState = PauseState.main;
        private bool isAdjustingMusic = false;
        private bool[] isCountDownActive = { false, false };
        private bool isCountDownComplete = false;
        private bool isDownPressed = false;
        private bool isEnterPressed = false;
        private bool isEscPressed = false;
        private bool isLeftPressed = false;
        private bool isRightPressed = false;
        private bool isUpPressed = false;
        private byte masterVolume;
        private Rectangle cursorPos;
        private Rectangle mainWindow;
        private Rectangle secondaryWindow;
        private readonly SpriteFont font;
        private readonly String[] mainOptions = new String[]{"Ekwip.", "Rzeczy", "Zdolnosci", "Zadania", "Mapa", "Opcje"};
        private readonly String[] options = new String[]{"Pelny ekran", "Glosnosc muzyki", "Glosnosc efektow", "Wyjscie"};
        private TimeSpan cooldown;
        private TimeSpan buttonBlock;
        private SByte index = 0;
        private SByte optionsIndex = 0;
        private SByte questIndex = 0;
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
                gameScreenManager.SpriteBatch.DrawString(font, mainOptions[i], new Vector2(50 + (mainWindow.Width / 2.75f), 50 + mainWindow.Height/15 + (mainWindow.Height / 7) * i), Color.White, 0, Vector2.Zero, new Vector2(/*mainWindow.Height/110, mainWindow.Height / 110*/mainWindow.Height * 0.006f, mainWindow.Height * 0.006f), SpriteEffects.None, 1f);
            //gameScreenManager.SpriteBatch.DrawString(font, mainOptions[i], new Vector2(50 + (int)(mainWindow.Width / 2.5), 50 + mainWindow.Height / 10 + (mainWindow.Height / 6) * i), Color.White);
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
                case (PauseState.quests):
                    gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("pause/Textures/mainWindow"), secondaryWindow, Color.White);
                    for (int i = 0; i < Math.Min(10, gameScreenManager.QuestManager.ActiveQuests.Count); i++)
                        gameScreenManager.SpriteBatch.DrawString(font, gameScreenManager.QuestManager.GetActiveQuestName(i, false), new Vector2(secondaryWindow.X + (secondaryWindow.Height/3), 50 + secondaryWindow.Height / 18 + (secondaryWindow.Height / 10) * i), Color.White, 0, Vector2.Zero, new Vector2(secondaryWindow.Height * 0.006f, secondaryWindow.Height * 0.006f), SpriteEffects.None, 1f);
                    break;
                case (PauseState.map):
                    gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("pause/Textures/secondaryWindow"), secondaryWindow, Color.White);
                    break;
                case (PauseState.options):
                    gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("pause/Textures/secondaryWindow"), secondaryWindow, Color.White);
                    for (int i = 0; i < options.Count(); i++)
                    {
                        gameScreenManager.SpriteBatch.DrawString(font, options[i], new Vector2(secondaryWindow.X + (secondaryWindow.Width / 6), 50 + secondaryWindow.Height / 18 + (secondaryWindow.Height / 4) * i), Color.White, 0, Vector2.Zero, new Vector2(mainWindow.Height * 0.008f, mainWindow.Height * 0.008f), SpriteEffects.None, 1f);
                        if (i == 1)
                        {
                            gameScreenManager.SpriteBatch.DrawString(font, masterVolume.ToString(), new Vector2(secondaryWindow.X + secondaryWindow.Width - (secondaryWindow.Width / 5), 50 + secondaryWindow.Height / 18 + (secondaryWindow.Height / 4) * i), Color.White, 0, Vector2.Zero, new Vector2(mainWindow.Height * 0.008f, mainWindow.Height * 0.008f), SpriteEffects.None, 1f);
                        }
                    }
                        
                    if(!isAdjustingMusic)
                        gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("pause/Textures/cursor"), cursorPos, Color.White);
                    else
                        gameScreenManager.SpriteBatch.DrawString(font, new string('_', masterVolume.ToString().Length), new Vector2(secondaryWindow.X + secondaryWindow.Width - (secondaryWindow.Width / 5), 50 + secondaryWindow.Height / 18 + (secondaryWindow.Height / 4)), Color.White, 0, Vector2.Zero, new Vector2(mainWindow.Height / 90f, mainWindow.Height / 90f), SpriteEffects.None, 1f);

                    break;
            }
            gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("pause/Textures/cursor"), cursorPos, Color.White);
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
                        if (++index > mainOptions.Count()-1)
                            index = 0;
                        isDownPressed = true;
                    }
                    else if (keyboardState.IsKeyUp(Keys.Down))
                        isDownPressed = false;
                    if (keyboardState.IsKeyDown(Keys.Up) && !isUpPressed)
                    {
                        if (--index < 0)
                            index = (sbyte)(mainOptions.Count()-1);
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
                case (PauseState.quests):
                    if (keyboardState.IsKeyDown(Keys.Escape) && !isEscPressed)
                    {
                        isEscPressed = true;
                        pauseState = PauseState.main;
                    }else if (keyboardState.IsKeyUp(Keys.Escape))
                    {
                        isEscPressed = false;
                    }
                    if (keyboardState.IsKeyDown(Keys.Down) && !isDownPressed)
                    {
                        if (++questIndex > Math.Min(10, gameScreenManager.QuestManager.ActiveQuests.Count-1))
                            questIndex = 0;
                        isDownPressed = true;
                    }
                    else if (keyboardState.IsKeyUp(Keys.Down))
                        isDownPressed = false;
                    if (keyboardState.IsKeyDown(Keys.Up) && !isUpPressed)
                    {
                        if (--questIndex < 0)
                            questIndex = (sbyte)Math.Min(10, gameScreenManager.QuestManager.ActiveQuests.Count-1);
                        isUpPressed = true;
                    }
                    else if (keyboardState.IsKeyUp(Keys.Up))
                        isUpPressed = false;
                    if (keyboardState.IsKeyDown(Keys.Enter) && !isEnterPressed)
                    {
                        gameScreenManager.InsertScreen(new QuestDescriptionScreen(questIndex));
                        isEscPressed = true;
                        isEnterPressed = true;
                    }
                    else if (keyboardState.IsKeyUp(Keys.Enter))
                        isEnterPressed = false;
                    break;
                case (PauseState.map):
                    if (keyboardState.IsKeyDown(Keys.Escape))
                    {
                        isEscPressed = true;
                        pauseState = PauseState.main;
                    }
                    break;
                case (PauseState.options):
                    if (keyboardState.IsKeyDown(Keys.Escape) && !isEscPressed)
                    {
                        isEscPressed = true;
                        if (!isAdjustingMusic)
                            pauseState = PauseState.main;
                        else
                            isAdjustingMusic = false;
                    }
                    else if (keyboardState.IsKeyUp(Keys.Escape))
                        isEscPressed = false;
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
                    if (keyboardState.IsKeyDown(Keys.Left) && isAdjustingMusic)
                    {
                        if (!isCountDownActive[0])
                        {
                            isCountDownActive[0] = true;
                            if (isAdjustingMusic && masterVolume > byte.MinValue)
                                masterVolume--;
                            gameScreenManager.MasterVolume = masterVolume * 0.01f;
                        }
                        if (isCountDownComplete && masterVolume > byte.MinValue)
                            masterVolume--;
                    }
                    else if (keyboardState.IsKeyUp(Keys.Left))
                    {
                        isLeftPressed = false;
                        if (isCountDownActive[0])
                        {
                            isCountDownActive[0] = false;
                            isCountDownComplete = false;
                            buttonBlock = new TimeSpan(0, 0, 0, 0, 750);
                        }
                    }
                    if (keyboardState.IsKeyDown(Keys.Right) && isAdjustingMusic)
                    {
                        if (!isCountDownActive[1])
                        {
                            isCountDownActive[1] = true;
                            if (isAdjustingMusic && masterVolume < 100)
                                masterVolume++;
                        }
                            
                        if (isCountDownComplete && masterVolume < 100)
                            masterVolume++;
                        gameScreenManager.MasterVolume = masterVolume * 0.01f;
                    }
                    else if (keyboardState.IsKeyUp(Keys.Right))
                    {
                        isRightPressed = false;
                        if (isCountDownActive[1])
                        {
                            isCountDownActive[1] = false;
                            isCountDownComplete = false;
                            buttonBlock = new TimeSpan(0, 0, 0, 0, 750);
                        }
                    }    
                    if (keyboardState.IsKeyDown(Keys.Enter))
                    {
                        switch (optionsIndex)
                        {
                            case (1):
                                isAdjustingMusic = true;
                                break;
                            case (3):
                                gameScreenManager.RemoveScreen(this);
                                gameScreenManager.GoToMenu();
                                break;
                        }
                        isRightPressed = true;
                    }
                    else if (keyboardState.IsKeyUp(Keys.Right))
                        isRightPressed = false;
                    break;

            }
           
        }

        public override void LoadContent()
        {
            gameScreenManager.LoadAssets("pause");
            masterVolume = (byte)(gameScreenManager.MasterVolume * 100);
        }

        public override void UnloadContent()
        {
            Assets.Instance.Remove("pause");
        }

        public override void Update(GameTime gameTime)
        {
            if (this.cooldown>TimeSpan.Zero)
                this.cooldown -= new TimeSpan(0, 0, 0, gameTime.ElapsedGameTime.Milliseconds);

            mainWindow = new Rectangle(50, 50, gameScreenManager.GraphicsDevice.Viewport.Height / 3, (int)(gameScreenManager.GraphicsDevice.Viewport.Height / 2.5f));
            switch (pauseState)
            {
                case (PauseState.main):
                    cursorPos = new Rectangle(50 + mainWindow.Width / 4, 50 + mainWindow.Height / 10 + (mainWindow.Height / 7) * index, mainWindow.Width / 12, mainWindow.Width / 12);
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
                case (PauseState.quests):
                    cursorPos = new Rectangle(secondaryWindow.X + (secondaryWindow.Width / 5), 50 + secondaryWindow.Height / 10 + (secondaryWindow.Height / 10) * questIndex, mainWindow.Width / 12, mainWindow.Width / 12);
                    secondaryWindow = new Rectangle(80 + mainWindow.Width, 50, gameScreenManager.GraphicsDevice.Viewport.Width / 3, gameScreenManager.GraphicsDevice.Viewport.Height / 2);
                    break;
                case (PauseState.map):
                    secondaryWindow = new Rectangle(80 + mainWindow.Width, 50, gameScreenManager.GraphicsDevice.Viewport.Width / 2, gameScreenManager.GraphicsDevice.Viewport.Height / 2);
                    break;
                case (PauseState.options):
                    cursorPos = new Rectangle(secondaryWindow.X + (secondaryWindow.Width / 10), 50 + (int)(secondaryWindow.Height / 10f) + (secondaryWindow.Height / 4) * optionsIndex, mainWindow.Width / 12, mainWindow.Width / 12);
                    secondaryWindow = new Rectangle(80 + mainWindow.Width, 50, gameScreenManager.GraphicsDevice.Viewport.Width / 2, gameScreenManager.GraphicsDevice.Viewport.Height / 2);
                    if (isCountDownActive.Contains(true)) CountDown(gameTime);
                    break;
            }
                
        }

        private void CountDown(GameTime gameTime)
        {
            buttonBlock -= new TimeSpan(0, 0, 0, 0, gameTime.ElapsedGameTime.Milliseconds);
            if (buttonBlock <= TimeSpan.Zero)
                isCountDownComplete = true;
            else
                isCountDownComplete = false;
        }
    }
}
