using Microsoft.Xna.Framework;
using System.Timers;
using Zold.Screens.Implemented.Combat.Skills;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.CombatObjects.Characters.Enemies
{
    class Ranged : Enemy
    {
        public Vector2 CenterPosition;
        private Skill skill;

        public Ranged(Player player, Vector2 position, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int width, int height) : base(player, position, SpriteBatchSpriteSheet, width, height)
        {
            CenterPosition = new Vector2(position.X + this.width / 2, position.Y + this.height / 2);
            skill = new Skill(CombatScreen);
        }

        public override void AI(GameTime gameTime)
        {
            CenterPosition = new Vector2(Position.X + this.width / 2, Position.Y + this.height / 2);
            CalculateDepth();
            CheckDirection();
            speed = 60f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            playerDirection = CalcDirection(player.Position, Position);
            Distance = Vector2.Distance(new Vector2(player.BottomPosition.X, player.BottomPosition.Y - height), Position);

            if (Distance <= 600)
            {
                skill.Destination = CalcDirection(player.CenterPosition, CenterPosition);
                skill.CombatScreen = CombatScreen;
                skill.StartPosition = CenterPosition;
                skill.Use();
            }
            else
            {
                action = "Moving";
                Move();
            }
        }

        public override void Animation(GameTime gameTime)
        {
            SpriteBatchSpriteSheet.Begin();

            SpriteBatchSpriteSheet.Draw(Position, 11, 0);

            SpriteBatchSpriteSheet.End();
        }

        public override void Move()
        {
            Position.X += playerDirection.X * speed;
            Position.Y += playerDirection.Y * speed;
        }
    }
}
