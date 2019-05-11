using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.Characters.Enemies
{
    abstract class Enemy : Character
    {
        public Player player;
        public Vector2 playerDirection;
        public Vector2 attackPosition;
        public double Distance { get; set; }
        public double AttackStart { get; set; }
        public float AttackEnd { get; set; }

        public Enemy(Player player, Vector2 position, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int height, int width) : base(position, SpriteBatchSpriteSheet, height, width)
        {
            this.player = player;

            direction = "Left";
        }

        public abstract void AI(GameTime gameTime);

        public abstract void Move();

        public abstract void Animation();

        public Vector2 CalcDirection(Vector2 vector1, Vector2 vector2)
        {
            Distance = Vector2.Distance(vector1, vector2);
            Vector2 result = new Vector2();
            result = new Vector2(vector1.X - vector2.X, vector1.Y - vector2.Y);
            result.Normalize();
            return result;
        }

        public bool CheckPointCollision(Vector2 point)
        {
            if ((Position.X < point.X) && (Position.X + width > point.X) &&
                (Position.Y < point.Y) && (Position.Y + height > point.Y))
                return true;
            return false;
        }

        public bool CheckBoxCollision(Vector2 point, int height, int width)
        {
            if (Position.X < point.X + width &&
                Position.X + base.width > point.X &&
                Position.Y < point.Y + height &&
                Position.Y + base.height > point.Y)
                return true;
            return false;
        }
    }
}