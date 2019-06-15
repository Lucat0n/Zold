using System;
using Microsoft.Xna.Framework;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.CombatObjects
{
    class Box : CombatObject
    {
        public Box(Vector2 Position, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int width, int height) : base(Position, SpriteBatchSpriteSheet, width, height)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
