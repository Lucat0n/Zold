using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Zold.Inventory;
using Zold.Quests;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Pause
{
    class QuestDescriptionScreen : GameScreen
    {
        private ArrayList strings;
        private Rectangle questBox;
        private SByte questIndex;
        private readonly SpriteFont font;
        private Vector2 titlePos;
        private Vector2 firstLinePos;
        private byte signsPerLine;
        private byte lineAmount;

        internal QuestDescriptionScreen(SByte questIndex)
        {
            IsTransparent = true;
            font = Assets.Instance.Get("placeholders/Fonts/dialog");
            this.questIndex = questIndex;
        }

        public override void Draw(GameTime gameTime)
        {
            gameScreenManager.SpriteBatch.Begin();
            gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("pause/Textures/secondaryWindow"), questBox, Color.White);
            gameScreenManager.SpriteBatch.DrawString(font, gameScreenManager.QuestManager.GetActiveQuestName(questIndex, false), titlePos, Color.Yellow, 0.0f, Vector2.Zero, new Vector2(questBox.Height * 0.005f, questBox.Height * 0.005f), SpriteEffects.None, 1.0f);
            for(int i = 0; i < strings.Count; i++)
                gameScreenManager.SpriteBatch.DrawString(font, strings[i].ToString(), firstLinePos + new Vector2(0, i*(questBox.Height / 10)), Color.White, 0.0f, Vector2.Zero, new Vector2(questBox.Height * 0.005f, questBox.Height * 0.005f), SpriteEffects.None, 1.0f);
            gameScreenManager.SpriteBatch.End();
        }

        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Escape))
                gameScreenManager.RemoveScreen(this);
        }

        private ArrayList PrepareString()
        {
            ArrayList strings = new ArrayList();
            string[] ssize = gameScreenManager.QuestManager.ActiveQuests[gameScreenManager.QuestManager.GetActiveQuestID(questIndex, false)].Description.Split(null);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string s in ssize)
            {
                if(stringBuilder.Length + s.Length < signsPerLine)
                {
                    if (stringBuilder.Length != 0)
                        stringBuilder.Append(" ");
                    stringBuilder.Append(s);
                }
                else
                {
                    strings.Add(stringBuilder.ToString());
                    stringBuilder.Clear();
                    stringBuilder.Append(s);
                }
            }
            if(stringBuilder.Length != 0)
                strings.Add(stringBuilder.ToString());
            strings.Add("");
            string id = gameScreenManager.QuestManager.GetActiveQuestID(questIndex, false);
            if (id[0] == 'i')
            {
                ItemQuest itemQuest = (ItemQuest)gameScreenManager.QuestManager.ActiveQuests[id];
                foreach (KeyValuePair<Item, byte> pair in itemQuest.ItemsToCollect)
                    strings.Add(pair.Key.Name + " " + (itemQuest.ItemsCollected.ContainsKey(pair.Key) ? itemQuest.ItemsCollected[pair.Key] : 0) + " / " + pair.Value.ToString());
            }else if(id[0] == 'l')
            {
                LocationQuest LocationQuest = (LocationQuest)gameScreenManager.QuestManager.ActiveQuests[id];
                strings.Add("Odwiedz lokacje: " + LocationQuest.LocationToVisit);
            }
            return strings;
        }

        #region loadUnload
        public override void LoadContent()
        {
            questBox = new Rectangle(3 * gameScreenManager.GraphicsDevice.Viewport.Width / 10, gameScreenManager.GraphicsDevice.Viewport.Height / 4, (int)(gameScreenManager.GraphicsDevice.Viewport.Width / 2.5f), gameScreenManager.GraphicsDevice.Viewport.Height / 2);
            signsPerLine = (byte)((questBox.Width * 0.95) / (font.MeasureString("a").X * (questBox.Height * 0.005f)));
            titlePos = new Vector2(questBox.X + questBox.Width / 2 - (font.MeasureString(gameScreenManager.QuestManager.GetActiveQuestName(questIndex, false)).X / 2) * questBox.Height * 0.005f, questBox.Y + questBox.Height / 20);
            firstLinePos = new Vector2(questBox.X + questBox.Width * 0.025f, questBox.Y + questBox.Height / 7);
            this.strings = PrepareString();
        }
        public override void UnloadContent() { }
        #endregion

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
