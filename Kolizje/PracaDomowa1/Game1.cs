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
        int minCircleSize;
        int maxCircleSize;
        int minCircleMass;
        int maxCircleMass;

        List<GraphicCircleWithPhysics> greenBalls = new List<GraphicCircleWithPhysics>();
        List<GraphicCircleWithPhysics> redBalls = new List<GraphicCircleWithPhysics>();
        List<GraphicCircleWithPhysics> allBalls = new List<GraphicCircleWithPhysics>();

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
            var redBallsNumber = 12;
            gravityRatio = 0.02f;
            minCircleSize = 10;
            maxCircleSize = 120;
            minCircleMass = 1;
            maxCircleMass = 100;

            InitializeResolution(currentWindowWidth, currentWindowHeight);
            InitializeBallsColections(greenBallsNumber, redBallsNumber);

            base.Initialize();
        }

        private void InitializeBallsColections(int greenBallsNumber, int redBallsNumber)
        {
            InitializeBallsWithPhysicsList(greenBalls, greenBallsNumber, Color.Green, new Vector2(0, 1));
            InitializeBallsWithPhysicsList(redBalls, redBallsNumber, Color.Red, new Vector2(0, 0));
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

        private void InitializeBallsWithPhysicsList(List<GraphicCircleWithPhysics> list, int ballsNumber, Color color, Vector2 movementVector)
        {
            for (int i = 0; i < ballsNumber; i++)
            {
                var size = random.Next(minCircleSize, maxCircleSize);
                var mass = random.Next(minCircleMass, maxCircleMass);
                list.Add(new GraphicCircleWithPhysics(graphics.GraphicsDevice, size, mass, color, RandomPosition(), movementVector));
            }
        }

        private void RemoveCollisionBetweenBalls(List<GraphicCircleWithPhysics> list)
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
            return new Vector2(random.Next(minCircleSize, currentWindowWidth), random.Next(minCircleSize, currentWindowHeight));
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }


        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            RemoveBalls();

            BallsPhisics();

            base.Update(gameTime);
        }

        private void RemoveBalls()
        {
            RemoveBallsFromListWhenOutOfScreen(allBalls);
            RemoveBallsFromListWhenOutOfScreen(greenBalls);
        }

        private void BallsPhisics()
        {
            foreach (var ball in greenBalls.ToArray())
            {
                ball.Gravity(gravityRatio);
                foreach (var targetBall in allBalls.ToArray())
                {
                    if (ball == targetBall) continue;
                    ball.BounceOnCollisionWithOtherBall(targetBall);
                }
            }
        }

        private void RemoveBallsFromListWhenOutOfScreen(List<GraphicCircleWithPhysics> list)
        {
            foreach (var ball in list.ToArray())
            {
                var diam = ball.Radius * 2;
                var notInHeightOfWindow = ball.Position.Y - diam > currentWindowHeight || ball.Position.Y + diam < 0;
                var notInWidthOfWindow = ball.Position.X - diam > currentWindowWidth || ball.Position.X + diam < 0;
                if (notInHeightOfWindow || notInWidthOfWindow)
                {
                    list.Remove(ball);
                }
            }
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
