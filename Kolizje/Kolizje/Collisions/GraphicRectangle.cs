using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Kolizje
{
    class GraphicRectangle : GraphicObject, ICollisionObject
    {
        private BoundingBox boundingBox = new BoundingBox(new Vector3(0, 0, 0), new Vector3(0, 0, 0));

        public BoundingBox BoundingBox { get { return boundingBox; } }


        public GraphicRectangle(GraphicsDevice graphicsDevice, int width, int height, Color color) : base(graphicsDevice, width, height, color)
        {
            
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
