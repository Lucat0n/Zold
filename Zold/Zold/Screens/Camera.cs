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
        private float zoom;
        private float rotation;
        private Vector2 targetSize;

        public Camera(float zoom, float rotation, Vector2 cameraPosition, Vector2 targetSize)
        {
            this.zoom = zoom;
            this.rotation = rotation;
            this.cameraPosition = cameraPosition;
            this.targetSize = targetSize;
        }

        public Camera(Vector2 cameraPosition)
        {
            this.cameraPosition = cameraPosition;
        }

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

        private void CalculaterTransformation(float screenHeight, float screenWidth)
        {

            var position = Matrix.CreateTranslation(
                -cameraPosition.X - targetSize.X / 2,
                -cameraPosition.Y - targetSize.Y / 2,
                0);

            var offset = Matrix.CreateTranslation(
                screenWidth / 2,
                screenHeight / 2,
                0);

            position *= Matrix.CreateScale(new Vector3(zoom, zoom, 1));

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
