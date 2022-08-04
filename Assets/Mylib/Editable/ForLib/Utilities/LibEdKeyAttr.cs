using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine;



namespace Com.GNL.URP_MyLib
{
    [Serializable]
    public class LibEdKeyAttr
    {
        #region === Input Attibute ===
        //library
        public KeyCode MoveCam                  = KeyCode.Mouse2;
        public KeyCode AimTarget                = KeyCode.Mouse2;

        public KeyCode MoveForward              = KeyCode.W;
        public KeyCode MoveBack                 = KeyCode.S;
        public KeyCode MoveRight                = KeyCode.D;
        public KeyCode MoveLeft                 = KeyCode.A;
        //Movement 2
        public KeyCode Walk                     = KeyCode.C;
        public KeyCode BreakFront               = KeyCode.Space;
        public KeyCode BreakRear                = KeyCode.LeftShift;
        public KeyCode Gas                      = KeyCode.W;
        public KeyCode Reverse                  = KeyCode.S;
        //combat
        public KeyCode ReleaseBall              = KeyCode.Mouse0;
        //public KeyCode AttackSkill              = KeyCode.Mouse1;
        //weapon
        public KeyCode AddWeapon                = KeyCode.O;
        public KeyCode DeleteWeapon             = KeyCode.P;

        //Feature percobaan
        public KeyCode Addbox                   = KeyCode.K;
        public KeyCode AddRobot                 = KeyCode.L;
        public KeyCode ResetPlayer              = KeyCode.Backspace;

        public KeyCode Back                     = KeyCode.Escape;
        //Pause
        //public KeyCode Resume                   = KeyCode.Escape;
        //public KeyCode QuitGame = KeyCode.F11;
        //public KeyCode BackToMainmenu = KeyCode.F10;

        //Mainmenu
        //public KeyCode Start = KeyCode.;

        #endregion === Input Attibute ===


    }
}