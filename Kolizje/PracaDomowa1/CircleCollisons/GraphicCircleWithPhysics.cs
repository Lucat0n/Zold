using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracaDomowa1.CircleCollisons
{
    class GraphicCircleWithPhysics : GraphicCircle
    {
        public int Mass { get; private set; }

        public GraphicCircleWithPhysics(GraphicsDevice graphicsDevice, int radius, Color color) : base(graphicsDevice, radius, color)
        {
            Mass = radius;
        }

        public GraphicCircleWithPhysics(GraphicsDevice graphicsDevice, int radius, Color color, Vector2 position) : this(graphicsDevice, radius, color)
        {
            ChangePosition(position);
        }

        public void Gravity(int magnitude)
        {
            ChangePosition(new Vector2(position.X, position.Y + magnitude));
        }
    }
}
