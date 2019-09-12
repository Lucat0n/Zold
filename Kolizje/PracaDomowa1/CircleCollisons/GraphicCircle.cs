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


        public GraphicCircle(GraphicsDevice graphicsDevice, int radius, Color color) : base(graphicsDevice, radius, radius)
        {
            FillWithColor(radius, radius, color);
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


        public bool CheckCollisionWithBoundingSphere(BoundingSphere boundingSphere)
        {
            throw new NotImplementedException();
        }

        
    }
}
