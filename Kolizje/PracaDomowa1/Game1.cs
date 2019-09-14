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
        List<GraphicCircle> allBalls = new List<GraphicCircle>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            var greenBallsNumber = 4;
            var redBallsNumber = 6;

            InitializeBallList(greenBalls, greenBallsNumber, Color.Green);
            InitializeBallList(redBalls, redBallsNumber, Color.Red);
            allBalls.AddRange(greenBalls);
            allBalls.AddRange(redBalls);

            RemoveCollisionBetweenBalls(allBalls);
            

            base.Initialize();
        }

        private void InitializeBallList(List<GraphicCircle> list, int ballsNumber, Color color)
        {
            for (int i = 0; i < ballsNumber; i++)
            {
                list.Add(new GraphicCircle(graphics.GraphicsDevice, random.Next(10, 100), color, RandomPosition()));
            }

        }

        private void RemoveCollisionBetweenBalls(List<GraphicCircle> list)
        {
            while (true)
            {
                var allSepareted = true;
                foreach (var i in list)
                {
                    foreach (var j in list)
                    {
                        if (i == j) continue;
                        var collisionDetected =  i.CheckCollisionWithBoundingSphere(j.BoundingSphere);
                        if (collisionDetected)
                        {
                            allSepareted = false;
                            i.ChangePosition(RandomPosition());
                        }
                    }
                }
                if (allSepareted) break;
            }
        }


        private Vector2 RandomPosition()
        {
            return new Vector2(random.Next(5, graphics.PreferredBackBufferWidth), random.Next(5, graphics.PreferredBackBufferHeight));
        }

        private Vector2 TranslateVectorByRadius(Vector2 vector, int radius)
        {
            return new Vector2(vector.X + radius, vector.Y + radius) * 2;
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
