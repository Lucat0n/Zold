using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zold.Utilities;
using static Zold.Utilities.Map;

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
        private int directionChangeStep;
        private int route;
        private int initialRoute;
        private int posX;
        private int posY;
        private int routeProgress;
        private Zold.Utilities.Map map;
        private Rectangle playerRec;
        private SpriteFont font;
        private Texture2D background;
        private Texture2D friendlyLocation;
        private Texture2D hostileLocation;
        private Texture2D line;
        private Texture2D pietr;
        private Texture2D unknownLocation;

        internal WorldMapScreen()
        {
        }

        public override void Draw(GameTime gameTime)
        {
            gameScreenManager.SpriteBatch.Begin();
            gameScreenManager.SpriteBatch.Draw(background, new Rectangle(0, 0, gameScreenManager.GraphicsDevice.Viewport.Width, gameScreenManager.GraphicsDevice.Viewport.Height), Color.White);
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
                gameScreenManager.SpriteBatch.DrawString(font, mn.Name, new Vector2(r.X + r.Width / 2 - font.MeasureString(mn.Name).Length() / 2, r.Bottom), Color.Red);

            }
            gameScreenManager.SpriteBatch.Draw(pietr, playerRec, Color.White);
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
                    initialRoute = route;
                    //Debug.WriteLine(route);
                    isMoving = true;
                    locationToVisit = 0;
                    routeProgress = 0;
                }
            }
            else if (keyboardState.IsKeyUp(Keys.Down))
                isDownPressed = false;
            if (!isMoving && keyboardState.IsKeyDown(Keys.Left) && !isLeftPressed)
            {
                if (map.PlayerLocation.Neighbours[1] != null)
                {
                    route = Math.Abs(map.PlayerLocation.Location.Y - map.PlayerLocation.Neighbours[1].Location.Y) + Math.Abs(map.PlayerLocation.Location.X - map.PlayerLocation.Neighbours[1].Location.X);
                    initialRoute = route;
                    //Debug.WriteLine(route);
                    isMoving = true;
                    locationToVisit = 1;
                    routeProgress = 0;
                }
            }
            else if (keyboardState.IsKeyUp(Keys.Left))
                isLeftPressed = false;
            if (!isMoving && keyboardState.IsKeyDown(Keys.Up) && !isUpPressed)
            {
                if (map.PlayerLocation.Neighbours[2] != null)
                {
                    route = Math.Abs(map.PlayerLocation.Location.Y - map.PlayerLocation.Neighbours[2].Location.Y) + Math.Abs(map.PlayerLocation.Location.X - map.PlayerLocation.Neighbours[2].Location.X);
                    initialRoute = route;
                    //Debug.WriteLine(route);
                    isMoving = true;
                    locationToVisit = 2;
                    routeProgress = 0;
                }
            }
            else if (keyboardState.IsKeyUp(Keys.Up))
                isUpPressed = false;
            if (!isMoving && keyboardState.IsKeyDown(Keys.Right) && !isRightPressed)
            {
                if (map.PlayerLocation.Neighbours[3] != null)
                {
                    route = Math.Abs(map.PlayerLocation.Location.Y - map.PlayerLocation.Neighbours[3].Location.Y) + Math.Abs(map.PlayerLocation.Location.X - map.PlayerLocation.Neighbours[3].Location.X);
                    initialRoute = route;
                    //Debug.WriteLine(route);
                    isMoving = true;
                    locationToVisit = 3;
                    routeProgress = 0;
                }
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
                isRightPressed = false;
        }

        public override void Update(GameTime gametime)
        {
            if (isMoving)
            {
                //Debug.WriteLine(gametime.ElapsedGameTime.Milliseconds);
                switch (locationToVisit) {
                    case 0:
                        if (posY < map.PlayerLocation.Neighbours[locationToVisit].Location.Y - gameScreenManager.GraphicsDevice.Viewport.Height / 15)
                        {
                            directionChangeStep = routeProgress;
                            posY = map.PlayerLocation.Location.Y - gameScreenManager.GraphicsDevice.Viewport.Height / 10 + initialRoute / 50 * routeProgress;
                        }
                        else if (posX < map.PlayerLocation.Neighbours[locationToVisit].Location.X)
                        {
                            if (routeProgress < 49)
                                posX = map.PlayerLocation.Location.X + initialRoute / 50 * (routeProgress - directionChangeStep);
                            else
                                posX = map.PlayerLocation.Neighbours[locationToVisit].Location.X - 4;
                        }
                        else if (posX > map.PlayerLocation.Neighbours[locationToVisit].Location.X)
                        {
                            if (routeProgress < 49)
                                posX = map.PlayerLocation.Location.X - initialRoute / 50 * (routeProgress - directionChangeStep);
                            else
                                posX = map.PlayerLocation.Neighbours[locationToVisit].Location.X;
                        }
                        break;
                    case 1:
                        if (posX > map.PlayerLocation.Neighbours[locationToVisit].Location.X)
                        {
                            directionChangeStep = routeProgress;
                            posX = map.PlayerLocation.Location.X - initialRoute / 50 * routeProgress;
                        }
                        else if (posY > map.PlayerLocation.Neighbours[locationToVisit].Location.Y)
                        {
                            posX = map.PlayerLocation.Neighbours[locationToVisit].Location.X;
                            posY = map.PlayerLocation.Location.Y - gameScreenManager.GraphicsDevice.Viewport.Height / 20 - initialRoute / 50 * (routeProgress - directionChangeStep);
                        }
                        else if (posY < map.PlayerLocation.Neighbours[locationToVisit].Location.Y)
                        {
                            posX = map.PlayerLocation.Neighbours[locationToVisit].Location.X;
                            posY = map.PlayerLocation.Location.Y - gameScreenManager.GraphicsDevice.Viewport.Height / 20 + initialRoute / 50 * (routeProgress - directionChangeStep);
                        }

                        break;
                    case 2:
                        if (posY > map.PlayerLocation.Neighbours[locationToVisit].Location.Y - gameScreenManager.GraphicsDevice.Viewport.Height / 15)
                        {
                            directionChangeStep = routeProgress;
                            posY = map.PlayerLocation.Location.Y - gameScreenManager.GraphicsDevice.Viewport.Height / 10 - initialRoute / 50 * routeProgress;
                        }
                        else if (posX < map.PlayerLocation.Neighbours[locationToVisit].Location.X)
                        {
                            if (routeProgress < 48)
                                posX = map.PlayerLocation.Location.X + initialRoute / 50 * (routeProgress - directionChangeStep);
                            else
                                posX = map.PlayerLocation.Neighbours[locationToVisit].Location.X - 4;
                        }
                        else if (posX > map.PlayerLocation.Neighbours[locationToVisit].Location.X)
                        {
                            if (routeProgress < 48)
                                posX = map.PlayerLocation.Location.X - initialRoute / 50 * (routeProgress - directionChangeStep);
                            else
                                posX = map.PlayerLocation.Neighbours[locationToVisit].Location.X;
                        }
                        break;
                    case 3:
                        if (posX < map.PlayerLocation.Neighbours[locationToVisit].Location.X)
                        {
                            directionChangeStep = routeProgress;
                            posX = map.PlayerLocation.Location.X + initialRoute / 50 * routeProgress;
                            Console.WriteLine("Przed");
                            Console.WriteLine(map.PlayerLocation.Neighbours[locationToVisit].Location.X);
                            Console.WriteLine(posX);
                            posX = posX > map.PlayerLocation.Neighbours[locationToVisit].Location.X ? map.PlayerLocation.Neighbours[locationToVisit].Location.X : posX;
                            Console.WriteLine("Po");
                            Console.WriteLine(map.PlayerLocation.Neighbours[locationToVisit].Location.X);
                            Console.WriteLine(posX);
                        }
                        else if (posY < map.PlayerLocation.Neighbours[locationToVisit].Location.Y)
                        {
                            posY = map.PlayerLocation.Location.Y - gameScreenManager.GraphicsDevice.Viewport.Height / 20 + initialRoute / 50 * (routeProgress - directionChangeStep);
                            Console.WriteLine("Powyżej. Pozycja: " + posY);
                            Console.WriteLine("");
                            //Console.WriteLine("Route: " + routeProgress.ToString());
                            //Console.WriteLine("Direct: " + directionChangeStep.ToString());
                        }
                        else if (posY > map.PlayerLocation.Neighbours[locationToVisit].Location.Y)
                        {
                            posY = map.PlayerLocation.Location.Y - gameScreenManager.GraphicsDevice.Viewport.Height / 20 - initialRoute / 50 * (routeProgress - directionChangeStep);
                            Console.WriteLine("Poniżej. Pozycja: " + posY);
                            Console.WriteLine("");
                            //Console.WriteLine("Route: " + routeProgress.ToString());
                            //Console.WriteLine("Direct: " + directionChangeStep.ToString());
                        }
                        break;
                }
                Point playerPos = new Point(posX, posY + gameScreenManager.GraphicsDevice.Viewport.Height / 40);
                playerRec = new Rectangle(playerPos, new Point(gameScreenManager.GraphicsDevice.Viewport.Width / 40, gameScreenManager.GraphicsDevice.Viewport.Height / 20));
                route -= initialRoute / 50;
                if (route > 0)
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
            background = Assets.Instance.Get("worldMap/Textures/miasto");
            line = Assets.Instance.Get("worldMap/Textures/lineTex");
            pietr = Assets.Instance.Get("worldMap/Textures/pietr");
            font = Assets.Instance.Get("placeholders/Fonts/dialog");
            map = gameScreenManager.Map;
            map.Locations.Clear();
            mapNode PlayerLocation = new mapNode(new Point((int)(gameScreenManager.GraphicsDevice.Viewport.Width / 2.8), (int)(gameScreenManager.GraphicsDevice.Viewport.Height / 1.75)), "Dormitory", null);
            PlayerLocation.IsVisited = true;
            PlayerLocation.IsFriendly = true;
            PlayerLocation.Name = "Akademik";
            mapNode[] mn = new mapNode[] { PlayerLocation, null, null, null };
            //mapNode dormitoryOutside = new mapNode(new Point(400, 200), "DormOutside", mn);
            mapNode[] mn2 = new mapNode[] { null, null, PlayerLocation, null };
            mapNode dormitoryOutside2 = new mapNode(new Point((int)(gameScreenManager.GraphicsDevice.Viewport.Width / 3.35), 2*((int)(gameScreenManager.GraphicsDevice.Viewport.Height / 2.7))), "DormOutside2", mn2);
            //dormitoryOutside.Name = "Dziedziniec akademika";
            dormitoryOutside2.Name = "Rog ulicy";
            //PlayerLocation.Neighbours[3] = dormitoryOutside;
            PlayerLocation.Neighbours[1] = dormitoryOutside2;
            map.PlayerLocation = PlayerLocation;
            map.addLocation(PlayerLocation);
            //map.addLocation(dormitoryOutside);
            map.addLocation(dormitoryOutside2);
            playerRec = new Rectangle(new Point(map.PlayerLocation.Location.X, map.PlayerLocation.Location.Y - gameScreenManager.GraphicsDevice.Viewport.Height / 20), new Point(gameScreenManager.GraphicsDevice.Viewport.Width / 40, gameScreenManager.GraphicsDevice.Viewport.Height / 20));
            posX = map.PlayerLocation.Location.X;
            posY = map.PlayerLocation.Location.Y - gameScreenManager.GraphicsDevice.Viewport.Width / 20;
        }

        public override void UnloadContent()
        {
            gameScreenManager.ContentLoader.UnloadLocation("worldMap");
        }
    }
}
