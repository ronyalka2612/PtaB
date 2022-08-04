
using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.GNL.URP_MyLib
{
    [InitializeOnLoad]
    public static class EditorSceneMemoryManager
    {
        static EditorSceneMemoryManager()
        {
            EditorSceneManager.sceneOpened += OnSceneOpened;
        }

        static void OnSceneOpened(Scene scene, OpenSceneMode mode)
        {
            GarbageCollect();
        }

        [MenuItem("Tools/Force Garbage Collection")]
        static void GarbageCollect()
        {
            Debug.Log("cek scene opened: "+ (int) EditorSceneManager.loadedSceneCount);
            EditorUtility.UnloadUnusedAssetsImmediate();
            GC.Collect();
        }
    }
}