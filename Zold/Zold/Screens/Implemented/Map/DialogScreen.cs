using System;
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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Zold.Screens.Implemented.Map
{
    class DialogScreen : GameScreen
    {
        private bool isEscPressed = false;
        Texture2D dymek;
        SpriteFont dialog;
        Vector2 TextPosition;
        string text;
        Rectangle tlo;
        GameScreenManager gameScreenManager;

        public DialogScreen(GameScreenManager gameScreenManager, string text)
        {
            this.gameScreenManager = gameScreenManager;

            int width = MapManager.screenWdth;
            int height = MapManager.screenHeight;

            dialog = Assets.Instance.Get("placeholders/Fonts/dialog");
            TextPosition = new Vector2(42, 412);
            this.text = text;
            

            if (gameScreenManager.IsFullScreenOn)
            {
                tlo = new Rectangle(50, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 60, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 100, 50);

            }
            else
            {
                tlo = new Rectangle(-15, 410, width, 30);
            }

        }
        public override void Draw(GameTime gameTime)
        {
            gameScreenManager.SpriteBatch.Begin();
            gameScreenManager.SpriteBatch.Draw(Assets.Instance.Get("placeholders/Textures/dotekstu"), tlo, Color.White);
            gameScreenManager.SpriteBatch.DrawString(dialog, text, TextPosition, Color.White);
            gameScreenManager.SpriteBatch.End();
        }

        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Space) && !isEscPressed)
            {
                gameScreenManager.RemoveScreen(this);
                //Assets.Instance.Remove("pause");
            }
            else if (keyboardState.IsKeyUp(Keys.Escape))
                isEscPressed = false;
        }

        public override void LoadContent()
        {
           
        }

        public override void UnloadContent()
        {
            
        }
    }
}
