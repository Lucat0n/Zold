using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat
{
    static class CombatCamera
    {
        private const float zoom = 1.1f;
        private const float rotation = 0.0f;
        private static Vector2 defaultCameraPosition = new Vector2(0, 0);
        //private static Vector2 spriteSize = new Vector2(ResolutionManager.CurrentCombatResolution.X, ResolutionManager.CurrentCombatResolution.Y);
        private static Vector2 spriteSize = new Vector2(32, 48);
        private static Camera camera = new Camera(zoom, rotation, defaultCameraPosition, spriteSize);

        //private static Vector2 playerMovement = new Vector2(0, ResolutionManager.CurrentMapResolution.Y / 2);
        public static void Follow(Vector2 playerPosition)
        {
            //playerMovement.X = playerPosition.X;
            //camera.Follow(playerMovement, ResolutionManager.CurrentCombatResolution.Y, ResolutionManager.CurrentCombatResolution.X);
            camera.Follow(playerPosition, ResolutionManager.CurrentMapResolution.Y, ResolutionManager.CurrentMapResolution.X);
        }
        public static Matrix BindCameraTransformation()
        {
            return camera.Transform();
        }
    }
}
