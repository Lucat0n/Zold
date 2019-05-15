using Microsoft.Xna.Framework;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.CombatObject
{
    abstract class CombatObject
    {
        public Vector2 Position;
        public SpriteBatchSpriteSheet SpriteBatchSpriteSheet;
        protected Vector2 tempPosition;
        protected float rotation;
        protected float layerDepth;
        protected float scale;
        protected string direction;
        protected int damage;
        protected float speed;
        protected int height;
        protected int width;

        public CombatObject(Vector2 Position, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int width, int height)
        {
            this.Position = Position;
            this.SpriteBatchSpriteSheet = SpriteBatchSpriteSheet;

            this.height = height;
            this.width = width;
        }

        public abstract void Animation(GameTime gameTime);

        public void CalculateDepth()
        {
            if (Position.Y >= 450)
                layerDepth = 1.0f;
            else
                layerDepth = (Position.Y - 100) / 350;
        }

        public Vector2 CalcDirection(Vector2 vector1, Vector2 vector2)
        {
            Vector2 result = new Vector2();
            result = new Vector2(vector1.X - vector2.X, vector1.Y - vector2.Y);
            result.Normalize();
            return result;
        }

        public void CheckDirection()
        {
            if (Position.X > tempPosition.X)
                direction = "Right";
            else if (Position.X < tempPosition.X)
                direction = "Left";

            tempPosition = Position;
        }

        public bool CheckBoxCollision(Vector2 point, int height, int width)
        {
            if (Position.X < point.X + width &&
                Position.X + this.width > point.X &&
                Position.Y < point.Y + height &&
                Position.Y + this.height > point.Y)
                return true;
            return false;
        }
    }
}
