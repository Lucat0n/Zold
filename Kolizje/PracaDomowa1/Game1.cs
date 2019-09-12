using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PracaDomowa1.CircleCollisons;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PracaDomowa1
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random random = new Random();

        List<GraphicCircle> greenBalls = new List<GraphicCircle>();
        List<GraphicCircle> redBalls = new List<GraphicCircle>();
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            var greenBallsNumber = 5;
            var redBallsNumber = 4;

            InitializeBallList(greenBalls, greenBallsNumber, Color.Green);
            InitializeBallList(redBalls, redBallsNumber, Color.Red);

            base.Initialize();
        }

        private void InitializeBallList(List<GraphicCircle> list, int ballsNumber, Color color)
        {
            for (int i = 0; i < ballsNumber; i++)
            {
                list.Add(new GraphicCircle(graphics.GraphicsDevice, random.Next(10, 100), color, RandomVector2()));
            }

        }

        private Vector2 RandomVector2()
        {
            return new Vector2(random.Next(5, 600), random.Next(5, 200));
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            greenBalls.ForEach(i => i.Draw());
            redBalls.ForEach(i => i.Draw());

            base.Draw(gameTime);
        }
    }
}
