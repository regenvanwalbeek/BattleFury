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
        public static bool IsPauseGame(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return InputState.IsNewKeyPress(Keys.Escape, controllingPlayer, out playerIndex) ||
                   InputState.IsNewButtonPress(Buttons.Back, controllingPlayer, out playerIndex) ||
                   InputState.IsNewButtonPress(Buttons.Start, controllingPlayer, out playerIndex);
        }

        /// <summary>
        /// Binds Y Button to Jump.
        /// </summary>
        public static bool IsJump(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return InputState.IsNewKeyPress(Keys.J, controllingPlayer, out playerIndex) ||
                InputState.IsNewButtonPress(Buttons.A, controllingPlayer, out playerIndex);
        }


        public static float MoveAmount(PlayerIndex controllingPlayer)
        {
            PlayerIndex playerIndex;
            if (InputState.IsKeyPressed(Keys.K, controllingPlayer, out playerIndex))
            {
                return -1.0f;
            }
            else if (InputState.IsKeyPressed(Keys.L, controllingPlayer, out playerIndex))
            {
                return 1.0f;
            }
            return InputState.GetLeftAnalogStick(controllingPlayer).X;
        }


    }
}
