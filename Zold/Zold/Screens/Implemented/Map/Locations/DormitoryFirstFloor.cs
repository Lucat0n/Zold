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
    internal class DormitoryFirstFloor : Location

    {
        List<Enemy> enemies;
        Player player;
        List<Npc> characters;

        TmxMap currentMap;
        public DormitoryFirstFloor(GameScreenManager gameScreenManager, SpriteBatchSpriteSheet spriteSheet, Player player, bool postproc) : base(gameScreenManager, spriteSheet, player, postproc)
        {
            currentMap = new TmxMap(@"Content/graphic/locations/Dormitory/dormitory_i.tmx");
            this.player = player;
        }

        public override List<Npc> GetCharacters()
        {
            characters = new List<Npc>();
            List<string> powiedzenia = new List<string>();
            powiedzenia.Add("Elo");
            powiedzenia.Add("A ty tu czego?");
            powiedzenia.Add("Spadaj");


            characters.Add(new Npc(Assets.Instance.Get("placeholders/Textures/zks"), new Vector2(416, 162), powiedzenia));
            return characters;
        }

        public override Npc GetCharacter(int i)
        {
            return characters[i];
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
            return enemies;
        }

        public override Enemy Getenemy(int i)
        {
            return enemies[i];
        }

        public override List<int> getLayerNumbers()
        {
            List<int> LayerNumbers = new List<int>();
            //LayerNumbers.Add(2);
            LayerNumbers.Add(3);
            LayerNumbers.Add(4);
            LayerNumbers.Add(5);
            return LayerNumbers;
        }

        public override List<Vector2> getPortals()
        {
            List<Vector2> Portals = new List<Vector2>();
            Portals.Add(new Vector2(0, 544));
            return Portals;
        }

        public override List<Vector2> playersNewPositions()
        {
            List<Vector2> Exits = new List<Vector2>();
            Exits.Add(new Vector2(544, 160));
          //  Exits.Add(new Vector2(544-64, 224));
            return Exits;
        }

        public override string getLocQuest()
        {
            return null;
        }

        public override List<Location> ListofNextPlaces()
        {
            return null;
        }
        public override List<int> offsets()
        {
            List<int> offsets = new List<int>();
            offsets.Add(-1);
            return offsets;
        }

        public override string getName()
        {
            return "dormitory_i";
        }
    }
}