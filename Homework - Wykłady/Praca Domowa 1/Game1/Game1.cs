using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D scott;
        Texture2D poww;
        Vector2 pos;

        Color bacgrund = Color.Green;
        Color bacgrundAfterHit = Color.Red;

        int moveSpeed = 3;

        Texture2D wallace;
        Color kolorPow = Color.White * 0;

        int wallacePosX = 300; 
        int wallacePosY = 200; 
        static int wallaceWidth = 80; 
        static int wallaceHeight = 120; 
        
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
        /// 

        void MoveWallace()     /////// moves wallace
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if ((pos.X + scott.Width >= wallacePosX + 4 && pos.X + 2 <= wallacePosX + wallaceWidth) && (pos.Y <= wallacePosY + wallaceHeight && pos.Y >= wallacePosY))
                {
                    wallacePosY -= 0;
                }
                else
                {
                    wallacePosY -= moveSpeed;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                // if (pos.X + scott.Width <= wallacePosX || pos.X > wallacePosX + wallaceWidth || pos.Y + scott.Height <= wallacePosY)
                // {
                if ((pos.X + scott.Width >= wallacePosX + 6 && pos.X + 3 <= wallacePosX + wallaceWidth) && (pos.Y <= wallacePosY + wallaceHeight - 6 && pos.Y + scott.Height >= wallacePosY))
                {
                    wallacePosY += 0;
                }
                else
                {
                    wallacePosY += moveSpeed;
                }
                //}
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if ((pos.X + scott.Width > wallacePosX && pos.X < wallacePosX) && (pos.Y + scott.Height > wallacePosY && pos.Y < wallacePosY + wallaceHeight))
                {
                    wallacePosX += 0;
                }
                else
                {
                    wallacePosX += moveSpeed;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if ((pos.X <= wallacePosX + wallaceWidth && pos.X >= wallacePosX) && (pos.Y >= wallacePosY - scott.Height && pos.Y <= wallacePosY + wallaceHeight))
                {
                    wallacePosX -= 0;
                }
                else
                {
                    wallacePosX -= moveSpeed;
                }

            }

        }

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
            scott = Content.Load<Texture2D>("sct");
            poww = Content.Load<Texture2D>("poww");
            wallace = Content.Load<Texture2D>("wallace");

            pos = new Vector2(10, 10);


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
            //  if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            // Exit();

            moveScott();
            MoveWallace();

            if((pos.X + scott.Width >= wallacePosX && pos.X < wallacePosX + wallaceWidth) && (pos.Y + scott.Height >= wallacePosY && pos.Y < wallacePosY + wallaceHeight))
            {
                bacgrund = bacgrundAfterHit;
                kolorPow = Color.White;
            }
            else
            {
                bacgrund = Color.Green;
                kolorPow = Color.White*0;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(bacgrund);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(scott, pos, Color.White);
            spriteBatch.Draw(wallace, new Rectangle(wallacePosX, wallacePosY, wallaceWidth, wallaceHeight), Color.White);

            
            spriteBatch.Draw(poww, new Rectangle(480, 20,222,180), kolorPow);

            spriteBatch.End();

            base.Draw(gameTime);
        }

       

        public void moveScott()   ////// moves SCOTT
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if ((pos.X + scott.Width >= wallacePosX + 4 && pos.X + 2 <= wallacePosX + wallaceWidth) && (pos.Y <= wallacePosY + wallaceHeight && pos.Y >= wallacePosY))
                {
                    pos.Y -= 0;
                }
                else
                {
                    pos.Y -= moveSpeed;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                // if (pos.X + scott.Width <= wallacePosX || pos.X > wallacePosX + wallaceWidth || pos.Y + scott.Height <= wallacePosY)
                // {
                if ((pos.X + scott.Width >= wallacePosX + 6 && pos.X + 3 <= wallacePosX + wallaceWidth) && (pos.Y <= wallacePosY + wallaceHeight - 6 && pos.Y + scott.Height >= wallacePosY))
                {
                    pos.Y += 0;
                }
                else
                {
                    pos.Y += moveSpeed;
                }
                //}
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if ((pos.X + scott.Width > wallacePosX && pos.X < wallacePosX) && (pos.Y + scott.Height > wallacePosY && pos.Y < wallacePosY + wallaceHeight))
                {
                    pos.X += 0;
                }
                else
                {
                    pos.X += moveSpeed;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if ((pos.X <= wallacePosX + wallaceWidth && pos.X >= wallacePosX) && (pos.Y >= wallacePosY - scott.Height && pos.Y <= wallacePosY + wallaceHeight))
                {
                    pos.X -= 0;
                }
                else
                {
                    pos.X -= moveSpeed;
                }

            }
        }
    }
}
