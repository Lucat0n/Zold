using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using TiledSharp;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Map
{

    class InteractionManager
    {
        private bool disp = false;
        private bool di = false;
        int index;

        GameScreenManager gameScreenManager;
        Texture2D dymek;
        SpriteFont dialog;
        // bool randomDialog;
        public List<string> powiedzonka = new List<string>();

        Vector2 TextPosition = new Vector2(42, 412);

        public InteractionManager(GameScreenManager gameScreenManager, Location location)
        {
            this.gameScreenManager = gameScreenManager;
            dialog = Assets.Instance.Get("placeholders/Fonts/dialog");
            // AddDialogs(powiedzonka);
            // this.randomDialog = randomDialog;

            var random = new Random();
            index = random.Next(powiedzonka.Count);
        }

        int getRandomIndex(List<string> powiedzenia)
        {
            var random = new Random();
            return random.Next(powiedzenia.Count);
        }


        public void displayDialog(Player playerOne, Texture2D npcet, int posx, int posy, List<string> powiedzenia)
        {
            // int index = 0; ;
            int width = MapManager.screenWdth;
            int height = MapManager.screenHeight;

            if (playerOne.GetPosition().X >= posx - 50 && playerOne.GetPosition().X < posx + npcet.Width + 20
                && playerOne.GetPosition().Y >= posy && playerOne.GetPosition().Y < posy + npcet.Height + 30)

            {
                if (!Keyboard.GetState().IsKeyDown(Keys.Space))
                    index = getRandomIndex(powiedzenia);

                Rectangle tlo;
                if (gameScreenManager.IsFullScreenOn)
                {
                    tlo = new Rectangle(50, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 60, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 100, 50);

                }
                else
                {
                    tlo = new Rectangle(-15, 410, width, 30);
                }

                if (!disp)
                {
                    dymek = Assets.Instance.Get("placeholders/Textures/dymek");
                    gameScreenManager.SpriteBatch.Draw(dymek, new Rectangle(posx - 12, posy - 14, dymek.Width * 2, dymek.Height * 2), Color.White);
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !disp)
                    {
                        disp = true;
                    }

                }

                if (disp)
                {

                    // gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("placeholders/Textures/dotekstu"), tlo, Color.White);
                    if (!(gameScreenManager.QuestManager.ActiveQuests.ContainsKey("lQ1") || gameScreenManager.QuestManager.CompletedQuests.ContainsKey("lQ1")))
                    {
                        gameScreenManager.InsertScreen(new DialogScreen(gameScreenManager, powiedzenia[index]));
                        //    gameScreenManager.SpriteBatch.DrawString(dialog, "Sprawdz questy, potem wyjdz z pokoju i je ponownie zobacz.", TextPosition, Color.White);
                        gameScreenManager.QuestManager.AddLocationQuest("lQ1");
                    }
                    if (gameScreenManager.QuestManager.ActiveQuests.ContainsKey("lQ1"))
                    {
                        gameScreenManager.InsertScreen(new DialogScreen(gameScreenManager, powiedzenia[index]));
                    }

                    //      gameScreenManager.SpriteBatch.DrawString(dialog, "Sprawdz zadania, potem wyjdz z pokoju i je ponownie zobacz.", TextPosition, Color.White);
                    else
                    {
                        gameScreenManager.InsertScreen(new DialogScreen(gameScreenManager, powiedzonka[index]));
                    }
                    //   gameScreenManager.SpriteBatch.DrawString(dialog, powiedzonka[index], TextPosition, Color.White);

                    disp = false;

                }
            }
        }
    }
}