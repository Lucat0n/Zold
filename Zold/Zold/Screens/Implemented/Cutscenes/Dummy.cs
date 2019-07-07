using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Cutscenes
{
    class Dummy
    {
        public static readonly List<string> BasicAnimationNames = new List<string>() { "up", "right", "down", "left" };
        public static readonly List<string> DirectionsNames = new List<string>() { "up", "right", "down", "left" };

        public SpriteBatchSpriteSheet SpriteBatchSpriteSheet { get; private set; }
        public Vector2 Posiotion { get; set; }

        public Dummy(SpriteBatchSpriteSheet spriteBatchSpriteSheet, Vector2 posiotion)
        {
            SpriteBatchSpriteSheet = spriteBatchSpriteSheet;
            Posiotion = posiotion;
            MakeDirectonFrames();
            MakeBasicAnimations();
        }

        //TODO: oddzielić to od klasy
        private void MakeBasicAnimations()
        {
            SpriteBatchSpriteSheet.MakeAnimation(3, "left", 250);
            SpriteBatchSpriteSheet.MakeAnimation(1, "right", 250);
            SpriteBatchSpriteSheet.MakeAnimation(2, "down", 250);
            SpriteBatchSpriteSheet.MakeAnimation(0, "up", 250);
        }

        private void MakeDirectonFrames()
        {
            SpriteBatchSpriteSheet.MakeDirection(0, 0, "up");
            SpriteBatchSpriteSheet.MakeDirection(1, 0, "right");
            SpriteBatchSpriteSheet.MakeDirection(2, 0, "down");
            SpriteBatchSpriteSheet.MakeDirection(3, 0, "left");
        }
    }
}
