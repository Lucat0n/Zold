using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Zold.Inventory;
using Zold.Inventory.Items;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Pause
{
    class ItemDecisionScreen : GameScreen
    {
        private Item item;
        private int yPos;
        private Rectangle decisionBox;
        private sbyte decision;
        private SpriteFont font;

        public ItemDecisionScreen(Item item, int yPos)
        {
            decision = 0;
            font = Assets.Instance.Get("placeholders/Fonts/dialog");
            System.Diagnostics.Debug.WriteLine(item.GetType());
            this.item = item;
            this.yPos = yPos;
        }
        public override void Draw(GameTime gameTime)
        {
            gameScreenManager.SpriteBatch.Begin();
            gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("pause/Textures/mainWindow"), decisionBox, Color.White);
            switch (item)
            {
                case BuffItem bit:
                    gameScreenManager.SpriteBatch.DrawString(font, "Uzyj", new Vector2(decisionBox.X + decisionBox.Width / 4, decisionBox.Y + decisionBox.Height / 8), Color.White, 0.0f, Vector2.Zero, new Vector2(decisionBox.Height * 0.015f, decisionBox.Height * 0.015f), SpriteEffects.None, 1.0f);
                    gameScreenManager.SpriteBatch.DrawString(font, "Zobacz", new Vector2(decisionBox.X + decisionBox.Width / 4, decisionBox.Y + 2 * decisionBox.Height / 4), Color.White, 0.0f, Vector2.Zero, new Vector2(decisionBox.Height * 0.015f, decisionBox.Height * 0.015f), SpriteEffects.None, 1.0f);
                break;
                case Weapon weap:
                    gameScreenManager.SpriteBatch.DrawString(font, "Zaloz", new Vector2(decisionBox.X + decisionBox.Width / 4, decisionBox.Y + decisionBox.Height / 8), Color.White, 0.0f, Vector2.Zero, new Vector2(decisionBox.Height * 0.015f, decisionBox.Height * 0.015f), SpriteEffects.None, 1.0f);
                    gameScreenManager.SpriteBatch.DrawString(font, "Zobacz", new Vector2(decisionBox.X + decisionBox.Width / 4, decisionBox.Y + 2 * decisionBox.Height / 4), Color.White, 0.0f, Vector2.Zero, new Vector2(decisionBox.Height * 0.015f, decisionBox.Height * 0.015f), SpriteEffects.None, 1.0f);
                    break;
                case Item it:
                    gameScreenManager.SpriteBatch.DrawString(font, "Zobacz", new Vector2(decisionBox.X + decisionBox.Width / 4, decisionBox.Y + 2 * decisionBox.Height / 4), Color.White, 0.0f, Vector2.Zero, new Vector2(decisionBox.Height * 0.015f, decisionBox.Height * 0.015f), SpriteEffects.None, 1.0f);
                break;
            }
            gameScreenManager.SpriteBatch.End();
        }

        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Escape))
                gameScreenManager.RemoveScreen(this);
        }

        public override void LoadContent()
        {
            decisionBox = new Rectangle(gameScreenManager.GraphicsDevice.Viewport.Width / 2, yPos, gameScreenManager.GraphicsDevice.Viewport.Height / 4, gameScreenManager.GraphicsDevice.Viewport.Height / 6);
        }

        public override void UnloadContent() { }
    }
}
