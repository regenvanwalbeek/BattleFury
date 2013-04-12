using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.ScreenManagement;
using BattleFury.Entities;
using BattleFury.Input;
using BattleFury.Settings;
using Microsoft.Xna.Framework;

namespace BattleFury.Screens
{
    public class GameOverScreen : GameScreen
    {

        private GameResults results;

        /// <summary>
        /// Minimum milliseconds the user will see this screen.
        /// Don't let the user accidentally exit this screen. 
        /// </summary>
        private const int MIN_SCREEN_TIME = 2000;
        private int timeOnScreen = 0;

        public GameOverScreen(GameResults results)
        {
            this.results = results;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            // TODO
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            timeOnScreen += gameTime.ElapsedGameTime.Milliseconds;

            if (timeOnScreen > MIN_SCREEN_TIME)
            {
                PlayerIndex playerIndex;
                if (MenuBindings.IsMenuSelect(null, out playerIndex))
                {
                    // Go to the main menu screen
                    LoadingScreen.Load(ScreenManager, null, new BackgroundScreen(), new MainMenuScreen());
                }
                // TODO
            }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);

            // TODO
        }

       

    }
}
