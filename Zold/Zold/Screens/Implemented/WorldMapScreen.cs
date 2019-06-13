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

namespace Zold.Screens.Implemented
{
    class WorldMapScreen : GameScreen
    {
        private bool isDownPressed = false;
        private bool isLeftPressed = false;
        private bool isRightPressed = false;
        private bool isUpPressed = false;
        private List<mapNode> locations;
        private List<Tuple<mapNode, mapNode>> visitedVertices;
        private mapNode playerLocation;
        private SpriteFont font;
        private Texture2D background;
        private Texture2D friendlyLocation;
        private Texture2D hostileLocation;
        private Texture2D line;
        private Texture2D unknownLocation;

        internal WorldMapScreen()
        {
            locations = new List<mapNode>();
            visitedVertices = new List<Tuple<mapNode, mapNode>>();
            playerLocation = new mapNode(new Point(310, 310), "Dormitory", null);
            playerLocation.IsVisited = true;
            playerLocation.IsFriendly = true;
            playerLocation.Name = "Akademik";
            mapNode[] mn = new mapNode[] { null, playerLocation, null, null };
            mapNode dormitoryOutside = new mapNode(new Point(400, 200), "DormOutside", mn);
            dormitoryOutside.Name = "Dziedziniec akademika";
            playerLocation.Neighbours[3] = dormitoryOutside;
            locations.Add(playerLocation);
            locations.Add(dormitoryOutside);
        }

        internal class mapNode
        {
            private Point location;
            private mapNode[] neighbours;
            private String name;
            private String targetMapID;
            private bool isFriendly = false;
            private bool isVisited = false;

            public mapNode()
            {
                location = new Point();
                neighbours = new mapNode[4];
                targetMapID = "";
            }

            public mapNode(Point location, String targetMapID, mapNode[] neighbours)
            {
                this.location = location;
                this.neighbours = neighbours ?? new mapNode[4];
                this.targetMapID = targetMapID;
            }

            public mapNode(Point location, String targetMapID, bool isFriendly, bool isVisited, mapNode[] neighbours)
            {
                this.location = location;
                this.neighbours = neighbours ?? new mapNode[4];
                this.targetMapID = targetMapID;
                this.isFriendly = isFriendly;
                this.isVisited = isVisited;
            }

            public string Name { get => name; set => name = value; }
            internal bool IsFriendly { get => isFriendly; set => isFriendly = value; }
            internal bool IsVisited { get => isVisited; set => isVisited = value; }
            internal Point Location { get => location; set => location = value; }
            internal mapNode[] Neighbours { get => neighbours; }
            internal String TargetMapID { get => targetMapID; set => targetMapID = value; }
        }

        public override void Draw(GameTime gameTime)
        {
            gameScreenManager.SpriteBatch.Begin();
            gameScreenManager.SpriteBatch.Draw(background, new Rectangle(0,0,gameScreenManager.GraphicsDevice.Viewport.Width, gameScreenManager.GraphicsDevice.Viewport.Height), Color.White);
            
            foreach (mapNode mn in locations)
            {
                Rectangle r = new Rectangle(mn.Location, new Point(gameScreenManager.GraphicsDevice.Viewport.Width / 40, gameScreenManager.GraphicsDevice.Viewport.Width / 40));
                foreach (mapNode neigh in mn.Neighbours)
                {
                    //IF TU MA BYĆ CZY ISTNIEJE TA DROGA TO NIE RYSUJ JAK NIE TO RYSUJ
                    if (neigh != null)
                    {
                        gameScreenManager.SpriteBatch.Draw(line, new Rectangle(mn.Location.X + r.Width/2 - r.Width / 12, mn.Location.Y + r.Height/2, neigh.Location.X - mn.Location.X, r.Width / 6), Color.NavajoWhite);
                        gameScreenManager.SpriteBatch.Draw(line, new Rectangle(mn.Location.X + r.Width/2 - r.Width / 8, mn.Location.Y + r.Height/2, 4, neigh.Location.Y - mn.Location.Y), Color.NavajoWhite);
                    }
                }
                gameScreenManager.SpriteBatch.Draw(mn.IsVisited ? (mn.IsFriendly ? friendlyLocation : hostileLocation) : unknownLocation, r, Color.White);
                //gameScreenManager.SpriteBatch.DrawString(font, mn.Name, new Vector2(400,400/*r.X + r.Width/2 - font.MeasureString(mn.Name).Length() / 2, r.Bottom + r.Height/2*/), Color.White, 0.0f, Vector2.Zero, 0.05f, SpriteEffects.None, 1f);
                gameScreenManager.SpriteBatch.DrawString(font, mn.Name, new Vector2(r.X + r.Width/2 - font.MeasureString(mn.Name).Length() / 2, r.Bottom), Color.Orange);
                
            }
            gameScreenManager.SpriteBatch.End();
        }

        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Escape))
                gameScreenManager.RemoveScreen(this);
            if (keyboardState.IsKeyDown(Keys.Down) && !isDownPressed)
            {
                if (playerLocation.Neighbours[0] != null)
                {
                    playerLocation = playerLocation.Neighbours[0];
                    Debug.WriteLine(playerLocation.TargetMapID);
                }
            }
            else if (keyboardState.IsKeyUp(Keys.Down))
                isDownPressed = false;
            if (keyboardState.IsKeyDown(Keys.Left) && !isLeftPressed)
            {
                if (playerLocation.Neighbours[1] != null)
                {
                    playerLocation = playerLocation.Neighbours[1];
                    Debug.WriteLine(playerLocation.TargetMapID);
                }
            }
            else if (keyboardState.IsKeyUp(Keys.Left))
                isLeftPressed = false;
            if (keyboardState.IsKeyDown(Keys.Up) && !isUpPressed)
            {
                if (playerLocation.Neighbours[2] != null)
                {
                    playerLocation = playerLocation.Neighbours[2];
                    Debug.WriteLine(playerLocation.TargetMapID);
                }
            }
            else if (keyboardState.IsKeyUp(Keys.Up))
                isUpPressed = false;
            if (keyboardState.IsKeyDown(Keys.Right) && !isRightPressed)
            {
                if (playerLocation.Neighbours[3] != null)
                {
                    playerLocation = playerLocation.Neighbours[3];
                    Debug.WriteLine(playerLocation.TargetMapID);
                }
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
                isRightPressed = false;
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
        }

        public override void UnloadContent()
        {
            gameScreenManager.ContentLoader.UnloadLocation("worldMap");
        }
    }
}
