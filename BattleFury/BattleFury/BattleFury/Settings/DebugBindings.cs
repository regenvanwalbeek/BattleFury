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
        public static bool IsForceQuit(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return InputState.IsNewKeyPress(Keys.F12, controllingPlayer, out playerIndex);
        }

        /// <summary>
        /// Binds F11 swapping camera.
        /// </summary>
        public static bool IsSwitchCamera(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return InputState.IsNewKeyPress(Keys.F11, controllingPlayer, out playerIndex);
        }

        /// <summary>
        /// Binds Forward camera movement to W key.
        /// </summary>
        public static bool IsCameraForward(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;
            return InputState.IsKeyPressed(Keys.W, controllingPlayer, out playerIndex);
        }

        /// <summary>
        /// Binds Backward camera movement to S key.
        /// </summary>
        public static bool IsCameraBackward(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;
            return InputState.IsKeyPressed(Keys.S, controllingPlayer, out playerIndex);
        }

        /// <summary>
        /// Binds Left camera movement to A key.
        /// </summary>
        public static bool IsCameraLeft(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;
            return InputState.IsKeyPressed(Keys.A, controllingPlayer, out playerIndex);
        }

        /// <summary>
        /// Binds Right camera movement to D key.
        /// </summary>
        public static bool IsCameraRight(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;
            return InputState.IsKeyPressed(Keys.D, controllingPlayer, out playerIndex);
        }

        /// <summary>
        /// Binds Up Look Camera Movement to Up key.
        /// </summary>
        public static bool IsCameraLookUp(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;
            return InputState.IsKeyPressed(Keys.Up, controllingPlayer, out playerIndex);
        }

        /// <summary>
        /// Binds Down Look Camera Movement to Down key.
        /// </summary>
        public static bool IsCameraLookDown(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;
            return InputState.IsKeyPressed(Keys.Down, controllingPlayer, out playerIndex);
        }

        /// <summary>
        /// Binds Left Look Camera Movement to Left key.
        /// </summary>
        public static bool IsCameraLookLeft(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;
            return InputState.IsKeyPressed(Keys.Left, controllingPlayer, out playerIndex);
        }

        /// <summary>
        /// Binds Right Look Camera Movement to Right key.
        /// </summary>
        public static bool IsCameraLookRight(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;
            return InputState.IsKeyPressed(Keys.Right, controllingPlayer, out playerIndex);
        }
     

       

        

        


    }
}
