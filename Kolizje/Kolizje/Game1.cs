using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Kolizje
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Texture2D rect;
        private List<IGraphicObject> rectangles = new List<IGraphicObject>();
        private GraphicRectangle movingRectangle;
        private GraphicRectangle obstacleRectangle;

        private Vector2 rectanglePosition;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            InitializeGraphicElements();

            base.Initialize();
        }

        private void InitializeGraphicElements()
        {
            movingRectangle = new GraphicRectangle(graphics.GraphicsDevice, 100, 50, Color.Orange);
            rectanglePosition = new Vector2(0, 0);

            obstacleRectangle = new GraphicRectangle(graphics.GraphicsDevice, 50, 50, Color.Black);
            obstacleRectangle.UpdatePosition(new Vector2(200, 100));

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            movingRectangle.UpdatePosition(rectanglePosition);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            rectangles.ForEach(rectangle => rectangle.Draw());

            base.Draw(gameTime);
        }
    }
}
