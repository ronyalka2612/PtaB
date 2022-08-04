
using UnityEngine;
using UnityEditor;

namespace Com.GNL.URPProduction
{
    [InitializeOnLoadAttribute]
    public static class PlayModeStateChangedExample 
    {
        // register an event handler when the class is initialized
        static PlayModeStateChangedExample()
        {
            EditorApplication.playModeStateChanged += LogPlayModeState;
        }

        private static void LogPlayModeState(PlayModeStateChange state)
        {
            //Debug.Log("Cek LogPlayModeState " + state);
            //if (state == PlayModeStateChange.ExitingEditMode)
            //{
            //EditorApplication.isPlaying = false;
            //}
        }
    }
}