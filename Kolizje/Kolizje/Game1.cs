using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Kolizje
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private List<IGraphicObject> rectangles = new List<IGraphicObject>();
        private GraphicRectangle movingRectangle;
        private GraphicRectangle obstacleRectangle;

        private KeyboardMovingKeys keyboardMovingKeys;

        private Vector2 movingRectanglePosition;
        private Color currentBackgroundColor;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            InitializeGraphicElements();

            keyboardMovingKeys = new KeyboardMovingKeys(Keys.W, Keys.D, Keys.S, Keys.A);


            base.Initialize();
        }

        private void InitializeGraphicElements()
        {
            movingRectangle = new GraphicRectangle(graphics.GraphicsDevice, 100, 50, Color.Orange);
            movingRectanglePosition = new Vector2(0, 0);

            obstacleRectangle = new GraphicRectangle(graphics.GraphicsDevice, 50, 50, Color.Black);
            obstacleRectangle.ChangePosition(new Vector2(200, 100));

            rectangles.Add(movingRectangle);
            rectangles.Add(obstacleRectangle);
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
            UpdateMovingRectangle();

            base.Update(gameTime);

        }

        private void UpdateMovingRectangle()
        {
            if (RectangleMovementEventDetected())
                movingRectangle.ChangePosition(movingRectanglePosition);

            if (RectangleCollisionDetected()) currentBackgroundColor = Color.Red;
            else currentBackgroundColor = Color.CornflowerBlue;
        }

        private bool RectangleCollisionDetected()
        {
            return movingRectangle.CheckCollsionWithObject(obstacleRectangle);
        }

        private bool RectangleMovementEventDetected()
        {
            if (Keyboard.GetState().GetPressedKeys().Length > 0)
            {
                switch (keyboardMovingKeys.GetMovementKey(Keyboard.GetState().GetPressedKeys()[0]))
                {
                    case MovingKeyEnum.UP:
                        movingRectanglePosition.Y -= 1;
                        break;
                    case MovingKeyEnum.RIGHT:
                        movingRectanglePosition.X += 1;
                        break;
                    case MovingKeyEnum.DOWN:
                        movingRectanglePosition.Y += 1;
                        break;
                    case MovingKeyEnum.LEFT:
                        movingRectanglePosition.X -= 1;
                        break;
                }
                return true;
            }
            else return false;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(currentBackgroundColor);

            rectangles.ForEach(rectangle => rectangle.Draw());

            base.Draw(gameTime);
        }
    }
}
