using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Zold.Screens
{

    public enum ScreenState
    {
        FadeIn,
        Active,
        FadeOut,
    }

    public abstract class GameScreen
    {
        #region zmienne
        private bool isExiting = false;
        private bool isTransparent = false;
        private float fadeUpdateDelta;
        private TimeSpan fadeInTime;
        private TimeSpan fadeOutTime;
        protected GameScreenManager gameScreenManager;
        private ScreenState screenState = ScreenState.FadeIn;
        #endregion

        #region getset
        public bool IsExiting { get { return isExiting; } protected set { isExiting = value; } }
        public bool IsTransparent { get { return isTransparent; } protected set { isTransparent = value; } }
        public TimeSpan FadeInTime { get { return fadeInTime; } protected set { fadeInTime = value; } }
        public TimeSpan FadeOutTime { get { return fadeOutTime; } protected set { fadeOutTime = value; } }
        public GameScreenManager GameScreenManager { get { return gameScreenManager; } internal set { gameScreenManager = value; } }
        public ScreenState ScreenState { get { return screenState; } protected set { screenState = value; } }
        #endregion

        #region init
        public abstract void LoadContent();
        public abstract void UnloadContent();
        #endregion

        #region update
        public virtual void Update(GameTime gameTime)
        {
            if (isExiting)
            {
                screenState = ScreenState.FadeOut;
                if (UpdateFade(gameTime, fadeOutTime))
                {
                    gameScreenManager.RemoveScreen(this);
                }
            }
            else
            {
                if (!UpdateFade(gameTime, fadeInTime))
                    screenState = ScreenState.FadeIn;
                else
                    screenState = ScreenState.Active;
            }
        }
        #endregion

        public abstract void Draw(GameTime gameTime);
        public abstract void HandleInput(MouseState mouseState, Point mousePos, KeyboardState keyboardState);

        /// <summary>
        /// Ustawia wartość kanału alfa w przejściu dla GameScreenManagera
        /// </summary>
        /// <returns>true jeśli zakończy przenikanie, false jeśli jest w trakcie</returns>
        bool UpdateFade(GameTime gameTime, TimeSpan time)
        {
            if (time == TimeSpan.Zero)
            {
                fadeUpdateDelta = 1;
                return true;
            }
            else
            {
                fadeUpdateDelta = (float)(gameTime.ElapsedGameTime.TotalMilliseconds /
                time.TotalMilliseconds);
                return false;
            }
            
        }
    }
}
