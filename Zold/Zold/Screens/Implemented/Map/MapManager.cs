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
        public List<Color> ListofColors = new List<Color>();
        public List<Enemy> ListofEnemies = new List<Enemy>();

        List<Location> ListofNextPlacess = new List<Location>();

        public static List<Location> ListofPlaces = new List<Location>();

        TmxMap currentMap;

        //spritefonts
        SpriteFont dialog;

        // //postacie
        Vector2 pos;

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
        Effect opacity;
        Effect light;
        Effect op2;
        RenderTarget2D lightsTarget, mainTarget, opacTarget, mainTarget2;

        bool postprocessing;
        private object random;

        private SoundEffect bgMusic;
        private SoundEffect combatMusic;
        private SoundEffectInstance bg;

        public MapManager()
        {

        }

        #region init
        public override void LoadContent()
        {
            gameScreenManager.LoadAssets("menu");

            

            ListofColors.Add(Color.Green);
            ListofColors.Add(Color.DarkGreen);
            ListofColors.Add(Color.Blue);
            ListofColors.Add(Color.Red);
            ListofColors.Add(Color.Orange);
            ListofColors.Add(Color.Cyan);
            ListofColors.Add(Color.Yellow);
            ListofColors.Add(Color.Purple);
            ListofColors.Add(Color.Indigo);
            ListofColors.Add(Color.Crimson);


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
            opacity = Assets.Instance.Get("placeholders/shaders/opacityShader");
            light = Assets.Instance.Get("placeholders/shaders/lightShader");
            op2 = Assets.Instance.Get("placeholders/shaders/otherOpacity");

            var pp = gameScreenManager.GraphicsDevice.PresentationParameters;
            lightsTarget = new RenderTarget2D(gameScreenManager.GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);
            mainTarget = new RenderTarget2D(gameScreenManager.GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);


            opacTarget = new RenderTarget2D(gameScreenManager.GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);
            mainTarget2 = new RenderTarget2D(gameScreenManager.GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);

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
            

            location = new Locations.TheRoom(gameScreenManager, spriteSheet, player, true);
            ListofPlaces = location.ListofPlaces();
            ListofPlaces[0] = location;
            initTheLocation(location);
            pos = location.playersNewPositions()[0];

            interactionManager = new InteractionManager(GameScreenManager, location);

            cameraPlayer = new Screens.Camera(player.GetPosition());


            bgMusic = Assets.Instance.Get("placeholders/Music/explo");
            combatMusic = Assets.Instance.Get("placeholders/Music/comba");
            bg = bgMusic.CreateInstance();

        }

        public override void UnloadContent()
        {
            Assets.Instance.Remove("placeholders");
        }
        #endregion

        #region drawupdate

        void initTheLocation(Location location)
        {

            postprocessing = location.postproc;
            currentMap = location.getCurrentMap();
            location.initMap(gameScreenManager, currentMap);

            LayerNumbers = location.getLayerNumbers();
            ColideLayers = location.getColideLayers();
            ListofNpcs = location.GetCharacters();
            ListofEnemies = location.GetEnemies();
            ListofNextPlacess = location.ListofNextPlaces();

            ColideLayers.ForEach(layer =>
            {
                location.getColideObjects(layer, currentMap, colisionTiles);
            });
        }

        public override void Draw(GameTime gameTime)
        {
            if (postprocessing)
            {

               // var rand = new Random().Next(ListofColors.Count);
                Texture2D lightMask = Assets.Instance.Get("placeholders/Textures/lightmask3");
                gameScreenManager.GraphicsDevice.SetRenderTarget(lightsTarget);
                gameScreenManager.GraphicsDevice.Clear(Color.TransparentBlack);
                gameScreenManager.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, transformMatrix: cameraPlayer.Transform());

                // gameScreenManager.SpriteBatch.Draw(lightMask, new Rectangle((int)npc.GetPosition().X, (int)npc.GetPosition().Y, lightMask.Width / 4, lightMask.Height / 4), Color.White);
                gameScreenManager.SpriteBatch.Draw(lightMask, new Rectangle((int)player.GetPosition().X - 120, (int)player.GetPosition().Y - 120, lightMask.Width + 15, lightMask.Height + 15), Color.White);
                // gameScreenManager.SpriteBatch.Draw(lightMask, new Rectangle(100,100, lightMask.Width / 2, lightMask.Height / 2), Color.White);
                //  gameScreenManager.SpriteBatch.Draw(lightMask, new Rectangle(200,100, lightMask.Width /2, lightMask.Height / 2), Color.White);

                // gameScreenManager.SpriteBatch.Draw(lightMask, new Vector2((int)npc.GetPosition().X, (int)npc.GetPosition().Y), Color.White);

                gameScreenManager.SpriteBatch.End();


                gameScreenManager.GraphicsDevice.SetRenderTarget(mainTarget);
            }

            

            gameScreenManager.GraphicsDevice.Clear(Color.Black);
            gameScreenManager.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, transformMatrix: cameraPlayer.Transform());

            LayerNumbers.ForEach(layer =>
            {
                if (layer == 5)
                {
                    player.Animation(gameTime, cameraPlayer.Transform());
                }
                //effect.CurrentTechnique.Passes[0].Apply();
                location.drawTiles(layer, currentMap, cameraPlayer.Transform());
            });

            gameScreenManager.SpriteBatch.End();

            //if (Location.opac)
            //{
            //   // postprocessing = true;
            //    Texture2D lightMask = Assets.Instance.Get("placeholders/Textures/lightmask3");
            //    gameScreenManager.GraphicsDevice.SetRenderTarget(opacTarget);
            //    gameScreenManager.GraphicsDevice.Clear(Color.TransparentBlack);
            //    gameScreenManager.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, transformMatrix: cameraPlayer.Transform());

            //    // gameScreenManager.SpriteBatch.Draw(lightMask, new Rectangle((int)npc.GetPosition().X, (int)npc.GetPosition().Y, lightMask.Width / 4, lightMask.Height / 4), Color.White);
            //    gameScreenManager.SpriteBatch.Draw(lightMask, new Rectangle((int)player.GetPosition().X - 100, (int)player.GetPosition().Y - 100, lightMask.Width - 15, lightMask.Height - 15), Color.White);

            //    gameScreenManager.SpriteBatch.End();


            //    gameScreenManager.GraphicsDevice.SetRenderTarget(mainTarget2);
            //}

            if (ListofNpcs != null)
            {
                ListofNpcs.ForEach(npc =>
                {


                    gameScreenManager.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, transformMatrix: cameraPlayer.Transform());


                    Texture2D tex = npc.GetTexture();
                    gameScreenManager.SpriteBatch.Draw(tex, new Rectangle((int)npc.GetPosition().X, (int)npc.GetPosition().Y, npc.GetTexture().Width, npc.GetTexture().Height), Color.White);

                    interactionManager.displayDialog(player, tex, (int)npc.GetPosition().X, (int)npc.GetPosition().Y);
                    gameScreenManager.SpriteBatch.End();

                });
            }

            //   gameScreenManager.GraphicsDevice.SetRenderTarget(null);
            // gameScreenManager.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, transformMatrix: cameraPlayer.Transform());
            //gameScreenManager.SpriteBatch.End();
            if (ListofEnemies != null)
            {
                ListofEnemies.ForEach(npc =>
                {
                    // gameScreenManager.GraphicsDevice.SetRenderTarget(lightsTarget);
                    //gameScreenManager.GraphicsDevice.Clear(Color.Black);
                    gameScreenManager.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, transformMatrix: cameraPlayer.Transform());

                    opacity.Parameters["param1"].SetValue(1.5f);
                    opacity.CurrentTechnique.Passes[0].Apply();
                    Texture2D tex = npc.GetTexture();
                    gameScreenManager.SpriteBatch.Draw(tex, new Rectangle((int)npc.GetPosition().X, (int)npc.GetPosition().Y, npc.GetTexture().Width, npc.GetTexture().Height), Color.White);


                    gameScreenManager.SpriteBatch.End();


                });
            }

            if (postprocessing)
            {
                gameScreenManager.GraphicsDevice.SetRenderTarget(null);
                gameScreenManager.GraphicsDevice.Clear(Color.TransparentBlack);
                gameScreenManager.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                light.Parameters["lightMask"].SetValue(lightsTarget);
                light.CurrentTechnique.Passes[0].Apply();  // ten dziala nie tylko na adasiu ale tez na chmurce i tle do tekstu
                gameScreenManager.SpriteBatch.Draw(mainTarget, new Vector2(0, 0), Color.White);
                gameScreenManager.SpriteBatch.End();
            }

            //if (Location.opac)
            //{
            //    postprocessing = true;
            //    gameScreenManager.GraphicsDevice.SetRenderTarget(null);
            //    gameScreenManager.GraphicsDevice.Clear(Color.TransparentBlack);
            //    gameScreenManager.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            //    op2.Parameters["lightMask"].SetValue(opacTarget);
            //    op2.Parameters["param2"].SetValue(.6f);
            //    op2.CurrentTechnique.Passes[0].Apply();  // ten dziala nie tylko na adasiu ale tez na chmurce i tle do tekstu
            //    gameScreenManager.SpriteBatch.Draw(mainTarget2, new Vector2(0, 0), Color.White);
            //    gameScreenManager.SpriteBatch.End();
            //}

            //gameScreenManager.GraphicsDevice.SetRenderTarget(null);
            // gameScreenManager.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, transformMatrix: cameraPlayer.Transform());

            // player.Animation(gameTime, cameraPlayer.Transform());
            gameScreenManager.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, transformMatrix: cameraPlayer.Transform());
            gameScreenManager.SpriteBatch.DrawString(dialog, "X: " + player.GetPosition().X.ToString() +"--"+ location.getPortals()[0].X, new Vector2(player.GetPosition().X-30, player.GetPosition().Y - 30), Color.White);
            gameScreenManager.SpriteBatch.DrawString(dialog, "Y: " + player.GetPosition().Y.ToString() + "--" + location.getPortals()[0].Y, new Vector2(player.GetPosition().X - 30, player.GetPosition().Y - 90), Color.White);
         //   gameScreenManager.SpriteBatch.DrawString(dialog, "xdxd"+ListofPlaces.IndexOf(location.ListofNextPlaces()[0]), new Vector2(player.GetPosition().X - 30, player.GetPosition().Y - 120), Color.White);

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

            if (!songStart)
            {
              //  bg = bgMusic.CreateInstance();
               // bg.Volume = gameScreenManager.MasterVolume;
                bg.Play();
                songStart = true;
            }


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
                            bg.Stop();
                            bg = combatMusic.CreateInstance();
                            bg.Play();
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

        void ManageLocations(GameTime gametime) 
        {

            foreach (var place in ListofPlaces)
            {

                if (place == location)
                {
                    if (location.getPortals()[0] != null)
                    {
                       // int i = 0;
                        for(int i=0; i< location.getPortals().Count; i++)
                        {

                        if (player.GetPosition().X == location.getPortals()[i].X && player.GetPosition().Y == location.getPortals()[i].Y)
                        {
                            location = ListofPlaces[ListofPlaces.IndexOf(place)+place.offsets()[i]];
                               // location = place.ListofNextPlaces()[i];
                            player.SetPosition(location.playersNewPositions()[i]);
                            initTheLocation(location);
                            if (location.getLocQuest() != null)
                            {
                                if (gameScreenManager.QuestManager.ActiveQuests.ContainsKey(location.getLocQuest()))
                                {
                                    var lq = (LocationQuest)gameScreenManager.QuestManager.ActiveQuests[location.getLocQuest()];
                                    lq.TriggerVisit();
                                    gameScreenManager.QuestManager.UpdateQuests();
                                }
                            }
                        }
                        }
                    }
                }
            }
            #endregion
            #endregion
        }
    }
}
