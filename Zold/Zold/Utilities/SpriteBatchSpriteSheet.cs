using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Zold.Utilities
{
    public class SpriteBatchSpriteSheet : SpriteBatch
    {
        public Texture2D SpriteSheet { get; private set; }

        public int Rows { get; set; }

        public int Cols { get; set; }

        public int TextureWidthPixels { get; set; }

        public int TextureHeightPixels { get; set; }

        public float LayerDepth { get; set; }

        public float Rotation { get; set; }

        public float Scale { get; set; }

        public int SpriteSheetWidth { get { return TextureWidthPixels * Cols; } }

        public int SpriteSheetHeight { get { return TextureHeightPixels * Rows; } }

        private Rectangle[,] Frames;

        private Dictionary<string, Animation> Animations = new Dictionary<string, Animation>();

        public SpriteBatchSpriteSheet(GraphicsDevice graphicsDevice, Texture2D spriteSheet, int rows, int cols, int textureWidthPixels, int textureHeightPixels) : base(graphicsDevice)
        {
            SpriteSheet = spriteSheet;
            Rows = rows;
            Cols = cols;
            TextureWidthPixels = textureWidthPixels;
            TextureHeightPixels = textureHeightPixels;

            Rotation = 0.0f;
            Scale = 1.0f;

            Frames = new Rectangle[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    Frames[i,j] = new Rectangle(j*textureWidthPixels, i*TextureHeightPixels, textureWidthPixels, textureHeightPixels);
        }

        public void Draw(Vector2 place, int x, int y)
        {
            base.Draw(SpriteSheet, place, Frames[x, y], Color.White, Rotation, Vector2.Zero, Scale, SpriteEffects.None, LayerDepth);
        }

        public void MakeAnimation(int rowNumber, String name, int time)
        {
            List<Rectangle> tmp = new List<Rectangle>();
            for (int i = 0; i < Cols; i++) tmp.Add(Frames[rowNumber, i]);
            Animations.Add(name, new Animation(tmp, time, Cols));
        }

        public void PlayFullAniamtion(Vector2 place, String name, GameTime gameTime)
        {
            try
            {
                base.Draw(SpriteSheet, place, Animations[name].Update(gameTime), Color.White, Rotation, Vector2.Zero, Scale, SpriteEffects.None, LayerDepth);
            }
            catch (KeyNotFoundException e)
            {
                base.Draw(SpriteSheet, place, Frames[0, 0], Color.Pink);
            }
        }
    }


}
