using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Collision
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D Player;
        Texture2D Bot;

        Color bckgrnd = Color.Azure;
        Color bckgrndHitReg = Color.Red;

        int playerX = 200;
        int playerY = 200;
        int playerSpeed = 4;
        Vector2 playerPos = new Vector2(50, 50);
        Vector2 botPos = new Vector2(400, 400);
        Vector2 botOrigin = new Vector2(350, 150);

        private bool isColliding = false;

        double time;
        double speed = MathHelper.PiOver2;
        double radius = 150.0f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Player = Content.Load<Texture2D>("player");
            Bot = Content.Load<Texture2D>("trashmaster");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {


            // TODO: Add your update logic here
            if (isColliding) bckgrnd = bckgrndHitReg;
            else bckgrnd = Color.Azure;
            movePlayer();
            moveBot(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(bckgrnd);
            spriteBatch.Begin();

            //Color[] data = new Color[80 * 30];
            //for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
            //Bot.SetData(data);

            
            spriteBatch.Draw(Bot, botPos, Color.White);
            spriteBatch.Draw(Player, playerPos, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void movePlayer()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                playerPos.Y -= playerSpeed;
                if (playerPos.X - 15 <= botPos.X + 60 && playerPos.X + 15 >= botPos.X - 20 && playerPos.Y + 25 >= botPos.Y - 15 && playerPos.Y-25 <= botPos.Y+15)
                {
                    isColliding = true;
                }
                else
                {
                    isColliding = false;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                playerPos.Y += playerSpeed;
                if (playerPos.X - 15 <= botPos.X + 60 && playerPos.X + 15 >= botPos.X - 20 && playerPos.Y + 25 >= botPos.Y - 15 && playerPos.Y - 25 <= botPos.Y + 15)
                {
                    isColliding = true;
                }
                else
                {
                    isColliding = false;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                playerPos.X += playerSpeed;
                if (playerPos.X + 15 >= botPos.X -20 && playerPos.X - 15 <= botPos.X + 60 && playerPos.Y -25 < botPos.Y + 15 && playerPos.Y+25 >= botPos.Y-15)
                {
                    isColliding = true;
                }
                else
                {
                    isColliding = false;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                playerPos.X -= playerSpeed;
                if (playerPos.X - 15 <= botPos.X + 60 && playerPos.X + 15 >= botPos.X - 20 && playerPos.Y - 25 < botPos.Y + 15 && playerPos.Y + 25 > botPos.Y - 15)
                {
                    isColliding = true;
                }
                else
                {
                    isColliding = false;
                }

            }
        }

        private void moveBot(GameTime gameTime)
        {
            time = gameTime.TotalGameTime.TotalSeconds;
            botPos.Y = (float)(System.Math.Sin(time * speed) * radius + botOrigin.Y);
            if(botPos.Y - 15 <= playerPos.Y + 25 && botPos.Y + 15 >= playerPos.Y + 25 && botPos.X -20 <= playerPos.X + 15 && botPos.X + 60 >= playerPos.X - 15)
            {
                isColliding = true;
            }
            else
            {
                isColliding = false;
            }
        }

    }
}
