using Kolizje.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Kolizje
{
    class GraphicRectangle : GraphicObject, ICollisionWithBoundingBox
    {
        private BoundingBox boundingBox = new BoundingBox(new Vector3(0, 0, 0), new Vector3(0, 0, 0));

        public BoundingBox BoundingBox { get { return boundingBox; } }


        public GraphicRectangle(GraphicsDevice graphicsDevice, int width, int height, Color color) : base(graphicsDevice, width, height)
        {
            FillWithColor(width, height, color);
        }

        protected override void FillWithColor(int width, int height, Color color)
        {
            {
                pixels = new Color[width * height];

                for (int i = 0; i < width * height; i++)
                    pixels[i] = color;

                SetData(pixels);
            }
        }

        public override void ChangePosition(Vector2 vector)
        {
            base.ChangePosition(vector);
            UpdateBoundingBoxMinMaxPoints();
        }

        public void UpdateBoundingBoxMinMaxPoints()
        {
            boundingBox.Min = new Vector3(position.X, position.Y, 0);
            boundingBox.Max = new Vector3(position.X + Width, position.Y + Height, 0);
        }

        public bool CheckCollsionWithBoundingBox(BoundingBox TargetBoundingBox)
        {
            return boundingBox.Intersects(TargetBoundingBox);
        }

        
    }
}
