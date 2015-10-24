using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using glitch.Physics;
using System.Collections.Generic;

namespace glitch
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle Screen;
        PlayerObject player;
        Level currentLevel;
        List<GameObject> gameObjects;
        int levelNumber = 0;

        List<Level> levels = new List<Level>();
        Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

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

            InputHandler.Instance.handlePlayerInput(player);

            player.Location = player.physComp.ApplyVelocity(gameTime, player.Location);

            PhysicsSystem.Instance.applyGravityToPlayer(gameTime);
            PhysicsSystem.Instance.checkPlayerCollisions();
            PhysicsSystem.Instance.handleCollisions();

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
            textures.Add("I", Content.Load<Texture2D>("I"));
            textures.Add("Player", Content.Load<Texture2D>("Player"));
        }


        private void CreateLevel()
        {
            if (currentLevel == null)
            {
                player.SpawnPoint = new Point(30, 300);

                currentLevel = new Level(1, player.SpawnPoint, new Point(Screen.Width - 100, 540), 600, textures["Door"]);
                currentLevel.AddObject(new Point(-100, 600), new Point(Screen.Width + 200, 400), textures["Ground"], true);
                currentLevel.AddObject(new Point(0, 150), new Point(200, 100), textures["Ground"], true);
            }
            else if(currentLevel.LevelNumber == 1)
            {
                //Render level 2
            }
            else if(currentLevel.LevelNumber == 2)
            {
                //Render level 3
            }

        }

    }
}
