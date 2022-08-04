//using UnityEngine;

namespace Com.GNL.URP_MyLib
{
    public static class LibGameSetting
    {
        //GameSetting
        public static float Gravity = 0.3f;

        //Rendering Property
        public static float DepthOfField_Start = 20;
        public static float DepthOfField_End = 90;

        //game mode
        public static bool IsPause = false;

        //Unity and Platform
        public static bool IsUnityPlayerUseAndroidUI;
        public static bool IsUnityPlayerUseAndroidPreRender;

        //debug mode
        public static bool IsUsingUnityDebug;
        public static bool IsUsingUnityDebugToPlatform;
        public static bool IsAllPlatFormUseButton = true;

        public static bool IsForAndroid;
        public static bool IsForWindows;
        public static bool IsPlatformAndroid;
        public static bool IsPlatformWindows;

        public static bool IsJustTestInUnityEditor;


        public static bool debugScreen;
        public static float JS_UP;
        public static float JS_FW;
        public static float FDS;
        public static void SetJS_UP(float value)
        {
            if (debugScreen)
            {
                JS_UP = value;
            }
        }

        public static void SetJS_FW(float value)
        {
            if (debugScreen)
            {
                JS_FW = value;
            }
        }
        public static void SetFDS(float value)
        {
            if (debugScreen)
            {
                FDS = value;
            }
        }

    }
}