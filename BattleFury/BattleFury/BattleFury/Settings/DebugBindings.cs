using BattleFury.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

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

    }
}
