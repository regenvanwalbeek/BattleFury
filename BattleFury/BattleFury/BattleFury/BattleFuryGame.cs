using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using BattleFury.ScreenManagement;
using BattleFury.Screens;
using BattleFury.Settings;

namespace BattleFury
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class BattleFuryGame : Microsoft.Xna.Framework.Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ScreenManager screenManager;

        public BattleFuryGame()
        {
            
            Content.RootDirectory = "Content";
            
            // Set up the graphics device.
            graphics = new GraphicsDeviceManager(this);
            // Initialize the screen resolution, no smaller than the min height/width supported by the game.
            DisplayModeCollection displayModes = GraphicsAdapter.DefaultAdapter.SupportedDisplayModes;
            int height = GlobalGameConstants.MIN_SCREEN_HEIGHT, width = GlobalGameConstants.MIN_SCREEN_WIDTH;
            foreach (DisplayMode mode in displayModes){
                if (mode.Width > GlobalGameConstants.MIN_SCREEN_WIDTH && mode.Height > GlobalGameConstants.MIN_SCREEN_HEIGHT)
                {
                    width = mode.Width;
                    height = mode.Height;
                }
                break;
            }
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height ;
            GameSettings.LoadDefaultGameSettings(width, height);
            

            Services.AddService(typeof(GraphicsDeviceManager), graphics);
            Services.AddService(typeof(DisplayModeCollection), displayModes);

            // Create the new screen manager component
            screenManager = new ScreenManager(this);

            Components.Add(screenManager);

            // Activate the first screens
            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);
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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
          
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
