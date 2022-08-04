using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Com.GNL.URP_MyLibProjectTest;
using Photon.Pun;

namespace Com.GNL.URP_MyLib
{
    public class LibScenesController : LibMasterScenesController, ILibController
    {


        //#region === Singleton_Lib ===
        //private static LibScenesController _instance;
        //public static LibScenesController InstanceLib
        //{
        //    get
        //    {

        //        if (_instance == null)
        //        {
        //            {
        //                //Scene activeScene = SceneManager.GetSceneAt(0);

        //                var instances = FindObjectsOfType<LibScenesController>();
        //                //GameObject obj = instance.;
        //                var count = instances.Length;
        //                if (count > 0)
        //                {
        //                    if (count == 1)
        //                    {
        //                        Debug.Log("cekcekcek Is InstanceLib LibScenesController T name :" + instances[0].name + " hanya 1");
        //                        return _instance = instances[0];
        //                    }
        //                    Debug.LogWarning($"[{nameof(LibScenesController)}] There should never be more than one {nameof(LibScenesController)} in the scene, but {count} were found. The first instance found will be used, and all others will be destroyed.");
        //                    for (var i = 1; i < instances.Length; i++)
        //                        Destroy(instances[i]);
        //                    Debug.Log("cekcekcek Is InstanceLib LibScenesController T name :" + instances[0].name + " tingal 1");
        //                    return _instance = instances[0];
        //                }

        //                Debug.Log($"[{nameof(LibScenesController)}] An instance is needed in the scene and no existing instances were found, so a new instance will be created.");
        //                return _instance = new GameObject($"({nameof(LibScenesController)})")
        //                           .AddComponent<LibScenesController>();
        //            }
        //        }

        //        return _instance;
        //    }
        //}
        //#endregion === Singleton_Lib ===


        #region === State Changing ===

        public override void StateChanging()
        {
            //this.enabled = false;
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == State MAINMENU ==
                case LibEdStateUtilities.GameStates.MAINMENU:
                    State_MainMenu_Changing();
                    break;
                #endregion
            }
            
        }

        public override void SubStateChanging()
        {
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == State MAINMENU ==
                case LibEdStateUtilities.GameStates.MAINMENU:
                    //SubStateMainMenuChanging();
                    StateFunc.SubState(SubState_MainMenu_Changing, true);

                    break;
                #endregion
            }
        }

        public override void CheckingAllfind()
        {
            StateFunc.SetFindAll(true);
        }


        private void State_Gameplay_Changing()
        {

        }

        private void State_MainMenu_Changing()
        {

        }

        private void SubState_MainMenu_Changing(LibEdStateUtilities.GameSubStates substate)
        {
            
        }

        private void SubState_Gameplay_Changing(LibEdStateUtilities.GameSubStates substate)
        {
            //switch (substate)
            //{
                
            //}
        }

        #endregion === State Changing ===

        #region === State Update ===


        public override void Update_State()
        {
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == State MAINMENU ==
                case LibEdStateUtilities.GameStates.MAINMENU:

                    break;
                #endregion
            }
        }



        #endregion === State Update ===
        public new void LoadScene(List<AsyncOperation> _SubScenesToLoad, LibEdSceneUtilities.ScenesAdditive scn)
        {
            if (Formulation.GetInstansLibGameController().IsUsePhotonPUN && Formulation.GetMultiPlayer() && PhotonNetwork.AutomaticallySyncScene)
            {
                Debug.Log("cekcekcek LoadScene with photon");
                //PhotonNetwork.LoadLevel(scn.ToString());
                _SubScenesToLoad.Add(SceneManager.LoadSceneAsync(scn.ToString(), LoadSceneMode.Additive));
            }
            else
            {
                Debug.Log("cekcekcek LoadScene Default");
                _SubScenesToLoad.Add(SceneManager.LoadSceneAsync(scn.ToString(), LoadSceneMode.Additive));
            }
        }

        public new void UnLoadScene(List<AsyncOperation> _SubScenesToLoad, LibEdSceneUtilities.ScenesAdditive scn)
        {
            //if (Formulation.GetInstansLibGameController().IsUsePhotonPUN && Formulation.GetMultiPlayer() && PhotonNetwork.AutomaticallySyncScene)
            //{

            //    PhotonNetwork.LoadLevel(scn.ToString());
            //}
            //else
            {
                _SubScenesToLoad.Add(SceneManager.UnloadSceneAsync(scn.ToString()));

            }

        }
        #region === State Called In Update ===

        #region === Change Scenes ===


        #endregion === Change Scenes ===

        #endregion === State Called In Update ===

    }
}