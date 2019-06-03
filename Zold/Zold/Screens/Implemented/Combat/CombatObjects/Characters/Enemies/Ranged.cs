using Microsoft.Xna.Framework;
using System.Timers;
using Zold.Screens.Implemented.Combat.Skills;
using Zold.Statistics;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.CombatObjects.Characters.Enemies
{
    class Ranged : Enemy
    {
        private Vector2 moveDirection;
        private Skill skill;
        private int mapOffset;

        public Ranged(Player player, Stats statistics, Vector2 position, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int width, int height) : base(player, statistics, position, SpriteBatchSpriteSheet, width, height)
        {
            SpriteBatchSpriteSheet.MakeAnimation(3, "Left", 250);
            SpriteBatchSpriteSheet.MakeAnimation(1, "Right", 250);

            mapOffset = 50;
            skill = new Skill(CombatScreen);
        }

        public override void AI(GameTime gameTime)
        {
            CalculateDepth();
            CheckDirection();
            playerDirection = CalcDirection(Position, player.Position);
            Distance = Vector2.Distance(new Vector2(player.BottomPosition.X, player.BottomPosition.Y - height), Position);

            if (Distance <= 100)
            {
                action = "Running";
                Run();
            }
            else if(Distance <= 600)
            {
                action = "Shootin'";
                skill.Destination = CalcDirection(CenterPosition, player.CenterPosition);
                skill.CombatScreen = CombatScreen;
                skill.StartPosition = CenterPosition;
                skill.Use("Enemy", Statistics.Damage);
            }
            else if (Distance <= 100)
            {
                action = "Running";
                Run();
            }
            else
            {
                action = "Moving";
                Move(playerDirection);
            }
        }

        public override void Animation(GameTime gameTime)
        {
            SpriteBatchSpriteSheet.Begin();

            if (action == "Moving" || action == "Running")
            {
                SpriteBatchSpriteSheet.PlayFullAniamtion(Position, direction, gameTime);
            }
            else
            {
                direction = player.Position.X >= Position.X ? "Right" : "Left";
                if (direction == "Right")
                    SpriteBatchSpriteSheet.Draw(Position, 1, 0);
                if (direction == "Left")
                    SpriteBatchSpriteSheet.Draw(Position, 3, 0);
            }

            SpriteBatchSpriteSheet.End();
        }

        public void Run()
        {
            //if (IsCloseToBot() && IsCloseToRight())
            //{
            //    // TO DO
            //}
            //else if (IsCloseToBot() && IsCloseToLeft())
            //{
            //    // TO DO
            //}
            //else if (IsCloseToTop() && IsCloseToRight())
            //{
            //    // TO DO
            //}
            //else if (IsCloseToTop() && IsCloseToLeft())
            //{
            //    // TO DO
            //}
            if (IsCloseToTop())
            {
                if (player.BottomPosition.X <= BottomPosition.X)
                {
                    moveDirection = CalcDirection(BottomPosition, new Vector2(rightMapEdge, topMapEdge));
                }
                else if (player.BottomPosition.X > BottomPosition.X)
                {
                    moveDirection = CalcDirection(BottomPosition, new Vector2(leflMapEdge, topMapEdge));
                }
            }
            
            else if (IsCloseToBot())
            {
                if (player.BottomPosition.X <= BottomPosition.X)
                {
                    moveDirection = CalcDirection(BottomPosition, new Vector2(rightMapEdge, bottomMapEdge));
                }
                else
                {
                    moveDirection = CalcDirection(BottomPosition, new Vector2(leflMapEdge, bottomMapEdge));
                }
            }
            else if (IsCloseToRight())
            {
                if (player.BottomPosition.Y <= BottomPosition.Y)
                {
                    moveDirection = CalcDirection(BottomPosition, new Vector2(rightMapEdge, bottomMapEdge));
                }
                else
                {
                    moveDirection = CalcDirection(BottomPosition, new Vector2(rightMapEdge, topMapEdge));
                }
            }
            else if (IsCloseToLeft())
            {
                if (player.BottomPosition.Y <= BottomPosition.Y)
                {
                    moveDirection = CalcDirection(BottomPosition, new Vector2(leflMapEdge, bottomMapEdge));
                }
                else
                {
                    moveDirection = CalcDirection(BottomPosition, new Vector2(leflMapEdge, topMapEdge));
                }
            }
            else
                moveDirection = playerDirection * -1;
            Move(moveDirection);
        }

        private bool IsCloseToLeft()
        {
            return player.BottomPosition.X >= BottomPosition.X && BottomPosition.X <= rightMapEdge - mapOffset;
        }

        private bool IsCloseToRight()
        {
            return player.BottomPosition.X <= BottomPosition.X && BottomPosition.X >= rightMapEdge - mapOffset;
        }

        private bool IsCloseToBot()
        {
            return player.BottomPosition.Y <= BottomPosition.Y && BottomPosition.Y >= bottomMapEdge - mapOffset;
        }

        private bool IsCloseToTop()
        {
            return player.BottomPosition.Y >= BottomPosition.Y && BottomPosition.Y <= topMapEdge + mapOffset;
        }
    }
}
