using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.Characters
{
    abstract class Character
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 bottomPosition;
        public SpriteBatchSpriteSheet SpriteBatchSpriteSheet;
        public float LayerDepth { get; set; }
        public float Rotation { get; set; }
        public float Scale { get; set; }
        public int Damage { get; set; }
        public int Hp { get; set; }
        public float Speed { get; set; }
        public string Action { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public Character(Vector2 position, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int width, int height)
        {
            this.position = position;
            this.SpriteBatchSpriteSheet = SpriteBatchSpriteSheet;

            Height = height;
            Width = width;

            LayerDepth = (position.Y - 100) / 350;
            Rotation = 0.0f;
            Scale = 1.0f;
            Damage = 5;
            Hp = 50;

            Action = "Idle";
        }

        public void CalculateDepth()
        {
            if (position.Y >= 450)
                LayerDepth = 1.0f;
            else
                LayerDepth = (position.Y - 100) / 350;
        }
    }
}
