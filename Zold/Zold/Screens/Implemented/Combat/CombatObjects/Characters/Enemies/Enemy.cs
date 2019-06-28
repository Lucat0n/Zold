using Microsoft.Xna.Framework;
using RoyT.AStar;
using System;
using Zold.Statistics;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.CombatObjects.Characters.Enemies
{
    abstract class Enemy : Character
    {
        public Player player;
        public Vector2 playerDirection;
        public Vector2 attackPosition;
        public double Distance { get; set; }
        public double AttackStart { get; set; }
        public float AttackEnd { get; set; }
        protected Position[] path;

        public Enemy(Player player, Stats statistics, Vector2 position, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int height, int width) : base(position, statistics, SpriteBatchSpriteSheet, height, width)
        {
            this.player = player;

            direction = "Left_" + name;
        }

        public abstract void AI(GameTime gameTime);

        public override void Update(GameTime gameTime)
        {
            SetPath();
            SetGridPosition();
            AI(gameTime);
        }

        protected void Move(Vector2 direction)
        {
            UpdatePosition(direction * GetSpeed());
        }

        protected void GetPlayerDirection()
        {
            playerDirection = CalcDirection(BottomPosition, player.BottomPosition);
        }

        protected Vector2 GetNextNodeDirection()
        {
            if (path != null)
                return CalcDirection(BottomPosition, CombatScreen.Map.Nodes[path[1]].Center);
            return Vector2.Zero;
        }

        private void SetPath()
        {
            if (path != null)
            {
                Array.ForEach(path, node =>
                {
                    CombatScreen.Map.Nodes[node].Path = false;
                });
            }
            path = CombatScreen.Map.PathfindingGrid.GetPath(GridPosition, player.GridPosition);
            Array.ForEach(path, node =>
            {
                CombatScreen.Map.Nodes[node].Path = true;
            });
        }
    }
}