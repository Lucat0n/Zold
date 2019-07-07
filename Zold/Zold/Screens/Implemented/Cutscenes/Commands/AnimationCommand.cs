using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zold.Screens.Implemented.Cutscenes.Commands
{
    class AnimationCommand : CutsceneCommand
    {
        private readonly string animationName;
        private readonly GameTime gameTime;

        public AnimationCommand(Dummy dummy, string animationName, GameTime gameTime) : base(dummy)
        {
            this.animationName = animationName;
            this.gameTime = gameTime;
        }

        public override void execute()
        {
            PlayAnimationFromSpriteSheet();
        }

        private void PlayAnimationFromSpriteSheet()
        {
            dummy.SpriteBatchSpriteSheet.PlayFullAniamtion(dummy.Posiotion, animationName, gameTime);
        }
    }
}
