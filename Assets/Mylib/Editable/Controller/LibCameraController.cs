
using UnityEngine;
using Cinemachine;

using Com.GNL.URP_MyLibProjectTest;

namespace Com.GNL.URP_MyLib
{
    public class LibCameraController : LibMasterCameraController, ILibController
    {
        [Header("Camera Referance")]
        [SerializeField] 
        [LibReadOnly]
        private CinemachineFreeLook _Cam;

        //#region === Singleton_Lib ===


        //private static LibCameraController _instance;
        //public static LibCameraController InstanceLib
        //{
        //    get
        //    {

        //        if (_instance == null)
        //        {
        //            {
        //                //Scene activeScene = SceneManager.GetSceneAt(0);
        //                var instances = FindObjectsOfType<LibCameraController>();
        //                //GameObject obj = instance.;
        //                var count = instances.Length;
        //                if (count > 0)
        //                {
        //                    if (count == 1)
        //                    {
        //                        Debug.Log("cekcekcek Is InstanceLib LibCameraController T name :" + instances[0].name + " hanya 1");
        //                        return _instance = instances[0];
        //                    }
        //                    Debug.LogWarning($"[{nameof(LibCameraController)}] There should never be more than one {nameof(LibCameraController)} in the scene, but {count} were found. The first instance found will be used, and all others will be destroyed.");
        //                    for (var i = 1; i < instances.Length; i++)
        //                        Destroy(instances[i]);
        //                    Debug.Log("cekcekcek Is LibSingletonController T name :" + instances[0].name + " tingal 1");
        //                    return _instance = instances[0];
        //                }

        //                Debug.Log($"[{nameof(LibCameraController)}] An instance is needed in the scene and no existing instances were found, so a new instance will be created.");
        //                return _instance = new GameObject($"({nameof(LibCameraController)})")
        //                           .AddComponent<LibCameraController>();
        //            }
        //        }

        //        return _instance;
        //    }
        //}
        //#endregion === Singleton_Lib ===
        #region === State Changing ===
        private void Start()
        { 
            //if(_Cam == null)
            //    _Cam = Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineFreeLook>();
            ////if (LibGameSetting.IsPlatformAndroid)
            //Debug.Log("cekcekcek _Cam:" + _Cam != null ? 1 : 0);
        }

        // StateChanging dan Substate Chaning haurs dipisah, karna substae bisa berubah walaupun state tidak berubah, untuk updatenya bisa sama
        public override void StateChanging() 
        {
            //this.enabled = false;
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == State MAINMENU ==
                case LibEdStateUtilities.GameStates.MAINMENU:
                    //dont do chaning sub in here, couse maybe when substate change but the Stae not, it will not come through here, do in SubStateChanging

                    break;
                #endregion
                #region == State MAIN_GP ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    Initiate_MAIN_GP_Changing();
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

        public override void CheckingAllfind()
        {
            if (VirtualStateManager.Instance.CurState == LibEdStateUtilities.GameStates.MAIN_GP  )
            {
                if (_Cam != null)
                {

                    StateFunc.SetFindAll(true);
                }
                else
                { 
                    //StateFunc.ClearState();
                    StateFunc.SetFindAll(false);
                }
                //if (LibGameSetting.IsPlatformAndroid)
                //Debug.Log("cekcekcek all find _Cam:"+_Cam != null ? 1 : 0);
            }
            else
            {
                StateFunc.SetFindAll(true);
            }
        }

        private void SubState_MainMenu_Changing(LibEdStateUtilities.GameSubStates substate)
        {
            
        }
        private void SubState_MAIN_GP_Changing(LibEdStateUtilities.GameSubStates substate)
        {
            
        }

        private void Initiate_MAIN_GP_Changing()
        {
            _Cam = Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineFreeLook>();
            //_Cam = LibFormulation.FindObjectByTagThenName(
            //    Utilities.TAG.CONTROLLER.ToString(),
            //    Utilities.FIND_GO.CMFreeLook1.ToString()
            //    )
            //    .GetComponent<CinemachineFreeLook>();
        }


        #endregion === State Changing ===

        #region === Stete Update ===

        public override void Update_State()
        {
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == Input State MAINMENU ==
                case LibEdStateUtilities.GameStates.MAINMENU:
                    State_MainMenu_Update();
                    StateFunc.SubState(SubState_MainMenu_Update);
                    break;
                #endregion

                #region == Input State MAIN_GP ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    State_MAIN_GP_Update();
                    break;
                    #endregion
            }
        }

        public override void Update_StateFU()
        {
            
        }
        public override void Update_StateLU()
        {
            
        }
        //void Update()
        //{


        //    if (StateFunc.StateChanging(StateChanging, SubStateChanging, CheckingAllfind))
        //    {
        //        GetAnySelectionData();
        //        switch (VirtualStateManager.Instance.CurState)
        //        {
        //            #region == Input State MAINMENU ==
        //            case LibEdStateUtilities.GameStates.MAINMENU:
        //                State_MainMenu_Update();
        //                StateFunc.SubState(SubState_MainMenu_Update);
        //                break;
        //            #endregion

        //            #region == Input State MAIN_GP ==
        //            case LibEdStateUtilities.GameStates.MAIN_GP:
        //                State_MAIN_GP_Update();
        //                break;
        //                #endregion
        //        }
        //    }
        //}

        private void State_MainMenu_Update()
        {
            
        }

        private void State_MAIN_GP_Update()
        {
            ControlCam();
        }

        private void SubState_MainMenu_Update(LibEdStateUtilities.GameSubStates curSubState)
        {
        }
        #endregion === Stete Update ===

        #region === Stete Called IN Update ===


        private void ControlCam()
        {
            if (LibControlCam(new Vector2(_Cam.m_XAxis.Value, _Cam.m_YAxis.Value)))
            {
                
                var temp = GetPosCamMove();
                //if (LibGameSetting.IsPlatformWindows)
                //    Debug.Log("cekcekcek Windows temp.x:" + temp.x + ", temp.y:" + temp.y);
                //if (LibGameSetting.IsPlatformAndroid)
                //    Debug.Log("cekcekcek android temp.x:"+ temp.x + ", temp.y:"+ temp.y);
                _Cam.m_XAxis.Value = temp.x;
                _Cam.m_YAxis.Value = temp.y;
            }
        }




        #endregion === Stete Called IN Update ===
        #region === Func in this class ===
        public void LibGetAnySelectionData()
        {
            if (VirtualInputManager.Instance.InputAttr.AimTarget)
            {
                if (VirtualSelectionObjectManager.Instance.AnyObject((byte)VirtualSelectionObjectManager.SelectedObjectName.ENEMY))
                {
                   
                }
            }
        }


        #endregion === Func in this class ===
    }
}
