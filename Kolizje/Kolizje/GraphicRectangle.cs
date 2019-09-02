using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolizje
{
    class GraphicRectangle : Texture2D, IGraphicObject
    {
        private readonly SpriteBatch spriteBatch;
        private Vector2 position;

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

        public void UpdatePosition(Vector2 vector)
        {
            position = vector;
        }
    }
}
