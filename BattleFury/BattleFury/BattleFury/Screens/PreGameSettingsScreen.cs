using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.ScreenManagement;
using BattleFury.Input;
using BattleFury.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BattleFury.Screens
{
    public class PreGameSettingsScreen : MenuScreen
    {
        /// <summary>
        /// Menu entry for choosing the number of players
        /// </summary>
        private MenuEntry numPlayersMenuEntry;

        /// <summary>
        /// Menu entry for choosing a number of items
        /// </summary>
        private MenuEntry itemsMenuEntry;

        /// <summary>
        /// Menu entry for choosing number of lives
        /// </summary>
        private MenuEntry numLivesMenuEntry;

        

        private const int MAX_LIVES = 10;

        private const int MIN_LIVES = 1;

        public PreGameSettingsScreen() : base("Battle Settings")
        {
            // Create the menus
            numPlayersMenuEntry = new MenuEntry(string.Empty);
            itemsMenuEntry = new MenuEntry(string.Empty);
            numLivesMenuEntry = new MenuEntry(string.Empty);
            MenuEntry fightEntry = new MenuEntry("Fight!");

            // Attach the event handlers            
            numPlayersMenuEntry.LeftSelected += NumPlayersLeftSelected;
            numPlayersMenuEntry.RightSelected += NumPlayersRightSelected;
            itemsMenuEntry.LeftSelected += ItemsSelected;
            itemsMenuEntry.RightSelected += ItemsSelected;
            numLivesMenuEntry.LeftSelected += NumLivesLeftSelected;
            numLivesMenuEntry.RightSelected += NumLivesRightSelected;
            fightEntry.Selected += FightSelected;

            // Set the text
            setItemsText();
            setNumLivesText();
            setNumPlayersText();

            // Add the items
            MenuEntries.Add(numPlayersMenuEntry);
            MenuEntries.Add(itemsMenuEntry);
            MenuEntries.Add(numLivesMenuEntry);
            MenuEntries.Add(fightEntry);
        }

        #region TextSetters

        public void setNumPlayersText()
        {
            numPlayersMenuEntry.Text = "Number of Players: " + GameSettings.NumPlayers;
        }

        public void setItemsText()
        {
            itemsMenuEntry.Text = "Items: " + (GameSettings.ItemsOn ? "on" : "off");
        }

        public void setNumLivesText()
        {
            numLivesMenuEntry.Text = "Lives: " + GameSettings.NumLives;
        }

        #endregion

        #region MenuInputHandlers

        private void NumPlayersLeftSelected(object sender, PlayerIndexEventArgs e)
        {
            if (GameSettings.NumPlayers > 2)
            {
                GameSettings.NumPlayers--;
                setNumPlayersText();
            }
        }

        private void NumPlayersRightSelected(object sender, PlayerIndexEventArgs e)
        {
            if (GameSettings.NumPlayers < 4)
            {
                GameSettings.NumPlayers++;
                setNumPlayersText();
            }
        }

        // Toggle whether items are on or off
        private void ItemsSelected(object sender, PlayerIndexEventArgs e)
        {
            GameSettings.ItemsOn = !GameSettings.ItemsOn;
            setItemsText();
        }

        private void NumLivesLeftSelected(object sender, PlayerIndexEventArgs e)
        {
            if (GameSettings.NumLives > MIN_LIVES)
            {
                GameSettings.NumLives--;
                setNumLivesText();
            }
        }

        private void NumLivesRightSelected(object sender, PlayerIndexEventArgs e)
        {
            if (GameSettings.NumLives < MAX_LIVES)
            {
                GameSettings.NumLives++;
                setNumLivesText();
            }
        }

        private void FightSelected(object sender, PlayerIndexEventArgs e)
        {
            PlayerIndex playerIndex;

            // Go to the next screen
            if (MenuBindings.IsMenuSelect(null, out playerIndex))
            {
                LoadingScreen.Load(ScreenManager, null, new BattleScreen(GameSettings.NumLives, GameSettings.NumPlayers, GameSettings.ItemsOn));
            }
        }

        #endregion

    }
}
