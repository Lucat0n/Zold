using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolizje.Collisions
{
    public interface ICollisionWithBoundingBox
    {
        bool CheckCollsionWithBoundingBox(BoundingBox boundingBox);
    }
}
