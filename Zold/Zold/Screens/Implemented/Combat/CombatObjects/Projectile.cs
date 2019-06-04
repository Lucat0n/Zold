using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters;
using Zold.Statistics;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.CombatObjects
{
    class Projectile : CombatObject
    {
        public List<Character> Targets;
        private Vector2 destinationDirections;

        public Projectile(Vector2 Position, int damage, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, Vector2 destinationDirections, int width, int height) : base(Position, SpriteBatchSpriteSheet, width, height)
        {
            this.Position = Position;
            this.destinationDirections = destinationDirections;
            this.SpriteBatchSpriteSheet = SpriteBatchSpriteSheet;
            this.width = width;
            this.height = height;
            Statistics = new Stats(0, 0, damage, 200, 0, 0);

            Targets = new List<Character>();
        }

        public void Move(GameTime gameTime)
        {
            Position.X += destinationDirections.X * GetSpeed();
            Position.Y += destinationDirections.Y * GetSpeed();
        }

        public override void Draw(GameTime gameTime)
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
