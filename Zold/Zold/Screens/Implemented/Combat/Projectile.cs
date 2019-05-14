using Microsoft.Xna.Framework;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat
{
    class Projectile
    {
        public Vector2 Position;
        public SpriteBatchSpriteSheet SpriteBatchSpriteSheet;
        private Vector2 direction;
        private float rotation;
        private int damage;
        private float speed;
        private int height;
        private int width;

        public Projectile(Vector2 Position, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, Vector2 direction, int width, int height)
        {
            this.Position = Position;
            this.SpriteBatchSpriteSheet = SpriteBatchSpriteSheet;
            this.width = width;
            this.height = height;
        }

        public void Move(GameTime gameTime)
        {
            Position += direction;
        }
    }
}
