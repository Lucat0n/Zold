using Kolizje;
using Kolizje.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracaDomowa1.CircleCollisons
{
    class GraphicCircle : GraphicObject, ICollisionObject
    {

        public GraphicCircle(GraphicsDevice graphicsDevice, int width, int height, Color color) : base(graphicsDevice, width, height)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            FillWithColor(width, height, color);
        }

        public BoundingBox BoundingBox => throw new NotImplementedException();

        public void ChangePosition(Vector2 vector2)
        {
            throw new NotImplementedException();
        }

        public bool CheckCollsionWithObject(ICollisionObject collisionObject)
        {
            throw new NotImplementedException();
        }

        public void Draw()
        {
            throw new NotImplementedException();
        }

        public void UpdateBoundingBoxMinMaxPoints()
        {
            throw new NotImplementedException();
        }
    }
}
