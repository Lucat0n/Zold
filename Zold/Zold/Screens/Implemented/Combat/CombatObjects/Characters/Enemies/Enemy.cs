using Microsoft.Xna.Framework;
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

        public Enemy(Player player, Stats statistics, Vector2 position, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int height, int width) : base(position, statistics, SpriteBatchSpriteSheet, height, width)
        {
            this.player = player;

            direction = "Left_" + name;
        }

        public abstract void AI(GameTime gameTime);

        public override void Update(GameTime gameTime)
        {
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
    }
}