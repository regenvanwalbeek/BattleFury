using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.ScreenManagement;
using Microsoft.Xna.Framework;
using BattleFury.Input;
using BattleFury.Settings;

namespace BattleFury.Screens
{
    /// <summary>
    /// The main menu the player will see when starting up the game.
    /// </summary>
    public class MainMenuScreen : MenuScreen
    {
        #region Initialization

        public MainMenuScreen()
            : base("Battle Fury")
        {
            // Create menu entries
            MenuEntry playGameMenuEntry = new MenuEntry("Play");
            MenuEntry optionsMenuEntry = new MenuEntry("Options");
            MenuEntry quitGameMenuEntry = new MenuEntry("Quit");

            // Add event handlers
            playGameMenuEntry.Selected += PlayGameMenuSelected;
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            quitGameMenuEntry.Selected += QuitMenuEntrySelected;

            // Add the menu entries to the menu
            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(quitGameMenuEntry);
        }

        #endregion

        #region Handle Input

        /// <summary>
        /// Handler goes to the options screen.
        /// </summary>
        protected void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new OptionsScreen(), null);
        }

        /// <summary>
        /// Handler quits the game.
        /// </summary>
        protected void QuitMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }

        /// <summary>
        /// Handler goes to start a game.
        /// </summary>
        protected void PlayGameMenuSelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new BattleScreen(), null);
            LoadingScreen.Load(ScreenManager, e.PlayerIndex, new BattleScreen());
        }

        
        public override void HandleInput(InputState input)
        {
            PlayerIndex playerIndex;
            if (MenuBindings.IsMenuCancel(input, ControllingPlayer, out playerIndex)){
                ScreenManager.Game.Exit();
            }
            base.HandleInput(input);
        }

        #endregion
    }
}
