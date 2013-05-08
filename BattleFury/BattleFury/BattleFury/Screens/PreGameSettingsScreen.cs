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

        /// <summary>
        /// Menu entry for choosing the arena
        /// </summary>
        private MenuEntry arenaMenuEntry;

        /// <summary>
        /// Index of current arena selected
        /// </summary>
        private int arenaIndex = 0;

        private List<GameSettings.ARENA_SETTING> arenas;

        private const int MAX_LIVES = 10;

        private const int MIN_LIVES = 1;

        public PreGameSettingsScreen() : base("Battle Settings")
        {
            // Initialize the arenas
            arenas = new List<GameSettings.ARENA_SETTING>();
            arenas.Add(GameSettings.ARENA_SETTING.PLAIN_ARENA);
            arenas.Add(GameSettings.ARENA_SETTING.WALLED_ARENA);
            arenas.Add(GameSettings.ARENA_SETTING.SPLIT_BASE);
            arenas.Add(GameSettings.ARENA_SETTING.PIT_OF_DEATH);

            // Create the menus
            numPlayersMenuEntry = new MenuEntry(string.Empty);
            itemsMenuEntry = new MenuEntry(string.Empty);
            arenaMenuEntry = new MenuEntry(string.Empty);
            numLivesMenuEntry = new MenuEntry(string.Empty);
            MenuEntry fightEntry = new MenuEntry("Fight!");

            // Attach the event handlers            
            numPlayersMenuEntry.LeftSelected += NumPlayersLeftSelected;
            numPlayersMenuEntry.RightSelected += NumPlayersRightSelected;
            itemsMenuEntry.LeftSelected += ItemsSelected;
            itemsMenuEntry.RightSelected += ItemsSelected;
            arenaMenuEntry.LeftSelected += ArenaLeftSelected;
            arenaMenuEntry.RightSelected += ArenaRightSelected;
            numLivesMenuEntry.LeftSelected += NumLivesLeftSelected;
            numLivesMenuEntry.RightSelected += NumLivesRightSelected;
            fightEntry.Selected += FightSelected;
            fightEntry.StartSelected += FightSelected;

            // Set the text
            setItemsText();
            setNumLivesText();
            setNumPlayersText();
            setArenaText();

            // Add the items
            MenuEntries.Add(numPlayersMenuEntry);
            MenuEntries.Add(itemsMenuEntry);
            MenuEntries.Add(arenaMenuEntry);
            MenuEntries.Add(numLivesMenuEntry);
            MenuEntries.Add(fightEntry);
        }

        #region TextSetters

        public void setNumPlayersText()
        {
            numPlayersMenuEntry.Text = "Players: " + GameSettings.NumPlayers;
        }

        public void setItemsText()
        {
            itemsMenuEntry.Text = "Items: " + (GameSettings.ItemsOn ? "on" : "off");
        }

        public void setNumLivesText()
        {
            numLivesMenuEntry.Text = "Lives: " + GameSettings.NumLives;
        }

        public void setArenaText()
        {
            String text = "Arena: ";

            GameSettings.ARENA_SETTING currentSetting = arenas[arenaIndex];
            if (currentSetting == GameSettings.ARENA_SETTING.PLAIN_ARENA)
            {
                text += "Peaceful Plains";
            }
            else if (currentSetting == GameSettings.ARENA_SETTING.WALLED_ARENA)
            {
                text += "Walls of Death";
            }
            else if (currentSetting == GameSettings.ARENA_SETTING.SPLIT_BASE)
            {
                text += "Split Base";
            }
            else if (currentSetting == GameSettings.ARENA_SETTING.PIT_OF_DEATH)
            {
                text += "Pit of Doom";
            }

            arenaMenuEntry.Text = text;
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

        private void ArenaRightSelected(object sender, PlayerIndexEventArgs e)
        {
            arenaIndex++;
            if (arenaIndex >= arenas.Count)
            {
                arenaIndex = 0;
            }
            GameSettings.Arena = arenas[arenaIndex];
            setArenaText();
        }

        private void ArenaLeftSelected(object sender, PlayerIndexEventArgs e)
        {
            arenaIndex--;
            if (arenaIndex < 0)
            {
                arenaIndex = arenas.Count - 1;
            }
            GameSettings.Arena = arenas[arenaIndex];
            setArenaText();
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
            LoadingScreen.Load(ScreenManager, null, new BattleScreen(GameSettings.NumLives, GameSettings.NumPlayers, GameSettings.ItemsOn));
        }

        #endregion

    }
}
