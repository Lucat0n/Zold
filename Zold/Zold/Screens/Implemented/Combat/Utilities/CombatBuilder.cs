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
        SpriteBatchSpriteSheet playerSprite;
        SpriteBatchSpriteSheet ratSprite;
        SpriteBatchSpriteSheet punkSprite;

        public CombatBuilder(GraphicsDevice graphicsDevice, Stats playerStats)
        {
            this.playerStats = playerStats;
            statistics = new StatisticsManager();

            playerSprite = new SpriteBatchSpriteSheet(graphicsDevice, Assets.Instance.Get("combat/Textures/main"), 4, 3, 32, 48);
            playerSprite.MakeAnimation(3, "Left_Player", 250);
            playerSprite.MakeAnimation(1, "Right_Player", 250);

            ratSprite = new SpriteBatchSpriteSheet(graphicsDevice, Assets.Instance.Get("combat/Textures/rat"), 5, 4, 44, 20);
            ratSprite.MakeAnimation(0, "Right_Charger", 250);
            ratSprite.MakeAnimation(1, "Left_Charger", 250);
            ratSprite.MakeAnimation(3, "Death_Right_Charger", 250);
            ratSprite.MakeAnimation(4, "Death_Left_Charger", 250);

            punkSprite = new SpriteBatchSpriteSheet(graphicsDevice, Assets.Instance.Get("combat/Textures/punk"), 20, 3, 32, 56);
            punkSprite.MakeAnimation(3, "Left_Ranged", 250);
            punkSprite.MakeAnimation(1, "Right_Ranged", 250);
            punkSprite.MakeAnimation(3, "Left_Mob", 250);
            punkSprite.MakeAnimation(1, "Right_Mob", 250);

            player = new Player(new Vector2(0, 200), playerStats, enemies, playerSprite, 32, 48);

            this.graphicsDevice = graphicsDevice;
        }

        public void Reset()
        {
            player = null;
            enemies.Clear();
        }

        public void SetPlayerPosition(int positionX, int positionY)
        {
            player = new Player(new Vector2(positionX, positionY), playerStats, enemies, playerSprite, 32, 48);
        }

        public void AddRat(int lvl, int positionX, int positionY)
        {
            enemies.Add(new Charger(player, statistics.SetStats("Rat", lvl), new Vector2(positionX, positionY), ratSprite, 44, 20));
        }

        public void AddPunk(int lvl, int positionX, int positionY)
        {
            enemies.Add(new Mob(player, statistics.SetStats("Punk", lvl), new Vector2(positionX, positionY), punkSprite, 32, 56));
        }

        public void AddRanged(int lvl, int positionX, int positionY)
        {
            enemies.Add(new Ranged(player, statistics.SetStats("Ranged", lvl), new Vector2(positionX, positionY), punkSprite, 32, 56));
        }

        public CombatScreen Build()
        {
            return new CombatScreen(player, enemies);
        }
    }
}
