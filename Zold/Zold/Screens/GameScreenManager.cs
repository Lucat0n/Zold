using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.GamerServices;

namespace Zold.Screens
{
    public class GameScreenManager : DrawableGameComponent
    {
        #region zmienne
        private bool isStackModified = true;
        private ContentManager content;
        private GraphicsDeviceManager graphics;
        private Point cursor;
        private Stack<GameScreen> ScreenStack = new Stack<GameScreen>();
        private List<GameScreen> ScreensToDraw = new List<GameScreen>();
        private SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        private Texture2D blank;

        private MouseState mouseState;
        private KeyboardState keyboardState;

        //public new Game Game { get { return base.Game; } }
        #endregion

        #region setget
        public ContentManager Content
        {
            get { return content; }
        }

        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }
        #endregion

        #region init
        public GameScreenManager(Game1 game) : base(game)
        {
            graphics = game.graphics;
            content = game.Content;
            content.RootDirectory = "Content";
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void LoadContent()
        {
            spriteFont = content.Load<SpriteFont>("placeholders/font");
            blank = content.Load<Texture2D>("placeholders/blank");

            foreach(GameScreen screen in ScreenStack)
            {
                screen.LoadContent();
            }
        }

        protected override void UnloadContent()
        {
            content.Unload();
            foreach (GameScreen screen in ScreenStack)
            {
                screen.UnloadContent();
            }
        }
        #endregion

        #region update
        public override void Update(GameTime gameTime)
        {
            updateInput();
            ScreenStack.Peek().HandleInput(mouseState, cursor, keyboardState);
            ScreenStack.Peek().Update(gameTime);
        }
        #endregion

        #region draw
        //jak na razie poprawnie będzie wyświetlać jeden przezroczysty ekran na jakimś innym nieprzezroczystym
        public override void Draw(GameTime gameTime)
        {
            if (isStackModified)
            {
                GameScreen temp;
                do
                {
                    temp = ScreenStack.Peek();
                    ScreensToDraw.Insert(0, ScreenStack.Pop());
                } while (temp.IsTransparent);
                isStackModified = false;
            }
            foreach (GameScreen gameScreen in ScreensToDraw)
                gameScreen.Draw(gameTime);
        }
        #endregion

        private void updateInput()
        {
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            cursor.X = mouseState.X;
            cursor.Y = mouseState.Y;
        }

        #region public
        public void InsertScreen(GameScreen gameScreen)
        {
            gameScreen.GameScreenManager = this;
            gameScreen.LoadContent();
            ScreenStack.Push(gameScreen);
            isStackModified = true;
        }

        public void RemoveScreen(GameScreen gameScreen)
        {
            if(!gameScreen.IsTransparent)
                isStackModified = true;
            ScreensToDraw.Remove(gameScreen);
            //ScreenStack.Pop();
        }

        public void fadeScreen()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(blank, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White * 0.5f);
            spriteBatch.End();
        }
        #endregion
    }
}
