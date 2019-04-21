using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using Zold.Screens;
using Zold.Screens.Implemented;

namespace Zold
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        GameScreenManager gameScreenManager;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            gameScreenManager = new GameScreenManager(this);
            base.Initialize();

            IsMouseVisible = true;
            Window.AllowUserResizing = false;
        }

        protected override void LoadContent()
        {
            gameScreenManager.InsertScreen(new SplashScreen());

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
