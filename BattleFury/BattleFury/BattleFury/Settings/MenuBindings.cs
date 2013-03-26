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
        public static bool IsMenuSelect(InputState inputState, PlayerIndex? controllingPlayer,
                                 out PlayerIndex playerIndex)
        {
            return inputState.IsNewKeyPress(Keys.Space, controllingPlayer, out playerIndex) ||
                   inputState.IsNewKeyPress(Keys.Enter, controllingPlayer, out playerIndex) ||
                   inputState.IsNewButtonPress(Buttons.A, controllingPlayer, out playerIndex) ||
                   inputState.IsNewButtonPress(Buttons.Start, controllingPlayer, out playerIndex);
        }


        /// <summary>
        /// Binds Escape, B, and Back Button to "menu cancel" input action.
        /// The controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When the action
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public static bool IsMenuCancel(InputState inputState, PlayerIndex? controllingPlayer,
                                 out PlayerIndex playerIndex)
        {
            return inputState.IsNewKeyPress(Keys.Escape, controllingPlayer, out playerIndex) ||
                   inputState.IsNewButtonPress(Buttons.B, controllingPlayer, out playerIndex) ||
                   inputState.IsNewButtonPress(Buttons.Back, controllingPlayer, out playerIndex);
        }


        /// <summary>
        /// Binds Up Arrow key, Dpad up, and LThumbstick up to "menu up" input action.
        /// The controllingPlayer parameter specifies which player to read
        /// input for. If this is null, it will accept input from any player.
        /// </summary>
        public static bool IsMenuUp(InputState inputState, PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return inputState.IsNewKeyPress(Keys.Up, controllingPlayer, out playerIndex) ||
                   inputState.IsNewButtonPress(Buttons.DPadUp, controllingPlayer, out playerIndex) ||
                   inputState.IsNewButtonPress(Buttons.LeftThumbstickUp, controllingPlayer, out playerIndex);
        }


        /// <summary>
        /// Binds Down arrow key, Dpad down, and LThumbstick down to "menu down" input action.
        /// The controllingPlayer parameter specifies which player to read
        /// input for. If this is null, it will accept input from any player.
        /// </summary>
        public static bool IsMenuDown(InputState inputState, PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return inputState.IsNewKeyPress(Keys.Down, controllingPlayer, out playerIndex) ||
                   inputState.IsNewButtonPress(Buttons.DPadDown, controllingPlayer, out playerIndex) ||
                   inputState.IsNewButtonPress(Buttons.LeftThumbstickDown, controllingPlayer, out playerIndex);
        }


        
    }
}
