using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

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

        public enum CHARACTER_SETTING
        {
            ROBOT
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

        /// <summary>
        /// The number of players.
        /// </summary>
        public static int NumPlayers = 4;

        /// <summary>
        /// The number of lives to play with
        /// </summary>
        public static int NumLives = 3;

        /// <summary>
        /// List of characters each player will use to play.
        /// </summary>
        public static List<PlayerSettings> Players;

        public static void LoadDefaultGameSettings(int windowWidth, int windowHeight)
        {
            WindowWidth = windowWidth;
            WindowHeight = windowHeight;

            Players = new List<PlayerSettings>();
            Players.Add(new PlayerSettings(CHARACTER_SETTING.ROBOT, 1, Color.Blue, 1));
            Players.Add(new PlayerSettings(CHARACTER_SETTING.ROBOT, 2, Color.Red, 2));
            Players.Add(new PlayerSettings(CHARACTER_SETTING.ROBOT, 3, Color.Green, 3));
            Players.Add(new PlayerSettings(CHARACTER_SETTING.ROBOT, 4, Color.Yellow, 4));
        }
    }
}
