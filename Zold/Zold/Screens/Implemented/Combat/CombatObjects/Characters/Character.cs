using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zold.Buffs;
using Zold.Statistics;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.CombatObjects.Characters
{
    abstract class Character : CombatObject
    {
        public Vector2 TopPosition;
        public Vector2 CenterPosition;
        public Vector2 BottomPosition;
        public CombatScreen CombatScreen;
        public string action;
        public HashSet<IBuff> buffSet = new HashSet<IBuff>();

        public Character(Vector2 Position, Stats Statistics, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int width, int height) : base(Position, SpriteBatchSpriteSheet, width, height)
        {
            this.Position = Position;
            this.Statistics = Statistics;
            this.SpriteBatchSpriteSheet = SpriteBatchSpriteSheet;

            this.height = height;
            this.width = width;

            TopPosition = new Vector2(Position.X + this.width / 2, Position.Y);
            CenterPosition = new Vector2(Position.X + this.width / 2, Position.Y + this.height / 2);
            BottomPosition = new Vector2(Position.X + this.width / 2, Position.Y + this.height);

            CalculateDepth();
            tempPosition = Position;
            layerDepth = (Position.Y - 100) / 350;
            rotation = 0.0f;
            scale = 1.0f;

            action = "Idle";
        }

        protected void UpdatePosition(float x, float y)
        {
            Position.X += x;
            Position.Y += y;
            TopPosition.Y += y;
            TopPosition.X += x;
            CenterPosition.X += x;
            CenterPosition.Y += y;
            BottomPosition.X += x;
            BottomPosition.Y += y;
        }

        public void UpdateBuffs()
        {
            IBuff[] buffs = buffSet.ToArray();
            foreach (IBuff buff in buffs)
                buff.Start();
        }
    }
}
