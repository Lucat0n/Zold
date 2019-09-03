using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using C3.MonoGame;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BallPhysics
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteBatch HUDSpriteBatch;

        List<Ball> allBalls;
        List<Ball> ballsOut;

        MouseState lastMouseState;
        MouseState currentMouseState;

        SpriteFont HUDFont;

        string explanationText = "Press Left Mouse to place green balls, press Right Mouse to place red balls";

        Random RNG = new Random();

        FrameCounter frameCounter = new FrameCounter();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1000;
            Content.RootDirectory = "Content";


            graphics.SynchronizeWithVerticalRetrace = false; //Vsync
            IsFixedTimeStep = false;
            //TargetElapsedTime = System.TimeSpan.FromMilliseconds(1000.0f / targetFPS);
        }

        protected override void Initialize()
        {

            allBalls = new List<Ball>();
            ballsOut = new List<Ball>();

            this.IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            HUDSpriteBatch = new SpriteBatch(GraphicsDevice);

            HUDFont = Content.Load<SpriteFont>("hudFontSmall");



        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Update FPS Count
            frameCounter.Update((float)gameTime.ElapsedGameTime.TotalSeconds);


            // The active state from the last frame is now old
            lastMouseState = currentMouseState;

            // Get the mouse state relevant for this frame
            currentMouseState = Mouse.GetState();

            if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
            {
                Ball greenBall = new Ball(BallTypes.Green, currentMouseState.Position.ToVector2(), RNG.Next(15, 30), RNG.Next(1, 3), applyGravity: true);
                if (CanDropBall(greenBall))
                {
                    allBalls.Add(greenBall);
                }

            }

            if (lastMouseState.RightButton == ButtonState.Released && currentMouseState.RightButton == ButtonState.Pressed)
            {
                Ball redBall = new Ball(BallTypes.Red, currentMouseState.Position.ToVector2(), RNG.Next(30, 40));
                if (CanDropBall(redBall))
                {
                    allBalls.Add(redBall);
                }

            }


            foreach (Ball ball in allBalls)
            {
                if (ballOutside(ball))
                {
                    ballsOut.Add(ball);
                }
                else
                {
                    ball.Update(gameTime);
                    reactToColisions(ball);
                }

            }

            foreach (Ball outerBall in ballsOut)
            {
                allBalls.Remove(outerBall);
            }


            Debug.Print("FPS: {0}", 1 / (float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach (var ball in allBalls)
            {
                ball.Draw(spriteBatch);
                
            }


            spriteBatch.End();

            HUDSpriteBatch.Begin();
            HUDSpriteBatch.DrawString(HUDFont, explanationText, new Vector2(20, 20), Color.White);
            HUDSpriteBatch.DrawString(HUDFont, "FPS: " + frameCounter.CurrentFramesPerSecond.ToString("0.0"), new Vector2(20, 40), Color.White);
            HUDSpriteBatch.DrawString(HUDFont, "Average FPS: " + frameCounter.AverageFramesPerSecond.ToString("0.0"), new Vector2(20, 60), Color.White);
            HUDSpriteBatch.DrawString(HUDFont, "Number of balls: " + allBalls.Count.ToString(), new Vector2(20, 80), Color.White);

            HUDSpriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        double calculateDistance(Ball b1, Ball b2)
        {
            double num = (b1.Center.X - b2.Center.X);
            double nuv2 = (b1.Center.Y - b2.Center.Y);
            return Math.Sqrt(Math.Pow(num, 2) + Math.Pow(nuv2, 2));
        }

        void reactToColisions(Ball ball)
        {
            foreach (Ball otherBall in allBalls)
            {
                if (ball != otherBall)
                {
                    float length = ball.Radius + otherBall.Radius;
                    float distance = (float)calculateDistance(otherBall, ball);

                    if (distance <= length)
                    {

                        float overlap = (distance - ball.Radius - otherBall.Radius) / 2;

                        // normal
                        float nx = (otherBall.Center.X - ball.Center.X) / distance;
                        float ny = (otherBall.Center.Y - ball.Center.Y) / distance;

                        // tangent
                        float tx = -ny;
                        float ty = nx;

                        //dot produkt tangent
                        float dpTan1 = ball.speed.X * tx + ball.speed.Y * ty;
                        float dpTan2 = otherBall.speed.X * tx + otherBall.speed.Y * ty;

                        //dot produkt normal
                        float dpNorv1 = ball.speed.X * nx + ball.speed.Y * ny;
                        float dpNorv2 = otherBall.speed.X * nx + otherBall.speed.Y * ny;

                        // conservation of momentum
                        float v1 = (dpNorv1 * (otherBall.Mass - otherBall.Mass) + 2.0f * otherBall.Mass * dpNorv2) / (ball.Mass + otherBall.Mass);
                        float v2 = (dpNorv2 * (otherBall.Mass - ball.Mass) + 2.0f * ball.Mass * dpNorv1) / (ball.Mass + otherBall.Mass);

                        float ballPx = ball.Center.X - overlap * (ball.Center.X - otherBall.Center.X) / distance;
                        float ballPy = ball.Center.Y - overlap * (ball.Center.Y - otherBall.Center.Y) / distance;
                        float otherBallPx = otherBall.Center.X + overlap * (ball.Center.X - otherBall.Center.X) / distance;
                        float otherBallPy = otherBall.Center.Y + overlap * (ball.Center.Y - otherBall.Center.Y) / distance;


                        ball.Center = new Vector2(ballPx, ballPy);
                        otherBall.Center = new Vector2(otherBallPx, otherBallPy);

                        ball.speed += new Vector2(tx * dpTan1 + nx * v1, ty * dpTan1 + ny * v1);
                        otherBall.speed += new Vector2(tx * dpTan2 + nx * v2, ty * dpTan2 + ny * v2);

                    }
                }
            }
        }

        bool ballOutside(Ball ball)
        {
            return (ball.Center.Y - ball.Radius > graphics.PreferredBackBufferHeight || ball.Center.Y + ball.Radius < 0
                || ball.Center.X - ball.Radius > graphics.PreferredBackBufferWidth || ball.Center.X + ball.Radius < 0);
        }

        bool CanDropBall(Ball newBall)
        {
            foreach (Ball ball in allBalls)
            {
                if (newBall.IsCollidingWith(ball))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
