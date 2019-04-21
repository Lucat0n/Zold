using System;
using System.Diagnostics;
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
        private float fadeQuant;
        private float fadeProgress;
        private TimeSpan fadeInTime;
        private TimeSpan fadeOutTime;
        protected GameScreenManager gameScreenManager;
        private ScreenState screenState = ScreenState.FadeIn;
        #endregion

        #region getset
        public bool IsExiting { get { return isExiting; } protected set { isExiting = value; } }
        public bool IsTransparent { get { return isTransparent; } protected set { isTransparent = value; } }
        public byte FadeAlpha { get { return (byte)(255 - fadeProgress * 255); } }
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
        protected bool UpdateFade(GameTime gameTime, TimeSpan time)
        {
            if (time <= TimeSpan.Zero)
                fadeQuant = 1;
            else
                fadeQuant = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / time.TotalMilliseconds);

            if (ScreenState == ScreenState.FadeIn)
                fadeProgress += fadeQuant;
            else if(ScreenState == ScreenState.FadeOut)
                fadeProgress -= fadeQuant;

            if((fadeProgress >= 0) && (fadeProgress <= 1))
                return false;
            else
                return true;
        }
    }
}
