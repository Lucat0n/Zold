using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using TiledSharp;
using System;
using Zold.Utilities;
using Zold.Quests;
using Zold.Screens.Implemented.Combat;

namespace Zold.Screens.Implemented.Map
{
    class MapManager : GameScreen
    {
        public List<Rectangle> colisionTiles = new List<Rectangle>();
        public List<int> LayerNumbers = new List<int>();
        public List<int> ColideLayers = new List<int>();
        
        TmxMap currentMap;

        // here is dope music
        SoundEffect currentSong;
        SoundEffect bgMusic;

        //spritefonts
        SpriteFont dialog;

        // //postacie
        Vector2 pos;

        Texture2D adven;
        int advenPosX = 256;
        int advenPosY = 64;

        // Combat
        Combat.CombatScreen Combat;
        
        Player player;
        Enemy enemy;

        //bools
        bool wasPaused = false;
        bool songStart = false;
        bool pressed = false;
        bool location1;
        bool location2;
        bool location3;
        bool isPaused = false;
        private bool isEscPressed = false;

        public static bool canMoveLeft;
        public static bool canMoveUp;
        public static bool canMoveRight;
        public static bool canMoveDown;

        public static int screenWdth = 800;
        public static int screenHeight = 480;

        public static Rectangle bounds; //camera bounds 

        SpriteBatchSpriteSheet spriteSheet;
        SpriteBatchSpriteSheet spriteSheetHP;

        //measures
        int playerWidth = 32;
        int playerHeight = 48;

        TimeSpan PauseCooldown;

        int hp;

        Location location;
        Camera camera;
        Zold.Screens.Camera cameraPlayer;
        InteractionManager interactionManager;

        public MapManager()
        {

        }

        #region init
        public override void LoadContent()
        {
            gameScreenManager.ContentLoader.LoadLocation("placeholders");
            gameScreenManager.ContentLoader.LoadLocation("combat");

            canMoveDown = true;
            canMoveLeft = true;
            canMoveRight = true;
            canMoveUp = true;

            hp = 100;

            PauseCooldown = new TimeSpan(0, 0, 0, 500);

            // loading music
            //combatMusic = Assets.Instance.Get("placeholders/Music/kombat");

            //loading fonts
            dialog = Assets.Instance.Get("placeholders/Fonts/dialog");

            location1 = true;
            location2 = false;
            location3 = false;

            pos = new Vector2(64, 96);

            spriteSheet = new SpriteBatchSpriteSheet(gameScreenManager.GraphicsDevice, Assets.Instance.Get("placeholders/Textures/main"), 4, 3, playerWidth, playerHeight);
            player = new Map.Player(pos, Assets.Instance.Get("placeholders/Textures/main"), 2.7f, spriteSheet, hp);

            //hpbar 
            spriteSheetHP = new SpriteBatchSpriteSheet(gameScreenManager.GraphicsDevice, Assets.Instance.Get("placeholders/Textures/hpbars"), 101, 1, 250,32);

            enemy = new Enemy(player, new Vector2(400, 200));
            enemy.SetTexture(Assets.Instance.Get("placeholders/Textures/rat"));

            // Combat
            CombatBuilder combatBuilder = new CombatBuilder(gameScreenManager.GraphicsDevice, new Statistics.Stats());
            combatBuilder.AddPunk(1, 300, 300);
            combatBuilder.AddRanged(1, 400, 300);
            combatBuilder.AddRat(1, 300, 350);
            Combat = combatBuilder.Build();

            //camera
            bounds = new Rectangle(0, 0, 0, 0);

            location = new Locations.TheRoom(gameScreenManager, spriteSheet, player);
            initTheLocation(location);
            pos = location.playersNewPosition();
            camera = new Camera(gameScreenManager, location);

            interactionManager = new InteractionManager(GameScreenManager, location);

            cameraPlayer = new Screens.Camera(player.GetPosition());
        }

        public override void UnloadContent()
        {
            Assets.Instance.Remove("placeholders");
        }
        #endregion

        #region drawupdate

        void initTheLocation(Location location)
        {
            currentMap = location.getCurrentMap();
            location.initMap(gameScreenManager, currentMap);

            LayerNumbers = location.getLayerNumbers();
            ColideLayers = location.getColideLayers();

            ColideLayers.ForEach(layer =>
            {
                location.getColideObjects(layer, currentMap, bounds, colisionTiles);
            });
        }

