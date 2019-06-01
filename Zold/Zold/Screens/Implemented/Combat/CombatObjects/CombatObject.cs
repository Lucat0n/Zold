using Microsoft.Xna.Framework;
using Zold.Statistics;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.CombatObjects
{
    abstract class CombatObject
    {
        public Vector2 Position;
        public SpriteBatchSpriteSheet SpriteBatchSpriteSheet;
        public Stats Statistics;
        public float BaseSpeed { set; protected get; }
        protected Vector2 tempPosition;
        protected float rotation;
        protected float layerDepth;
        protected float scale;
        protected string direction;
        protected int height;
        protected int width;
        protected readonly int topMapEdge;
        protected readonly int bottomMapEdge;
        protected readonly int rightMapEdge;
        protected readonly int leflMapEdge;

        public CombatObject(Vector2 Position, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int width, int height)
        {
            this.Position = Position;
            this.SpriteBatchSpriteSheet = SpriteBatchSpriteSheet;

            topMapEdge = 150;
            bottomMapEdge = 450;
            rightMapEdge = 750;
            leflMapEdge = 0;

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

        protected float GetSpeed()
        {
            return BaseSpeed * Statistics.Speed;
        }
    }
}
