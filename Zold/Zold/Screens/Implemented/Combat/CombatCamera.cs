using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zold.Screens.Implemented.Combat
{
    static class CombatCamera
    {
        private const float zoom = 2.5f;
        private const float rotation = 0.0f;
        private static Vector2 defaultCameraPosition = new Vector2(0, 0);
        private static Vector2 spriteSize = new Vector2(32, 48);
        private static Camera camera = new Camera(zoom, rotation, defaultCameraPosition, spriteSize);

        //private static float screenHeight = MapManager.CurrentScreenHeight;
        //private static float screenWidth = MapManager.CurrentScreenWidth;

        public static void Follow(Vector2 cameraTargert)
        {
            //screenHeight = MapManager.CurrentScreenHeight;
            //screenWidth = MapManager.CurrentScreenWidth;
            //camera.Follow(cameraTargert, screenHeight, screenWidth);
        }

        public static Matrix BindCameraTransformation()
        {
            return camera.Transform();
        }
    }
}
