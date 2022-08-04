using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.GNL.URP_MyLib
{
    public class LibSingletonManager<T> : MonoBehaviourMyBase where T : MonoBehaviourMyBase
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


                    

                    //if(_instance == null)
                    {
                        Scene activeScene = SceneManager.GetActiveScene();
                        SceneManager.SetActiveScene(SceneManager.GetSceneByName(LibUtilities.SCANE_MANAGER));
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        _instance = obj.AddComponent<T>();
                        SceneManager.SetActiveScene(activeScene);
                    }
                }

                return _instance;
            }
        }

        //public void OnDestroy()
        //{
        //    base.OnDestroy();
        //    if (_instance == this)
        //    {
        //        _instance = null;
        //    }
        //}
    }

    //public class SingletonPersistent<T> : MonoBehaviourMyBase where T : MonoBehaviourMyBase
    //{
    //    private static T _instance;

    //    public static T Instance
    //    {
    //        get
    //        {

    //            //if (_instance == null)
    //            //{

    //            //    Scene activeScene = SceneManager.GetActiveScene();
    //            //    SceneManager.SetActiveScene(SceneManager.GetSceneByName(Utilities.SCANE_MANAGER));
    //            //    GameObject obj = new GameObject();
    //            //    obj.name = typeof(T).Name;
    //            //    _instance = obj.AddComponent<T>();
    //            //    SceneManager.SetActiveScene(activeScene);
                    
    //            //}

    //            return _instance;
    //        }
    //    }

    //    private void OnDestroy()
    //    {
    //        base.OnDestroy();
    //        if (_instance == this)
    //        {
    //            _instance = null;
    //        }
    //    }


    //    public virtual void Awake()
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = this as T;
    //            DontDestroyOnLoad(gameObject);
    //        }
    //        else 
    //        {
    //            Destroy(this);
    //        }
    //    }
    //}

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
    //                //    Debug.LogError("There is more Than one" + typeof(T).Name +" in the scene");
    //                //}




    //                //if(_instance == null)
    //                {
    //                    Scene activeScene = SceneManager.GetActiveScene();
    //                    SceneManager.SetActiveScene(SceneManager.GetSceneByName(Utilities.SCANE_MANAGER));
    //                    GameObject obj = new GameObject();
    //                    obj.name = typeof(T).Name;
    //                    _instance = obj.AddComponent<T>();
    //                    SceneManager.SetActiveScene(activeScene);
    //                }
    //            }

    //            return _instance;
    //        }
    //    }

    //    public void OnDestroy()
    //    {
    //        base.OnDestroy();
    //        if (_instance == this)
    //        {
    //            Debug.Log("cekcek OnDestroy in singleton 1 :" + _instance.name);
    //            _instance = null;
    //            //Debug.Log("cekcek OnDestroy in singleton 2");
    //        }
    //    }
    //}
}