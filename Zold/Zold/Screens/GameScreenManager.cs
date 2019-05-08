﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.GamerServices;
using Zold.Utilities;
using Zold.Screens.Implemented;

namespace Zold.Screens
{
    public class GameScreenManager : DrawableGameComponent
    {
        #region zmienne
        private bool isFullScreenOn = false;
        private ContentManager content;
        private ContentLoader contentLoader;
        private GraphicsDeviceManager graphics;
        private Rectangle cursor;
        private List<GameScreen> ScreenList = new List<GameScreen>();
        private List<GameScreen> ScreensToDraw = new List<GameScreen>();
        private SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        private Texture2D blank;

        private MouseState mouseState;
        private KeyboardState keyboardState;

        //public new Game Game { get { return base.Game; } }
        #endregion

        #region setget
        public bool IsFullScreenOn
        {
            get { return isFullScreenOn; }
            set { isFullScreenOn = value; }
        }
        public ContentManager Content
        {
            get { return content; }
        }
        public GraphicsDeviceManager Graphics
        {
            get { return graphics; }
        }
        public KeyboardState KeyboardState
        {
            get { return keyboardState; }
        }
        public MouseState MouseState
        {
            get { return mouseState; }
        }
        public Rectangle Cursor
        {
            get { return cursor; }
        }
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        internal ContentLoader ContentLoader { get => contentLoader; set => contentLoader = value; }
        #endregion

        #region init
        public GameScreenManager(Game1 game) : base(game)
        {
            graphics = game.graphics;
            content = game.Content;
            content.RootDirectory = "Content";
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentLoader = new ContentLoader(game, Content);
            this.LoadContent();
        }

        protected override void LoadContent()
        {
            LoadAssets("screenManager");
            //spriteFont = Assets.Instance.Get("placeholders/Fonts/dialog");
            blank = Assets.Instance.Get("screenManager/Textures/blank");

            foreach(GameScreen screen in ScreenList)
            {
                screen.LoadContent();
            }
        }

        protected override void UnloadContent()
        {
            content.Unload();
            foreach (GameScreen screen in ScreenList)
            {
                screen.UnloadContent();
            }
        }
        #endregion

        #region update
        public override void Update(GameTime gameTime)
        {
            UpdateInput();
            if (ScreensToDraw.Count > 0)
            {
                ScreensToDraw[ScreensToDraw.Count - 1].HandleInput(mouseState, cursor, keyboardState);
                ScreensToDraw[ScreensToDraw.Count - 1].Update(gameTime);
            }
        }
        #endregion

        #region draw
        public override void Draw(GameTime gameTime)
        {
            graphics.IsFullScreen = isFullScreenOn;
            ScreensToDraw.Clear();
            ScreensToDraw = ScreenList.GetRange(ScreenList.FindLastIndex(FindNonTransparent), ScreenList.Count);
            foreach (GameScreen gameScreen in ScreensToDraw)
                gameScreen.Draw(gameTime);
        }
        #endregion

        private void UpdateInput()
        {
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            cursor.X = mouseState.X;
            cursor.Y = mouseState.Y;
        }

        private static bool FindNonTransparent(GameScreen gameScreen)
        {
            if (gameScreen.IsTransparent)
                return false;
            else
                return true;
        }

        #region public
        public void LoadAssets(string name)
        {
            contentLoader.LoadLocation(name);
        }

        public void InsertScreen(GameScreen gameScreen)
        {
            gameScreen.GameScreenManager = this;
            gameScreen.LoadContent();
            ScreenList.Add(gameScreen);
        }

        public void RemoveScreen(GameScreen gameScreen)
        {
            ScreensToDraw.Remove(gameScreen);
            ScreenList.Remove(gameScreen);
            gameScreen.UnloadContent();
        }

        public void GoToMenu()
        {
            UnloadContent();
            ScreenList.Clear();
            InsertScreen(new MenuScreen());
        }

        public void DarkenScreen()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(blank, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White * 0.5f);
            spriteBatch.End();
        }

        public void FadeScreen(byte alpha)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(blank, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), new Color((byte)0, (byte)0, (byte)0, (byte)alpha));
            spriteBatch.End();
        }
        #endregion
    }
}
