using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zold.Screens.Implemented.Map;

namespace Zold.Screens
{
    static class ExploreCamera
    {
        private const float zoom = 1.5f;
        private const float rotation = 0.0f;
        private static Vector2 defaultCameraPosition = new Vector2(0,0);
        private static Camera camera = new Camera(zoom, rotation, defaultCameraPosition);

        private static Vector2 cameraTargert = defaultCameraPosition;
        private static float screenHeight = MapManager.CurrentScreenHeight;
        private static float screenWidth = MapManager.CurrentScreenWidth;

        public static void SetTargetToFollow(Vector2 cameraTarget)
        {
            
        }

        public static RefreshCamera()
        {
            camera.Follow(cameraTargert, screenHeight, screenWidth);
        }

        public static
    }
}
