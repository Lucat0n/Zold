using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Zold.Utilities;
using System.Collections.Generic;

namespace Zold.Screens.Implemented.Map
{
    class Npc
    {
    
        public Texture2D texture { get; set; }
        private Vector2 position;
        public float speed { get; private set; }
        
        public int Width { get; set; }
        public int Height { get; set; }

        public string Direction { get; private set; }

        public int hp;

        List<string> powiedzenie;

        public Npc(Texture2D texture, Vector2 position, List<string> powiedzenie)
        {
            this.texture = texture;
            this.position = position;
            this.powiedzenie = powiedzenie;
            Width = texture.Width;
            Height = texture.Height;

        }

        public List<string> getPowiedzanie()
        {
            return powiedzenie;
        }



        public Vector2 GetPosition()
        {
            return position;
        }

        public void SetPosition(Vector2 value)
        {
            position = value;
        }

        public void SetPosition(float px, float py)
        {
            Vector2 value = new Vector2(px, py);
            position = value;
        }

        public Texture2D GetTexture()
        {
            return texture;
        }

        public void SetTexture(Texture2D newTexture)
        {
            texture = newTexture;
        }


    }
}
