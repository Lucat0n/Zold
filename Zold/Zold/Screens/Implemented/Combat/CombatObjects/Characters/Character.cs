using Microsoft.Xna.Framework;
using RoyT.AStar;
using System;
using Zold.Statistics;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.CombatObjects.Characters
{
    abstract class Character : CombatObject
    {
        public string action;
        public Position GridPosition;
        protected double actualHealthWidth;
        protected int healthWidth;
        protected Rectangle healthRectangle;
        protected Rectangle healthBackgorundRectangle;

        public Character(Vector2 Position, Stats Statistics, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int width, int height) : base(Position, SpriteBatchSpriteSheet, width, height)
        {
            this.Position = Position;
            this.Statistics = Statistics;
            this.SpriteBatchSpriteSheet = SpriteBatchSpriteSheet;

            healthWidth = 48;
            healthBackgorundRectangle = new Rectangle((int)Position.X, (int)Position.Y - 10, healthWidth, 7);
            HitBox = new BoundingBox(new Vector3(Position, 0), new Vector3(Position.X + width, Position.Y + height, 0));
            GridPosition = new Position();

            CalculateDepth();
            tempPosition = Position;
            layerDepth = (Position.Y - 100) / 350;
            rotation = 0.0f;
            scale = 1.0f;

            action = "Idle";
        }

        protected void GetHeathPercentage()
        {
            actualHealthWidth = ((double)Statistics.Health / (double)Statistics.MaxHealth) * healthWidth;
            healthRectangle = new Rectangle((int)Position.X, (int)Position.Y - 15, (int)actualHealthWidth, 7);
        }

        protected void DrawHealth(SpriteBatchSpriteSheet SpriteBatchSpriteSheet, string color)
        {
            GetHeathPercentage();
            SpriteBatchSpriteSheet.Draw(Assets.Instance.Get("combat/Textures/black"), new Vector2(CenterPosition.X - healthWidth/2, Position.Y - 15), healthBackgorundRectangle, Color.White);
            SpriteBatchSpriteSheet.Draw(Assets.Instance.Get("combat/Textures/" + color), new Vector2(CenterPosition.X - healthWidth/2, Position.Y - 15), healthRectangle, Color.White);
        }

        protected void SetGridPosition()
        {
            CombatScreen.Map.Nodes[GridPosition].Occupied = false;
            Position newPosition = GetGridPositionOf(BottomPosition);
            Boolean what = CombatScreen.Map.Nodes.ContainsKey(new Position(1,0));
            if (CombatScreen.Map.Nodes.ContainsKey(newPosition) && CombatScreen.Map.Nodes[newPosition].Passable)
                GridPosition = newPosition;
            CombatScreen.Map.Nodes[GridPosition].Occupied = true;
        }

        protected Position GetGridPositionOf(Vector2 position)
        {
            return new Position((int)position.X / CombatScreen.Map.NodeWidth, ((int)position.Y - CombatScreen.Map.TopMapEdge) / CombatScreen.Map.NodeHeight);
        }
    }
}
