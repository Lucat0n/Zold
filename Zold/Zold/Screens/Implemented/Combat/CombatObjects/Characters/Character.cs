using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zold.Buffs;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat.CombatObjects.Characters
{
    abstract class Character : CombatObject
    {
        public Vector2 BottomPosition;
        public CombatScreen CombatScreen;
        public int Hp { set; get; }
        public int MaxHp { get; }
        public string action;
        public HashSet<IBuff> buffSet = new HashSet<IBuff>();

        public Character(Vector2 Position, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int width, int height) : base(Position, SpriteBatchSpriteSheet, width, height)
        {
            this.Position = Position;
            this.SpriteBatchSpriteSheet = SpriteBatchSpriteSheet;

            this.height = height;
            this.width = width;

            CalculateDepth();
            tempPosition = Position;
            layerDepth = (Position.Y - 100) / 350;
            rotation = 0.0f;
            scale = 1.0f;
            damage = 5;
            Hp = 50;
            MaxHp = Hp;

            action = "Idle";
        }

        public void UpdateBuffs()
        {
            IBuff[] buffs = buffSet.ToArray();
            foreach (IBuff buff in buffs)
                buff.Start();
        }
    }
}
