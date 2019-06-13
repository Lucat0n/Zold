using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters.Enemies;
using Zold.Statistics;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Combat
{
    class CombatBuilder
    {
        Player player;
        StatisticsManager statistics;
        List<Enemy> enemies = new List<Enemy>();
        GraphicsDevice graphicsDevice;
        Stats playerStats;

        public CombatBuilder(GraphicsDevice graphicsDevice, Stats playerStats)
        {
            this.playerStats = playerStats;
            statistics = new StatisticsManager();
            player = new Player(new Vector2(0, 200), playerStats, enemies, new SpriteBatchSpriteSheet(graphicsDevice, Assets.Instance.Get("combat/Textures/main"), 4, 3, 32, 48), 32, 48);
            this.graphicsDevice = graphicsDevice;
        }

        public void Reset()
        {
            player = null;
            enemies.Clear();
        }

        public void SetPlayerPosition(int positionX, int positionY)
        {
            player = new Player(new Vector2(positionX, positionY), playerStats, enemies, new SpriteBatchSpriteSheet(graphicsDevice, Assets.Instance.Get("combat/Textures/main"), 4, 3, 32, 48), 32, 48);
        }

        public void AddRat(int lvl, int positionX, int positionY)
        {
            enemies.Add(new Charger(player, statistics.SetStats("Rat", lvl), new Vector2(positionX, positionY), new SpriteBatchSpriteSheet(graphicsDevice, Assets.Instance.Get("combat/Textures/rat"), 5, 4, 44, 20), 44, 20));
        }

        public void AddPunk(int lvl, int positionX, int positionY)
        {
            enemies.Add(new Mob(player, statistics.SetStats("Punk", lvl), new Vector2(positionX, positionY), new SpriteBatchSpriteSheet(graphicsDevice, Assets.Instance.Get("combat/Textures/punk"), 20, 3, 32, 56), 32, 56));
        }

        public void AddRanged(int lvl, int positionX, int positionY)
        {
            enemies.Add(new Ranged(player, statistics.SetStats("Ranged", lvl), new Vector2(positionX, positionY), new SpriteBatchSpriteSheet(graphicsDevice, Assets.Instance.Get("combat/Textures/punk"), 20, 3, 32, 56), 32, 56));
        }

        public CombatScreen Build()
        {
            return new CombatScreen(player, enemies);
        }
    }
}
