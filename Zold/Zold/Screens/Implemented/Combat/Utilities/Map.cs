using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TiledSharp;
using Zold.Screens.Implemented.Combat.CombatObjects;
using Zold.Utilities;
using RoyT.AStar;

namespace Zold.Screens.Implemented.Combat.Utilities
{
    class Map
    {
        private GameScreenManager gameScreenManager;
        private CombatScreen combatScreen;

        private TmxMap currentMap;
        private SpriteBatchSpriteSheet mapSprite;
        private Texture2D tileset;
        private int tileWidth;
        private int tileHeight;
        private int tilesetTilesWidth;
        private int tilesetTilesHeight;
        private int tilesetHeightOffset;
        private int nodesY;
        private int nodesX;

        public Dictionary<Position,Node> Nodes;
        public List<Node> CollisionNodes;
        public Grid PathfindingGrid;
        public int NodeWidth;
        public int NodeHeight;
        public int TopMapEdge;
        public int BottomMapEdge;
        public int RightMapEdge;
        public int LeflMapEdge;

        public Map(GameScreenManager gameScreenManager, CombatScreen combatScreen)
        {
            this.gameScreenManager = gameScreenManager;
            this.combatScreen = combatScreen;
            CollisionNodes = new List<Node>();

            currentMap = new TmxMap("Content/graphic/combat/Dormitory-combat.tmx");
            mapSprite = new SpriteBatchSpriteSheet(gameScreenManager.GraphicsDevice, null, 0, 0, 0, 0);

            InitMap(currentMap);
            SetPathfindingGridSize();
            PathfindingGrid = new Grid(nodesX, nodesY);

            CreateNodeGrid();
            GenerateRandomObstacles(gameScreenManager, combatScreen, 20);
        }

        private void SetPathfindingGridSize()
        {
            nodesX = tileset.Width / 16;
            nodesY = (tileset.Height - TopMapEdge) / 16;
        }

        private void GenerateRandomObstacles(GameScreenManager gameScreenManager, CombatScreen combatScreen, int count)
        {
            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                int randX = rand.Next(0, nodesX-1);
                int randY = rand.Next(0, nodesY);
                Node node1 = Nodes[new Position(randX, randY)];
                Node node2 = Nodes[new Position(randX + 1, randY)];
                if (!node1.Passable || !node2.Passable)
                {
                    i--;
                    continue;
                }
                combatScreen.AddObstacle(new Obstacle(new Vector2(node1.PosX, node1.PosY - node1.Width), new SpriteBatchSpriteSheet(gameScreenManager.GraphicsDevice, Assets.Instance.Get("combat/Textures/stone"), 1, 1, 32, 32), node1.Height, node1.Width));
                node1.Passable = false;
                node2.Passable = false;
                CollisionNodes.Add(node1);
                CollisionNodes.Add(node2);
                PathfindingGrid.BlockCell(node1.Position);
                PathfindingGrid.BlockCell(node2.Position);
            }
        }

        private void CreateNodeGrid()
        {
            Nodes = new Dictionary<Position, Node>(nodesX * nodesY);
            for (int x = 0; x < nodesX; x++)
                for (int y = 0; y < nodesY; y++)
                {
                    Nodes.Add(new Position(x, y), new Node(x, y, TopMapEdge));
                }
        }

        public virtual void InitMap(TmxMap currentMap)
        {
            tileset = gameScreenManager.Content.Load<Texture2D>("graphic\\combat\\" + currentMap.Tilesets[0].Name.ToString());
            tileWidth = currentMap.Tilesets[0].TileWidth;
            tileHeight = currentMap.Tilesets[0].TileHeight;
            tilesetTilesWidth = tileset.Width / tileWidth;
            tilesetTilesHeight = tileset.Height / tileHeight;
            tilesetHeightOffset = int.Parse(currentMap.Layers[1].Properties["Height"]);

            NodeWidth = tileWidth / 2;
            NodeHeight = tileHeight / 2;

            TopMapEdge = tilesetHeightOffset * tileHeight;
            BottomMapEdge = tileset.Height;
            RightMapEdge = tileset.Width;
            LeflMapEdge = 0;
        }

        public virtual void DrawTiles(int layer)
        {
            for (var i = 0; i < currentMap.Layers[layer].Tiles.Count; i++)
            {
                int gid = currentMap.Layers[layer].Tiles[i].Gid;

                if (gid != 0)
                {
                    int tileFrame = gid - 1;
                    int column = tileFrame % tilesetTilesWidth;
                    int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWidth);
                    float x = (i % currentMap.Width) * currentMap.TileWidth;
                    float y = (float)Math.Floor(i / (double)currentMap.Width) * currentMap.TileHeight;

                    Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);
                    mapSprite.Begin(transformMatrix:CombatCamera.BindCameraTransformation());
                    mapSprite.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                    mapSprite.End();
                }
            }
        }
    }
}
