using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.CombatObjects
{
    class Obstacle : CombatObject
    {
        public Obstacle(Vector2 Position, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int width, int height) : base(Position, SpriteBatchSpriteSheet, width, height)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatchSpriteSheet.Begin();

            SpriteBatchSpriteSheet.Draw(Position, 0, 0);

            SpriteBatchSpriteSheet.End();
        }
    }
}
