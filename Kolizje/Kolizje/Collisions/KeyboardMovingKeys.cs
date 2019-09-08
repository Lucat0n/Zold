using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolizje
{
     class KeyboardMovingKeys
    {
        public Dictionary<Keys, MovingKeyEnum> movingKeys = new Dictionary<Keys, MovingKeyEnum>(); 

        public KeyboardMovingKeys(Keys up, Keys right, Keys down, Keys left)
        {
            movingKeys.Add(up, MovingKeyEnum.UP);
            movingKeys.Add(right, MovingKeyEnum.RIGHT);
            movingKeys.Add(down, MovingKeyEnum.DOWN);
            movingKeys.Add(left, MovingKeyEnum.LEFT);
        }

        public MovingKeyEnum GetMovementKey(Keys key)
        {
            return movingKeys.ContainsKey(key) ? movingKeys[key] : MovingKeyEnum.NONE;
        }
       
    }
}
