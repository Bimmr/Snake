using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BimmCore.MonoGame;
using BimmCore.MonoGame.Components;
using Snakez.Screens;

namespace Snakez
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SnakeGame : Game
    {

        public static string AUTHOR = "Randy Bimm";
        public static string VERSION = "v2.0.1";

        private ScreenHandler screenHandler;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public SnakeGame()
        {
            _graphics = new GraphicsDeviceManager(this);
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
            // TODO: Add your initialization logic here
            base.Initialize();

            screenHandler = new ScreenHandler();
            screenHandler.addScreen("main", new MainScreen());
            screenHandler.addScreen("game", new GameScreen());
            screenHandler.addScreen("leaderboards", new LeaderboardsScreen());
            screenHandler.addScreen("settings", new SettingsScreen());

            screenHandler.show("main");
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            MonoHelper.setup(this, _spriteBatch, _graphics);
            FontHandler.setup(this);
            SpriteHandler.setup(this);
            // TODO: use this.Content to load your game content here
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
            if (MonoHelper.Game.IsActive)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    screenHandler.show("main");

                screenHandler.getCurrent().Update(gameTime);

                base.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();

            screenHandler.getCurrent().Draw(gameTime);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
