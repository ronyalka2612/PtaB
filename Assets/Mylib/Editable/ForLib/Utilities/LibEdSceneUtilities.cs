using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine;



namespace Com.GNL.URP_MyLib
{
    public static class LibEdSceneUtilities
    {
        #region === Lib Attibute ===
        


        //// ScenesAdditive tobe
        //public static int SCENE_ADDITIVE_SCN_STATE_MAINMENU = 0;
        //public static int SCENE_ADDITIVE_SCN_STATE_GAMEPLAY = SCENE_ADDITIVE_SCN_STATE_MAINMENU + 1;
        //public static int SCENE_ADDITIVE_SCN_STATE_MAIN_GP = SCENE_ADDITIVE_SCN_STATE_GAMEPLAY + 1;
        //public static int SCENE_ADDITIVE_COUNT = SCENE_ADDITIVE_SCN_STATE_MAIN_GP + 1;

        #endregion === Lib Attibute ===


        #region === ScenesState ===
        public enum ScenesState
        {
            NO_SCENE = 0,
            //lib
            ScnManagers,
            //lib
        }

        #endregion === ScenesState ===

        #region === ScenesAdditive ===

        public enum ScenesAdditive
        {
            //Scene State
            SS_MainMenu,
            //SS_MultiPlayer,
            SS_MAIN_GP,
            COUNT,
        }

        #endregion === ScenesAdditive ===

    }
}