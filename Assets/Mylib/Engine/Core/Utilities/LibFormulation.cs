
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Com.GNL.URP_MyLib
{
    public static class LibFormulation
    {
        public static void UIAdditionalActive(ref GameObject ListUIAdditional, bool flag)
        {
            if (LibGameSetting.IsUsingUnityDebug)
            {
                if (LibGameSetting.IsUsingUnityDebugToPlatform)
                {
                    ListUIAdditional.SetActive(flag);
                }
                else
                {
#if UNITY_EDITOR
                    ListUIAdditional.SetActive(flag);
#else
                    ListUIAdditional.SetActive(false);
#endif
                }
            }
            else
            {
                ListUIAdditional.SetActive(false);
            }
        }

        public static void CanvasInitialitation(GameObject Substate, ref GameObject CanvasScreen, ref GameObject ListUiUsed, ref GameObject ListUIWindows, ref GameObject ListUIAndroid, ref GameObject ListUIAdditional, bool UIUsedActive)
        {
            CanvasScreen.transform.position = new Vector3(0f, 0f, 0f);
            CanvasScreen.transform.localScale = new Vector3(1f, 1f, 1f);

            LibMasterCanvasController cc = Substate.GetComponent<LibMasterCanvasController>();
            ListUIWindows = cc.ListUIWindows;
            ListUIAndroid = cc.ListUIAndroid;
            ListUIAdditional = cc.ListUIAdditional;

            CanvasInitialitationProcess(ref ListUiUsed, ref ListUIWindows, ref ListUIAndroid, ref ListUIAdditional, UIUsedActive);
        }

        public static void CanvasInitialitation(ref GameObject CanvasScreen, ref GameObject ListUiUsed, ref GameObject ListUIWindows, ref GameObject ListUIAndroid, ref GameObject ListUIAdditional, bool UIUsedActive)
        {
            CanvasScreen.transform.position = new Vector3(0f, 0f, 0f);
            CanvasScreen.transform.localScale = new Vector3(1f, 1f, 1f);


            ListUIWindows = LibFormulation.GetChildWithName(CanvasScreen.transform, LibUtilities.FIND_GO.UIWindows.ToString()).gameObject;
            ListUIAndroid = LibFormulation.GetChildWithName(CanvasScreen.transform, LibUtilities.FIND_GO.UIAndroid.ToString()).gameObject;
            ListUIAdditional = LibFormulation.GetChildWithName(CanvasScreen.transform, LibUtilities.FIND_GO.UIAdditional.ToString()).gameObject;

            CanvasInitialitationProcess(ref ListUiUsed, ref ListUIWindows, ref ListUIAndroid, ref ListUIAdditional, UIUsedActive);
        }

        public static void CanvasInitialitationProcess(ref GameObject ListUiUsed, ref GameObject ListUIWindows, ref GameObject ListUIAndroid, ref GameObject ListUIAdditional, bool UIUsedActive)
        {
            if (LibGameSetting.IsPlatformWindows
#if UNITY_EDITOR
                        && !LibGameSetting.IsUnityPlayerUseAndroidUI
#endif
                )
            {
                ListUiUsed = ListUIWindows;
                foreach (Transform child in ListUIAndroid.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
            else if (LibGameSetting.IsPlatformAndroid
#if UNITY_EDITOR
                        || LibGameSetting.IsUnityPlayerUseAndroidUI
#endif
                        )
            {
                ListUiUsed = ListUIAndroid;
                foreach (Transform child in ListUIWindows.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
            else
            {
                ListUiUsed = ListUIWindows;
                foreach (Transform child in ListUIAndroid.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }

            ListUIAndroid.SetActive(false);
            ListUIWindows.SetActive(false);
            ListUIAdditional.SetActive(false);
            ListUiUsed.SetActive(UIUsedActive);
        }

        public static GameObject FindObjectByTagThenName(string tag, string goName)
        {
            GameObject[] valGO = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject go in valGO)
            {
                if (go.name == goName)
                {
                    return go;
                }
            }
            //Debug.LogWarning("cekcek FindObjectByTagThenName can not find object. ob name to find="+ goName);
            return null;
        }

        // disini digunakan untuk declare method yang digunakan secara umum
        public static Transform GetChildWithName(Transform parent, string child)
        {
            foreach (Transform eachChild in parent)
            {
                //Debug.Log("cekcek weapon :" + eachChild.name);
                if (eachChild.name == child) // WeaponUtilities.WeaponAttach.HAND_RIGHT.ToString()
                {
                    return eachChild;
                }
                Transform getTransform = GetChildWithName(eachChild, child);
                if (getTransform != null)
                {
                    return getTransform;
                }
            }

            return null;
        }

        public static void AwakeSingletonObj(GameObject gameObject)
        {
            
            GameObject go = GameObject.Find(LibUtilities.MANAGER);
            if (go == null)
            {
                go = new GameObject();
                go.name = LibUtilities.MANAGER;
            }
            Object.DontDestroyOnLoad(gameObject);
            gameObject.transform.SetParent(go.transform);
        }

        public static void GetChildWithNameList(out List<Transform> listTrsm, Transform parent, string[] child)
        {
            GetChildWithNameList(out listTrsm, parent, child, false);
        }

        public static void GetChildWithNameList(out List<Transform> listTrsm, Transform parent, string[] child, bool isGetTheParentTransformToo)
        {
            listTrsm = new List<Transform>();
            if (isGetTheParentTransformToo)
            {
                listTrsm.Add(parent.GetComponent<Transform>());
            }
            GetChildWithNameListCounting(ref listTrsm, parent, child);
        }


        private static void GetChildWithNameListCounting(ref List<Transform> listTrsm, Transform parent, string[] child)
        {
            foreach (Transform eachChild in parent)
            {
                for (int counter = 0; counter < child.Length; counter++)
                {
                    //Debug.Log("cekcek on search child :" + child[counter]);
                    if (eachChild.name == child[counter])
                    {
                        //Debug.Log("cekcek find child :" + eachChild.name);
                        counter++;
                        listTrsm.Add(eachChild.GetComponent<Transform>());
                        if (listTrsm.Count == child.Length)
                            break;
                    }
                }
                GetChildWithNameListCounting(ref listTrsm, eachChild, child);
                if (listTrsm.Count == child.Length)
                    break;
            }
        }

        public static void ClearLog() //you can copy/paste this code to the bottom of your script
        {
#if UNITY_EDITOR
            var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
#endif
        }

    }

}