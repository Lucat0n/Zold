using Microsoft.Xna.Framework;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat
{
    class Projectile
    {
        public Vector2 Position;
        public SpriteBatchSpriteSheet SpriteBatchSpriteSheet;
        private Vector2 destinationDirections;
        private Vector2 tempPosition;
        private float rotation;
        private string direction;
        private string owner;
        private int damage;
        private float speed;
        private int height;
        private int width;

        public Projectile(Vector2 Position, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, Vector2 destinationDirections, int width, int height)
        {
            this.Position = Position;
            this.destinationDirections = destinationDirections;
            this.SpriteBatchSpriteSheet = SpriteBatchSpriteSheet;
            this.width = width;
            this.height = height;

            speed = 5;
            damage = 1;
        }

        public void Move(GameTime gameTime)
        {
            Position.X += destinationDirections.X * speed;
            Position.Y += destinationDirections.Y * speed;
        }

        public void Animation(GameTime gameTime)
        {
            SpriteBatchSpriteSheet.Begin();
            CheckDirection();
            if (direction == "Right")
                SpriteBatchSpriteSheet.Draw(Position, 0, 0);
            if (direction == "Left")
                SpriteBatchSpriteSheet.Draw(Position, 1, 0);
            SpriteBatchSpriteSheet.End();
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
