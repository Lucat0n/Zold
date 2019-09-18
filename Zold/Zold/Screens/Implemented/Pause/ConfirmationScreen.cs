using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Pause
{
    class ConfirmationScreen : GameScreen
    {
        private bool isDownPressed = false;
        private bool isUpPressed = false;
        private bool isEnterPressed = true;
        private Rectangle confirmationBox;
        private Rectangle cursor;
        private Rectangle decisionBox;
        private sbyte confirmation;
        private SoundEffect scrollUp;
        private SoundEffect scrollDown;
        private SoundEffect select;
        private SoundEffect back;
        private SpriteFont font;
        private Delegate delegation;

        internal ConfirmationScreen(Rectangle source, Delegate method)
        {
            delegation = method;
            decisionBox = source;
            confirmation = 2;
        }

        public override void Draw(GameTime gameTime)
        {
            gameScreenManager.SpriteBatch.Begin();
            gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("pause/Textures/secondaryWindow"), confirmationBox, Color.White);
            gameScreenManager.SpriteBatch.DrawString(font, "Na pewno?", new Vector2(decisionBox.X + decisionBox.Width / 4, decisionBox.Y), Color.White, 0.0f, Vector2.Zero, new Vector2(decisionBox.Height * 0.015f, decisionBox.Height * 0.015f), SpriteEffects.None, 1.0f);
            gameScreenManager.SpriteBatch.DrawString(font, "Tak", new Vector2(decisionBox.X + decisionBox.Width / 4, decisionBox.Y + (int)(1.5 * decisionBox.Height / 5)), Color.White, 0.0f, Vector2.Zero, new Vector2(decisionBox.Height * 0.015f, decisionBox.Height * 0.015f), SpriteEffects.None, 1.0f);
            gameScreenManager.SpriteBatch.DrawString(font, "Nie", new Vector2(decisionBox.X + decisionBox.Width / 4, decisionBox.Y + 3 * decisionBox.Height / 5), Color.White, 0.0f, Vector2.Zero, new Vector2(decisionBox.Height * 0.015f, decisionBox.Height * 0.015f), SpriteEffects.None, 1.0f);
            gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("pause/Textures/cursor"), cursor, Color.White);
            gameScreenManager.SpriteBatch.End();
        }

        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                back.Play();
                gameScreenManager.RemoveScreen(this);
            }
            if (keyboardState.IsKeyDown(Keys.Down) && !isDownPressed)
            {
                if (++confirmation > 2)
                    confirmation = 1;
                isDownPressed = true;
                scrollDown.Play();
            }
            else if (keyboardState.IsKeyUp(Keys.Down))
                isDownPressed = false;
            if (keyboardState.IsKeyDown(Keys.Up) && !isUpPressed)
            {
                if (--confirmation < 1)
                    confirmation = 2;
                isUpPressed = true;
                scrollUp.Play();
            }
            else if (keyboardState.IsKeyUp(Keys.Up))
                isUpPressed = false;
            if (keyboardState.IsKeyDown(Keys.Enter) && !isEnterPressed)
            {
                select.Play();
                if (confirmation == 1)
                    delegation.DynamicInvoke();
                gameScreenManager.RemoveScreen(this);
                    
            }
            else if (keyboardState.IsKeyUp(Keys.Enter))
                isEnterPressed = false;
        }

        public override void LoadContent()
        {
            font = Assets.Instance.Get("placeholders/Fonts/dialog");
            scrollUp = Assets.Instance.Get("pause/Sounds/scrollup");
            scrollDown = Assets.Instance.Get("pause/Sounds/scrolldown");
            select = Assets.Instance.Get("pause/Sounds/enter");
            back = Assets.Instance.Get("pause/Sounds/back");
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            confirmationBox = new Rectangle(gameScreenManager.GraphicsDevice.Viewport.Width / 2, decisionBox.Y, gameScreenManager.GraphicsDevice.Viewport.Height / 4, (int)(font.MeasureString("A").Y * 3));
            cursor = new Rectangle(confirmationBox.X + confirmationBox.Width / 10, confirmationBox.Y + confirmationBox.Y / 16 + confirmation * (int)(confirmationBox.Height / 3.25), confirmationBox.Height / 6, confirmationBox.Height / 6);
        }
    }
}
