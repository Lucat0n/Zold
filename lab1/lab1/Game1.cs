using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace lab1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D player, enemy;
        Vector2 playerPos, enemyPos, direction;
        SpriteFont font;
        int playerSpeed = 2;
        int enemySpeed = 1;
        bool colisionX = false;
        bool colisionY = false;
        double distance;

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
            playerPos = new Vector2(0, 0);
            enemyPos = new Vector2(300, 300);

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
            font = Content.Load<SpriteFont>("font");

            player = Content.Load<Texture2D>("fox");
            enemy = Content.Load<Texture2D>("skeleton");

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                playerPos.Y -= playerSpeed;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                playerPos.Y += playerSpeed;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                playerPos.X -= playerSpeed;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                playerPos.X += playerSpeed;

            distance = Math.Sqrt(Math.Pow(playerPos.X - enemyPos.X, 2) + Math.Pow(playerPos.Y - enemyPos.Y, 2));
            direction = new Vector2(playerPos.X - enemyPos.X, playerPos.Y - enemyPos.Y);
            direction.Normalize();

            if (distance <= 100)
            {
                enemyPos.X += direction.X * enemySpeed;
                enemyPos.Y += direction.Y * enemySpeed;
            }
            /*
            if ((position1.X > position2.X && position1.X < position2.X + 50) || (position1.X + 50 > position2.X && position1.X + 50 < position2.X + 50))
                colisionX = true;
            else
                colisionX = false;

            if ((position1.Y > position2.Y && position1.Y < position2.Y + 50) || (position1.Y + 50 > position2.Y && position1.Y + 50 < position2.Y + 50))
                colisionY = true;
            else
                colisionY = false;
                */
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(enemy, enemyPos);
            spriteBatch.Draw(player, playerPos);
            spriteBatch.DrawString(font, "Distance: " + distance.ToString(), new Vector2(100, 80), Color.Black);
            spriteBatch.DrawString(font, "Direction: \n x: " + direction.X.ToString() + " y: " + direction.Y.ToString(), new Vector2(100, 100), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
