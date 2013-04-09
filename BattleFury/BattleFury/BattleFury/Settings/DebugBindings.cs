using BattleFury.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace BattleFury.Settings
{
    /// <summary>
    /// Class which holds bindings for debugging.
    /// </summary>
    public class DebugBindings
    {
        /// <summary>
        /// Binds F12 to Force Quit.
        /// </summary>
        public static bool IsForceQuit(InputState inputState, PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return inputState.IsNewKeyPress(Keys.F12, controllingPlayer, out playerIndex);
        }

        /// <summary>
        /// Binds F11 swapping camera.
        /// </summary>
        public static bool IsSwitchCamera(InputState inputState, PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return inputState.IsNewKeyPress(Keys.F11, controllingPlayer, out playerIndex);
        }

        /// <summary>
        /// Binds Forward movement to 
        /// </summary>
        public static bool IsCameraForward(InputState inputState, PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;
            return inputState.IsNewKeyPress(Keys.W, controllingPlayer, out playerIndex);
            throw new NotImplementedException();
        }

        public static bool IsCameraBackward(InputState inputState, PlayerIndex? controllingPlayer)
        {
            throw new NotImplementedException();
        }

        public static bool IsCameraUp(InputState inputState, PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;
            return inputState.IsNewKeyPress(Keys.Up, controllingPlayer, out playerIndex);
        }

        public static bool IsCameraDown(InputState inputState, PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;
            return inputState.IsNewKeyPress(Keys.Down, controllingPlayer, out playerIndex);
        }

        public static bool IsCameraLeft(InputState inputState, PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;
            return inputState.IsNewKeyPress(Keys.Left, controllingPlayer, out playerIndex)
                || inputState.IsNewKeyPress(Keys.A, controllingPlayer, out playerIndex);
        }

        public static bool IsCameraRight(InputState inputState, PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;
            return inputState.IsNewKeyPress(Keys.Right, controllingPlayer, out playerIndex) 
                || inputState.IsNewKeyPress(Keys.D, controllingPlayer, out playerIndex);
        }

        

        


    }
}
