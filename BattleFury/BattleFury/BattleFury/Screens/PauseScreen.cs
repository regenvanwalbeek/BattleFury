using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.ScreenManagement;

namespace BattleFury.Screens
{
    /// <summary>
    /// Screen to be displayed when the game is paused.
    /// </summary>
    public class PauseScreen : MenuScreen
    {

        /// <summary>
        /// Construct the pause screen
        /// </summary>
        public PauseScreen() : base("Pause")
        {
            // Create menu entries
            MenuEntry resumeGameMenuEntry = new MenuEntry("Resume");
            MenuEntry quitGameMenuEntry = new MenuEntry("Quit");

            // Add event handlers
            resumeGameMenuEntry.Selected += OnCancel;
            resumeGameMenuEntry.StartSelected += OnCancel;
            quitGameMenuEntry.Selected += QuitMenuEntrySelected;

            // Add the menu entries to the menu
            MenuEntries.Add(resumeGameMenuEntry);
            MenuEntries.Add(quitGameMenuEntry);
        }

        /// <summary>
        /// Quit when selected
        /// </summary>
        protected void QuitMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, e.PlayerIndex, new MainMenuBackgroundScreen(), new MainMenuScreen());
        }

    }
}
