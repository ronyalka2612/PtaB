using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Com.GNL.URP_MyLib;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

namespace Com.GNL.URP_MyLibProjectTest
{
    [Serializable]
    public class StateMainmenu : BaseState
    {

        #region === Atrribute ===

        [Header("All Child")]
        public StateMainmenuMultiPlayer SubStt_MultiPlayer;
        public StateMainmenuOption SubStt_Option;
        [Space(10)]
        //[Header("Referance CTS")]
        [HideInInspector] public ConnectToServer CTS;

        [Header("Set Change State from here")]
        public LibMasterSceneConstruct[] Scenes;


        private GameObject LoadingLayer_main;


        #endregion === Atrribute ===

        #region === Base Func ===
        public override void Serialize(BaseState classOfMainState, string nameState)
        {
            base.Serialize(classOfMainState, nameState);
            SubStt_MultiPlayer = new StateMainmenuMultiPlayer(classOfMainState, LibEdStateUtilities.GameSubStates.MAINMENU_MULTI_PLAYER.ToString());
            SubStt_Option = new StateMainmenuOption(classOfMainState, LibEdStateUtilities.GameSubStates.MAINMENU_OPTION.ToString());

            CTS = new ConnectToServer(this);
        }

        public override bool MySttFind()
        {

            LoadingLayer_main = LibFormulation.FindObjectByTagThenName(LibUtilities.TAG.UI.ToString(), Utilities.FIND_GO.LoadingLayer_main.ToString());
            
            if (SubStt_Option.SerializeFind() && 
                SubStt_MultiPlayer.SerializeFind()
                )
            {
                Debug.Log("cekcekcek StateMainMenu Find Substate : Fine");
                if (LoadingLayer_main
                    )
                return true;
            }
            //LibStatesController.Instance.CheckingEnable(this);
            //LibStatesController.Instance.CheckingEnable(SubStt_Option);
            //LibStatesController.Instance.CheckingEnable(SubStt_MultiPlayer);

            return false;
        }


        //when you nee to do someting when state not active, but still same scene, you can do in this something in this func
        //mostly to do with sub state
        public override void MySttDisable() 
        {
            
        }
        // when in one any many state butthe state curent not active, and you one to do something when state enable, put on this function
        // one time after use this state
        public override void MySttEnable(bool isFindALl) 
        {
            //Debug.Log("cekcekcek StateMainMenu MySttEnable cekcekcek");
            if (isFindALl)
                LoadingLayer_main.SetActive(false);
        }

        public override void MySttExit() 
        {
            SubStt_MultiPlayer.SerializeExit();
            SubStt_Option.SerializeExit();

            PhotonNetwork.RemoveCallbackTarget(CTS);
            Debug.Log("cekcekcek StateMainMenu MySttExit cekcekcek");
        }

        // when already change to the scene that any this class, there is time to do something just for that time
        // one time after load the scene
        public override void MySttStart() 
        {
            //Debug.Log("cekcekcek StateMainMenu MySttStart cekcekcek");
            Formulation.SetMultiPlayer(false);
        }


        public override void MySttUpdate() 
        {
            if (!LoadingLayer_main.activeSelf)
            {
                InputState();
            }
            else if (LoadingLayer_main.activeSelf)
            {
                SelectBack();
                if (PhotonNetwork.InLobby)
                {
                    TryChangeToSubStateMultiPlayer();
                }
            }
        }
        #endregion === Base Func ===

        #region === Pivate Func ===


        private void InputState()
        {
            SelectStart();
            SelectMultiPlayer();
            SelectOption();
            SelectClose();
        }


        private void SelectStart()
        {
            if (VirtualInputManager.Instance.InputAttr.Start_Game)
            {
                SetIsUpdate(false);
                if (Formulation.GetInstansLibGameController().IsMultiPlayer)
                {
                    PhotonNetwork.OfflineMode = true;

                    Formulation.GetInstansLibGameController().IsMultiPlayer = false;
                }
                Formulation.GetInstansLibSceneController().SetChangeScene(Scenes[0]);
            }
        }
        private void SelectMultiPlayer()
        {
            if (VirtualInputManager.Instance.InputAttr.Start_MultiPlayer)
            {
                //if (!Formulation.CheckUsePhoton()) return;
                CTS.Start();
                LoadingLayer_main.SetActive(true);
                Debug.Log("cekcekcek SelectMultiPlayer");
            }
        }


