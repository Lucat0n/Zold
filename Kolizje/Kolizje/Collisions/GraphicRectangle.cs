using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Kolizje
{
    class GraphicRectangle : Texture2D, IGraphicObject, ICollisionObject
    {
        private readonly SpriteBatch spriteBatch;
        private Vector2 position = new Vector2(0, 0);
        private BoundingBox boundingBox = new BoundingBox(new Vector3(0, 0, 0), new Vector3(0, 0, 0));

        public BoundingBox BoundingBox { get { return boundingBox; } }


        public GraphicRectangle(GraphicsDevice graphicsDevice, int width, int height, Color color) : base(graphicsDevice, width, height)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            FillWithColor(width, height, color);
        }

        private void FillWithColor(int width, int height, Color color)
        {
            Color[] pixelColors = new Color[width * height];

            for (int i = 0; i < width * height; i++)
            {
                pixelColors[i] = color;
            }

            SetData(pixelColors);
        }

        public void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(this, position, Color.White);
            spriteBatch.End();
        }

        public void ChangePosition(Vector2 vector)
        {
            position = vector;
        }

        public bool CheckCollsionWithObject(ICollisionObject collisionObject)
        {
            UpdateBoundingBoxMinMaxPoints();
            collisionObject.UpdateBoundingBoxMinMaxPoints();

            return boundingBox.Intersects(collisionObject.BoundingBox);
        }

        public void UpdateBoundingBoxMinMaxPoints()
        {
            boundingBox.Min = new Vector3(position.X, position.Y, 0);
            boundingBox.Max = new Vector3(position.X + Width, position.Y + Height, 0);
        }
    }
}