        public override void Draw(GameTime gameTime)
        {
            gameScreenManager.GraphicsDevice.Clear(Color.Black);
            gameScreenManager.SpriteBatch.Begin(transformMatrix: cameraPlayer.Transform());

            LayerNumbers.ForEach(layer=>
            {
                location.drawTiles(layer, currentMap, bounds, cameraPlayer.Transform());
            });

            player.Animation(gameTime, cameraPlayer.Transform());

            gameScreenManager.SpriteBatch.DrawString(dialog, "X: "+player.GetPosition().X.ToString(), new Vector2(10, 10), Color.White);
            gameScreenManager.SpriteBatch.DrawString(dialog, "Y: "+player.GetPosition().Y.ToString(), new Vector2(10, 40), Color.White);
            gameScreenManager.SpriteBatch.DrawString(dialog, "boundsX: "+bounds.X.ToString(), new Vector2(10, 70), Color.White);
            gameScreenManager.SpriteBatch.DrawString(dialog, "boundsY: "+bounds.Y.ToString(), new Vector2(10, 110), Color.White);
            
           // gameScreenManager.SpriteBatch.Draw(player.texture, player.GetPosition(), Color.White);

            //hpbar
            if (gameScreenManager.IsFullScreenOn)
            {
                int posY= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 300;
                player.AnimateHealth(gameTime, spriteSheetHP, posY);
            }
            else
            {
                player.AnimateHealth(gameTime, spriteSheetHP,550);
            }
           
            //postacie
            gameScreenManager.SpriteBatch.End();
            ManageLocations(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            //camera.Follow(player);
            //if (gameScreenManager.IsFullScreenOn)
            //{
            //    camera.moveCamera(256, 256, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 256, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 256, gameTime, player, currentMap,bounds, colisionTiles);
            //}
            //else
            //{
            //    camera.moveCamera(128, 128, 650, 352,gameTime, player, currentMap, bounds, colisionTiles); /// trub okienkowy
            //}
            //if (!songStart)
            //{
            //    MediaPlayer.Play(currentSong);
            //    songStart = true;
            //}
            
            location.checkIfColide(colisionTiles);
            dontGoOutsideMap();

            if (!isPaused)
            {
                player.move(player.Width, player.Height, canMoveLeft,canMoveUp, canMoveRight, canMoveDown, gameTime);
               // ManageLocations(gameTime);
                if (location2)
                {
                    enemy.AI(gameTime);
                }
            }

            else
            {
                gameScreenManager.InsertScreen(new Pause.PauseScreen());
                isPaused = false;
            }

            cameraPlayer.Follow(player.GetPosition(), screenHeight, screenWdth);
        }

        
        void dontGoOutsideMap()
        {
            int sh;
            int sw;
            if (gameScreenManager.IsFullScreenOn)
            {
                sh = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                sw = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            }
            else
            {
                sh = screenHeight;
                sw = screenWdth;
            }

            if (player.GetPosition().X <= 0)
            {
                canMoveLeft = false;
            }

            if (player.GetPosition().Y <= 0)
            {
                canMoveUp = false;
            }

            if (player.GetPosition().Y +playerHeight >sh)
            {
                canMoveDown = false;
            }
            if (player.GetPosition().X + playerWidth >= sw )
            {
                canMoveRight = false;
            }
        }

       
        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState)
        {
            if(keyboardState.IsKeyDown(Keys.Escape) && !pressed) 
            {
                pressed = true;
                isPaused = !isPaused;
                //Debug.WriteLine(isPaused ? "paused" : "unpaused");
            }else if (keyboardState.IsKeyUp(Keys.Escape) && pressed)
            {
                pressed = false;
            }
        }

        #region managelocations

        void ManageLocations(GameTime gametime) 
        {
            gameScreenManager.SpriteBatch.Begin(transformMatrix: cameraPlayer.Transform());
            if (location1)
            {
                adven = Assets.Instance.Get("placeholders/Textures/Adven");
                gameScreenManager.SpriteBatch.Draw(adven, new Rectangle(advenPosX + bounds.X, advenPosY + bounds.Y, adven.Width, adven.Height), Color.White);

                interactionManager.displayDialog(player, adven, advenPosX + bounds.X, advenPosY + bounds.Y, bounds);
                if (location.getPortal().X >= 0)
                {
                    if (player.GetPosition().X == location.getPortal().X && player.GetPosition().Y == location.getPortal().Y)
                    {
                        location = new Locations.Dormitory(gameScreenManager, spriteSheet, player);
                        player.SetPosition(location.playersNewPosition());
                        initTheLocation(location);
                        if (gameScreenManager.QuestManager.ActiveQuests.ContainsKey("lQ1")){
                            var lq = (LocationQuest)gameScreenManager.QuestManager.ActiveQuests["lQ1"];
                            lq.TriggerVisit();
                            gameScreenManager.QuestManager.UpdateQuests();
                        }

                        location1 = false;
                        location2 = true;
                    }
                }
            }

            if (location2)
            {
                gameScreenManager.SpriteBatch.Draw(enemy.GetTexture(), new Vector2(enemy.GetPosition().X+ bounds.X, enemy.GetPosition().Y + bounds.Y), Color.White);

                if (player.GetPosition().X + player.Width >= enemy.GetPosition().X
                    && player.GetPosition().Y + player.Width >= enemy.GetPosition().Y)
                {
                    enemy.position.Y = 5000;
                    gameScreenManager.InsertScreen(Combat);

                    //if (songStart)
                    //{
                    //    MediaPlayer.Stop();
                    //    combatMusic.Play();
                    //    songStart = false;
                    //}
                }
            }
            /*
            if (location3)
            {
                location1 = false;
                location2 = false;
                location3 = true;
            }*/
            gameScreenManager.SpriteBatch.End();
        }
        #endregion

        /*private void CalculatePause(GameTime gameTime)
        {
            this.PauseCooldown -= new TimeSpan(0,0,0,gameTime.ElapsedGameTime.Milliseconds);
            if (PauseCooldown <= TimeSpan.Zero)
            {
                Debug.WriteLine("Halo salut");
                wasPaused = false;
            }
        }*/
    }
}
#endregion