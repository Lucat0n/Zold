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
    class GraphicCircle : GraphicObject, ICollisionWithBoundinSphere
    {

        public BoundingBox BoundingBox => throw new NotImplementedException();


        public GraphicCircle(GraphicsDevice graphicsDevice, int width, int height, Color color) : base(graphicsDevice, width, height)
        {
        }

        protected override void FillWithColor(int width, int height, Color color)
        {
            base.FillWithColor(width, height, color);
        }



        public bool CheckCollsionWithObject(ICollisionWithBoundingBox collisionObject)
        {
            throw new NotImplementedException();
        }

        public bool CheckCollisionWithBoundingSphere(BoundingSphere boundingSphere)
        {
            throw new NotImplementedException();
        }
    }
}
