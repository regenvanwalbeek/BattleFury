using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.ScreenManagement;
using BattleFury.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BattleFury.Screens
{
    /// <summary>
    /// Screen for setting the gameplay options
    /// </summary>
    public class OptionsScreen : MenuScreen
    {
        /// <summary>
        /// Menu entry for whether items should drop in game.
        /// </summary>
        private MenuEntry itemsMenuEntry;

        /// <summary>
        /// Menu entry for choosing a screen resolution.
        /// </summary>
        private MenuEntry resolutionMenuEntry;

        /// <summary>
        /// Menu entry for choosing full screen.
        /// </summary>
        private MenuEntry windowedMenuEntry;

        private int displayModeIndex = 0;

        private List<DisplayMode> displayModes = new List<DisplayMode>();

        bool firstTime = true;

        #region Initialization
        public OptionsScreen()
            : base("Options")
        {
            itemsMenuEntry = new MenuEntry(string.Empty);
            windowedMenuEntry = new MenuEntry(string.Empty);
            resolutionMenuEntry = new MenuEntry(string.Empty);
            MenuEntry backMenuEntry = new MenuEntry("Back");

            setMenuEntryText();
            setWindowedEntryText();
            setResolutionEntryText();

            // Attach the event handlers
            itemsMenuEntry.Selected += ItemsMenuEntrySelected;
            windowedMenuEntry.Selected += FullscreenMenuEntrySelected;
            resolutionMenuEntry.Selected += ResolutionMenuEntrySelected;
            backMenuEntry.Selected += OnComplete;
            backMenuEntry.Selected += OnCancel;

            MenuEntries.Add(itemsMenuEntry);
            MenuEntries.Add(windowedMenuEntry);
            MenuEntries.Add(resolutionMenuEntry);
            MenuEntries.Add(backMenuEntry);
        }

        #endregion

        #region Handle Input

        private void setMenuEntryText()
        {
            itemsMenuEntry.Text = "Items: " + (UserGameSettings.ItemsOn ? "on" : "off");
        }

        private void setWindowedEntryText()
        {
            windowedMenuEntry.Text = UserGameSettings.Windowed ? "Windowed" : "Fullscreen";
        }

        private void setResolutionEntryText()
        {
            resolutionMenuEntry.Text = "Resolution: " + UserGameSettings.WindowWidth + "x" + UserGameSettings.WindowHeight;
        }

        // Toggle whether items are on or off
        private void ItemsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            UserGameSettings.ItemsOn = !UserGameSettings.ItemsOn;
            setMenuEntryText();
        }

        // Toggle fullscreen
        private void FullscreenMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            UserGameSettings.Windowed = !UserGameSettings.Windowed;
            setWindowedEntryText();
        }

        // Change Resolution
        private void ResolutionMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            // Initialize resolutions the first time this is called
            if (firstTime)
            {
                foreach (DisplayMode mode in ScreenManager.DisplayModes){
                    // ignore duplicates
                    bool alreadyAdded = false;
                    for (int i = 0; i < displayModes.Count; i++)
                    {
                        if (displayModes[i].Height == mode.Height && displayModes[i].Width == mode.Width)
                        {
                            alreadyAdded = true;
                        }
                    }

                    bool tooSmall = false;
                    if (mode.Height < GlobalGameConstants.MIN_SCREEN_HEIGHT || mode.Width < GlobalGameConstants.MIN_SCREEN_WIDTH)
                    {
                        tooSmall = true;
                    }

                    if (!alreadyAdded && !tooSmall)
                    {
                        displayModes.Add(mode);
                    }
                }

                // initialze the display mode index by finding the current resolution
                for (int i = 0; i < displayModes.Count; i++)
                {
                    DisplayMode mode = displayModes[i];
                    if (mode.Width == UserGameSettings.WindowWidth && mode.Height == UserGameSettings.WindowHeight)
                    {
                        displayModeIndex = i;
                        break;
                    }
                }
                
                firstTime = false;
            }

            // Get the next display mode.
            displayModeIndex++;
            if (displayModeIndex >= displayModes.Count)
            {
                displayModeIndex = 0;
            }

            // Update the settings
            DisplayMode newDisplayMode = displayModes[displayModeIndex];
            UserGameSettings.WindowWidth = newDisplayMode.Width;
            UserGameSettings.WindowHeight = newDisplayMode.Height;

            setResolutionEntryText();
        }

        // Modify screen resolution as specified by settings.
        private void OnComplete(object sender, PlayerIndexEventArgs e)
        {
            // Set full screen, if necessary
            if (UserGameSettings.Windowed == ScreenManager.GraphicsDeviceManager.IsFullScreen)
            {
                ScreenManager.GraphicsDeviceManager.ToggleFullScreen();
            }

            // Resize window, if necessary
            if (UserGameSettings.WindowWidth != ScreenManager.GraphicsDevice.Viewport.Width || 
                UserGameSettings.WindowHeight != ScreenManager.GraphicsDevice.Viewport.Height)
            {
                ScreenManager.GraphicsDeviceManager.PreferredBackBufferWidth = UserGameSettings.WindowWidth;
                ScreenManager.GraphicsDeviceManager.PreferredBackBufferHeight = UserGameSettings.WindowHeight;
                ScreenManager.GraphicsDeviceManager.ApplyChanges();
            }

        }

        #endregion
    }
}
