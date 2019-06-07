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
    class OtherLocation: Location
    {

        TmxMap currentMap;
        public OtherLocation(GameScreenManager gameScreenManager, SpriteBatchSpriteSheet spriteSheet, Player player) : base(gameScreenManager, spriteSheet, player)
        {
            currentMap = new TmxMap(@"Content/mapa2v2.tmx");
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
            LayerNumbers.Add(1);
            LayerNumbers.Add(0);
            return LayerNumbers;
        }

        public override Vector2 getPortal()
        {
            return new Vector2(-100,0);
        }
    }
}