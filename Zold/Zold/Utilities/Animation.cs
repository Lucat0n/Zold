using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zold.Utilities
{
    class Animation
    {
        public List<Rectangle> Frames { get; set; }

        public int FrameSpeed { get; set; }

        public int NumberOfFrames { get; set; }

        private int _currentFrame = 0;

        private double _timeElapsedMinSec = 0;

        public int CurrentFrame
        {
            get
            {
                if (_currentFrame >= NumberOfFrames) _currentFrame = 0;
                return _currentFrame;
            }
            set
            {
                _currentFrame = value;
            }
        }

        public Rectangle NextFrame { get { return Frames[CurrentFrame]; }}

        public Animation(List<Rectangle> frames, int frameSpeed, int numberOfFrames)
        {
            Frames = frames;
            FrameSpeed = frameSpeed;
            NumberOfFrames = numberOfFrames;
        }

        public Rectangle Update (GameTime gameTime)
        {
            _timeElapsedMinSec +=  gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_timeElapsedMinSec > FrameSpeed)
            {
                _timeElapsedMinSec = 0;
                CurrentFrame++;
            }
            return NextFrame;
        }

    }
}
