using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zold.Screens.Implemented.Map;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Map
{
    static class MapCamera
    {
        private const float zoom = 2.5f;
        private const float rotation = 0.0f;
        private static Vector2 defaultCameraPosition = new Vector2(0,0);
        private static Vector2 spriteSize = new Vector2(32, 48);
        private static Camera camera = new Camera(zoom, rotation, defaultCameraPosition, spriteSize);

        public static void Follow(Vector2 cameraTargert)
        {
            camera.Follow(cameraTargert, ResolutionManager.CurrentMapResolution.Y, ResolutionManager.CurrentMapResolution.X);
        }

        public static Matrix BindCameraTransformation()
        {
            return camera.Transform();
        }

    }
}
