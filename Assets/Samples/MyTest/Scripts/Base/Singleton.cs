using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.GNLTest.Test1
{
    public class Singleton<T> : MonoBehaviourBase where T : MonoBehaviourBase
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                // no matter what this must be null, no need to find it, but just use if to make sure
                //_instance = (T)FindObjectOfType(typeof(T));
                
                if (_instance == null)
                {

                    //var objs = (T)FindObjectOfType(typeof(T)) as T[];
                    //if (objs.Length > 0)
                    //{
                    //    _instance = objs[0];
                    //}
                    //if (objs.Length > 1)
                    //{
                    //    Debug.LogError("There is more Than one" + typeof(T).Name +" in the scene");
                    //}
                    //Scene 


                    //if(_instance == null)
                    { 
                        GameObject obj = new GameObject();
                        _instance = obj.AddComponent<T>();
                        obj.name = typeof(T).Name;
                    }
                }

                return _instance;
            }
        }

        //private void OnDestroy()
        //{
        //    base.OnDestroy();
        //    if (_instance == this)
        //    {
        //        //Debug.Log("cekcek OnDestroy in singleton 1");
        //        _instance = null;
        //        //Debug.Log("cekcek OnDestroy in singleton 2");
        //    }
        //}
    }

    //public class SingletonTest<T> : MonoBehaviourMyBaseTest where T : MonoBehaviourMyBaseTest
    //{
    //    private static T _instance;

    //    public static T Instance
    //    {
    //        get
    //        {
    //            // no matter what this must be null, no need to find it, but just use if to make sure
    //            //_instance = (T)FindObjectOfType(typeof(T));

    //            if (_instance == null)
    //            {

    //                //var objs = (T)FindObjectOfType(typeof(T)) as T[];
    //                //if (objs.Length > 0)
    //                //{
    //                //    _instance = objs[0];
    //                //}
    //                //if (objs.Length > 1)
    //                //{
    //                //    Debug.LogError("There is more Than one" + typeof(T).Name + " in the scene");
    //                //}

    //                //if (_instance == null)
    //                {
    //                    GameObject obj = new GameObject();
    //                    _instance = obj.AddComponent<T>();
    //                    obj.name = typeof(T).Name;
    //                }
    //            }

    //            return _instance;
    //        }
    //    }

    //    //private void OnDestroy()
    //    //{
            
    //    //    Debug.Log("cekcek OnDestroy in singletonTest");
    //    //    //if (_instance == this)
    //    //    //{
    //    //    //    _instance = null;
    //    //    //}
    //    //}
    //}
}