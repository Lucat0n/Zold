using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolizje
{
    public interface ICollisionObject
    {
        BoundingBox BoundingBox { get; }

        void UpdateBoundingBoxMinMaxPoints();

        bool CheckCollsionWithObject(ICollisionObject collisionObject);
    }
}
