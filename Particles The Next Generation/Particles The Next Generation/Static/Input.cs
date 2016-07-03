using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Particles_The_Next_Generation
{
    static class Input
    {
        #region members

        private static MouseState currentMouseState;
        private static MouseState previousMouseState;

        private static KeyboardState currentKeyboardState;
        private static KeyboardState previousKeyboardState;

        private static Vector2 mouseVelocity;
        private static Vector2 mousePosition;

        private static Point mousePosition_Point;

        private static bool lmb_Clicked, lmb_Holding;
        private static bool rmb_Clicked, rmb_Holding;

        #endregion

        public static KeyboardState KeyboardState
        {
            get { return currentKeyboardState; }
        }

        public static bool KeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        public static bool KeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key) && previousKeyboardState.IsKeyDown(key);
        }

        public static MouseState MouseState
        {
            get { return currentMouseState; }
        }

        public static Vector2 MousePosition
        {
            get { return mousePosition; }
        }

        public static Point MousePosition_Point
        {
            get { return mousePosition_Point; }
        }

        public static Vector2 MouseVelocity
        {
            get { return mouseVelocity; }
        }

        public static bool LMB_Pressed
        {
            get { return lmb_Holding; }
        }

        public static bool LMB_Clicked
        {
            get { return lmb_Clicked; }
        }

        public static bool RMB_Pressed
        {
            get { return rmb_Holding; }
        }

        public static bool RMB_Clicked
        {
            get { return rmb_Clicked; }
        }

        public static void Update()
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            lmb_Holding = currentMouseState.LeftButton == ButtonState.Pressed;
            rmb_Holding = currentMouseState.RightButton == ButtonState.Pressed;

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();


            if (previousMouseState != null)
            {
                Vector2 previousPos = new Vector2(previousMouseState.X, previousMouseState.Y);
                Vector2 currentPos = new Vector2(currentMouseState.X, currentMouseState.Y);

                mousePosition_Point = new Point((int)currentPos.X, (int)currentPos.Y);

                mouseVelocity = currentPos - previousPos;
                mousePosition = currentPos;

                bool prevHold = previousMouseState.LeftButton == ButtonState.Pressed;
                lmb_Clicked = lmb_Holding && !prevHold;

                prevHold = previousMouseState.RightButton == ButtonState.Pressed;
                rmb_Clicked = rmb_Holding && !prevHold;
            }
        }
    }
}
