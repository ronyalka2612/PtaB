using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine;



namespace Com.GNL.URP_MyLib
{
    [Serializable]
    public class LibEdInputAttr
    {

        #region === Input Attibute ===
        public void NormalizeInput()
        {
            //Movement
            Move                = false;
            MoveForward         = false;
            MoveBack            = false;
            MoveRight           = false;
            MoveLeft            = false;

            Sprint              = false;
            Walk                = false;
            Jump                = false;

            MoveCam             = false;

            ReleaseBall         = false;
            AimTarget           = false;

            AddWeapon           = false;
            DeleteWeapon        = false;


            Addbox              = false;
            AddRobot            = false;

            MenuPause           = false;
            QuitGame            = false;
            BackToMainmenu      = false;

            Resume              = false;
            BreakFront          = false;
            BreakRear           = false;
            ResetPlayer         = false;

            Gas                 = false;
            Reverse             = false;

            Start_Game          = false;
            Start_MultiPlayer   = false;
            Start_Option        = false;

            MP_CreteLby         = false;
            MP_JoinLby          = false;
            MP_JoinRdmLby       = false;
        }


        //[Header("INPUT_GAMEPLAY")]
        //Movement
        public bool Move                = false;
        public bool MoveForward         = false;
        public bool MoveBack            = false;
        public bool MoveRight           = false;
        public bool MoveLeft            = false;
        //Movement 2
        public bool Sprint              = false;
        public bool Walk                = false;
        public bool Jump                = false;
        public bool BreakFront          = false;
        public bool BreakRear           = false;
        public bool Gas                 = false;
        public bool Reverse             = false;
        // cam
        public bool MoveCam             = false;
        //combat
        public bool ReleaseBall         = false;
        public bool AimTarget           = false;
        //weapon
        public bool AddWeapon           = false;
        public bool DeleteWeapon        = false;

        //Feature percobaan
        public bool Addbox              = false;
        public bool AddRobot            = false;
        public bool ResetPlayer         = false;

        public bool MenuPause           = false;
        //Pause
        public bool Resume              = false;
        public bool QuitGame            = false;
        public bool BackToMainmenu      = false;

        //Mainmenu
        public bool Start_Game          = false;
        public bool Start_MultiPlayer   = false;
        public bool Start_Option        = false;
        public bool Back                = false;

        //Mainmenu Multiplayer lobby
        public bool MP_CreteLby         = false;
        public bool MP_JoinLby          = false;
        public bool MP_JoinRdmLby       = false;

        #endregion === Input Attibute ===


    }
}