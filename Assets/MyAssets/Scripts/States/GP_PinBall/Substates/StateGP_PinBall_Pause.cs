using System;
using UnityEngine;
using Com.GNL.URP_MyLib;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

namespace Com.GNL.URP_MyLibProjectTest
{
    [Serializable]
    public class StateGP_PinBall_Pause : BaseSubstate, IConnectionCallbacks
    {

        //[Header("Canvas Screen")]
        //[MyBox.ReadOnly] public GameObject CanvasScreen;

        //[Header("Listed UI")]
        //[MyBox.ReadOnly] [SerializeField] private GameObject ListUIAndroid;
        //[MyBox.ReadOnly] [SerializeField] private GameObject ListUIWindows;
        //[MyBox.ReadOnly] [SerializeField] private GameObject ListUIAdditional;

        private PhotonController photonController;
        private bool _isLeavingRoom;
        public StateGP_PinBall_Pause(BaseState classOfMainState, string subStateName)
        {
            SerializeState(classOfMainState, subStateName);
            PhotonNetwork.AddCallbackTarget(this);
        }
        public override bool MySttFind()
        {
            photonController = LibFormulation.FindObjectByTagThenName(LibUtilities.TAG.CONTROLLER.ToString(), Utilities.FIND_GO.PhotonController.ToString()).GetComponent<PhotonController>();

                if (photonController
                    )
                    return true;

            return false;
        }

        //when you nee to do someting when state not active, but still same scene, you can do in this something in this func
        //mostly to do with sub state, it called in FindALL with chaker iSenable for forst time(set on inspector State Controller)
        public override void MySttDisable()
        {
            

        }
        // when in one any many state butthe state curent not active, and you one to do something when state enable, put on this function
        // one time after use this state
        public override void MySttEnable(bool isFindALl)
        {
            
        }

        public override void MySttExit() 
        {
            if (Formulation.GetInstansLibGameController().IsMultiPlayer)
            {
                Debug.Log("cekcekcek MySttExit exit multiplayer mode");
            }
            else
            {
                Debug.Log("cekcekcek MySttExit exit singlePlayer mode");
            }
        }

        // when already change to the scene that any this class, there is time to do something just for that time
        // one time after load the scene
        public override void MySttStart()
        {
            _isLeavingRoom = false;
        }

        public override void MySttUpdate()
        {
            DoResume();
            DoBacktoMainMenu();
            DoQuitGame();
        }

        private void DoQuitGame()
        {
            if (VirtualInputManager.Instance.InputAttr.QuitGame)
            {
//                Debug.Log("cekcekcek DoQuitGame btn");
//                SerializeDisable();
//                Formulation.GetInstansLibGameController().UnPause();
//                VirtualInputManager.Instance.InputAttr.NormalizeInput();
//                ClassOfMainState.SerializeEnable();
//#if UNITY_EDITOR
//                UnityEditor.EditorApplication.isPlaying = false;
//#else
//                Application.Quit();
//#endif
                Formulation.QuitGameFromSubState(this);

            }

        }

        private void DoBacktoMainMenu()
        {
            if (VirtualInputManager.Instance.InputAttr.BackToMainmenu)
            {
                Debug.Log("cekcekcek DoBacktoMainMenu btn");
                SerializeDisable();
                Formulation.GetInstansLibGameController().UnPause();
                VirtualInputManager.Instance.InputAttr.NormalizeInput();
                ClassOfMainState.SerializeEnable();

                if(Formulation.GetMultiPlayer())
                {
                    _isLeavingRoom =true;
                    PhotonClearLeave();
                    PhotonNetwork.LeaveRoom();

                }
                else 
                {
                    Debug.Log("cekcekcek DoBacktoMainMenu btn");
                    Formulation.GetInstansLibSceneController().SetChangeScene(((StateGP_PinBall)GetMainState()).Scenes[0]);
                }

            }
        }

        private void DoResume()
        {
            if (VirtualInputManager.Instance.InputAttr.Resume)
            {
                Debug.Log("cekcekcek DoResume btn");
                SerializeDisable();
                Formulation.GetInstansLibGameController().UnPause();
                VirtualInputManager.Instance.InputAttr.NormalizeInput();
                ClassOfMainState.SerializeEnable();
            }
        }

        public void OnConnected()
        {
            
        }

        public void OnConnectedToMaster()
        {

        }
        public void OnDisconnected(DisconnectCause cause)
        {
            if (_isLeavingRoom)
            {
                _isLeavingRoom = false;
                Formulation.GetInstansLibSceneController().SetChangeScene(((StateGP_PinBall)GetMainState()).Scenes[0]);
            }
        }

        public void OnRegionListReceived(RegionHandler regionHandler)
        {
            
        }

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {
            
        }

        public void OnCustomAuthenticationFailed(string debugMessage)
        {
            
        }

        private void PhotonClearLeave()
        {

            PhotonNetwork.RemoveBufferedRPCs();
            PhotonNetwork.OpRemoveCompleteCache();
        }

    }

}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  