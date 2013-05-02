#region File Description
//-----------------------------------------------------------------------------
// InputState.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
#endregion

namespace BattleFury.Input
{
    /// <summary>
    /// Helper for reading input from keyboard, gamepad, and touch input. This class 
    /// tracks both the current and previous state of the input devices, and implements 
    /// query methods for high level input actions such as "move up through the menu"
    /// or "pause the game".
    /// </summary>
    public class InputState
    {
        #region Fields

        public const int MaxInputs = 4;

        public static readonly KeyboardState[] CurrentKeyboardStates = new KeyboardState[MaxInputs];
        public static readonly GamePadState[] CurrentGamePadStates = new GamePadState[MaxInputs];
        public static MouseState CurrentMouseState { get; private set; }

        public static readonly KeyboardState[] LastKeyboardStates = new KeyboardState[MaxInputs];
        public static readonly GamePadState[] LastGamePadStates = new GamePadState[MaxInputs];
        public static MouseState LastMouseState { get; private set;  }

        public static readonly bool[] GamePadWasConnected = new bool[MaxInputs];

        #endregion

        #region Public Methods


        /// <summary>
        /// Reads the latest state of the keyboard and gamepad.
        /// </summary>
        public static void Update()
        {
            LastMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
            for (int i = 0; i < MaxInputs; i++)
            {
                LastKeyboardStates[i] = CurrentKeyboardStates[i];
                LastGamePadStates[i] = CurrentGamePadStates[i];

                CurrentKeyboardStates[i] = Keyboard.GetState((PlayerIndex)i);
                CurrentGamePadStates[i] = GamePad.GetState((PlayerIndex)i);


                // Keep track of whether a gamepad has ever been
                // connected, so we can detect if it is unplugged.
                if (CurrentGamePadStates[i].IsConnected)
                {
                    GamePadWasConnected[i] = true;
                }
            }
        }


        /// <summary>
        /// Helper for checking if a key was newly pressed during this update. The
        /// controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When a keypress
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public static bool IsNewKeyPress(Keys key, PlayerIndex? controllingPlayer,
                                            out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;

                return (CurrentKeyboardStates[i].IsKeyDown(key) &&
                        LastKeyboardStates[i].IsKeyUp(key));
            }
            else
            {
                // Accept input from any player.
                return (IsNewKeyPress(key, PlayerIndex.One, out playerIndex) ||
                        IsNewKeyPress(key, PlayerIndex.Two, out playerIndex) ||
                        IsNewKeyPress(key, PlayerIndex.Three, out playerIndex) ||
                        IsNewKeyPress(key, PlayerIndex.Four, out playerIndex));
            }
        }


        /// <summary>
        /// Helper for checking if a button was newly pressed during this update.
        /// The controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When a button press
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public static bool IsNewButtonPress(Buttons button, PlayerIndex? controllingPlayer,
                                                     out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;

                return (CurrentGamePadStates[i].IsButtonDown(button) &&
                        LastGamePadStates[i].IsButtonUp(button));
            }
            else
            {
                // Accept input from any player.
                return (IsNewButtonPress(button, PlayerIndex.One, out playerIndex) ||
                        IsNewButtonPress(button, PlayerIndex.Two, out playerIndex) ||
                        IsNewButtonPress(button, PlayerIndex.Three, out playerIndex) ||
                        IsNewButtonPress(button, PlayerIndex.Four, out playerIndex));
            }
        }

        /// <summary>
        /// Helper for checking if a key is pressed for a given player index.
        /// </summary>
        public static bool IsKeyPressed(Keys key, PlayerIndex? controllingPlayer,
                                            out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;

                return (CurrentKeyboardStates[i].IsKeyDown(key));
            }
            else
            {
                // Accept input from any player.
                return (IsKeyPressed(key, PlayerIndex.One, out playerIndex) ||
                        IsKeyPressed(key, PlayerIndex.Two, out playerIndex) ||
                        IsKeyPressed(key, PlayerIndex.Three, out playerIndex) ||
                        IsKeyPressed(key, PlayerIndex.Four, out playerIndex));
            }
        }

        /// <summary>
        /// Helper for checking if a button is pressed for a given player index.
        /// </summary>
        public static bool IsButtonPressed(Buttons button, PlayerIndex? controllingPlayer,
                                                     out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;

                return (CurrentGamePadStates[i].IsButtonDown(button));
            }
            else
            {
                // Accept input from any player.
                return (IsButtonPressed(button, PlayerIndex.One, out playerIndex) ||
                        IsButtonPressed(button, PlayerIndex.Two, out playerIndex) ||
                        IsButtonPressed(button, PlayerIndex.Three, out playerIndex) ||
                        IsButtonPressed(button, PlayerIndex.Four, out playerIndex));
            }
        }

        public static void Rumble(PlayerIndex playerIndex, float leftMotor, float rightMotor)
        {
            GamePad.SetVibration(playerIndex, leftMotor, rightMotor);
        }

        /// <summary>
        /// Helper for Checking Mouse X Change between Updates
        /// </summary>
        public static int GetMouseDeltaX()
        {
            return CurrentMouseState.X - LastMouseState.X;
        }

        /// <summary>
        /// Helper for Checking Mouse Y change between Updates
        /// </summary>
        public static int GetMouseDeltaY()
        {
            return CurrentMouseState.Y - LastMouseState.Y;
        }

        /// <summary>
        /// Helper method for getting the Left analog stick value
        /// </summary>
        public static Vector2 GetLeftAnalogStick(PlayerIndex playerIndex)
        {
            int i = (int) playerIndex;
            return CurrentGamePadStates[i].ThumbSticks.Left;
        }

        /// <summary>
        /// Helper method to check if the thumbstick was pressed left over a certain threshhold. Messy, but I need it.
        /// </summary>
        public static bool IsNewLeftThumbstickPressedHardLeft(PlayerIndex? controllingPlayer, out PlayerIndex playerIndex, float threshold)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;

                return (CurrentGamePadStates[i].ThumbSticks.Left.X < threshold &&
                        LastGamePadStates[i].ThumbSticks.Left.X > threshold);
            }
            else
            {
                // Accept input from any player.
                return (IsNewLeftThumbstickPressedHardLeft(PlayerIndex.One, out playerIndex, threshold) ||
                        IsNewLeftThumbstickPressedHardLeft(PlayerIndex.Two, out playerIndex, threshold) ||
                        IsNewLeftThumbstickPressedHardLeft(PlayerIndex.Three, out playerIndex, threshold) ||
                        IsNewLeftThumbstickPressedHardLeft(PlayerIndex.Four, out playerIndex, threshold));
            }
        }

        /// <summary>
        /// Helper method to check if the thumbstick was pressed right over a certain threshhold. Messy, but I need it.
        /// </summary>
        public static bool IsNewLeftThumbstickPressedHardRight(PlayerIndex? controllingPlayer, out PlayerIndex playerIndex, float threshold)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;

                return (CurrentGamePadStates[i].ThumbSticks.Left.X > threshold &&
                        LastGamePadStates[i].ThumbSticks.Left.X < threshold);
            }
            else
            {
                // Accept input from any player.
                return (IsNewLeftThumbstickPressedHardRight(PlayerIndex.One, out playerIndex, threshold) ||
                        IsNewLeftThumbstickPressedHardRight(PlayerIndex.Two, out playerIndex, threshold) ||
                        IsNewLeftThumbstickPressedHardRight(PlayerIndex.Three, out playerIndex, threshold) ||
                        IsNewLeftThumbstickPressedHardRight(PlayerIndex.Four, out playerIndex, threshold));
            }
        }

        #endregion
    }
}