        private void SelectOption()
        {
            if (VirtualInputManager.Instance.InputAttr.Start_Option)
            {
                SubStt_Option.SerializeEnable();
                SerializeDisable();
                VirtualInputManager.Instance.InputAttr.NormalizeInput();
                Debug.Log("cekcekcek SelectOption");
            }
        }

        private void SelectClose()
        {
            if (VirtualInputManager.Instance.InputAttr.QuitGame)
            {
                Formulation.QuitGameFromState(this);
            }
        }

        private void SelectBack()
        {
            if (VirtualInputManager.Instance.InputAttr.Back)
            {
                LoadingLayer_main.SetActive(false);
            }
        }

        #endregion === Pivate Func ===
        #region === Public Func ===

        public void TryChangeToSubStateMultiPlayer()
        {
            SubStt_MultiPlayer.SerializeEnable();
            SetIsUpdate(false);
            SerializeDisable();
            VirtualInputManager.Instance.InputAttr.NormalizeInput();
        }

        #endregion === Public Func ===



    }
    [Serializable]
    public class ConnectToServer : IConnectionCallbacks, IMatchmakingCallbacks, IInRoomCallbacks, ILobbyCallbacks, IWebRpcCallback, IErrorInfoCallback
    {
        private StateMainmenu _stateMM;

        #region === IConnectionCallbacks ===
        public ConnectToServer(StateMainmenu stateMainmenu)
        {
            _stateMM = stateMainmenu;
        }
        private bool isAlreadySetting = false;
        public void Start()
        {
            if (Formulation.CheckUsePhoton())
            {
                PhotonNetwork.OfflineMode = false;
                PhotonNetwork.AddCallbackTarget(this);
                if (!isAlreadySetting)
                {
                    isAlreadySetting = true;
                    PhotonNetwork.ConnectUsingSettings();
                    PhotonNetwork.GameVersion = Application.version;

                    Debug.Log("cekcek ConnectToServer Start, App Ver:" + Application.version);
                }
                else
                {
                    PhotonNetwork.Reconnect();
                }
                
            }


        }
        public void OnConnectedToMaster()
        {
            if (PhotonNetwork.IsConnected)
            {
                Formulation.SetConnectToServerPhotonPUN(PhotonNetwork.IsConnected);
                if (!PhotonNetwork.InLobby)
                {
                    PhotonNetwork.JoinLobby();
                }
                else 
                {
                    PhotonNetwork.LeaveLobby(); 
                    PhotonNetwork.JoinLobby();
                }
                Debug.Log("cekcek ConnectToServer OnConnectedToMaster");
                //PhotonNetwork.AutomaticallySyncScene = true;
            }
            else
            {
                Debug.Log("cekcek ConnectToServer OnConnectedToMaster but not connected");
            }
        }

        public void OnDisconnected(DisconnectCause cause)
        {
            InteruptedWhenInLoby();
            
            //switch (cause)
            //{ 
            //    case DisconnectCause.
            //}
            Debug.Log("cekcek ConnectToServer OnDisconnected, cause : "+ cause);
        }

        public void OnRegionListReceived(RegionHandler regionHandler)
        {
            Debug.Log("cekcek ConnectToServer OnRegionListReceived regionHandler HostAndPort : " + regionHandler.BestRegion.HostAndPort);
        }

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {
            Debug.Log("cekcek ConnectToServer OnCustomAuthenticationResponse");
        }

        public void OnCustomAuthenticationFailed(string debugMessage)
        {
            Debug.Log("cekcek ConnectToServer OnCustomAuthenticationFailed debugMessage:" + debugMessage);
        }

        public void OnConnected()
        {
            Debug.Log("cekcek ConnectToServer OnConnected ");
        }

        #endregion === IConnectionCallbacks ===
        #region === ILobbyCallbacks ===

        public void OnJoinedLobby()
        {
           
            _stateMM.TryChangeToSubStateMultiPlayer();
            Debug.Log("cekcek ConnectToServer OnJoinedLobby");
        }
        public void OnLeftLobby()
        {
            Debug.Log("cekcek ConnectToServer OnLeftLobby ");
        }

        public void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            Debug.Log("cekcek ConnectToServer OnRoomListUpdate ");
        }

        public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
        {
            Debug.Log("cekcek ConnectToServer OnLobbyStatisticsUpdate ");
        }


        #endregion === ILobbyCallbacks ===

        #region === IInRoomCallbacks ===


