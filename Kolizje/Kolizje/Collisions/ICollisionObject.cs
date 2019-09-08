using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolizje
{
    interface ICollisionObject
    {
        Rectangle BoundingBox { get;}

        Vector2 BoundingBoxPosition { get;}
        bool CheckCollsionWithObject(ICollisionObject collisionObject);
    }
}
