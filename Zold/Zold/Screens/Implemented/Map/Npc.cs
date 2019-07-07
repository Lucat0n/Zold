using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Map
{
    class Npc
    {
        private SpriteBatchSpriteSheet SpriteBatchSpriteSheet;

        public Texture2D texture { get; set; }
        private Vector2 position;
        private Vector2 centerPosition;
        public float speed { get; private set; }

        public KeyboardState current;
        public KeyboardState previous;

        public int Width { get; set; }
        public int Height { get; set; }

        public string Direction { get; private set; }
        public bool isMoving;

        public int hp;
        Vector2 m_from;
        Vector2 m_to;

        float m_animPercent = 1;
        float m_animSpeed = 1.0f / .5f;

        public List<string> powiedzenie;

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


        //TODO: wywalić matrixa z parametrów
        public void Animation(GameTime gameTime, Matrix cameraTransform)
        {
            SpriteBatchSpriteSheet.Begin(transformMatrix: cameraTransform);
            if (isMoving)
            {
                SpriteBatchSpriteSheet.PlayFullAniamtion(GetPosition(), Direction, gameTime);
            }
            else
            {
                if (Direction == "up")
                    SpriteBatchSpriteSheet.Draw(GetPosition(), 0, 0);
                else if (Direction == "right")
                    SpriteBatchSpriteSheet.Draw(GetPosition(), 1, 0);
                else if (Direction == "down")
                    SpriteBatchSpriteSheet.Draw(GetPosition(), 2, 0);
                else if (Direction == "left")
                    SpriteBatchSpriteSheet.Draw(GetPosition(), 3, 0);
                else
                {
                    SpriteBatchSpriteSheet.Draw(GetPosition(), 2, 1);
                }
            }
            //SpriteBatchSpriteSheet.Draw(GetPosition(), 3, 0);
            SpriteBatchSpriteSheet.End();
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
