using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolizje
{
    class GraphicRectangle : Texture2D, IGraphicObject, ICollisionObject
    {
        private readonly SpriteBatch spriteBatch;
        private Vector2 position = new Vector2(0, 0);

        public Rectangle BoundingBox { get { return Bounds; } }
        public Vector2 BoundingBoxPosition { get { return position; } }

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
            var collisionBox = collisionObject.BoundingBox;

            var xCollision = BoundingBoxPosition.X > collisionObject.BoundingBoxPosition.X && BoundingBoxPosition.X < collisionObject.BoundingBoxPosition.X + collisionBox.Width ? true : false;
            var yCollision = BoundingBoxPosition.Y > collisionObject.BoundingBoxPosition.Y && BoundingBoxPosition.Y < collisionObject.BoundingBoxPosition.Y + collisionBox.Height ? true : false;

            return xCollision && yCollision ? true : false;
        }
    }
}
