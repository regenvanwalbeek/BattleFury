using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.ScreenManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BattleFury.Screens
{
    /// <summary>
    /// Screen to display while loading. Adapted from Microsoft GameStateManager example.
    /// </summary>
    public class LoadingScreen : GameScreen
    {
        /// <summary>
        /// Screens to load.
        /// </summary>
        private GameScreen[] screensToLoad;

        /// <summary>
        /// True if the other screens have transitioned off.
        /// </summary>
        private bool otherScreensAreGone;


        private LoadingScreen(GameScreen[] screensToLoad)
        {
            this.screensToLoad = screensToLoad;
            this.otherScreensAreGone = false;
        }

        /// <summary>
        /// Activates the loading screen. Shuts off all existing screens and loads the screensToLoad.
        /// </summary>
        /// <param name="screenManager">The screen manager.</param>
        /// <param name="screensToLoad">Array of screens to load.</param>
        public static void Load(ScreenManager screenManager, PlayerIndex? controllingPlayer, params GameScreen[] screensToLoad)
        {
            // Turn off all existing screens
            GameScreen[] screens = screenManager.GetScreens();
            for (int i = 0; i < screens.Length; i++)
            {
                screens[i].ExitScreen();
            }

            // Create and activate the loading screen
            LoadingScreen loadingScreen = new LoadingScreen(screensToLoad);

            screenManager.AddScreen(loadingScreen, controllingPlayer);
        }

        /// <summary>
        /// Updates the loading screen.
        /// </summary>
        /// <param name="gameTime">The current GameTime.</param>
        /// <param name="otherScreenHasFocus">True if other screen has focus.</param>
        /// <param name="coveredByOtherScreen">True if covered by anotehr screen.</param>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            // If all the previous screens have finished transitioning
            // off, it is time to actually perform the load.
            if (otherScreensAreGone)
            {
                ScreenManager.RemoveScreen(this);

                foreach (GameScreen screen in screensToLoad)
                {
                    if (screen != null)
                    {
                        ScreenManager.AddScreen(screen, ControllingPlayer);
                    }
                }

                // Once the load has finished, we use ResetElapsedTime to tell
                // the  game timing mechanism that we have just finished a very
                // long frame, and that it should not try to catch up.
                ScreenManager.Game.ResetElapsedTime();
            }

        }

        /// <summary>
        /// Draws the loading screen.
        /// </summary>
        /// <param name="gameTime">The current GameTime.</param>
        public override void Draw(GameTime gameTime)
        {
            // If we are the only active screen, that means all the previous screens
            // must have finished transitioning off. We check for this in the Draw
            // method, rather than in Update, because it isn't enough just for the
            // screens to be gone: in order for the transition to look good we must
            // have actually drawn a frame without them before we perform the load.
            if ((ScreenState == ScreenState.Active) &&
                (ScreenManager.GetScreens().Length == 1))
            {
                otherScreensAreGone = true;
            }

            // Display the loading message.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            const string message = "Loading...";

            // Center the text in the viewport.
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Vector2 viewportSize = new Vector2(viewport.Width, viewport.Height);
            Vector2 textSize = font.MeasureString(message);
            Vector2 textPosition = (viewportSize - textSize) / 2;

            Color color = Color.White * TransitionAlpha;

            // Draw the text.
            spriteBatch.Begin();
            spriteBatch.DrawString(font, message, textPosition, color);
            spriteBatch.End();
        }


    }
}
