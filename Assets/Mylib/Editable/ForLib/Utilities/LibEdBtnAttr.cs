using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine;



namespace Com.GNL.URP_MyLib
{

    public enum MY_BTN_CODE
    {
        //special from unity
        ////KeyCode.Escape escape  android back button, pc esc button
        Btn_Back = KeyCode.Escape,//27

        //Lib
        Btn_None = 0,
        Btn_AimTarget,
        Btn_MoveCam,
        //Lib

        Btn_Start_Game,
        Btn_Start_MultiPlayer,
        Btn_Start_Option,

        Btn_MoveForward,
        Btn_MoveBack,
        Btn_MoveRight,
        Btn_MoveLeftt,
        Btn_Sprintt,//10

        Btn_Walk,
        Btn_Jump,
        Btn_BreakFront,
        Btn_BreakRear,
        Btn_Gas,
        Btn_Reverse,
        Btn_ReleaseBall,
        Btn_Shoot,
        Btn_Exit,//20

        Btn_BackMM,
        Btn_Pause,
        Btn_Resume,
        Btn_Select,
        Btn_AddWeapon,
        Btn_DeleteWeapon,
        Btn_Move,

        //State multiplayer Lobby
        Btn_MP_CreteLby,
        Btn_MP_JoinLby,
        Btn_MP_JoinRdmLby,//30


        Btn_Addbox,
        Btn_AddRobot,
        Btn_ResetPlayer
    }

    [Serializable]
    public class LibEdBtnAttr
    {


        #region === Lib Attibute ===
        //from //dont change the namer of atttibute, just change the value

        //public KeyCode Btn_AimTarget = KeyCode.Mouse2;
        //public KeyCode Btn_MoveCam = KeyCode.Mouse3;
        public enum MY_BTN_CODE_LIB
        {
            //Btn_AimTarget = KeyCode.Mouse2,
            //Btn_MoveCam = KeyCode.Mouse3,
        }
        #endregion === Lib Attibute ===

        //Till //dont change the namer of atttibute, just change the value

        #region === Btn Attibute ===
        public enum KEY_MOUSE
        {
            Left,
            Right,
            Center
        }

        public enum KEY_NAME
        {
            Vertical,
            Horizontal,
        }

        #endregion === Btn Attibute ===



    }
}