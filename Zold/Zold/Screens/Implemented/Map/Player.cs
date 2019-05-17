using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Map
{
    class Player
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

        public Player(Vector2 position, Texture2D texture, float sped, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int hp)
        {
            this.texture = texture;
            this.position = position;

            Width = texture.Width;
            Height = texture.Height;
            speed = sped;

            this.SpriteBatchSpriteSheet = SpriteBatchSpriteSheet;
            SpriteBatchSpriteSheet.MakeAnimation(3, "left", 250);
            SpriteBatchSpriteSheet.MakeAnimation(1, "right", 250);
            SpriteBatchSpriteSheet.MakeAnimation(2, "down", 250);
            SpriteBatchSpriteSheet.MakeAnimation(0, "up", 250);
            this.hp = hp;
            isMoving = false;
        }

        public void move(int wid, int heigh, bool canMoveLeft, bool canMoveUp, bool canMoveRight, bool canMoveDown, GameTime gt)
        {

            current = Keyboard.GetState();

            if (m_animPercent == 1)
            {
                // Can check for inputs
                if (canMoveRight && current.IsKeyDown(Keys.Right) && !current.IsKeyDown(Keys.Up) && !current.IsKeyDown(Keys.Down)) // Check for pressed only, so the player can hold the key down.
                {
                    m_from = GetPosition();
                    m_to = GetPosition() + new Vector2(64, 0); // Set the destination tile position
                    m_animPercent = 0;
                    Direction = "right";
                    isMoving = true;
                }

                else if (canMoveUp && current.IsKeyDown(Keys.Up) && !current.IsKeyDown(Keys.Left) && !current.IsKeyDown(Keys.Right))
                {
                    m_from = GetPosition();
                    m_to = GetPosition() + new Vector2(0, -64); // Set the destination tile position
                    m_animPercent = 0;
                    Direction = "up";
                    isMoving = true;
                }

                else if (canMoveDown && current.IsKeyDown(Keys.Down) && !current.IsKeyDown(Keys.Left) && !current.IsKeyDown(Keys.Right))
                {
                    m_from = GetPosition();
                    m_to = GetPosition() + new Vector2(0, 64); // Set the destination tile position
                    m_animPercent = 0;
                    Direction = "down";
                    isMoving = true;
                }

                else if (canMoveLeft && current.IsKeyDown(Keys.Left) && !current.IsKeyDown(Keys.Up) && !current.IsKeyDown(Keys.Down))
                {
                    m_from = GetPosition();
                    m_to = GetPosition() + new Vector2(-64, 0); // Set the destination tile position
                    m_animPercent = 0;
                    Direction = "left";
                    isMoving = true;
                }
                else
                {
                    isMoving = false;
                }

            }
            else
            {
                // Animate to the new position
                m_animPercent += 4 * (float)gt.ElapsedGameTime.TotalSeconds;
                if (m_animPercent >= 1) m_animPercent = 1;
                position = m_from + ((m_to - m_from) / 2) * m_animPercent;
                //isMoving = false;
            }
            centerPosition = new Vector2(position.X + 16, position.Y + 24);
        }

        public void Animation(GameTime gameTime)
        {
            SpriteBatchSpriteSheet.Begin();
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
                    SpriteBatchSpriteSheet.Draw(GetPosition(),2,1);
                }
            }
            //SpriteBatchSpriteSheet.Draw(GetPosition(), 3, 0);
            SpriteBatchSpriteSheet.End();
        }

        public void AnimateHealth(GameTime gameTime, SpriteBatchSpriteSheet SpriteBatchSpriteSheet, int posY)
        {
            SpriteBatchSpriteSheet.Begin();
            SpriteBatchSpriteSheet.Draw(new Vector2(posY, 16), 100-hp ,0);

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

        public Vector2 GetCenterPosition()
        {
            return centerPosition;
        }

        public void SetCenterPosition(Vector2 value)
        {
            centerPosition = value;
        }
    }
}