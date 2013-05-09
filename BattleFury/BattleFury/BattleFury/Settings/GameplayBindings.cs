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
            #if DEBUG
            return InputState.IsNewKeyPress(Keys.J, PlayerIndex.One, out playerIndex) ||
                InputState.IsNewButtonPress(Buttons.A, controllingPlayer, out playerIndex);
            #else
            return InputState.IsNewButtonPress(Buttons.A, controllingPlayer, out playerIndex);
            #endif
        }

        /// <summary>
        /// Binds X Button to Grab
        /// </summary>
        public static bool IsGrab(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;
            #if DEBUG
            return InputState.IsNewButtonPress(Buttons.X, controllingPlayer, out playerIndex) ||
                InputState.IsNewKeyPress(Keys.G, PlayerIndex.One, out playerIndex) ;
            #else
            return InputState.IsNewButtonPress(Buttons.X, controllingPlayer, out playerIndex);
            #endif
        }

        /// <summary>
        /// Binds Grab button to throw.
        /// </summary>
        public static bool IsThrow(PlayerIndex? controllingPlayer)
        {
            return IsGrab(controllingPlayer);
        }

        /// <summary>
        /// Binds Punch to B button.
        /// </summary>
        public static bool IsPunch(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;
            return InputState.IsNewButtonPress(Buttons.B, controllingPlayer, out playerIndex);
        }

        /// <summary>
        /// Binds Punch to B + Up
        /// </summary>
        public static bool IsUpPunch(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;
            return InputState.IsNewButtonPress(Buttons.B, controllingPlayer, out playerIndex) &&
                InputState.GetLeftAnalogStick(playerIndex).Y >= .80f;
        }

        /// <summary>
        /// Binds Ground pound to B + Down.
        /// </summary>
        public static bool IsGroundPound(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;
      
            return InputState.IsNewButtonPress(Buttons.B, controllingPlayer, out playerIndex) &&
               InputState.GetLeftAnalogStick(playerIndex).Y <= -.80f;
        }

        /// <summary>
        /// Binds Punch to Y button.
        /// </summary>
        public static bool IsFire(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;
           
            return InputState.IsNewButtonPress(Buttons.Y, controllingPlayer, out playerIndex);
          
        }

        /// <summary>
        /// Gets the amount moved left or right.
        /// </summary>
        public static float MoveAmount(PlayerIndex controllingPlayer)
        {
            #if DEBUG
            PlayerIndex playerIndex;
            if (InputState.IsKeyPressed(Keys.K, controllingPlayer, out playerIndex))
            {
                if (controllingPlayer != PlayerIndex.One)
                {
                    return 0;
                }
                else
                {
                    return -1.0f;
                }
            }
            else if (InputState.IsKeyPressed(Keys.L, controllingPlayer, out playerIndex))
            {
                if (controllingPlayer != PlayerIndex.One)
                {
                    return 0;
                }
                else
                {
                    return 1.0f;
                }
            }
            #endif

            return InputState.GetLeftAnalogStick(controllingPlayer).X;
        }

    }
}
