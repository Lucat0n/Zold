using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zold.Utilities
{
    static class ResolutionManager
    {
        public static readonly Vector2 DefaultMapResolution = new Vector2(800, 480);
        public static readonly Vector2 DefaultCombatResolution = new Vector2(640, 416);

        public static Vector2 CurrentMapResolution { get; set; } = DefaultMapResolution;
        public static Vector2 CurrentCombatResolution { get; set; } = DefaultCombatResolution;
    }
}
