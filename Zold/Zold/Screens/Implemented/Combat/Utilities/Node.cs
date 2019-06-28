using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zold.Screens.Implemented.Combat.Utilities
{
    class Node
    {
        public Rectangle HitBox;
        public Vector2 Center;
        public bool Passable;
        public readonly int Width;
        public readonly int Height;
        public readonly int PosX;
        public readonly int PosY;

        // Border
        Color borderColor;
        Rectangle topBorder;
        Rectangle leftBorder;
        Rectangle rightBorder;
        Rectangle bottomBorder;
        Rectangle centerDot;

        public Node(int x, int y, int offset)
        {
            Passable = true;
            Width = 16;
            Height = 16;
            PosX = x * Width;
            PosY = y * Height + offset;
            HitBox = new Rectangle(PosX, PosY, Width, Height);
            Center = new Vector2(PosX + Width / 2, PosY + Height / 2);

            // Border rectangles
            topBorder = new Rectangle(PosX, PosY, Width, 1);
            leftBorder = new Rectangle(PosX, PosY, 1, Height);
            rightBorder = new Rectangle(PosX + Width - 1, PosY, 1, Height);
            bottomBorder = new Rectangle(PosX, PosY + Height - 1, Width, 1);
            centerDot = new Rectangle((int)Center.X-1, (int)Center.Y-1, 3, 3);
        }

        public void DrawBorder(Texture2D pixel, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            if (Passable)
                borderColor = Color.Green;
            else
                borderColor = Color.Red;

            // Draw top line
            spriteBatch.Draw(pixel, topBorder, borderColor);
            // Draw left line
            spriteBatch.Draw(pixel, leftBorder, borderColor);
            // Draw right line
            spriteBatch.Draw(pixel, rightBorder, borderColor);
            // Draw bottom line
            spriteBatch.Draw(pixel, bottomBorder, borderColor);
            // Draw dot
            spriteBatch.Draw(pixel, centerDot, borderColor);
        }
    }
}
