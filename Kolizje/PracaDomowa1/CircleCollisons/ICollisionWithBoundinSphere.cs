using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracaDomowa1.CircleCollisons
{
    public interface ICollisionWithBoundinSphere
    {
        bool CheckCollisionWithBoundingSphere(BoundingSphere boundingSphere);
    }
}
