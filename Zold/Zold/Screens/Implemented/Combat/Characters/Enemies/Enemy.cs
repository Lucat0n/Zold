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
            if ((position.X < point.X) && (position.X + Width > point.X) &&
                (position.Y < point.Y) && (position.Y + Height > point.Y))
                return true;
            return false;
        }

        public bool CheckBoxCollision(Vector2 point, int height, int width)
        {
            if (position.X < point.X + width &&
                position.X + Width > point.X &&
                position.Y < point.Y + height &&
                position.Y + Height > point.Y)
                return true;
            return false;
        }

        public Texture2D GetTexture()
        {
            return texture;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public Vector2 GetDirection()
        {
            return playerDirection;
        }
    }
}