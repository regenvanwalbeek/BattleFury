using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.ScreenManagement;

namespace BattleFury.Screens
{
    /// <summary>
    /// Screen for setting the gameplay options
    /// </summary>
    public class OptionsScreen : MenuScreen
    {

        private MenuEntry itemsMenuEntry;

        #region Initialization
        public OptionsScreen()
            : base("Options")
        {
            itemsMenuEntry = new MenuEntry(string.Empty);
            MenuEntry backMenuEntry = new MenuEntry("Back");

            setMenuEntryText();

            // Attach the event handlers
            backMenuEntry.Selected += OnCancel;
            itemsMenuEntry.Selected += ItemsMenuEntrySelected;

            MenuEntries.Add(itemsMenuEntry);
            MenuEntries.Add(backMenuEntry);
        }

        #endregion

        #region Handle Input

        private void setMenuEntryText()
        {
            itemsMenuEntry.Text = "Items: " + (GameSettings.ItemsOn ? "on" : "off");
        }

        // Toggle whether items are on or off
        private void ItemsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            GameSettings.ItemsOn = !GameSettings.ItemsOn;
            setMenuEntryText();
        }

        #endregion
    }
}
