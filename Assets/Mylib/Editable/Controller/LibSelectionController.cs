using System;
using System.Collections.Generic;
using UnityEngine;

using Com.GNL.URP_MyLibProjectTest;

namespace Com.GNL.URP_MyLib
{
    public class LibSelectionController : LibMasterSelectionController, ILibController, ILibSelectionHandler
    {


        //#region === Singleton_Lib ===
        //private static LibSelectionController _instance;
        //public static LibSelectionController InstanceLib
        //{
        //    get
        //    {

        //        if (_instance == null)
        //        {
        //            {
        //                //Scene activeScene = SceneManager.GetSceneAt(0);

        //                var instances = FindObjectsOfType<LibSelectionController>();
        //                //GameObject obj = instance.;
        //                var count = instances.Length;
        //                if (count > 0)
        //                {
        //                    if (count == 1)
        //                    {
        //                        Debug.Log("cekcekcek Is InstanceLib LibSelectionController T name :" + instances[0].name + " hanya 1");
        //                        return _instance = instances[0];
        //                    }
        //                    Debug.LogWarning($"[{nameof(LibSelectionController)}] There should never be more than one {nameof(LibSelectionController)} in the scene, but {count} were found. The first instance found will be used, and all others will be destroyed.");
        //                    for (var i = 1; i < instances.Length; i++)
        //                        Destroy(instances[i]);
        //                    Debug.Log("cekcekcek Is InstanceLib LibSelectionController T name :" + instances[0].name + " tingal 1");
        //                    return _instance = instances[0];
        //                }

        //                Debug.Log($"[{nameof(LibSelectionController)}] An instance is needed in the scene and no existing instances were found, so a new instance will be created.");
        //                return _instance = new GameObject($"({nameof(LibSelectionController)})")
        //                           .AddComponent<LibSelectionController>();
        //            }
        //        }

        //        return _instance;
        //    }
        //}
        //#endregion === Singleton_Lib ===

        public override void StateChanging()
        {
            //this.enabled = false;
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == State MAINMENU ==
                case LibEdStateUtilities.GameStates.MAINMENU:

                    break;
                #endregion

                #region == State MAIN_GP ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    State_MAIN_GP_Changing();
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

                    StateFunc.SubState(SubState_MainMenu_Changing, true);

                    break;
                #endregion
                #region == State MAIN_GP ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    StateFunc.SubState(SubState_MAIN_GP_Changing, true);
                    break;
                    #endregion
            }

        }


        private void State_MAIN_GP_Changing()
        {
            State_MAIN_GP_Initiate();
        }
        private void State_MAIN_GP_Initiate()
        {

        }

        private void SubState_MAIN_GP_Changing(LibEdStateUtilities.GameSubStates substate)
        {


        }


        private void SubState_MainMenu_Changing(LibEdStateUtilities.GameSubStates substate)
        {
            
        }
        public override void CheckingAllfind()
        {
            StateFunc.SetFindAll(true);
        }

        public override void Update_State()
        {
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == Input State MAINMENU ==
                case LibEdStateUtilities.GameStates.MAINMENU:
                    break;
                #endregion

                #region == Input State MAIN_GP ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    LibAimTarget();
                    break;
                    #endregion
            }
        }

        
        public override void LibSetObjectSelection(RaycastHit hit)
        {
            if (hit.transform.gameObject.tag.Equals(Utilities.TAG.ENEMY_LEAD.ToString()))
            {
                VirtualSelectionObjectManager.Instance.AnyObject((byte)VirtualSelectionObjectManager.SelectedObjectName.ENEMY);
                
            }
        }


    }
}