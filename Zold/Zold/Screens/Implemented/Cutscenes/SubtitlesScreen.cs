using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Zold.Screens.Implemented.Map;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Cutscenes
{
    class SubtitlesScreen : GameScreen
    {
        private SpriteFont font;
        private String text;
        private TimeSpan LifeTime = new TimeSpan(0, 0, 3);

        //Combat

        public SubtitlesScreen(String text)
        {
            FadeInTime = new TimeSpan(0, 0, 2);
            FadeOutTime = new TimeSpan(0, 0, 2);
            this.text = text;
            IsTransparent = false;
        }

        public override void Draw(GameTime gameTime)
        {
            gameScreenManager.SpriteBatch.Begin();
            gameScreenManager.SpriteBatch.GraphicsDevice.Clear(Color.Black);
            gameScreenManager.SpriteBatch.DrawString(font, text,new Vector2(gameScreenManager.GraphicsDevice.Viewport.Width/2 - font.MeasureString(text).Length()/2, gameScreenManager.GraphicsDevice.Viewport.Height/2) ,Color.White);
            gameScreenManager.SpriteBatch.End();
            gameScreenManager.FadeScreen(FadeAlpha);
        }

        public override void Update(GameTime gameTime)
        {
            switch (ScreenState)
            {
                case ScreenState.FadeIn:
                    if (UpdateFade(gameTime, FadeInTime))
                        this.ScreenState = ScreenState.Active;

                    break;
                case ScreenState.Active:
                    LifeTime -= new TimeSpan((long)gameTime.ElapsedGameTime.TotalMilliseconds * 10000);
                    if (LifeTime <= TimeSpan.Zero)
                        this.ScreenState = ScreenState.FadeOut;
                    break;
                case ScreenState.FadeOut:
                    if (UpdateFade(gameTime, FadeOutTime))
                    {
                        gameScreenManager.RemoveScreen(this);
                        gameScreenManager.InsertScreen(new MapManager());
                    }
                    break;
            }
        }

        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.ScreenState = ScreenState.FadeOut;
            }
        }

        public override void LoadContent()
        {
            gameScreenManager.LoadAssets("placeholders");
            font = Assets.Instance.Get("placeholders/Fonts/dialog");
        }

        public override void UnloadContent()
        {
            //gameScreenManager.ContentLoader.UnloadLocation("splash");
            //gameScreenManager.ContentLoader.UnloadLocation("combat");
        }
    }
}
