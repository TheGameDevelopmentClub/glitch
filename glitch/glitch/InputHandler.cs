﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace glitch
{
    public class InputHandler
    {
        public static InputHandler inputHandler = null;

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        // Gamepad states used to determine button presses
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        public InputHandler() { }

        public static InputHandler GetInstance()
        {
            if (inputHandler == null)
            {
                inputHandler = new InputHandler();
            }
            return inputHandler;
        }

        public void updateStates()
        {
            previousKeyboardState = currentKeyboardState;
            previousGamePadState = currentGamePadState;


            currentKeyboardState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
        }

        public void handlePlayerInput(GameObject gameObject)
        {
            
        }

    }
}
