using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using glitch.Physics;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Audio;


namespace glitch
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Rectangle Screen;
        PlayerObject player;
        public static Level currentLevel;
        List<GameObject> gameObjects;
        static Texture2D deathCounter;
        List<Level> levels = new List<Level>();
        Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        private SoundEffect titleSound;
        SoundEffectInstance soundEffectInstance;

        int forFlicker = 0;
        Random r = new Random();
        bool isColorTitle = false;
        int disableInputHandler = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            gameObjects = new List<GameObject>();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Setting up the screen
            Screen = new Rectangle(0, 0, 1366, 768);

            //Spawning the game window in the upper left of the screen
            var form = (System.Windows.Forms.Form)System.Windows.Forms.Control.FromHandle(this.Window.Handle);
            form.Location = new System.Drawing.Point(Screen.X, Screen.Y); 

            graphics.PreferredBackBufferHeight = Screen.Height;
            graphics.PreferredBackBufferWidth = Screen.Width;
            graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            //Loads assets into the dictionary
            LoadAssets();

            Texture2D playerSprite = textures["Player"];
            player = new PlayerObject(Screen.Center.ToVector2(), playerSprite, true, PhysicsType.Player);
            player.Size = new Point(playerSprite.Width / 2, playerSprite.Height / 2);

            //GameObject floor = new GameObject(0, Screen.Bottom - 100, playerSprite, true, PhysicsType.StaticObject);
            //floor.Size = new Point(Screen.Width, 200);

            //gameObjects.Add(floor);
            CreateLevel();
            
            player.Teleport(player.SpawnPoint);
            PhysicsSystem.Instance.player = player;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (PhysicsSystem.Instance.playerTouchedDoor)
            {
                PhysicsSystem.Instance.playerTouchedDoor = false;
                CreateLevel();
                disableInputHandler = 500;
            }

            if (disableInputHandler > 0)
            {
                disableInputHandler -= gameTime.ElapsedGameTime.Milliseconds;
                InputHandler.Instance.DisablePlayerInput(player);
            }
            else 
            {
                InputHandler.Instance.handlePlayerInput(player);
                disableInputHandler = 0;
            }


            player.Location = player.physComp.ApplyVelocity(gameTime, player.Location);

            PhysicsSystem.Instance.applyGravityToPlayer(gameTime);
            PhysicsSystem.Instance.checkPlayerCollisions();
            PhysicsSystem.Instance.handleCollisions();


            if(currentLevel != null && currentLevel.LevelNumber == 0)
            {
                if(forFlicker >= 0)
                {
                    forFlicker -= gameTime.ElapsedGameTime.Milliseconds;
                }
                else
                {
                    if(isColorTitle)
                    {
                        isColorTitle = false;
                        currentLevel.TitleFlicker.rendComp.sprite = textures["LogoW"];
                    }
                    else
                    {
                        isColorTitle = true;
                        currentLevel.TitleFlicker.rendComp.sprite = textures["LogoC"];
                    }
                    forFlicker = r.Next(75, 450);
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            player.Render(spriteBatch);
            currentLevel.RenderLevel(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        
         
        /// <summary>
        /// Called from the LoadContent method at the start.
        /// //noComment
        /// </summary>
        private void LoadAssets()
        {
            textures.Add("Deaths", Content.Load<Texture2D>("Deaths"));
            textures.Add("Door", Content.Load<Texture2D>("Door"));
            textures.Add("Ground", Content.Load<Texture2D>("Ground"));
            deathCounter = Content.Load<Texture2D>("I");
            textures.Add("LogoC", Content.Load<Texture2D>("GlitchColor"));
            textures.Add("LogoW", Content.Load <Texture2D>("GlitchWhite"));
            textures.Add("I", deathCounter);
            textures.Add("Portal", Content.Load<Texture2D>("Portal"));
            textures.Add("Player", Content.Load<Texture2D>("Player"));

            titleSound = Content.Load<SoundEffect>("MixedIntro");

        }


        public void CreateLevel()
        {
            PhysicsSystem.Instance.ClearStage();

            if (currentLevel != null)
            {
                currentLevel.LevelObjects.Clear();
            }

            if (currentLevel == null)
            {
                


            }//testing, remove after debug

            if (currentLevel == null)
            {
                player.SpawnPoint = new Point(30, 300);


                currentLevel = new Level(0, player.SpawnPoint, new Point(Screen.Width - 100, 540), 600, textures["Door"]);
                currentLevel.TitleFlicker = new GameObject(new Point(0, 0).ToVector2(), textures["LogoW"], true, PhysicsType.StaticObject);
                currentLevel.LevelObjects.Add(currentLevel.TitleFlicker);
                currentLevel.AddObject(new Point(-100, 600), new Point(Screen.Width + 100, 200), textures["Ground"], true);
                currentLevel.AddObject(new Point(-100, 0), new Point(100, Screen.Height + 200), textures["Ground"], true);
                currentLevel.AddObject(new Point(Screen.Width, 0), new Point(100, Screen.Height + 200), textures["Ground"], true);



                soundEffectInstance = titleSound.CreateInstance();
                soundEffectInstance.IsLooped = true;
                soundEffectInstance.Play();
            }
            else if (currentLevel.LevelNumber == 0)
            {
                soundEffectInstance.Stop();
                player.SpawnPoint = new Point(30, 300);


                currentLevel = new Level(1, player.SpawnPoint, new Point(Screen.Width - 100, 540), 600, textures["Door"]);
                currentLevel.AddObject(new Point(-100, 600), new Point(Screen.Width / 3, 200), textures["Ground"], true);

                currentLevel.AddObject(new Point((2 * Screen.Width) / 3, 600), new Point(Screen.Width / 3 + 100, 200), textures["Ground"], true);
                currentLevel.AddTeleportObject(new Point(0 - 25 - player.Size.X, 0), new Point(25, Screen.Height), textures["Portal"], true, new Point(Screen.Width, 600 - player.Size.Y));
                currentLevel.AddTeleportObject(new Point(Screen.Width + player.Size.X, 0), new Point(25, Screen.Height), textures["Portal"], true, new Point(0 - player.Size.X, 600 - player.Size.Y));


            }
            else if (currentLevel.LevelNumber == 1)
            {
                player.SpawnPoint = new Point(30, 300);
                player.DeathCount = 0;

                currentLevel = new Level(2, player.SpawnPoint, new Point(Screen.Width - 100, 540), 600, textures["Door"]);
                currentLevel.AddObject(new Point(-100, 600), new Point(Screen.Width / 3, 200), textures["Ground"], true);
                currentLevel.AddObject(new Point((2 * Screen.Width) / 3, 600), new Point(Screen.Width / 3 + 100, 200), textures["Ground"], true);
                currentLevel.AddObject(new Point(50, 650), textures["Deaths"], true);
            }
            else if (currentLevel.LevelNumber == 2)
            {
                player.SpawnPoint = new Point(30, 300);
                player.DeathCount = 0;

                currentLevel = new Level(3, player.SpawnPoint, new Point(Screen.Width - 100, 540), 600, textures["Door"]);

                currentLevel.AddObject(new Point(-100, 600), new Point(180, 200), textures["Ground"], true);
                currentLevel.AddObject(new Point(500, 600), new Point(Screen.Width / 5 + 200, 200), textures["Ground"], true);
                currentLevel.AddObject(new Point((17 * (Screen.Width / 20)), 600), new Point(Screen.Width / 3 + 200, 200), textures["Ground"], true);



                currentLevel.AddTeleportObject(new Point(0 - 25 - player.Size.X, 0), new Point(25, Screen.Height), textures["Portal"], true, new Point(550, 500 - player.Size.Y));
                currentLevel.AddTeleportObject(new Point(500 + (Screen.Width / 5), Screen.Height), new Point((17 * (Screen.Width / 20)) - (500 + (Screen.Width / 5)), 100), textures["Ground"], true, new Point(Screen.Width - 150, 400 - player.Size.Y));
                currentLevel.AddTeleportObject(new Point(500 + (Screen.Width / 5) + 350, 400), new Point((17 * (Screen.Width / 20)) - (500 + (Screen.Width / 5) + 350), 600), textures["Ground"], false, new Point(550, 500 - player.Size.Y));


            }
            else
            {

            }

            player.Teleport(player.SpawnPoint);

        }
            
        public static void AddDeathSymbols(int deaths)
        {
            if (currentLevel.LevelNumber == 2)
            {
                currentLevel.AddObject(new Point(355 + (deaths * 55), 650), deathCounter, true);
            }
        }

    }
}
