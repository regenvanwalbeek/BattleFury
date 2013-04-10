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

        public PlayerIndex PlayerIndex;

        public PlayerSettings(GameSettings.CHARACTER_SETTING character, int team, Color color, PlayerIndex playerIndex)
        {
            this.Character = character;
            this.Team = team;
            this.Color = color;
            this.PlayerIndex = playerIndex;
        }

    }
}
