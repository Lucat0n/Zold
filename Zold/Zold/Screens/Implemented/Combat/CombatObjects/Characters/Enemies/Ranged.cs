using Microsoft.Xna.Framework;
using Zold.Screens.Implemented.Combat.Skills;
using Zold.Statistics;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.CombatObjects.Characters.Enemies
{
    class Ranged : Enemy
    {
        private Vector2 moveDirection;
        private ShootEnemy skill;
        private int mapOffset;

        public Ranged(Player player, Stats statistics, Vector2 position, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int width, int height) : base(player, statistics, position, SpriteBatchSpriteSheet, width, height)
        {
            name = "Ranged";
            mapOffset = 50;
            skill = new ShootEnemy(CombatScreen, Statistics.Damage);
        }

        public override void AI(GameTime gameTime)
        {
            CalculateDepth();
            CheckDirection();
            playerDirection = CalcDirection(Position, player.Position);
            Distance = Vector2.Distance(new Vector2(player.BottomPosition.X, player.BottomPosition.Y - Height), Position);

            if (Distance <= 100)
            {
                action = "Running";
                skill.CooldownTimer.Interval = 1000;
                Run();
            }
            else if(Distance <= 500)
            {
                action = "Shootin'";
                skill.Destination = CalcDirection(CenterPosition, player.CenterPosition);
                skill.CombatScreen = CombatScreen;
                skill.StartPosition = CenterPosition;
                skill.Use();
            }
            else
            {
                action = "Moving";
                skill.CooldownTimer.Interval = 1000;
                Move(playerDirection);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatchSpriteSheet.Begin();
            DrawHealth(SpriteBatchSpriteSheet, "red");

            if (action == "Moving" || action == "Running")
            {
                SpriteBatchSpriteSheet.PlayFullAniamtion(Position, direction, gameTime);
            }
            else
            {
                direction = player.Position.X >= Position.X ? "Right_Charger" : "Left_Charger";
                if (direction == "Right_Charger")
                    SpriteBatchSpriteSheet.Draw(Position, 1, 0);
                if (direction == "Left_Charger")
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
                    moveDirection = new Vector2(CombatScreen.Map.RightMapEdge - 5, CombatScreen.Map.TopMapEdge + 5);
                }
                else if (player.BottomPosition.X > BottomPosition.X)
                {
                    moveDirection = new Vector2(CombatScreen.Map.LeflMapEdge + 5, CombatScreen.Map.TopMapEdge + 5);
                }
            }
            else if (IsCloseToBot())
            {
                if (player.BottomPosition.X <= BottomPosition.X)
                {
                    moveDirection = new Vector2(CombatScreen.Map.RightMapEdge - 5, CombatScreen.Map.BottomMapEdge - 5);
                }
                else
                {
                    moveDirection = new Vector2(CombatScreen.Map.LeflMapEdge + 5, CombatScreen.Map.BottomMapEdge - 5);
                }
            }
            else if (IsCloseToRight())
            {
                if (player.BottomPosition.Y <= BottomPosition.Y)
                {
                    moveDirection = new Vector2(CombatScreen.Map.RightMapEdge - 5, CombatScreen.Map.BottomMapEdge - 5);
                }
                else
                {
                    moveDirection = new Vector2(CombatScreen.Map.RightMapEdge - 5, CombatScreen.Map.TopMapEdge + 5);
                }
            }
            else if (IsCloseToLeft())
            {
                if (player.BottomPosition.Y <= BottomPosition.Y)
                {
                    moveDirection = new Vector2(CombatScreen.Map.LeflMapEdge + 5, CombatScreen.Map.BottomMapEdge - 5);
                }
                else
                {
                    moveDirection = new Vector2(CombatScreen.Map.LeflMapEdge + 5, CombatScreen.Map.TopMapEdge + 5);
                }
            }
            else
                moveDirection = BottomPosition + (playerDirection * -10);
            Move(moveDirection);
        }

        private bool IsCloseToLeft()
        {
            return player.BottomPosition.X >= BottomPosition.X && BottomPosition.X <= CombatScreen.Map.RightMapEdge - mapOffset;
        }

        private bool IsCloseToRight()
        {
            return player.BottomPosition.X <= BottomPosition.X && BottomPosition.X >= CombatScreen.Map.RightMapEdge - mapOffset;
        }

        private bool IsCloseToBot()
        {
            return player.BottomPosition.Y <= BottomPosition.Y && BottomPosition.Y >= CombatScreen.Map.BottomMapEdge - mapOffset;
        }

        private bool IsCloseToTop()
        {
            return player.BottomPosition.Y >= BottomPosition.Y && BottomPosition.Y <= CombatScreen.Map.TopMapEdge + mapOffset;
        }
    }
}
