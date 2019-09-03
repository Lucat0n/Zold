using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolizje
{
    interface IGraphicObject
    {
        void ChangePosition(Vector2 vector2);
        void Draw();
    }
}
