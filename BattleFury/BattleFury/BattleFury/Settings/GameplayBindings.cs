using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using BattleFury.Input;
using Microsoft.Xna.Framework;

namespace BattleFury.Settings
{
    /// <summary>
    /// Class which holds the gameplay bindings
    /// </summary>
    public class GameplayBindings
    {

        /// <summary>
        /// Binds Escape key, Back button, and Start button to pause the game" input action.
        /// The controllingPlayer parameter specifies which player to read
        /// input for. If this is null, it will accept input from any player.
        /// </summary>
        public static bool IsPauseGame(InputState inputState, PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return inputState.IsNewKeyPress(Keys.Escape, controllingPlayer, out playerIndex) ||
                   inputState.IsNewButtonPress(Buttons.Back, controllingPlayer, out playerIndex) ||
                   inputState.IsNewButtonPress(Buttons.Start, controllingPlayer, out playerIndex);
        }



    }
}
