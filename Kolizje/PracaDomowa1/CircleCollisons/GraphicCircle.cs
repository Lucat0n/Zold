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
        private BoundingSphere boundingSphere;
        private int radius;
        private int mass;
        public BoundingSphere BoundingSphere { get { return boundingSphere; } }
        public Vector2 Position { get { return position; } } 

        public int Radius { get { return radius; } }


        public GraphicCircle(GraphicsDevice graphicsDevice, int radius, Color color) : base(graphicsDevice, radius, radius)
        {
            this.radius = radius;
            mass = radius;
            FillWithColor(radius, radius, color);
            boundingSphere = new BoundingSphere(new Vector3(0, 0, 0), radius);
        }

        public GraphicCircle(GraphicsDevice graphicsDevice, int radius, Color color, Vector2 position) : this(graphicsDevice, radius, color)
        {
            ChangePosition(position);
        }

        protected override void FillWithColor(int width, int height, Color color)
        {
            var radius = width;
            var colorData = new Color[radius * radius];

            float diam = radius / 2f;
            float diamsq = diam * diam;

            for (int x = 0; x < radius; x++)
            {
                for (int y = 0; y < radius; y++)
                {
                    int index = x * radius + y;
                    Vector2 pos = new Vector2(x - diam, y - diam);
                    if (pos.LengthSquared() <= diamsq)
                    {
                        colorData[index] = color;
                    }
                    else
                    {
                        colorData[index] = Color.Transparent;
                    }
                }
            }

            SetData(colorData);
        }

        public override void ChangePosition(Vector2 vector)
        {
            base.ChangePosition(vector);
            UpdateBoundingSpherePosition();
        }

        private void UpdateBoundingSpherePosition()
        {
            boundingSphere.Center = new Vector3(position.X, position.Y, 0);
        }

        public bool CheckCollisionWithBoundingSphere(BoundingSphere TargetBoundingSphere)
        {
            return boundingSphere.Intersects(TargetBoundingSphere);
        }

        
    }
}