        public void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log("cekcek ConnectToServer OnPlayerEnteredRoom newPlayer : " + newPlayer);
        }

        public void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log("cekcek ConnectToServer OnPlayerLeftRoom otherPlayer : " + otherPlayer);
            PhotonNetwork.RemoveRPCs(otherPlayer);
        }

        public void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
        {
            Debug.Log("cekcek ConnectToServer OnRoomPropertiesUpdate, Photon.Hashtable : "+ propertiesThatChanged);
        }

        public void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
        {
            Debug.Log("cekcek ConnectToServer OnPlayerPropertiesUpdate, targetPlayer : " + targetPlayer+ ", Photon.Hashtable : " + changedProps);
        }

        public void OnMasterClientSwitched(Player newMasterClient)
        {
            Debug.Log("cekcek ConnectToServer OnMasterClientSwitched, newMasterClient :"+ newMasterClient);
        }

        #endregion === IInRoomCallbacks ===

        #region === IMatchmakingCallbacks ===

        public void OnFriendListUpdate(List<FriendInfo> friendList)
        {
            Debug.Log("cekcek ConnectToServer OnFriendListUpdate");
        }

        public void OnCreatedRoom()
        {
            PhotonNetwork.CurrentRoom.PlayerTtl = 4;
            Debug.Log("cekcek ConnectToServer OnCreatedRoom");
        }

        public void OnCreateRoomFailed(short returnCode, string message)
        {
            InteruptedWhenInLoby();
            Debug.Log("cekcek ConnectToServer OnCreateRoomFailed, returnCode : " + returnCode + ", message : " + message);
        }

        public void OnJoinedRoom()
        {
            //if(!PhotonNetwork.InRoom)
            _stateMM.SubStt_MultiPlayer.ToGamePlay();
            Debug.Log("cekcek ConnectToServer OnJoinedRoom");
        }

        public void OnJoinRoomFailed(short returnCode, string message)
        {
            FailWhenJoining();
            Debug.Log("cekcek ConnectToServer OnJoinRoomFailed, returnCode : " + returnCode + ", message : " + message);
        }

        public void OnJoinRandomFailed(short returnCode, string message)
        {
            FailWhenJoining();
            Debug.Log("cekcek ConnectToServer OnJoinRandomFailed, returnCode : "+ returnCode+ ", message : " + message);
        }

        public void OnLeftRoom()
        {
            InteruptedWhenInLoby();
            Debug.Log("cekcek ConnectToServer OnLeftRoom");
        }

        #endregion === IMatchmakingCallbacks ===

        #region === IErrorInfoCallback ===



        public void OnErrorInfo(ErrorInfo errorInfo)
        {
            Debug.Log("cekcek ConnectToServer OnErrorInfo, errorInfo : "+ errorInfo);
        }


        #endregion === IErrorInfoCallback ===


        #region === IWebRpcCallback ===

        public void OnWebRpcResponse(OperationResponse response)
        {
            Debug.Log("cekcek ConnectToServer OperationResponse, response:"+ response);
        }

        #endregion === IWebRpcCallback ===


        #region === Other Func ===
        private void FailWhenJoining()
        {
            NormalizeConntection();
            if (!_stateMM.SubStt_MultiPlayer.IsUpdate)
            {
                if (!_stateMM.IsUpdate)
                {
                    //on the update in SubStt_MultiPlayer
                    _stateMM.SubStt_MultiPlayer.SetActiveLoadingLayer(false);
                    _stateMM.SubStt_MultiPlayer.SetIsUpdate(true);

                }
                else if (!_stateMM.SubStt_Option.IsUpdate)
                {
                    //pindah ke mainmenu
                    _stateMM.SubStt_MultiPlayer.GoBack();
                }
            }
        }


        private void NormalizeConntection()
        {
            if(PhotonNetwork.InRoom)
            { 
                PhotonNetwork.RemoveBufferedRPCs();
                PhotonNetwork.OpRemoveCompleteCache();
            }
            PhotonNetwork.Disconnect();
        }
        private void InteruptedWhenInLoby()
        {
            NormalizeConntection();
            if (!_stateMM.SubStt_MultiPlayer.IsUpdate)
            {
                if (!_stateMM.IsUpdate)
                {
                    //on the update in SubStt_MultiPlayer
                    _stateMM.SubStt_MultiPlayer.SetActiveLoadingLayer(false);
                    _stateMM.SubStt_MultiPlayer.SetIsUpdate(true);
                }
                else 
                {
                    _stateMM.SubStt_MultiPlayer.GoBack();
                }
                //else if (!_stateMM.SubStt_Option.IsUpdate)
                //{
                //    //pindah ke mainmenu
                //    _stateMM.SubStt_MultiPlayer.GoBack();
                //}
            }
        }

        #endregion === Other Func ===
    }

}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  