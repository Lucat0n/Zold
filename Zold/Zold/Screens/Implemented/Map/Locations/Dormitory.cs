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
    internal class Dormitory: Location
    {

        TmxMap currentMap;
        public Dormitory(GameScreenManager gameScreenManager, SpriteBatchSpriteSheet spriteSheet, Player player) : base(gameScreenManager, spriteSheet, player)
        {
            currentMap = new TmxMap(@"Content/graphic/locations/Dormitory/dormitory_i.tmx");
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

        public override List<int> getLayerNumbers()
        {
            List<int> LayerNumbers = new List<int>();
           // LayerNumbers.Add(2);
            LayerNumbers.Add(3);
            LayerNumbers.Add(4);
            LayerNumbers.Add(5);
            return LayerNumbers;
        }

        public override Vector2 getPortal()
        {
            return new Vector2(-100,0);
        }

        public override Vector2 playersNewPosition()
        {
            return new Vector2(192, 64);
        }
    }
}