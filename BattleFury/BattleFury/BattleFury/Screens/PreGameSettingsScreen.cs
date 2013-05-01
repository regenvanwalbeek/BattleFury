using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.ScreenManagement;
using BattleFury.Input;
using BattleFury.Settings;
using Microsoft.Xna.Framework;

namespace BattleFury.Screens
{
    public class PreGameSettingsScreen : MenuScreen
    {
        private const int MAX_LIVES = 10;

        private const int MIN_LIVES = 1;

        private int numLives = 3;

        private int numPlayers = 2;

        public PreGameSettingsScreen() : base("Battle Settings")
        {

        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            PlayerIndex playerIndex;


            // Go to the next screen
            if (MenuBindings.IsMenuSelect(null, out playerIndex))
            {
                LoadingScreen.Load(ScreenManager, null, new BackgroundScreen(), new BattleScreen(numLives, numPlayers));
            }
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            
        }


    }
}
