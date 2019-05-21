using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters.Enemies;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat
{
    class CombatBuilder
    {
        Player player;
        List<Enemy> enemies = new List<Enemy>();
        GraphicsDevice graphicsDevice;

        public CombatBuilder(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            Reset();
        }

        public void Reset()
        {
            player = null;
            enemies.Clear();
        }

        public void AddPlayer()
        {
            player = new Player(new Vector2(0, 200), 100, enemies, new SpriteBatchSpriteSheet(graphicsDevice, Assets.Instance.Get("combat/Textures/main"), 4, 3, 32, 48), 32, 48);
        }

        public void AddRat(int lvl, int positionX, int positionY)
        {
            enemies.Add(new Charger(player, lvl, new Vector2(positionX, positionY), new SpriteBatchSpriteSheet(graphicsDevice, Assets.Instance.Get("combat/Textures/rat"), 5, 4, 44, 20), 44, 20));
        }

        public void AddPunk(int lvl, int positionX, int positionY)
        {
            enemies.Add(new Mob(player, lvl, new Vector2(positionX, positionY), new SpriteBatchSpriteSheet(graphicsDevice, Assets.Instance.Get("combat/Textures/punk"), 20, 3, 32, 56), 32, 56));
        }

        public void AddRanged(int lvl, int positionX, int positionY)
        {
            enemies.Add(new Ranged(player, lvl, new Vector2(positionX, positionY), new SpriteBatchSpriteSheet(graphicsDevice, Assets.Instance.Get("combat/Textures/punk"), 20, 3, 32, 56), 32, 56));
        }

        public CombatScreen Build()
        {
            return new CombatScreen(player, enemies);
        }
    }
}
