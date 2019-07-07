using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zold.Screens.Implemented.Cutscenes.Commands
{
    class DirectionCommand : CutsceneCommand
    {
        private readonly string directionName;

        public DirectionCommand(Dummy dummy, string directionName) : base(dummy)
        {
            this.directionName = directionName;
        }

        public override void execute()
        {
            DirectSprite();
        }

        private void DirectSprite()
        {
            dummy.SpriteBatchSpriteSheet.DirectSprite(dummy.Posiotion, directionName);
        }
    }
}
