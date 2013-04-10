using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BattleFury.Settings
{
    /// <summary>
    /// Defines the settings for a specific player as defined before matchmaking.
    /// </summary>
    public class PlayerSettings
    {
        public GameSettings.CHARACTER_SETTING Character;

        public int Team;

        public Color Color;

        public int PlayerIndex;

        public PlayerSettings(GameSettings.CHARACTER_SETTING character, int team, Color color, int playerIndex)
        {
            if (playerIndex > 4 || playerIndex < 0)
            {
                // Currently only support 4 players.
                throw new ArgumentOutOfRangeException();
            }

            this.Character = character;
            this.Team = team;
            this.Color = color;
            this.PlayerIndex = playerIndex;
        }

    }
}
