using UnityEngine;

namespace Com.GNLTest.Test1
{
    public static class InputHandle
    {
        public static bool Jump = false;

        public static bool MoveBack = false;
        public static bool MoveForward = false;
        public static bool MoveLeft = false;
        public static bool MoveRight = false;

        public static bool AddFPS = false;
        public static bool DecFPS = false;

        public static bool Move = false;

        // BTN

        public static bool BTN_Jump             = false;
        public static bool BTN_Move             = false;
        public static bool BTN_AddFPS           = false;
        public static bool BTN_DecFPS           = false;

        // KeyCode

        public static KeyCode Key_Jump          = KeyCode.Space ;
        public static KeyCode Key_Move          = KeyCode.Joystick8Button0;
        public static KeyCode Key_AddFPS        = KeyCode.Joystick8Button1;
        public static KeyCode Key_DecFPS        = KeyCode.Joystick8Button2;


    }
}