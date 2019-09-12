using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolizje
{
    public abstract class GraphicObject : Texture2D
    {
        protected readonly SpriteBatch spriteBatch;
        protected Vector2 position = new Vector2(0, 0);

        public GraphicObject(GraphicsDevice graphicsDevice, int width, int height, Color color) : base(graphicsDevice, width, height)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            FillWithColor(width, height, color);
        }
        protected void FillWithColor(int width, int height, Color color)
        {
            Color[] pixelColors = new Color[width * height];

            for (int i = 0; i < width * height; i++)
            {
                pixelColors[i] = color;
            }

            SetData(pixelColors);
        }
        public virtual void ChangePosition(Vector2 vector)
        {
            position = vector;
        }
        public virtual void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(this, position, Color.White);
            spriteBatch.End();
        }


    }
}
