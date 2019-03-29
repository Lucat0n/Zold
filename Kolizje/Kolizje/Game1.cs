using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Wyk1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D texture1, texture2;
        Vector2 position1, position2;
        SpriteFont font;
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
            position1 = new Vector2(0, 0);
            position2 = new Vector2(100, 100);

            texture1 = new Texture2D(this.GraphicsDevice, 50, 50);
            texture2 = new Texture2D(this.GraphicsDevice, 50, 50);

            Color[] rectangle1 = new Color[50 * 50];
            Color[] rectangle2 = new Color[50 * 50];

            for (int i = 0; i < 2500; i++)
            {
                rectangle1[i] = Color.Red;
                rectangle2[i] = Color.Purple;
            }

            texture1.SetData(rectangle1);
            texture2.SetData(rectangle2);

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
                position1.Y -= 2;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                position1.Y += 2;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                position1.X -= 2;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                position1.X += 2;

            distance = Math.Sqrt(Math.Pow(position1.X - position2.X, 2) + Math.Pow(position1.Y - position2.Y, 2));

            if ((position1.X > position2.X && position1.X < position2.X + 50) || (position1.X + 50 > position2.X && position1.X + 50 < position2.X + 50))
                colisionX = true;
            else
                colisionX = false;

            if ((position1.Y > position2.Y && position1.Y < position2.Y + 50) || (position1.Y + 50 > position2.Y && position1.Y + 50 < position2.Y + 50))
                colisionY = true;
            else
                colisionY = false;

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
            spriteBatch.Draw(texture1, position2);
            spriteBatch.Draw(texture2, position1);
                if(colisionX && colisionY)
            spriteBatch.DrawString(font, "Kolizja!", new Vector2(100, 80), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
