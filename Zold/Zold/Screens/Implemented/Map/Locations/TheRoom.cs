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
    class TheRoom : Location
    {
        TmxMap currentMap;
        List<int> colisions;
        List<Npc> characters;

        GameScreenManager gameScreenManager;
        SpriteBatchSpriteSheet spriteSheet;
        Player player;


        public TheRoom(GameScreenManager gameScreenManager, SpriteBatchSpriteSheet spriteSheet, Player player, bool postproc) : base(gameScreenManager, spriteSheet, player,postproc)
        {
            currentMap = new TmxMap(@"Content/graphic/locations/MainRoom/MainRoom.tmx");
            this.player = player;
            this.gameScreenManager = gameScreenManager;
            this.spriteSheet = spriteSheet;
        }

        public override List<int> getColideLayers()
        {
            colisions = new List<int>();
            colisions.Add(0);
            return colisions;
        }

        public override List<Npc> GetCharacters()
        {
            characters = new List<Npc>();
            List<string> powiedzenia = new List<string>();
            powiedzenia.Add("Sprawdz zadania, potem wyjdz z pokoju i je ponownie zobacz.");

            characters.Add(new Npc(Assets.Instance.Get("placeholders/Textures/Adven"), new Vector2(256, 64), powiedzenia));
            return characters;
        }

        public override Npc GetCharacter(int i)
        {
            return characters[i];
        }


        public override TmxMap getCurrentMap()
        {
            return currentMap;
        }

        public override List<int> getLayerNumbers()
        {
            List<int> LayerNumbers = new List<int>();
            // LayerNumbers.Add(2);
            LayerNumbers.Add(3);
            LayerNumbers.Add(4); // sciany
            LayerNumbers.Add(5);
            LayerNumbers.Add(6);
            //LayerNumbers.Add(0);

            //LayerNumbers.Add(4);
            // LayerNumbers.Add(6);
            return LayerNumbers;
        }
      

        public override List<Vector2> getPortals()
        {
            List<Vector2> Portals = new List<Vector2>();
            Portals.Add(new Vector2(192, 160));
            Portals.Add(new Vector2(256, 64));
            return Portals;
        }

        public override List<Vector2> playersNewPositions()
        {
            List<Vector2> Exits = new List<Vector2>();
            Exits.Add(new Vector2(64, 96));
            Exits.Add(new Vector2(192,128));
            return Exits;
            //return new Vector2(544,224);
        }

        public override List<Enemy> GetEnemies()
        {
            return null;
        }

        public override Enemy Getenemy(int i)
        {
            return null;
        }

        public override string getLocQuest()
        {
            return "lQ1";
        }

        public override List<Location> ListofNextPlaces()
        {
            List<Location> ListofLocs = new List<Location>();
            ListofLocs.Add(new Locations.Dormitory(gameScreenManager, spriteSheet, player, true));
            ListofLocs.Add(new Locations.DormitoryFirstFloor(gameScreenManager, spriteSheet, player, false));
            return ListofLocs;
        }

        public override List<int> offsets()
        {
            List<int> offsets = new List<int>();
            offsets.Add(1);
            return offsets;
        }

        public override string getName()
        {
            return "room";
        }
    }
}
