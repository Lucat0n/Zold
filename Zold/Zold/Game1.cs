using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using Zold.Utilities;
using Zold.Screens;

namespace Zold
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        GameScreenManager gameScreenManager;
        ContentLoader contentLoader;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            contentLoader = new ContentLoader(this, Content);
            base.Initialize();

            IsMouseVisible = true;
            Window.AllowUserResizing = false;
        }

        protected override void LoadContent()
        {
            contentLoader.LoadLocation("menu");
            contentLoader.LoadLocation("placeholders");
            gameScreenManager = new GameScreenManager(this);
           // gameScreenManager.InsertScreen(new Screens.Implemented.SplashScreen());
           // gameScreenManager.InsertScreen(new Screens.Implemented.Map.MapManager());
            gameScreenManager.InsertScreen(new Screens.Implemented.MenuScreen());
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            gameScreenManager.Update(gameTime);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            gameScreenManager.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
