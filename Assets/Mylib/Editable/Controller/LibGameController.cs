using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

using Com.GNL.URP_MyLibProjectTest;

namespace Com.GNL.URP_MyLib
{
    public class LibGameController : LibMasterGameController, ILibController
    {
        //[HideInInspector] public int CurrentTab;
        //public string ToolbarTab;
        #region === Public Properety ===
        [Header("Setting PhotonPUN")]
        public bool IsUsePhotonPUN = false;
        public bool IsConnectToServerPhotonPUN = false;
        [Range(1,30)]
        public float TimeRepetedCheckingConnection = 5;

        [Header("Setting MultiPlayer")]
        [MyBox.ReadOnly] public bool IsMultiPlayer = false;

        [Header("Blur Test")]
        public bool IsUseBlurTest;
        public float BlurTestGaussianStart = 0;
        public float BlurTestGaussianEnd = 0;

        
        [Header("Reference Player")]
        [MyBox.ReadOnly] public GameObject _PC;



        [Header("TEST Instantiate")]
        public GameObject prefabAddBox;
        public GameObject prefabAddRobot;

        //public string TabEnd;
        #endregion === Public Properety ===

        #region === Renderer Global Volume Properety ===

        //private GameObject GO_GlobalVolume;

        private DepthOfField GV_DepthOfField;

        #endregion === Renderer  Global Volume Properety ===

        //#region === Singleton_Lib ===
        //private static LibGameController _instance;
        //public static LibGameController InstanceLib
        //{
        //    get
        //    {

        //        if (_instance == null)
        //        {
        //            {
        //                //Scene activeScene = SceneManager.GetSceneAt(0);

        //                var instances = FindObjectsOfType<LibGameController>();
        //                //GameObject obj = instance.;
        //                var count = instances.Length;
        //                if (count > 0)
        //                {
        //                    if (count == 1)
        //                    {
        //                        Debug.Log("cekcekcek Is InstanceLib LibCameraController T name :" + instances[0].name + " hanya 1");
        //                        return _instance = instances[0];
        //                    }
        //                    Debug.LogWarning($"[{nameof(LibGameController)}] There should never be more than one {nameof(LibGameController)} in the scene, but {count} were found. The first instance found will be used, and all others will be destroyed.");
        //                    for (var i = 1; i < instances.Length; i++)
        //                        Destroy(instances[i]);
        //                    Debug.Log("cekcekcek Is InstanceLib LibCameraController T name :" + instances[0].name + " tingal 1");
        //                    return _instance = instances[0];
        //                }

        //                Debug.Log($"[{nameof(LibGameController)}] An instance is needed in the scene and no existing instances were found, so a new instance will be created.");
        //                return _instance = new GameObject($"({nameof(LibGameController)})")
        //                           .AddComponent<LibGameController>();
        //            }
        //        }

        //        return _instance;
        //    }
        //}
        //#endregion === Singleton_Lib ===



        #region  === State Changing ===
        public override void StateChanging()
        {
            //this.enabled = false;
            //GameSettingUpdate();
            LibFindLibGlobalVolume();
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == State MAINMENU ==
                case LibEdStateUtilities.GameStates.MAINMENU:
                    break;
                #endregion
                #region == State GAMEPLAY ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    FindInitiateState_MAIN_GP();
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
                    StateFunc.SubState(SubStateMainMenuChanging, true);
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
            if (VirtualStateManager.Instance.CurState == LibEdStateUtilities.GameStates.MAIN_GP && (
                 LibGlobalSetting == null
                ))
            {
                StateFunc.SetFindAll(false);
                //StateFunc.ClearState();
            }
            else
            {
                StateFunc.SetFindAll(true);
            }
            
        }

        private void FindInitiateState_MAIN_GP()
        {
            _PC = GameObject.FindWithTag(Utilities.TAG.Player.ToString()).gameObject;
            LibGlobalSetting = LibFormulation.FindObjectByTagThenName(LibUtilities.TAG.CONTROLLER.ToString(), LibUtilities.FIND_GO.LibGlobalVolume.ToString());
            if (LibGlobalSetting)
            {
                //Debug.Log("cekcekck LibGlobalSetting ada");
                Volume volume = LibGO_GlobalVolume.GetComponent<Volume>();
                if (volume.profile.TryGet<DepthOfField>(out DepthOfField tempDof))
                {
                    GV_DepthOfField = tempDof;
                }
                LibGameSetting.DepthOfField_Start = GV_DepthOfField.gaussianStart.value;
                LibGameSetting.DepthOfField_End = GV_DepthOfField.gaussianEnd.value;

                BlurTestGaussianStart = LibGameSetting.DepthOfField_Start;
                BlurTestGaussianEnd = LibGameSetting.DepthOfField_End;
            }
            
        }

        private void SubState_MAIN_GP_Changing(LibEdStateUtilities.GameSubStates substate)
        {
            switch (substate)
            {
                #region == SubState SubState_MainMenu_Changing ==
                case LibEdStateUtilities.GameSubStates.MAIN_GP_GAMEPLAY_PAUSE:

                    break;
                #endregion
            }
        }

        private void SubStateMainMenuChanging(LibEdStateUtilities.GameSubStates substate)
        {
          
        }
        #endregion  === State Changing ===
        #region  === State Update ===
        public override void Update_State()
        {
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == State MAINMENU ==
                case LibEdStateUtilities.GameStates.MAINMENU:
                    StateFunc.SubState(SubStatee_MainMenu_Update);
                    break;
                #endregion
                #region == State MAIN_GP ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    TestADD();
                    TestBlur();
                    break;
                    #endregion
            }
        }

        private void TestBlur()
        {
            
        }

        private void SubStatee_MainMenu_Update(LibEdStateUtilities.GameSubStates curSubState)
        {
            
        }

        #endregion  === State Update ===

        #region  === State Called in Update ===


        private void TestADD()
        {
            if (VirtualInputManager.Instance.InputAttr.Addbox)
            { 
                Instantiate(prefabAddBox, new Vector3(_PC.transform.position.x, _PC.transform.position.y + 20f, _PC.transform.position.z), _PC.transform.rotation);
                Debug.Log("cekcekcek Addbox");
            }

            if (VirtualInputManager.Instance.InputAttr.AddRobot)
            {
                Instantiate(prefabAddRobot, new Vector3(_PC.transform.position.x, _PC.transform.position.y + 20f, _PC.transform.position.z), _PC.transform.rotation);
                Debug.Log("cekcekcek AddRobot");
            }
        }
        #endregion  === State Update ===

        #region  === Other Function ===

        public void GV_Blury(float start, float end)
        {
            try
            { 
                GV_DepthOfField.gaussianStart.value = start;
                GV_DepthOfField.gaussianEnd.value = end;
            }
            catch
            {
                Debug.Log("cekcek GV_DepthOfField = null/tidak aktif");
            }
        }

        public override void LibPauseEffect()
        {
            GV_Blury(0, 0);
        }

        public override void LibUnPauseEffect()
        {
            GV_Blury(LibGameSetting.DepthOfField_Start, LibGameSetting.DepthOfField_End);
        }

        #endregion  === State Update ===
    }
}