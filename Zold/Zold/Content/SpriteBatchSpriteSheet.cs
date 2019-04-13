using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public class SpriteBatchSpriteSheet : SpriteBatch
{
    public Texture2D SpriteSheet { get; private set; }

    public int Rows { get;  set; }

    public int Cols { get; set; }

    public int TextureWidthPixels { get; set; }

    public int TextureHeightPixels { get; set; }

    public int SpriteSheetWidth { get { return TextureWidthPixels*Cols; }}

    public int SpriteSheetHeight { get { return TextureHeightPixels*Rows; }}

    private Rectangle FrameRectangle;

    private Rectangle TextureRectagle;

    public SpriteBatchSpriteSheet(GraphicsDevice graphicsDevice ,Texture2D spriteSheet, int rows, int cols, int textureWidthPixels, int textureHeightPixels) : base(graphicsDevice)
    {
        SpriteSheet = spriteSheet;
        Rows = rows;
        Cols = cols;
        TextureWidthPixels = textureWidthPixels;
        TextureHeightPixels = textureHeightPixels;

        FrameRectangle = new Rectangle(0, 0, SpriteSheetWidth, SpriteSheetHeight);
        TextureRectagle = new Rectangle(0, 0, TextureWidthPixels, TextureHeightPixels);
    }

    public void Draw(Vector2 place, int x, int y)
    {
        x *= TextureWidthPixels;
        y *= TextureHeightPixels;
        TextureRectagle.Offset(x,y);
        base.Draw(SpriteSheet, place, TextureRectagle, Color.White);
        TextureRectagle.Offset(-x, -y);
    }

}
