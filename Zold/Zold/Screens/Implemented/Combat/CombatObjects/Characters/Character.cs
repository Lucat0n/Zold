using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.CombatObjects.Characters
{
    abstract class Character : CombatObject
    {
        public Vector2 BottomPosition;
        public CombatScreen CombatScreen;
        public int Hp { set; get; }
        protected readonly int mapEdge;
        protected string action;

        public Character(Vector2 Position, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int width, int height) : base(Position, SpriteBatchSpriteSheet, width, height)
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
    }
}
