using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.GNL.URP_MyLib
{
    public class LibSingletonController<T> : MonoBehaviourMyBase where T : MonoBehaviourMyBase
    {
        private static T _instance;

        public static T InstanceLibMaster
        {
            get
            {
                // no matter what this must be null, no need to find it, but just use if to make sure
                //_instance = (T)FindObjectOfType(typeof(T));
                
                if (_instance == null)
                {
                    {
                        //Scene activeScene = SceneManager.GetSceneAt(0);
                        var instances = FindObjectsOfType<T>();
                        //GameObject obj = instance.;
                        var count = instances.Length;
                        if (count > 0)
                        {
                            if (count == 1)
                            {
                                Debug.Log("cekcekcek Is InstanceLibMaster LibSingletonController T name :" + instances[0].name + " hanya 1");
                                return _instance = instances[0];
                            }
                            Debug.LogWarning($"[{nameof(LibSingletonController)}<{typeof(T)}>] There should never be more than one {nameof(LibSingletonController)} of type {typeof(T)} in the scene, but {count} were found. The first instance found will be used, and all others will be destroyed.");
                            for (var i = 1; i < instances.Length; i++)
                                Destroy(instances[i]);
                            Debug.Log("cekcekcek Is LibSingletonController T name :" + instances[0].name+ " tingal 1");
                            return _instance = instances[0];
                        }

                        Debug.Log($"[{nameof(LibSingletonController)}<{typeof(T)}>] An instance is needed in the scene and no existing instances were found, so a new instance will be created.");
                        return _instance = new GameObject($"({nameof(LibSingletonController)}){typeof(T)}")
                                   .AddComponent<T>();

                        //Debug.Log("cekcekcek Is LibSingletonController T name:" + obj.name);
                        //obj.name = typeof(T).Name;
                        //_instance = obj.AddComponent<T>();
                        //SceneManager.SetActiveScene(activeScene);
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

    public abstract class LibSingletonController : MonoBehaviour
    {
        #region  Properties
        public static bool Quitting { get; private set; }
        #endregion

        #region  Methods
        private void OnApplicationQuit()
        {
            Quitting = true;
        }
        #endregion
    }
}