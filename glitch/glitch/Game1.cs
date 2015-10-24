using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        List<Level> levels = new List<Level>();
        Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            //Loads assets into a dicitonary
            LoadAssets();

            player = new PlayerObject(Screen.Center, textures["Player"], true, PhysicsType.Player);


         
            
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

            InputHandler.GetInstance().updateStates();
            InputHandler.GetInstance().handlePlayerInput(player);

            player.SetPosition(player.physComp.ApplyVelocity(gameTime, player.drawSpace.Location));
            // TODO: Add your update logic here

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

    }
}
