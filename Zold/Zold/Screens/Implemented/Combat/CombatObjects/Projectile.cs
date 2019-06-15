using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters;
using Zold.Screens.Implemented.Combat.Skills;
using Zold.Statistics;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.CombatObjects
{
    class Projectile : CombatObject
    {
        public List<Character> Targets;
        private Vector2 destinationDirections;
        public Skill Skill;

        public Projectile(Vector2 Position, Vector2 destinationDirections, Skill Skill, int damage, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int width, int height) : base(Position, SpriteBatchSpriteSheet, width, height)
        {
            this.Position = Position;
            this.destinationDirections = destinationDirections;
            this.Skill = Skill;
            this.SpriteBatchSpriteSheet = SpriteBatchSpriteSheet;
            this.width = width;
            this.height = height;
            Statistics = new Stats(0, 0, damage, 200, 0, 0);

            Targets = new List<Character>();
        }

        public override void Update(GameTime gameTime)
        {
            UpdatePosition(destinationDirections);
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
