using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace BattleFury.Settings
{
    /// <summary>
    /// A easily accessible class which maintains the settings for game play
    /// </summary>
    public class GameSettings
    {

        public enum ARENA_SETTING
        {
            PLAIN_ARENA
        }

        /// <summary>
        /// Whether or not items should drop.
        /// </summary>
        public static bool ItemsOn = true;

        /// <summary>
        /// Width of the game screen.
        /// </summary>
        public static int WindowWidth = -1;

        /// <summary>
        /// Heigh of the game screen.
        /// </summary>
        public static int WindowHeight = -1;

        /// <summary>
        /// Whether to display in fullscreen or windowed
        /// </summary>
        public static bool Windowed = true;

        /// <summary>
        /// The Arena to battle in.
        /// </summary>
        public static ARENA_SETTING Arena = ARENA_SETTING.PLAIN_ARENA;

        public static void LoadDefaultGameSettings(int windowWidth, int windowHeight)
        {
            WindowWidth = windowWidth;
            WindowHeight = windowHeight;
        }
    }
}
