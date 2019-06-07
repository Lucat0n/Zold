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

        public TheRoom(GameScreenManager gameScreenManager, SpriteBatchSpriteSheet spriteSheet, Player player) : base(gameScreenManager, spriteSheet, player)
        {
            currentMap = new TmxMap(@"Content/graphic/locations/MainRoom/MainRoom.tmx");

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
            characters.Add(new Npc(Assets.Instance.Get("placeholders/Textures/Adven"), new Vector2(256, 64)));
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
            //LayerNumbers.Add(0);

            //LayerNumbers.Add(4);
            // LayerNumbers.Add(6);
            return LayerNumbers;
        }

        public override Vector2 getPortal()
        {
            return new Vector2(192, 160);
        }

        public override Vector2 playersNewPosition()
        {
            return new Vector2(64, 96);
        }

        public override List<Enemy> GetEnemies()
        {
            return null;
        }

        public override Enemy Getenemy(int i)
        {
            return null;
        }
    }
}
