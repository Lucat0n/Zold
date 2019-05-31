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
        public List<Npc> ListofNpcs = new List<Npc>();
        public List<Enemy> ListofEnemies = new List<Enemy>();

        TmxMap currentMap;

        // here is dope music
        SoundEffect currentSong;
        SoundEffect bgMusic;

        //spritefonts
        SpriteFont dialog;

        // //postacie
        Vector2 pos;

        //Texture2D adven;
        //int advenPosX = 256;
        //int advenPosY = 64;

        // Combat
        Combat.CombatScreen Combat;
        Combat.CombatObjects.Characters.Player combatPlayer;
        Combat.CombatObjects.Characters.Enemies.Enemy mob;
        Combat.CombatObjects.Characters.Enemies.Enemy ranged;
        Combat.CombatObjects.Characters.Enemies.Enemy rat;
        List<Combat.CombatObjects.Characters.Enemies.Enemy> enemies;

        Player player;
        //Enemy enemy;

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

        //public static Rectangle bounds; //camera bounds 

        SpriteBatchSpriteSheet spriteSheet;
        SpriteBatchSpriteSheet spriteSheetHP;

        //measures
        int playerWidth = 32;
        int playerHeight = 48;

        TimeSpan PauseCooldown;

        int hp;

        Location location;
        Zold.Screens.Camera cameraPlayer;
        InteractionManager interactionManager;

        Effect effect;
        RenderTarget2D lightsTarget;

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
            effect = Assets.Instance.Get("placeholders/shaders/testShader");

            var pp = gameScreenManager.GraphicsDevice.PresentationParameters;
            lightsTarget = new RenderTarget2D(gameScreenManager.GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);

            location1 = true;
            location2 = false;
            location3 = false;

            pos = new Vector2(64, 96);

            spriteSheet = new SpriteBatchSpriteSheet(gameScreenManager.GraphicsDevice, Assets.Instance.Get("placeholders/Textures/main"), 4, 3, playerWidth, playerHeight);
            player = new Map.Player(pos, Assets.Instance.Get("placeholders/Textures/main"), 2.7f, spriteSheet, hp);

            //hpbar 
            spriteSheetHP = new SpriteBatchSpriteSheet(gameScreenManager.GraphicsDevice, Assets.Instance.Get("placeholders/Textures/hpbars"), 101, 1, 250, 32);

           // enemy = new Enemy(player, new Vector2(400, 200));
           // enemy.SetTexture(Assets.Instance.Get("placeholders/Textures/rat"));

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
            ListofNpcs = location.GetCharacters();
            ListofEnemies = location.GetEnemies();

            ColideLayers.ForEach(layer =>
            {
                location.getColideObjects(layer, currentMap, colisionTiles);
            });
        }

        public override void Draw(GameTime gameTime)
        {

            gameScreenManager.GraphicsDevice.Clear(Color.Black);
            gameScreenManager.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, transformMatrix: cameraPlayer.Transform());

            LayerNumbers.ForEach(layer =>
            {
                if(layer == 5)
                {
                    player.Animation(gameTime, cameraPlayer.Transform());
                }
                //effect.CurrentTechnique.Passes[0].Apply();
                location.drawTiles(layer, currentMap, cameraPlayer.Transform());
            });

            if (ListofNpcs != null)
            {
                ListofNpcs.ForEach(npc =>
                {
                //effect.CurrentTechnique.Passes[0].Apply();  // ten dziala nie tylko na adasiu ale tez na chmurce i tle do tekstu
                Texture2D tex = npc.GetTexture();
                    gameScreenManager.SpriteBatch.Draw(tex, new Rectangle((int)npc.GetPosition().X, (int)npc.GetPosition().Y, npc.GetTexture().Width, npc.GetTexture().Height), Color.White);
                    interactionManager.displayDialog(player, tex, (int)npc.GetPosition().X, (int)npc.GetPosition().Y);
                });
            }

            //gameScreenManager.SpriteBatch.End();
            if (ListofEnemies != null)
            {
                ListofEnemies.ForEach(npc =>
                {
                   // gameScreenManager.GraphicsDevice.SetRenderTarget(lightsTarget);
                    //gameScreenManager.GraphicsDevice.Clear(Color.Black);
                    //gameScreenManager.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, transformMatrix: cameraPlayer.Transform());

                    effect.CurrentTechnique.Passes[0].Apply();
                    Texture2D tex = npc.GetTexture();
                    gameScreenManager.SpriteBatch.Draw(tex, new Rectangle((int)npc.GetPosition().X, (int)npc.GetPosition().Y, npc.GetTexture().Width, npc.GetTexture().Height), Color.White);
                    // gameScreenManager.SpriteBatch.End();
                });
            }

            //gameScreenManager.GraphicsDevice.SetRenderTarget(null);
           // gameScreenManager.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, transformMatrix: cameraPlayer.Transform());

            // player.Animation(gameTime, cameraPlayer.Transform());

            gameScreenManager.SpriteBatch.DrawString(dialog, "X: " + player.GetPosition().X.ToString(), new Vector2(10, 10), Color.White);
            gameScreenManager.SpriteBatch.DrawString(dialog, "Y: " + player.GetPosition().Y.ToString(), new Vector2(10, 40), Color.White);

            if (gameScreenManager.IsFullScreenOn)
            {
                int posY = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 300;
                player.AnimateHealth(gameTime, spriteSheetHP, posY);
            }
            else
            {
                player.AnimateHealth(gameTime, spriteSheetHP, 550);
            }
            
            gameScreenManager.SpriteBatch.End();
            ManageLocations(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
 
            location.checkIfColide(colisionTiles);
            dontGoOutsideMap();

            if (!isPaused)
            {
                player.move(player.Width, player.Height, canMoveLeft, canMoveUp, canMoveRight, canMoveDown, gameTime);
                // ManageLocations(gameTime);
                
                if (ListofEnemies != null)
                {
                    ListofEnemies.ForEach(enemy =>
                    {
                        enemy.AI(gameTime);
                        
                            Rectangle ghostPlayer = new Rectangle((int)player.GetPosition().X, (int)player.GetPosition().Y, 32, 48);
                            Rectangle ghostEnemy = new Rectangle((int)enemy.GetPosition().X, (int)enemy.GetPosition().Y, 32, 48);
                            if (ghostPlayer.Intersects(ghostEnemy))
                            {
                                enemy.position.Y = 5000;
                                gameScreenManager.InsertScreen(Combat);
                            }       
                    });
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

            if (player.GetPosition().Y + playerHeight > sh)
            {
                canMoveDown = false;
            }
            if (player.GetPosition().X + playerWidth >= sw)
            {
                canMoveRight = false;
            }
        }


        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Escape) && !pressed)
            {
                pressed = true;
                isPaused = !isPaused;
                //Debug.WriteLine(isPaused ? "paused" : "unpaused");
            }
            else if (keyboardState.IsKeyUp(Keys.Escape) && pressed)
            {
                pressed = false;
            }
        }

        #region managelocations

        void ManageLocations(GameTime gametime) // Trzeba zmienic to bo wstyd xD, Tylko jak?
        {
            //gameScreenManager.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, transformMatrix: cameraPlayer.Transform());
            if (location1)
            {

                if (location.getPortal() != null)
                {
                    if (player.GetPosition().X == location.getPortal().X && player.GetPosition().Y == location.getPortal().Y)
                    {
                        location = new Locations.Dormitory(gameScreenManager, spriteSheet, player);
                        player.SetPosition(location.playersNewPosition());
                        initTheLocation(location);
                        if (gameScreenManager.QuestManager.ActiveQuests.ContainsKey("lQ1"))
                        {
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

            }
 
           // gameScreenManager.SpriteBatch.End();
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