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
        protected Color[] pixels;

        public GraphicObject(GraphicsDevice graphicsDevice, int width, int height) : base(graphicsDevice, width, height)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
        }

        protected abstract void FillWithColor(int width, int height, Color color);
        
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
