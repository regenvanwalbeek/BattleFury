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
        /// Menu entry for music volume
        /// </summary>
        private MenuEntry musicVolumeMenuEntry;

        /// <summary>
        /// Menu entry for sound effect volume
        /// </summary>
        private MenuEntry fxVolumeMenuEntry;

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

        private bool firstTime = true;

        #region Initialization

        public OptionsScreen()
            : base("Options")
        {
            musicVolumeMenuEntry = new MenuEntry(string.Empty);
            fxVolumeMenuEntry = new MenuEntry(string.Empty);
            windowedMenuEntry = new MenuEntry(string.Empty);
            resolutionMenuEntry = new MenuEntry(string.Empty);
            MenuEntry backMenuEntry = new MenuEntry("Back");

            setMusicVolumeText() ;
            setWindowedEntryText();
            setResolutionEntryText();
            setFXVolumeText();

            // Attach the event handlers
            musicVolumeMenuEntry.RightSelected += MusicVolumeIncrease;
            musicVolumeMenuEntry.LeftSelected += MusicVolumeDecrease;
            fxVolumeMenuEntry.RightSelected += FXVolumeIncrease;
            fxVolumeMenuEntry.LeftSelected += FXVolumeDecrease;
            windowedMenuEntry.Selected += FullscreenMenuEntrySelected;
            windowedMenuEntry.RightSelected += FullscreenMenuEntrySelected;
            windowedMenuEntry.LeftSelected += FullscreenMenuEntrySelected;
            resolutionMenuEntry.RightSelected += ResolutionRightSelected;
            resolutionMenuEntry.LeftSelected += ResolutionLeftSelected;
            backMenuEntry.Selected += OnComplete;
            backMenuEntry.Selected += OnCancel;

            MenuEntries.Add(musicVolumeMenuEntry);
            MenuEntries.Add(fxVolumeMenuEntry);
            MenuEntries.Add(windowedMenuEntry);
            MenuEntries.Add(resolutionMenuEntry);
            MenuEntries.Add(backMenuEntry);
        }

        #endregion

        #region Handle Input

        private void setMusicVolumeText()
        {
            musicVolumeMenuEntry.Text = "Music Volume: " + (GameSettings.MusicVolume);
        }

        private void setFXVolumeText()
        {
            fxVolumeMenuEntry.Text = "Effects Volume: " + (GameSettings.FXVolume);
        }

        private void setWindowedEntryText()
        {
            windowedMenuEntry.Text = GameSettings.Windowed ? "Windowed" : "Fullscreen";
        }

        private void setResolutionEntryText()
        {
            resolutionMenuEntry.Text = "Resolution: " + GameSettings.WindowWidth + "x" + GameSettings.WindowHeight;
        }

        // Increase music volume
        private void MusicVolumeIncrease(object sender, PlayerIndexEventArgs e)
        {
            GameSettings.MusicVolume += 1;
            if (GameSettings.MusicVolume > 10)
            {
                GameSettings.MusicVolume = 10;
            }
            setMusicVolumeText();
        }

        // Decrease music volume
        private void MusicVolumeDecrease(object sender, PlayerIndexEventArgs e)
        {
            GameSettings.MusicVolume -= 1;
            if (GameSettings.MusicVolume < 0)
            {
                GameSettings.MusicVolume = 0;
            }
            setMusicVolumeText();
        }

        // Increase effects volume
        private void FXVolumeIncrease(object sender, PlayerIndexEventArgs e)
        {
            GameSettings.FXVolume += 1;
            if (GameSettings.FXVolume > 10)
            {
                GameSettings.FXVolume = 10;
            }
            setFXVolumeText();
        }

        // Decrease effects volume
        private void FXVolumeDecrease(object sender, PlayerIndexEventArgs e)
        {
            GameSettings.FXVolume -= 1;
            if (GameSettings.FXVolume < 0)
            {
                GameSettings.FXVolume = 0;
            }
            setFXVolumeText();
        }


        // Toggle fullscreen
        private void FullscreenMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            GameSettings.Windowed = !GameSettings.Windowed;
            setWindowedEntryText();
        }

        // Change Resolution - Better Resolution
        private void ResolutionRightSelected(object sender, PlayerIndexEventArgs e)
        {
            initializeScreenSizes();

            // Get the next display mode.
            displayModeIndex++;
            if (displayModeIndex >= displayModes.Count)
            {
                displayModeIndex = 0;
            }

            // Update the settings
            DisplayMode newDisplayMode = displayModes[displayModeIndex];
            GameSettings.WindowWidth = newDisplayMode.Width;
            GameSettings.WindowHeight = newDisplayMode.Height;

            setResolutionEntryText();
        }

        // Change Resolution - Worse Resolution
        private void ResolutionLeftSelected(object sender, PlayerIndexEventArgs e)
        {
            initializeScreenSizes();

            // Get the next display mode
            displayModeIndex--;
            if (displayModeIndex < 0)
            {
                displayModeIndex = displayModes.Count - 1;
            }

            // Update the settings
            DisplayMode newDisplayMode = displayModes[displayModeIndex];
            GameSettings.WindowWidth = newDisplayMode.Width;
            GameSettings.WindowHeight = newDisplayMode.Height;

            setResolutionEntryText();
        }

        // Helper method for retreiving screen sizes
        private void initializeScreenSizes()
        {
            // Initialize resolutions the first time this is called
            if (firstTime)
            {
                foreach (DisplayMode mode in ScreenManager.DisplayModes)
                {
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
                    if (mode.Width == GameSettings.WindowWidth && mode.Height == GameSettings.WindowHeight)
                    {
                        displayModeIndex = i;
                        break;
                    }
                }

                firstTime = false;
            }
        }


        // Modify screen resolution as specified by settings.
        private void OnComplete(object sender, PlayerIndexEventArgs e)
        {
            // Set full screen, if necessary
            if (GameSettings.Windowed == ScreenManager.GraphicsDeviceManager.IsFullScreen)
            {
                ScreenManager.GraphicsDeviceManager.ToggleFullScreen();
            }

            // Resize window, if necessary
            if (GameSettings.WindowWidth != ScreenManager.GraphicsDevice.Viewport.Width || 
                GameSettings.WindowHeight != ScreenManager.GraphicsDevice.Viewport.Height)
            {
                ScreenManager.GraphicsDeviceManager.PreferredBackBufferWidth = GameSettings.WindowWidth;
                ScreenManager.GraphicsDeviceManager.PreferredBackBufferHeight = GameSettings.WindowHeight;
                ScreenManager.GraphicsDeviceManager.ApplyChanges();
            }

        }

        #endregion
    }
}
