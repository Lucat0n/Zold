﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zold.Utilities;

namespace Zold.Screens.Implemented
{
    class WorldMapScreen : GameScreen
    {
        private bool isDownPressed = false;
        private bool isLeftPressed = false;
        private bool isMoving = false;
        private bool isRightPressed = false;
        private bool isUpPressed = false;
        private byte locationToVisit;
        private int route;
        private int routeProgress;
        private Zold.Utilities.Map map;
        private SpriteFont font;
        private Texture2D background;
        private Texture2D friendlyLocation;
        private Texture2D hostileLocation;
        private Texture2D line;
        private Texture2D unknownLocation;

        internal WorldMapScreen()
        {
        }

        public override void Draw(GameTime gameTime)
        {
            gameScreenManager.SpriteBatch.Begin();
            gameScreenManager.SpriteBatch.Draw(background, new Rectangle(0, 0, gameScreenManager.GraphicsDevice.Viewport.Width, gameScreenManager.GraphicsDevice.Viewport.Height), Color.White);
            gameScreenManager.SpriteBatch.Draw(line, new Rectangle(new Point(map.PlayerLocation.Location.X, map.PlayerLocation.Location.Y - gameScreenManager.GraphicsDevice.Viewport.Width / 20), new Point(gameScreenManager.GraphicsDevice.Viewport.Width / 40, gameScreenManager.GraphicsDevice.Viewport.Width / 20)), Color.White);
            foreach (var mn in map.Locations)
            {
                Rectangle r = new Rectangle(mn.Location, new Point(gameScreenManager.GraphicsDevice.Viewport.Width / 40, gameScreenManager.GraphicsDevice.Viewport.Width / 40));
                for (int i = 0; i < 4; i++)
                {
                    //IF TU MA BYĆ CZY ISTNIEJE TA DROGA TO NIE RYSUJ JAK NIE TO RYSUJ
                    /*if (mn.Neighbours[i] != null)
                    {
                        if (i % 2 == 1)
                        {
                            if (mn.Location.Y < mn.Neighbours[i].Location.Y)
                            {
                                gameScreenManager.SpriteBatch.Draw(line, new Rectangle(mn.Location.X + r.Width / 2, mn.Location.Y + r.Height / 2 - r.Width / 16, mn.Neighbours[i].Location.X - mn.Location.X, r.Width / 8), Color.Yellow);
                                gameScreenManager.SpriteBatch.Draw(line, new Rectangle(mn.Location.X + r.Width / 2 - r.Width / 16, mn.Location.Y + r.Height / 2, r.Width / 8, mn.Neighbours[i].Location.Y - mn.Location.Y), Color.Green);
                            }
                            else
                            {
                                gameScreenManager.SpriteBatch.Draw(line, new Rectangle(mn.Location.X + r.Width / 2, mn.Neighbours[i].Location.Y + r.Height / 2 - r.Width / 16, mn.Neighbours[i].Location.X - mn.Location.X, r.Width / 8), Color.Yellow);
                                gameScreenManager.SpriteBatch.Draw(line, new Rectangle(mn.Location.X + r.Width / 2 - r.Width / 16, mn.Location.Y + r.Height / 2, r.Width / 8, mn.Neighbours[i].Location.Y - mn.Location.Y), Color.Green);

                            }
                        }
                        else
                        {
                            if (mn.Location.X < mn.Neighbours[i].Location.X)
                            {
                                gameScreenManager.SpriteBatch.Draw(line, new Rectangle(mn.Location.X + r.Width / 2, mn.Location.Y + r.Height / 2 - r.Width / 16, mn.Neighbours[i].Location.X - mn.Location.X, r.Width / 8), Color.Indigo);
                                gameScreenManager.SpriteBatch.Draw(line, new Rectangle(mn.Neighbours[i].Location.X + r.Width / 2 - r.Width / 16, mn.Location.Y + r.Height / 2, r.Width / 8, mn.Neighbours[i].Location.Y - mn.Location.Y), Color.IndianRed);
                            }
                            else
                            {
                                gameScreenManager.SpriteBatch.Draw(line, new Rectangle(mn.Neighbours[i].Location.X + r.Width / 2, mn.Location.Y + r.Height / 2 - r.Width / 16,mn.Location.X - mn.Neighbours[i].Location.X, r.Width / 8), Color.Indigo);
                                gameScreenManager.SpriteBatch.Draw(line, new Rectangle(mn.Neighbours[i].Location.X + r.Width / 2 - r.Width / 16, mn.Location.Y + r.Height / 2, r.Width / 8, mn.Neighbours[i].Location.Y - mn.Location.Y), Color.IndianRed);
                            }
                        }
                    }*/
                    if (mn.Neighbours[i] != null)
                    {
                        switch (i)
                        {
                            case 0:
                                gameScreenManager.SpriteBatch.Draw(line, new Rectangle(mn.Location.X + r.Width / 2 - r.Width / 16, mn.Location.Y + r.Height / 2 - r.Width/16, r.Width / 8, mn.Neighbours[i].Location.Y - mn.Location.Y), Color.AntiqueWhite);
                                break;
                            case 1:
                                gameScreenManager.SpriteBatch.Draw(line, new Rectangle(mn.Neighbours[i].Location.X + r.Width / 2, mn.Location.Y + r.Height / 2 - r.Height / 16, mn.Location.X - mn.Neighbours[i].Location.X, r.Height / 8), Color.AntiqueWhite);
                                break;
                            case 2:
                                gameScreenManager.SpriteBatch.Draw(line, new Rectangle(mn.Location.X + r.Width / 2 - r.Width / 16, mn.Neighbours[i].Location.Y + r.Height / 2 - r.Width / 16, r.Width / 8,mn.Location.Y - mn.Neighbours[i].Location.Y), Color.AntiqueWhite);
                                break;
                            case 3:
                                gameScreenManager.SpriteBatch.Draw(line, new Rectangle(mn.Location.X + r.Width / 2, mn.Location.Y + r.Height / 2 - r.Height / 16, mn.Neighbours[i].Location.X - mn.Location.X, r.Height / 8), Color.AntiqueWhite);
                                break;
                        }   
                    }
                }
                gameScreenManager.SpriteBatch.Draw(mn.IsVisited ? (mn.IsFriendly ? friendlyLocation : hostileLocation) : unknownLocation, r, Color.White);
                //gameScreenManager.SpriteBatch.DrawString(font, mn.Name, new Vector2(400,400/*r.X + r.Width/2 - font.MeasureString(mn.Name).Length() / 2, r.Bottom + r.Height/2*/), Color.White, 0.0f, Vector2.Zero, 0.05f, SpriteEffects.None, 1f);
                gameScreenManager.SpriteBatch.DrawString(font, mn.Name, new Vector2(r.X + r.Width / 2 - font.MeasureString(mn.Name).Length() / 2, r.Bottom), Color.Orange);

            }
            gameScreenManager.SpriteBatch.End();
        }

        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Escape))
                gameScreenManager.RemoveScreen(this);
            if (keyboardState.IsKeyDown(Keys.Down) && !isDownPressed)
            {
                if (!isMoving && map.PlayerLocation.Neighbours[0] != null)
                {
                    route = Math.Abs(map.PlayerLocation.Location.Y - map.PlayerLocation.Neighbours[0].Location.Y) + Math.Abs(map.PlayerLocation.Location.X - map.PlayerLocation.Neighbours[0].Location.X);
                    Debug.WriteLine(route);
                    isMoving = true;
                    locationToVisit = 0;
                }
            }
            else if (keyboardState.IsKeyUp(Keys.Down))
                isDownPressed = false;
            if (!isMoving && keyboardState.IsKeyDown(Keys.Left) && !isLeftPressed)
            {
                if (map.PlayerLocation.Neighbours[1] != null)
                {
                    route = Math.Abs(map.PlayerLocation.Location.Y - map.PlayerLocation.Neighbours[1].Location.Y) + Math.Abs(map.PlayerLocation.Location.X - map.PlayerLocation.Neighbours[1].Location.X);
                    Debug.WriteLine(route);
                    isMoving = true;
                    locationToVisit = 1;
                }
            }
            else if (keyboardState.IsKeyUp(Keys.Left))
                isLeftPressed = false;
            if (!isMoving && keyboardState.IsKeyDown(Keys.Up) && !isUpPressed)
            {
                if (map.PlayerLocation.Neighbours[2] != null)
                {
                    route = Math.Abs(map.PlayerLocation.Location.Y - map.PlayerLocation.Neighbours[2].Location.Y) + Math.Abs(map.PlayerLocation.Location.X - map.PlayerLocation.Neighbours[2].Location.X);
                    Debug.WriteLine(route);
                    isMoving = true;
                    locationToVisit = 2;
                }
            }
            else if (keyboardState.IsKeyUp(Keys.Up))
                isUpPressed = false;
            if (!isMoving && keyboardState.IsKeyDown(Keys.Right) && !isRightPressed)
            {
                if (map.PlayerLocation.Neighbours[3] != null)
                {
                    route = Math.Abs(map.PlayerLocation.Location.Y - map.PlayerLocation.Neighbours[3].Location.Y) + Math.Abs(map.PlayerLocation.Location.X - map.PlayerLocation.Neighbours[3].Location.X);
                    Debug.WriteLine(route);
                    isMoving = true;
                    locationToVisit = 3;
                }
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
                isRightPressed = false;
        }

        public override void Update(GameTime gametime)
        {
            if (isMoving)
            {
                if (route-- > 0)
                    routeProgress++;
                else
                {
                    isMoving = false;
                    map.PlayerLocation = map.PlayerLocation.Neighbours[locationToVisit];
                }
            }
        }


        public override void LoadContent()
        {
            gameScreenManager.ContentLoader.LoadLocation("worldMap");
            friendlyLocation = Assets.Instance.Get("worldMap/Textures/friendlyLoc");
            hostileLocation = Assets.Instance.Get("worldMap/Textures/hostileLoc");
            unknownLocation = Assets.Instance.Get("worldMap/Textures/unknownLoc");
            background = Assets.Instance.Get("worldMap/Textures/ulicapixelated");
            line = Assets.Instance.Get("worldMap/Textures/lineTex");
            font = Assets.Instance.Get("placeholders/Fonts/dialog");
            map = gameScreenManager.Map;
        }

        public override void UnloadContent()
        {
            gameScreenManager.ContentLoader.UnloadLocation("worldMap");
        }
    }
}
