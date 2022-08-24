using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine;



namespace Com.GNL.URP_MyLib
{
    public static class LibEdStateUtilities
    {
        #region === Lib Attibute ===
        //from //dont change the namer of atttibute, just change the value


        #endregion === Lib Attibute ===
        //Till //dont change the namer of atttibute, just change the value


        #region === GameState ===
        //reworkGameStates
        public enum GameStates
        {
            NO_STATE = -1,
            STARTER = 0,
            MAINMENU,
            MAINMENU2,
            //MULTI_PLAYER,
            //GP_3,
            MAIN_GP,
            COUNT,
        }

        #endregion === GameState ===
        #region === GameSubState ===
        public enum GameSubStates
        {

            NO_SUBSTATE = -2,
            STARTER_SUB = -1,
            //MAINMENU
            MAINMENU_MULTI_PLAYER = 0,
            MAINMENU_OPTION,
            //MAINMENU2
            MAINMENU2_OPTION,
            //GP_3,
            MAIN_GP_GAMEPLAY_PAUSE,
            COUNT,
        }

        #endregion === GameSubState ===


    }
}