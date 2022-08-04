using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine;



namespace Com.GNL.URP_MyLib
{
    public static class LibUtilities
    {

        //public static int NoState = -1;

        public static string MANAGER = "Manager";
        public static string SCANE_MANAGER = "ScnManagers";
        public static string SCANE_STARTER = "ScnStarter";

        #region === Animation Change Scene parameter ===
        //Animation Change Scene parameter
        public static string SPEED_MULTIPLIER = "SpeedMultiplier";
        public enum LOADING_TRIGGER_CONTENT
        {
            ANIM_START,
            ANIM_END
        }
        public enum LOADING_TRIGGER_TRANSITION
        {
            TST_START,
            TST_END
        }
        #endregion === Animation Change Scene parameter ===

        // when is Not All Capital, it caouse just one object in the scene
        // when is ALL Capital, it caouse many object in the scene that use this tag
        public enum TAG // free to place
        {
            
            UI,
            JOYPAD,
            CONTROLLER,
            SUBSTATE,
            COUNT
        }

        public enum FIND_GO // free to place
        {
            CanvasScreen,         
            UIAndroid,
            UIWindows,
            UIAdditional,
            JoyPad,
            LibGlobalVolume,
            COUNT
        }

        public enum LOADING_TYPE
        {
            CHANGE_SCENE,
            CHANGE_SUBSTATE,
            ADD_SUBSTATE,
            COUNT,
        }
        
        //Controller List, harus sama urutanya dengan yang ad di scene GO controller
        public enum GO_CONTROLLER
        {
            LibInputController,
            LibSelectionController,
            LibGameController,
            LibCameraController,
            LibStatesController,
            COUNT,
        }

        public enum GO_CONTROLLER_ALL
        {
            LibInputController,
            LibSelectionController,
            LibGameController,
            LibCameraController,
            LibStatesController,

            LibCanvasController,
            LibGlobalController,
            LibScenesController,
            LibTransitionController,

            COUNT,
        }

    }
}