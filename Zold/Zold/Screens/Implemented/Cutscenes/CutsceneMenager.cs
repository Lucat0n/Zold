using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zold.Screens.Implemented.Cutscenes.Commands;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Cutscenes
{
    static class CutsceneManager
    {
        public static Cutscene FirstCutscene()
        {
            Dummy playerDummy = new Dummy(Assets.Instance.Get("placeholders/Textures/main"), new Vector2(64, 96));
            List<Dummy> actors = new List<Dummy>() { playerDummy };
            Cutscene cutscene = new Cutscene(actors, new DirectionCommand(playerDummy, "down"));
            return cutscene;
        }
    }
}
