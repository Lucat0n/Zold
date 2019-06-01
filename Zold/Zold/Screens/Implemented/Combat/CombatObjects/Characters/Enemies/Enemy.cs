using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zold.Statistics;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.CombatObjects.Characters.Enemies
{
    abstract class Enemy : Character
    {
        public Player player;
        public Vector2 playerDirection;
        public Vector2 attackPosition;
        public double Distance { get; set; }
        public double AttackStart { get; set; }
        public float AttackEnd { get; set; }

        public Enemy(Player player, Stats statistics, Vector2 position, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int height, int width) : base(position, statistics, SpriteBatchSpriteSheet, height, width)
        {
            this.player = player;

            direction = "Left";
        }

        public abstract void AI(GameTime gameTime);

        protected void Move(Vector2 direction)
        {
            Position.X += direction.X * GetSpeed();
            Position.Y += direction.Y * GetSpeed();
        }

        public bool CheckPointCollision(Vector2 point)
        {
            if ((Position.X < point.X) && (Position.X + width > point.X) &&
                (Position.Y < point.Y) && (Position.Y + height > point.Y))
                return true;
            return false;
        }

        protected void GetPlayerDirection()
        {
            playerDirection = CalcDirection(Position, player.Position);
        }
    }
}