using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;
using System;

namespace Map
{
    class DialogManager
    {
        Texture2D dotekstu;
        SpriteFont dialog;

        public DialogManager(Texture2D dotekstu, SpriteFont dialog)
        {
            this.dotekstu = dotekstu;
            this.dialog = dialog;

        }

        public void displayDialog(SpriteBatch spriteBatch, Player playerOne, Texture2D npcet, int posx, int posy)
        {

            if (playerOne.GetPosition().X >= posx - 50 && playerOne.GetPosition().X < posx + npcet.Width + 20
                && playerOne.GetPosition().Y >= posy && playerOne.GetPosition().Y < posy + npcet.Height + 30
                )
            {
                Rectangle tlo = new Rectangle(100, 420, 500, 50);
                spriteBatch.Draw(dotekstu, tlo, Color.White);

                spriteBatch.DrawString(dialog, "Witaj zielona magnetyczna gwiazdo!", new Vector2(145, 425), Color.White);
            }
        }

    }
}
