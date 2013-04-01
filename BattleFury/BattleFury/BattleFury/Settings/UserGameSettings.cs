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
    public class UserGameSettings
    {



        /// <summary>
        /// Whether or not items should drop.
        /// </summary>
        public static bool ItemsOn = true;

        public static int WindowWidth = -1;

        public static int WindowHeight = -1;


        /// <summary>
        /// Whether to display in fullscreen or windowed
        /// </summary>
        public static bool Windowed = true;

        public static void LoadDefaultGameSettings(int windowWidth, int windowHeight)
        {
            WindowWidth = windowWidth;
            WindowHeight = windowHeight;
        }
    }
}
