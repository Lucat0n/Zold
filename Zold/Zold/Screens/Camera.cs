using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zold.Screens
{
    class Camera : SpriteBatch
    {
        private Matrix Transform { get; set; }

        public Camera(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {

        }
        
        public void Follow(Vector2 spritePosition,
            int screenHeight, int screenWidth,
            int spriteHeight, int spriteWidth)
        {
            var position = Matrix.CreateTranslation(
                -spritePosition.X - (spriteWidth / 2),
                -spritePosition.Y - (spriteHeight / 2),
                0);

            var offset = Matrix.CreateTranslation(
                screenWidth / 2,
                screenHeight / 2,
                0);

            Transform = position * offset;
        }


        public void BeginFilming()
        {
            Begin(transformMatrix: Transform);
        }

        public void EndFilming()
        {
            End();
        } 
    }
}
