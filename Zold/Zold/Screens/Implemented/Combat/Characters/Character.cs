using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.Characters
{
    abstract class Character
    {
        public Vector2 Position;
        public Vector2 BottomPosition;
        public SpriteBatchSpriteSheet SpriteBatchSpriteSheet;
        public float layerDepth { get; set; }
        public float rotation { get; set; }
        public float scale { get; set; }
        public int mapEdge { get; set; }
        public int damage { get; set; }
        public int hp { get; set; }
        public float speed { get; set; }
        public string action { get; set; }
        public string direction { get; set; }
        public int height { get; set; }
        public int width { get; set; }

        public Character(Vector2 position, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int width, int height)
        {
            this.Position = position;
            this.SpriteBatchSpriteSheet = SpriteBatchSpriteSheet;

            this.height = height;
            this.width = width;

            CalculateDepth();
            layerDepth = (position.Y - 100) / 350;
            mapEdge = 150;
            rotation = 0.0f;
            scale = 1.0f;
            damage = 5;
            hp = 50;

            action = "Idle";
        }

        public void CalculateDepth()
        {
            if (Position.Y >= 450)
                layerDepth = 1.0f;
            else
                layerDepth = (Position.Y - 100) / 350;
        }
    }
}
