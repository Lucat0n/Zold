using Microsoft.Xna.Framework;
using Zold.Statistics;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.CombatObjects
{
    abstract class CombatObject
    {
        public CombatScreen CombatScreen;
        public Vector2 Position;
        public Vector2 TopPosition;
        public Vector2 CenterPosition;
        public Vector2 BottomPosition;
        public SpriteBatchSpriteSheet SpriteBatchSpriteSheet;
        public Stats Statistics;
        public float BaseSpeed { set; protected get; }
        public CombatObject Map { set; protected get; }
        protected Vector2 tempPosition;
        protected float rotation;
        protected float layerDepth;
        protected float scale;
        protected string direction;
        protected int height;
        protected int width;

        public CombatObject(Vector2 Position, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int width, int height)
        {
            this.Position = Position;
            this.SpriteBatchSpriteSheet = SpriteBatchSpriteSheet;

            this.height = height;
            this.width = width;
            
            TopPosition = new Vector2(Position.X + this.width / 2, Position.Y);
            CenterPosition = new Vector2(Position.X + this.width / 2, Position.Y + this.height / 2);
            BottomPosition = new Vector2(Position.X + this.width / 2, Position.Y + this.height);
        }

        public abstract void Draw(GameTime gameTime);
        public abstract void Update(GameTime gameTime);

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
            result = new Vector2(vector2.X - vector1.X, vector2.Y - vector1.Y);
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

        public bool CheckBoxCollision(Vector2 point, CombatObject target)
        {
            if (Position.X < point.X + target.width &&
                Position.X + this.width > point.X &&
                Position.Y < point.Y + target.height &&
                Position.Y + this.height > point.Y)
                return true;
            return false;
        }

        public bool CheckPointCollision(Vector2 point)
        {
            if ((Position.X < point.X) && (Position.X + width > point.X) &&
                (Position.Y < point.Y) && (Position.Y + height > point.Y))
                return true;
            return false;
        }

        protected void UpdatePosition(float x, float y)
        {
            Position.X += x;
            Position.Y += y;
            TopPosition.Y += y;
            TopPosition.X += x;
            CenterPosition.X += x;
            CenterPosition.Y += y;
            BottomPosition.X += x;
            BottomPosition.Y += y;
        }

        protected void UpdatePosition(Vector2 direction)
        {
            Position += direction;
            TopPosition += direction;
            CenterPosition += direction;
            BottomPosition += direction;
        }

        protected float GetSpeed()
        {
            return BaseSpeed * Statistics.Speed;
        }
    }
}
