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

        int currentWindowWidth;
        int currentWindowHeight;

        List<GraphicCircleWithPhysics> greenBalls = new List<GraphicCircleWithPhysics>();
        List<GraphicCircle> redBalls = new List<GraphicCircle>();
        List<GraphicCircle> allBalls = new List<GraphicCircle>();
        //List<GraphicCircleWithPhysics> yellowBalls = new List<GraphicCircleWithPhysics>();

        float gravityRatio;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            currentWindowWidth = 800;
            currentWindowHeight = 1000;
            var greenBallsNumber = 10;
            var redBallsNumber = 6;
            gravityRatio = 0.02f;

            //yellowBalls.Add(new GraphicCircleWithPhysics(graphics.GraphicsDevice, 50, Color.Yellow, new Vector2(200,200)));
            //yellowBalls.Add(new GraphicCircleWithPhysics(graphics.GraphicsDevice, 50, Color.Yellow, new Vector2(250,100)));
            //yellowBalls[0].BounceOnCollisionWithOtherBall(yellowBalls[1]);

            InitializeResolution(currentWindowWidth, currentWindowHeight);
            InitializeBallsColections(greenBallsNumber, redBallsNumber);


            //Console.WriteLine($"{redBalls[0].BoundingSphere.Center.X} {redBalls[0].BoundingSphere.Center.Y}, {redBalls[0].Position.X} {redBalls[0].Position.Y} ");
            //Console.WriteLine($"{redBalls[0].Radius} {redBalls[0].BoundingSphere.Radius}");

            base.Initialize();
        }

        private void InitializeBallsColections(int greenBallsNumber, int redBallsNumber)
        {
            InitializeBallsWithPhysicsList(greenBalls, greenBallsNumber, Color.Green);
            InitializeBallList(redBalls, redBallsNumber, Color.Red);
            allBalls.AddRange(greenBalls);
            allBalls.AddRange(redBalls);

            RemoveCollisionBetweenBalls(allBalls);
        }

        private void InitializeResolution(int width, int height)
        {
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.ApplyChanges();
        }

        private void InitializeBallList(List<GraphicCircle> list, int ballsNumber, Color color)
        {
            for (int i = 0; i < ballsNumber; i++)
            {
                list.Add(new GraphicCircle(graphics.GraphicsDevice, random.Next(10, 100), color, RandomPosition()));
            }
        }

        private void InitializeBallsWithPhysicsList(List<GraphicCircleWithPhysics> list, int ballsNumber, Color color)
        {
            for (int i = 0; i < ballsNumber; i++)
            {
                list.Add(new GraphicCircleWithPhysics(graphics.GraphicsDevice, random.Next(10, 100), color, RandomPosition()));
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
                        var collisionDetected = i.CheckCollisionWithBoundingSphere(j.BoundingSphere);
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
            return new Vector2(random.Next(5, currentWindowWidth), random.Next(5, currentWindowHeight));
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
            RemoveBallsWhenOutOfScreen();

            foreach (var ball in greenBalls.ToArray())
            {
                ball.Gravity(gravityRatio);
                //if (gameTime.TotalGameTime.TotalSeconds % 1 > 0.5)
                    foreach (var targetBall in allBalls.ToArray())
                    {
                        if (ball == targetBall) continue;
                        ball.BounceOnCollisionWithOtherBall(targetBall);
                    }
            }



            base.Update(gameTime);
        }

        private void RemoveBallsWhenOutOfScreen()
        {
            foreach (var ball in allBalls.ToArray())
            {
                if (ball.Position.Y - ball.Radius > currentWindowHeight) allBalls.Remove(ball);
            }
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            greenBalls.ForEach(i => i.Draw());
            redBalls.ForEach(i => i.Draw());
            //yellowBalls.ForEach(i => i.Draw());

            base.Draw(gameTime);
        }
    }
}
