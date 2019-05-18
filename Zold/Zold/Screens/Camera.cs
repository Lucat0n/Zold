using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zold.Screens
{
    class Camera 
    {
        private Matrix transform;
        private Vector2 cameraPosition;
        private const float zoom = 1.5f;
        private const float rotation = 0.0f;

        public Camera(Vector2 cameraPosition) => this.cameraPosition = cameraPosition;
        

        public void Follow(Vector2 spritePosition, float screenHeight, float screenWidth)
        {
            UpdateMovement(spritePosition);
            CalculaterTransformation(screenHeight, screenWidth);
        } 
            
        private void UpdateMovement(Vector2 spritePosition)
        {
            cameraPosition.X += ((spritePosition.X - cameraPosition.X)); 
            cameraPosition.Y += ((spritePosition.Y - cameraPosition.Y)); 
        }

        //TODO: usunięcie magic numberów offsetu
        private void CalculaterTransformation(float screenHeight, float screenWidth)
        {
            
            var position = Matrix.CreateTranslation(
                -cameraPosition.X - (screenWidth / 2),
                -cameraPosition.Y - (screenHeight / 2),
                0);

            position *= Matrix.CreateScale(new Vector3(zoom, zoom, 0));

            var offset = Matrix.CreateTranslation(
                screenWidth*1.2f,
                screenHeight * 1.2f,
                0);

            transform = position * offset;

            transform.M41 = (float)Math.Round(transform.M41, 0);
            transform.M42 = (float)Math.Round(transform.M42, 0);

        }

        public Matrix Transform()
        {
            return transform;
        }
      

    }
}
