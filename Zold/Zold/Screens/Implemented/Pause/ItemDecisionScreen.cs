using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private delegate void targetMethod();

        private bool isUpPressed = false;
        private bool isDownPressed = false;
        private bool isEnterPressed = false;
        private bool isEnterPressed2 = false;
        private bool isEscPressed = false;
        private Item item;
        private int yPos;
        private Rectangle decisionBox;
        private Rectangle decisionBox2;
        private Rectangle cursor;
        private sbyte decision;
        private sbyte maxDecision;
        private SpriteFont font;

        private targetMethod tm;

        public ItemDecisionScreen(Item item, int yPos)
        {
            IsTransparent = true;
            decision = 0;
            font = Assets.Instance.Get("placeholders/Fonts/dialog");
            this.item = item;
            this.yPos = yPos;
        }
        public override void Draw(GameTime gameTime)
        {
            gameScreenManager.SpriteBatch.Begin();
            gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("pause/Textures/secondaryWindow"), decisionBox2, Color.White);
            switch (item)
            {
                case BuffItem bit:
                    gameScreenManager.SpriteBatch.DrawString(font, "Uzyj", new Vector2(decisionBox.X + decisionBox.Width / 4, decisionBox.Y), Color.White, 0.0f, Vector2.Zero, new Vector2(decisionBox.Height * 0.015f, decisionBox.Height * 0.015f), SpriteEffects.None, 1.0f);
                    gameScreenManager.SpriteBatch.DrawString(font, "Zobacz", new Vector2(decisionBox.X + decisionBox.Width / 4, decisionBox.Y + (int)(1.5 * decisionBox.Height / 5)), Color.White, 0.0f, Vector2.Zero, new Vector2(decisionBox.Height * 0.015f, decisionBox.Height * 0.015f), SpriteEffects.None, 1.0f);
                    gameScreenManager.SpriteBatch.DrawString(font, "Wyrzuc", new Vector2(decisionBox.X + decisionBox.Width / 4, decisionBox.Y + 3 * decisionBox.Height / 5), Color.White, 0.0f, Vector2.Zero, new Vector2(decisionBox.Height * 0.015f, decisionBox.Height * 0.015f), SpriteEffects.None, 1.0f);
                    break;
                case Weapon weap:
                    gameScreenManager.SpriteBatch.DrawString(font, "Wybierz", new Vector2(decisionBox.X + decisionBox.Width / 4, decisionBox.Y), Color.White, 0.0f, Vector2.Zero, new Vector2(decisionBox.Height * 0.015f, decisionBox.Height * 0.015f), SpriteEffects.None, 1.0f);
                    gameScreenManager.SpriteBatch.DrawString(font, "Zobacz", new Vector2(decisionBox.X + decisionBox.Width / 4, decisionBox.Y + (int)(1.5 * decisionBox.Height / 5)), Color.White, 0.0f, Vector2.Zero, new Vector2(decisionBox.Height * 0.015f, decisionBox.Height * 0.015f), SpriteEffects.None, 1.0f);
                    gameScreenManager.SpriteBatch.DrawString(font, "Wyrzuc", new Vector2(decisionBox.X + decisionBox.Width / 4, decisionBox.Y + 3 * decisionBox.Height / 5), Color.White, 0.0f, Vector2.Zero, new Vector2(decisionBox.Height * 0.015f, decisionBox.Height * 0.015f), SpriteEffects.None, 1.0f);
                    break;
                case Item it:
                    gameScreenManager.SpriteBatch.DrawString(font, "Zobacz", new Vector2(decisionBox2.X + decisionBox2.Width / 4, decisionBox2.Y + decisionBox2.Height / 16), Color.White, 0.0f, Vector2.Zero, new Vector2(decisionBox.Height * 0.015f, decisionBox.Height * 0.015f), SpriteEffects.None, 1.0f);
                    gameScreenManager.SpriteBatch.DrawString(font, "Wyrzuc", new Vector2(decisionBox2.X + decisionBox2.Width / 4, decisionBox2.Y + decisionBox2.Height / 2), Color.White, 0.0f, Vector2.Zero, new Vector2(decisionBox.Height * 0.015f, decisionBox.Height * 0.015f), SpriteEffects.None, 1.0f);
                    break;
            }
            gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("pause/Textures/cursor"), cursor, Color.White);
            gameScreenManager.SpriteBatch.End();
        }

        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Escape) && !isEscPressed)
            {
                if (!isEscPressed)
                    gameScreenManager.RemoveScreen(this);
            }
            else if (keyboardState.IsKeyUp(Keys.Escape))
            {
                isEscPressed = false;
            }
            if (keyboardState.IsKeyDown(Keys.Down) && !isDownPressed)
            {
                    if (++decision > maxDecision)
                        decision = 0;
                isDownPressed = true;
            }
            else if (keyboardState.IsKeyUp(Keys.Down))
                isDownPressed = false;
            if (keyboardState.IsKeyDown(Keys.Up) && !isUpPressed)
            {
                if (--decision < 0)
                    decision = maxDecision;
                isUpPressed = true;
            }
            else if (keyboardState.IsKeyUp(Keys.Up))
                isUpPressed = false;
            if (keyboardState.IsKeyDown(Keys.Enter) && !isEnterPressed)
            {
                isEnterPressed = true;
                isEnterPressed2 = !isEnterPressed2;
                if (!isEnterPressed2)
                {
                    isEnterPressed2 = true;
                    if (maxDecision == 2)
                    {
                        switch (decision)
                        {
                            case 0:
                                tm += UseItem;
                                gameScreenManager.InsertScreen(new ConfirmationScreen(decisionBox, tm));
                                isEscPressed = true;
                                tm = null;
                                break;
                            case 1:
                                break;
                            case 2:
                                tm += RemoveItem;
                                gameScreenManager.InsertScreen(new ConfirmationScreen(decisionBox, tm));
                                isEscPressed = true;
                                tm = null;
                                break;
                        }
                    }
                    else if (maxDecision == 1)
                    {
                        switch (decision)
                        {
                            case 0:
                                break;
                            case 1:
                                tm += RemoveItem;
                                gameScreenManager.InsertScreen(new ConfirmationScreen(decisionBox, tm));
                                isEscPressed = true;
                                tm = null;
                                break;
                        }
                    }
                }

            }
            else if (keyboardState.IsKeyUp(Keys.Enter))
            {
                isEnterPressed = false;
            }


        }

        public override void LoadContent()
        {
            switch (item)
            {
                case BuffItem bit:
                    maxDecision = 2;
                    decisionBox = new Rectangle(gameScreenManager.GraphicsDevice.Viewport.Width / 2, yPos, gameScreenManager.GraphicsDevice.Viewport.Width / 8, (int)(font.MeasureString("A").Y * 3));
                    decisionBox2 = decisionBox;
                    cursor = new Rectangle(decisionBox2.X + decisionBox2.Width / 10, decisionBox2.Y + decisionBox2.Height / 10 + decision * (int)(decisionBox2.Height / 3.25f), decisionBox2.Height / 6, decisionBox2.Height / 6);
                    break;
                case Weapon weap:
                    maxDecision = 2;
                    decisionBox = new Rectangle(gameScreenManager.GraphicsDevice.Viewport.Width / 2, yPos, gameScreenManager.GraphicsDevice.Viewport.Width / 8, (int)(font.MeasureString("A").Y * 3));
                    decisionBox2 = decisionBox;
                    cursor = new Rectangle(decisionBox2.X + decisionBox2.Width / 10, decisionBox2.Y + decisionBox2.Height / 10 + decision * (int)(decisionBox2.Height / 3.25f), decisionBox2.Height / 6, decisionBox2.Height / 6);
                    break;
                case Item it:
                    maxDecision = 1;
                    decisionBox = new Rectangle(gameScreenManager.GraphicsDevice.Viewport.Width / 2, yPos, gameScreenManager.GraphicsDevice.Viewport.Width / 8, (int)(font.MeasureString("A").Y * 3));
                    decisionBox2 = new Rectangle(gameScreenManager.GraphicsDevice.Viewport.Width / 2, yPos, gameScreenManager.GraphicsDevice.Viewport.Width / 8, (int)(font.MeasureString("A").Y * 2));
                    break;
            }

        }

        public override void UnloadContent() { }

        public override void Update(GameTime gameTime)
        {
            switch (item)
            {
                case BuffItem bit:
                    cursor = new Rectangle(decisionBox2.X + decisionBox2.Width / 10, decisionBox2.Y + decisionBox2.Height / 10 + decision * (int)(decisionBox2.Height / 3.25f), decisionBox.Height / 6, decisionBox.Height / 6);
                    break;
                case Weapon weap:
                    cursor = new Rectangle(decisionBox2.X + decisionBox2.Width / 10, decisionBox2.Y + decisionBox2.Height / 10 + decision * (int)(decisionBox2.Height / 3.25f), decisionBox.Height / 6, decisionBox.Height / 6);
                    break;
                case Item it:
                    cursor = new Rectangle(decisionBox2.X + decisionBox2.Width / 10, decisionBox2.Y + decisionBox2.Height / 5 + decision * (int)(decisionBox2.Height / 2), decisionBox.Height / 6, decisionBox.Height / 6);
                    break;
            }
        }

        public void RemoveItem()
        {
            gameScreenManager.InventoryManager.GetPlayerInventory().RemoveItem(item.Name);
            gameScreenManager.RemoveScreen(this);
        }

        public void UseItem()
        {
            //TODO - METODA UŻYWAJĄCA DANEGO ITEMKA - CZEKAM NA STATY
            gameScreenManager.InventoryManager.GetPlayerInventory().RemoveItem(item.Name, 1);
            gameScreenManager.RemoveScreen(this);
        }

        //TODO - EquipItem()
    }
}
