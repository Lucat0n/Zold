using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Zold.Utilities;
using Zold.Screens.Implemented.Combat;
using Zold.Screens.Implemented.Combat.Characters;
using Zold.Screens.Implemented.Combat.Characters.Enemies;

namespace Zold.Screens.Implemented
{
    class SplashScreen : GameScreen
    {
        Texture2D splash;
        TimeSpan LifeTime = new TimeSpan(0, 0, 3);

        //Combat
        CombatScreen Combat;
        Player combatPlayer;
        Enemy skeleton;
        Enemy rat;
        List<Enemy> enemies;

        public SplashScreen()
        {
            FadeInTime = new TimeSpan(0, 0, 5);
            FadeOutTime = new TimeSpan(0, 0, 5);
            IsTransparent = false;
        }

        public override void Draw(GameTime gameTime)
        {

            gameScreenManager.SpriteBatch.Begin();
            gameScreenManager.SpriteBatch.Draw(splash, new Vector2(gameScreenManager.GraphicsDevice.Viewport.Width / 2 - splash.Width / 2,
                gameScreenManager.GraphicsDevice.Viewport.Height / 2 - splash.Height / 2),
                Color.White);
            gameScreenManager.SpriteBatch.End();
            gameScreenManager.FadeScreen(FadeAlpha);
        }

        public override void Update(GameTime gameTime)
        {
            switch (ScreenState)
            {
                case ScreenState.FadeIn:
                    if (UpdateFade(gameTime, FadeInTime))
                        this.ScreenState = ScreenState.Active;
                    
                    break;
                case ScreenState.Active:
                    LifeTime -= new TimeSpan((long)gameTime.ElapsedGameTime.TotalMilliseconds * 10000);
                    if (LifeTime <= TimeSpan.Zero)
                        this.ScreenState = ScreenState.FadeOut;
                    break;
                case ScreenState.FadeOut:
                    if (UpdateFade(gameTime, FadeOutTime))
                    {
                        gameScreenManager.RemoveScreen(this);
                        gameScreenManager.InsertScreen(new MenuScreen());
                    }
                    break;
            }
        }

        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.ScreenState = ScreenState.FadeOut;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F1))
            {
                gameScreenManager.RemoveScreen(this);
                gameScreenManager.InsertScreen(Combat);
            }
        }

        public override void LoadContent()
        {
            gameScreenManager.ContentLoader.LoadLocation("splash");
            gameScreenManager.ContentLoader.LoadLocation("combat");
            splash = Assets.Instance.Get("splash/Textures/rzprod");

            // Combat
            enemies = new List<Enemy>();
            combatPlayer = new Player(new Vector2(0, 200), 100, enemies, new SpriteBatchSpriteSheet(gameScreenManager.GraphicsDevice, Assets.Instance.Get("combat/Textures/main"), 4, 3, 32, 48));
            skeleton = new Mob(combatPlayer, new Vector2(300, 300), new SpriteBatchSpriteSheet(gameScreenManager.GraphicsDevice, Assets.Instance.Get("combat/Textures/main"), 4, 3, 32, 48), 32, 48);
            rat = new Charger(combatPlayer, new Vector2(300, 400), new SpriteBatchSpriteSheet(gameScreenManager.GraphicsDevice, Assets.Instance.Get("combat/Textures/rat"), 5, 4, 44, 20), 44, 20);

            enemies.Add(skeleton);
            enemies.Add(rat);
            Combat = new Combat.CombatScreen(combatPlayer, enemies);
        }

        public override void UnloadContent()
        {
            gameScreenManager.ContentLoader.UnloadLocation("splash");
            gameScreenManager.ContentLoader.UnloadLocation("combat");
            splash.Dispose();
        }
    }
}
