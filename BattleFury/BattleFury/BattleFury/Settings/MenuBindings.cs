using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BattleFury.Input;
using Microsoft.Xna.Framework.Input;

namespace BattleFury.Settings
{
    /// <summary>
    /// Key and gamepad bindings for Menu select screens.
    /// </summary>
    public class MenuBindings
    {
        /// <summary>
        /// Binds Space, Enter, A button, and Start button to "menu select" input action.
        /// The controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When the action
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public static bool IsMenuSelect(PlayerIndex? controllingPlayer,
                                 out PlayerIndex playerIndex)
        {
            return InputState.IsNewKeyPress(Keys.Space, controllingPlayer, out playerIndex) ||
                   InputState.IsNewKeyPress(Keys.Enter, controllingPlayer, out playerIndex) ||
                   InputState.IsNewButtonPress(Buttons.A, controllingPlayer, out playerIndex) ||
                   InputState.IsNewButtonPress(Buttons.Start, controllingPlayer, out playerIndex);
        }


        /// <summary>
        /// Binds Escape, B, and Back Button to "menu cancel" input action.
        /// The controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When the action
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public static bool IsMenuCancel(PlayerIndex? controllingPlayer,
                                 out PlayerIndex playerIndex)
        {
            return InputState.IsNewKeyPress(Keys.Escape, controllingPlayer, out playerIndex) ||
                   InputState.IsNewButtonPress(Buttons.B, controllingPlayer, out playerIndex) ||
                   InputState.IsNewButtonPress(Buttons.Back, controllingPlayer, out playerIndex);
        }


        /// <summary>
        /// Binds Up Arrow key, Dpad up, and LThumbstick up to "menu up" input action.
        /// The controllingPlayer parameter specifies which player to read
        /// input for. If this is null, it will accept input from any player.
        /// </summary>
        public static bool IsMenuUp(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return InputState.IsNewKeyPress(Keys.Up, controllingPlayer, out playerIndex) ||
                   InputState.IsNewButtonPress(Buttons.DPadUp, controllingPlayer, out playerIndex) ||
                   InputState.IsNewButtonPress(Buttons.LeftThumbstickUp, controllingPlayer, out playerIndex);
        }


        /// <summary>
        /// Binds Down arrow key, Dpad down, and LThumbstick down to "menu down" input action.
        /// The controllingPlayer parameter specifies which player to read
        /// input for. If this is null, it will accept input from any player.
        /// </summary>
        public static bool IsMenuDown(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return InputState.IsNewKeyPress(Keys.Down, controllingPlayer, out playerIndex) ||
                   InputState.IsNewButtonPress(Buttons.DPadDown, controllingPlayer, out playerIndex) ||
                   InputState.IsNewButtonPress(Buttons.LeftThumbstickDown, controllingPlayer, out playerIndex);
        }

        /// <summary>
        /// Bidns for left/right (forward/back) scrolling options
        /// </summary>
        /// <param name="controllingPlayer"></param>
        /// <returns></returns>
        public static bool IsMenuLeft(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return InputState.IsNewKeyPress(Keys.Left, controllingPlayer, out playerIndex) ||
                   InputState.IsNewButtonPress(Buttons.DPadLeft, controllingPlayer, out playerIndex) ||
                   InputState.IsNewLeftThumbstickPressedHardLeft(controllingPlayer, out playerIndex, -.5f);
            
        }

        /// <summary>
        /// Bidns for left/right (forward/back) scrolling options
        /// </summary>
        /// <param name="controllingPlayer"></param>
        /// <returns></returns>
        public static bool IsMenuRight(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return InputState.IsNewKeyPress(Keys.Right, controllingPlayer, out playerIndex) ||
                   InputState.IsNewButtonPress(Buttons.DPadRight, controllingPlayer, out playerIndex) ||
                   InputState.IsNewLeftThumbstickPressedHardRight(controllingPlayer, out playerIndex, .5f);
        }

    }
}
