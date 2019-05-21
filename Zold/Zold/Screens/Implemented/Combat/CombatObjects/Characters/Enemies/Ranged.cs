using Microsoft.Xna.Framework;
using System.Timers;
using Zold.Screens.Implemented.Combat.Skills;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.CombatObjects.Characters.Enemies
{
    class Ranged : Enemy
    {
        public Vector2 CenterPosition;
        private Vector2 moveDirection;
        private Skill skill;
        private int mapOffset;

        public Ranged(Player player, int lvl, Vector2 position, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int width, int height) : base(player, lvl, position, SpriteBatchSpriteSheet, width, height)
        {
            SpriteBatchSpriteSheet.MakeAnimation(3, "Left", 250);
            SpriteBatchSpriteSheet.MakeAnimation(1, "Right", 250);

            mapOffset = 50;
            CenterPosition = new Vector2(position.X + this.width / 2, position.Y + this.height / 2);
            skill = new Skill(CombatScreen);
        }

        public override void AI(GameTime gameTime)
        {
            CenterPosition = new Vector2(Position.X + width / 2, Position.Y + height / 2);
            BottomPosition = new Vector2(Position.X + width, Position.Y + height);
            CalculateDepth();
            CheckDirection();
            speed = 60f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            playerDirection = CalcDirection(player.Position, Position);
            Distance = Vector2.Distance(new Vector2(player.BottomPosition.X, player.BottomPosition.Y - height), Position);

            if (Distance <= 100)
            {
                action = "Running";
                Run();
            }
            else if(Distance <= 600)
            {
                action = "Shootin'";
                skill.Destination = CalcDirection(player.CenterPosition, CenterPosition);
                skill.CombatScreen = CombatScreen;
                skill.StartPosition = CenterPosition;
                skill.Use("Enemy");
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
            if (player.Position.Y >= Position.Y && BottomPosition.Y <= topMapEdge + mapOffset )
            {
                if (player.Position.X <= Position.X)
                {
                    moveDirection = CalcDirection(new Vector2(rightMapEdge, topMapEdge), BottomPosition);
                }
                else if (player.Position.X > Position.X)
                {
                    moveDirection = CalcDirection(new Vector2(leflMapEdge, topMapEdge), BottomPosition);
                }
            }
            else if (player.Position.Y <= Position.Y && BottomPosition.Y >= bottomMapEdge - mapOffset)
            {
                if (player.Position.X <= Position.X)
                {
                    moveDirection = CalcDirection(new Vector2(rightMapEdge, bottomMapEdge), BottomPosition);
                }
                else
                {
                    moveDirection = CalcDirection(new Vector2(leflMapEdge, bottomMapEdge), BottomPosition);
                }
            }
            else
                moveDirection = playerDirection * -1;
            Move(moveDirection);
        }
    }
}
