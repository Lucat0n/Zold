using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TiledSharp;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Map.Locations
{
    internal class Dormitory : Location
    {
        List<Enemy> enemies;

        GameScreenManager gameScreenManager;
        SpriteBatchSpriteSheet spriteSheet;
        Player player;

        TmxMap currentMap;
        public Dormitory(GameScreenManager gameScreenManager, SpriteBatchSpriteSheet spriteSheet, Player player, bool postproc) : base(gameScreenManager, spriteSheet, player, postproc)
        {
            currentMap = new TmxMap(@"Content/graphic/locations/Dormitory/dormitory_v.tmx");
            this.player = player;
            this.gameScreenManager = gameScreenManager;
            this.spriteSheet = spriteSheet;
        }

        public override Npc GetCharacter(int i)
        {
            return null;
        }

        public override List<Npc> GetCharacters()
        {
            return null;
        }

        public override List<int> getColideLayers()
        {
            List<int> colisions = new List<int>();
            colisions.Add(0);
            return colisions;
        }

        public override TmxMap getCurrentMap()
        {
            return currentMap;
        }

        public override List<Enemy> GetEnemies()
        {
            enemies = new List<Enemy>();
            enemies.Add(new Enemy(player, new Vector2(400, 200), Assets.Instance.Get("placeholders/Textures/rat") ));
            return enemies;
        }

        public override Enemy Getenemy(int i)
        {
            return enemies[i];
        }

        public override List<int> getLayerNumbers()
        {
            List<int> LayerNumbers = new List<int>();
            // LayerNumbers.Add(2);
            LayerNumbers.Add(3);
            LayerNumbers.Add(4);
            LayerNumbers.Add(5);
            return LayerNumbers;
        }

        public override List<Vector2> getPortals()
        {
            List<Vector2> Portals = new List<Vector2>();
            Portals.Add(new Vector2(544, 64));
            Portals.Add(new Vector2(192, 32));
            return Portals;
        }

        public override List<Vector2> playersNewPositions()
        {
            List<Vector2> Exits = new List<Vector2>();
            Exits.Add(new Vector2(192, 64));
            Exits.Add(new Vector2(192, 64));
            return Exits;
        }

        
        public override string getLocQuest()
        {
            return null;
        }

        public override List<Location> ListofNextPlaces()
        {
            List<Location> ListofLocs = new List<Location>();
      
            ListofLocs.Add(new Locations.DormitoryFirstFloor(gameScreenManager, spriteSheet, player, false));
            ListofLocs.Add(new Locations.TheRoom(gameScreenManager, spriteSheet, player, false));
            return ListofLocs;
        }

        public override List<int> offsets()
        {
            List<int> offsets = new List<int>();
            offsets.Add(1);
            offsets.Add(-1);
            return offsets;
        }

        public override string getName()
        {
            return "dormitory_v";
        }
    }
}