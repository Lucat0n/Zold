﻿using System;
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
        public List<string> powiedzonka = new List<string>();
        


        public InteractionManager(GameScreenManager gameScreenManager, Location location)
        {
            this.gameScreenManager = gameScreenManager;
            dialog = Assets.Instance.Get("placeholders/Fonts/dialog");
            AddDialogs(powiedzonka);

            var random = new Random();
            index = random.Next(powiedzonka.Count);
        }

        int getRandomIndex()
        {
            var random = new Random();
            return random.Next(powiedzonka.Count);
        }

        public void AddDialogs(List<string> powiedzonka)
        {
            powiedzonka.Add("Witaj zielona magnetyczna gwiazdo");
            powiedzonka.Add("A ty tu czego?");
            powiedzonka.Add("Zbieram zlom, nie widzisz bulwa");
            powiedzonka.Add("Jestem zajety");
            powiedzonka.Add("Odejdz");
            powiedzonka.Add("Do samochodu i do widzenia");
            powiedzonka.Add("Ile razy sie zesrales? ");
            powiedzonka.Add("Niech zyje wolny zold, precz z komuniom");
            powiedzonka.Add("Elo");
            powiedzonka.Add("Tez kiedys bylem jak ty, ale sie jeblem i przestalem");
        }

        public void displayDialog(Player playerOne, Texture2D npcet, int posx, int posy, Rectangle bounds)
        {
            // int index = 0; ;
            int width = MapManager.screenWdth;
            int height = MapManager.screenHeight;

            if (playerOne.GetPosition().X >= posx - 50 && playerOne.GetPosition().X < posx + npcet.Width + 20
                && playerOne.GetPosition().Y >= posy && playerOne.GetPosition().Y < posy + npcet.Height + 30)

            {
                if (!Keyboard.GetState().IsKeyDown(Keys.Space))
                    index = getRandomIndex();

                Rectangle tlo;
                if (gameScreenManager.IsFullScreenOn)
                {
                    tlo = new Rectangle(50, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 60, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 100, 50);
                    
                }
                else
                {
                    tlo = new Rectangle(50, height-60, width-100, 50);
                }

                if (!disp)
                {
                    dymek = Assets.Instance.Get("placeholders/Textures/dymek");
                    gameScreenManager.SpriteBatch.Draw(dymek, new Rectangle(posx - 12 , posy - 14 + bounds.Y, dymek.Width * 2, dymek.Height * 2), Color.White);
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && !disp) {
                        disp = true;
                    }
                    

                }
                

                if (disp)
                {
                    
                    gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("placeholders/Textures/dotekstu"), tlo, Color.White);
                    gameScreenManager.SpriteBatch.DrawString(dialog, powiedzonka[index], new Vector2(145, 425), Color.White);
                    disp = false;

                }
            }
        }
    }
}
