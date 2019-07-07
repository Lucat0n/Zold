using Microsoft.Xna.Framework;
using System;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters.Enemies;
using Zold.Screens.Implemented.Combat.Utilities;
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
        public BoundingBox HitBox;
        public Vector2 Velocity;
        public int Height;
        public int Width;
        protected string name;
        protected Vector2 tempPosition;
        protected float rotation;
        protected float layerDepth;
        protected float scale;
        protected string direction;

        public CombatObject(Vector2 Position, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int width, int height)
        {
            this.Position = Position;
            this.SpriteBatchSpriteSheet = SpriteBatchSpriteSheet;
            HitBox = new BoundingBox();

            this.Height = height;
            this.Width = width;
            
            TopPosition = new Vector2(Position.X + this.Width / 2, Position.Y);
            CenterPosition = new Vector2(Position.X + this.Width / 2, Position.Y + this.Height / 2);
            BottomPosition = new Vector2(Position.X + this.Width / 2, Position.Y + this.Height);
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
                direction = "Right_" + name;
            else if (Position.X < tempPosition.X)
                direction = "Left_" + name;

            tempPosition = Position;
        }

        public bool CheckBoxCollision(CombatObject target)
        {
            if (Position.X < target.Position.X + target.Width &&
                Position.X + Width > target.Position.X &&
                Position.Y < target.Position.Y + target.Height &&
                Position.Y + Height > target.Position.Y)
                return true;
            return false;
        }

        public bool CheckPointCollision(Vector2 point)
        {
            if ((Position.X < point.X) && (Position.X + Width > point.X) &&
                (Position.Y < point.Y) && (Position.Y + Height > point.Y))
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
            HitBox.Min.X += Math.Sign(x);
            HitBox.Max.X += Math.Sign(x);
            HitBox.Min.Y += Math.Sign(y);
            HitBox.Max.Y += Math.Sign(y);
        }

        protected void UpdatePosition(Vector2 direction)
        {
            Position += direction;
            TopPosition += direction;
            CenterPosition += direction;
            BottomPosition += direction;
            HitBox.Min.X += direction.X;
            HitBox.Max.X += direction.X;
            HitBox.Min.Y += direction.Y;
            HitBox.Max.Y += direction.Y;
        }

        public void SetPosition(Vector2 position)
        {
            Position = position;
            TopPosition = new Vector2(position.X + Width / 2, position.Y);
            CenterPosition = new Vector2(position.X + Width / 2, position.Y + Height / 2);
            BottomPosition = new Vector2(position.X + Width / 2, position.Y + Height);
            HitBox.Min.X = position.X;
            HitBox.Max.X = position.X;
            HitBox.Min.Y = position.Y;
            HitBox.Max.Y = position.Y;
        }

        protected float GetSpeed()
        {
            return BaseSpeed * Statistics.Speed;
        }
    }
}
