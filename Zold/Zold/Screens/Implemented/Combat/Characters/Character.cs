using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.Characters
{
    abstract class Character
    {
        public Vector2 Position;
        public Vector2 tempPosition;
        public Vector2 BottomPosition;
        public CombatScreen CombatScreen;
        protected SpriteBatchSpriteSheet SpriteBatchSpriteSheet;
        public int Hp { set; get; }
        protected float layerDepth;
        protected float rotation;
        protected float scale;
        protected readonly int mapEdge;
        protected int damage;
        protected float speed;
        protected string action;
        protected string direction;
        protected int height;
        protected int width;

        public Character(Vector2 Position, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int width, int height)
        {
            this.Position = Position;
            this.SpriteBatchSpriteSheet = SpriteBatchSpriteSheet;

            this.height = height;
            this.width = width;

            CalculateDepth();
            tempPosition = Position;
            layerDepth = (Position.Y - 100) / 350;
            mapEdge = 150;
            rotation = 0.0f;
            scale = 1.0f;
            damage = 5;
            Hp = 50;

            action = "Idle";
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
    }
}
