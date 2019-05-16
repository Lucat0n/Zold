using Microsoft.Xna.Framework;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.CombatObjects
{
    class Projectile : CombatObject
    {
        private Vector2 destinationDirections;
        private string owner;

        public Projectile(Vector2 Position, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, Vector2 destinationDirections, int width, int height) : base(Position, SpriteBatchSpriteSheet, width, height)
        {
            this.Position = Position;
            this.destinationDirections = destinationDirections;
            this.SpriteBatchSpriteSheet = SpriteBatchSpriteSheet;
            this.width = width;
            this.height = height;

            speed = 5;
            damage = 1;
        }

        public void Move(GameTime gameTime)
        {
            Position.X += destinationDirections.X * speed;
            Position.Y += destinationDirections.Y * speed;
        }

        public override void Animation(GameTime gameTime)
        {
            SpriteBatchSpriteSheet.Begin();
            CheckDirection();
            if (direction == "Right")
                SpriteBatchSpriteSheet.Draw(Position, 0, 0);
            if (direction == "Left")
                SpriteBatchSpriteSheet.Draw(Position, 1, 0);
            SpriteBatchSpriteSheet.End();
        }
    }
}
