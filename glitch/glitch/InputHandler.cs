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

        public static InputHandler Instance
        {
            get
            {
                if (inputHandler == null)
                {
                    inputHandler = new InputHandler();
                }
                return inputHandler;
            }
        }

        public void handlePlayerInput(PlayerObject playerObject)
        {
            currentKeyboardState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(PlayerIndex.One);

            if (currentKeyboardState.IsKeyDown(Keys.Space) || currentGamePadState.IsButtonDown(Buttons.A))
            {
                if (!playerObject.IsJumping)
                {
                    playerObject.physComp.velocity.Y = -500.0f;
                    playerObject.IsJumping = true;
                    //TODO: Change the Vertical velocity to a higher or lower based on play.
                }
            }

            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentGamePadState.DPad.Left == ButtonState.Pressed)
            {
                playerObject.physComp.velocity.X = -playerObject.MaxHorizontalVelocity;
            }


            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentGamePadState.DPad.Right == ButtonState.Pressed)
            {
                playerObject.physComp.velocity.X = playerObject.MaxHorizontalVelocity;
            }

            if ((previousKeyboardState.IsKeyDown(Keys.Left)  && currentKeyboardState.IsKeyUp(Keys.Left)) || (previousGamePadState.DPad.Left == ButtonState.Pressed && currentGamePadState.DPad.Left == ButtonState.Released))
            {
                playerObject.physComp.velocity.X = 0;
            }


            if (previousKeyboardState.IsKeyDown(Keys.Right) && (currentKeyboardState.IsKeyUp(Keys.Right)) || ((previousGamePadState.DPad.Right == ButtonState.Pressed && currentGamePadState.DPad.Right == ButtonState.Released)))
            {
                playerObject.physComp.velocity.X = 0;
            }else 


            previousKeyboardState = currentKeyboardState;
            previousGamePadState = currentGamePadState;
        }

    }
}
