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
            path = null;

            direction = "Left_" + name;
        }

        public abstract void AI(GameTime gameTime);

        public override void Update(GameTime gameTime)
        {
            SetGridPosition();
            AI(gameTime);
        }

        protected void MoveTo(Vector2 destination)
        {
            if (!CombatScreen.Map.Nodes[GetGridPositionOf(destination)].Passable || !CombatScreen.Map.Nodes.ContainsKey(GetGridPositionOf(destination)))
            {
                ChangeToClosestAvailable(destination);
                return;
            }
            SetPathTo(destination);
            UpdatePosition(GetNextNodeDirection() * GetSpeed());
        }

        protected void MoveTo(Position destination)
        {
            if (!CombatScreen.Map.Nodes[destination].Passable)
            {
                ChangeToClosestAvailable(destination);
                return;
            }
            SetPathTo(destination);
            UpdatePosition(GetNextNodeDirection() * GetSpeed());
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

        protected void SetPathTo(Vector2 position)
        {
            if (path != null)
            {
                Array.ForEach(path, node =>
                {
                    CombatScreen.Map.Nodes[node].Path = false;
                });
            }
            Position destinationPosition = GetGridPositionOf(position);
            path = CombatScreen.Map.PathfindingGrid.GetPath(GridPosition, destinationPosition);
            Array.ForEach(path, node =>
            {
                CombatScreen.Map.Nodes[node].Path = true;
            });
        }

        protected void SetPathTo(Position position)
        {
            if (path != null)
            {
                Array.ForEach(path, node =>
                {
                    CombatScreen.Map.Nodes[node].Path = false;
                });
            }
            path = CombatScreen.Map.PathfindingGrid.GetPath(GridPosition, position);
            Array.ForEach(path, node =>
            {
                CombatScreen.Map.Nodes[node].Path = true;
            });
        }

        protected void ChangeToClosestAvailable(Vector2 destination)
        {
            Position destinationPosition = GetGridPositionOf(destination);
            MoveTo(new Position((int)(destinationPosition.X - Math.Round(playerDirection.X)), (int)(destinationPosition.Y - Math.Round(playerDirection.Y))));
        }

        protected void ChangeToClosestAvailable(Position destination)
        {
            MoveTo(new Position((int)(destination.X - Math.Round(playerDirection.X)), (int)(destination.Y - Math.Round(playerDirection.Y))));
        }
    }
}