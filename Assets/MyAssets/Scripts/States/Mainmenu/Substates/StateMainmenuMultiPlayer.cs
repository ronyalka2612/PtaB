using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Com.GNL.URP_MyLib;
using Photon.Pun;
using Photon.Realtime;

namespace Com.GNL.URP_MyLibProjectTest
{

    [Serializable]
    public class StateMainmenuMultiPlayer : BaseSubstate
    {
        #region === Atrribute ===

        //[Header("Referance CreateAndJoinRoom")]
        //[MyBox.ReadOnly] public CreateAndJoinRoom _createAndJoinRoom;

        private GameObject LoadingLayer;
        private TMP_InputField InpFld_CreateRoom;
        private TMP_InputField InpFld_JoinRoom;

        private float _curTimeChekingCon=0;


        #endregion === Atrribute ===

        public void SetActiveLoadingLayer(bool flag)
        {
            if(LoadingLayer != null)
                LoadingLayer.SetActive(flag);
        }

        public bool GetActiveLoadingLayer()
        {
            if (LoadingLayer != null)
                return LoadingLayer.activeSelf;
            return false;
        }

        #region === Base Func ===

        public StateMainmenuMultiPlayer(BaseState classOfMainState, string subStateName)
        {
            SerializeState(classOfMainState, subStateName);
            //_createAndJoinRoom = new CreateAndJoinRoom(this);
        }

        public override bool MySttFind()
        {
            LoadingLayer = LibFormulation.FindObjectByTagThenName(LibUtilities.TAG.UI.ToString(), Utilities.FIND_GO.LoadingLayer.ToString());
            InpFld_CreateRoom = LibFormulation.FindObjectByTagThenName(LibUtilities.TAG.UI.ToString(), Utilities.FIND_GO.IF_CreateRoom.ToString()).GetComponent<TMP_InputField>();
            InpFld_JoinRoom = LibFormulation.FindObjectByTagThenName(LibUtilities.TAG.UI.ToString(), Utilities.FIND_GO.IF_JoinRoom.ToString()).GetComponent<TMP_InputField>();
            //Debug.Log("cekcekcek LoadingLayer :" + LoadingLayer +
            //        ", InpFld_CreateRoom:" + (InpFld_CreateRoom == null) +
            //        ", InpFld_JoinRoom:" + (InpFld_JoinRoom == null) 
            //        );
            if (LoadingLayer && 
                InpFld_CreateRoom && 
                InpFld_JoinRoom
                )
            {
                return true;
            }
            return false;
        }

        //when you nee to do someting when state not active, but still same scene, you can do in this something in this func
        //mostly to do with sub state
        public override void MySttDisable()
        {
            if(PhotonNetwork.IsConnected)
            {
                PhotonNetwork.LeaveLobby();
                PhotonNetwork.Disconnect();
               
            }
            Formulation.SetConnectToServerPhotonPUN(false);
            LoadingLayer.SetActive(true);

        }
        // when in one any many state butthe state curent not active, and you one to do something when state enable, put on this function
        // one time after use this state
        public override void MySttEnable(bool isFindAll)
        {
            if(isFindAll)
            {
                LoadingLayer.SetActive(true);
                 
            }
        }

        public override void MySttExit() { }

        // when already change to the scene that any this class, there is time to do something just for that time
        // one time after load the scene
        public override void MySttStart()
        {
            //Debug.Log("cekcekcek StateMainmenuOption MySttUpdate");

        }

        public override void MySttUpdate()
        {
            if (!LoadingLayer.activeSelf)
            {
                if (PhotonNetwork.IsConnected 
                    && PhotonNetwork.InLobby 
                    && PhotonNetwork.NetworkingClient.State != ClientState.Authenticating
                    )
                {
                    Input();
                    Formulation.SetConnectToServerPhotonPUN(PhotonNetwork.IsConnected);
                }
                else 
                {
                    LoadingLayer.SetActive(true);
                }
            }
            else if (PhotonNetwork.IsConnected)
            {
                SelectBack();
                if (PhotonNetwork.InRoom)
                {
                    LoadingLayer.SetActive(true);
                }
                else if (!PhotonNetwork.InLobby)
                {
                    LoadingLayer.SetActive(true);
                    Formulation.SetConnectToServerPhotonPUN(PhotonNetwork.IsConnected);
                    PhotonNetwork.JoinLobby();
                }
                else if (PhotonNetwork.InLobby)
                {
                    LoadingLayer.SetActive(false);
                }
            }
            else if (!PhotonNetwork.IsConnected)
            {
                SelectBack();
                LoadingLayer.SetActive(true);

                if (_curTimeChekingCon > Formulation.GetTimeCheckConnection())
                {
                    PhotonNetwork.ConnectUsingSettings();
                    PhotonNetwork.GameVersion = Application.version;
                    _curTimeChekingCon = 0;
                }
                else
                {
                    _curTimeChekingCon += Time.deltaTime;
                }
                Formulation.SetConnectToServerPhotonPUN(PhotonNetwork.IsConnected);
            }
        }

        #endregion === Base Func ===

        #region === Pivate Func ===

        private void Input()
        {
            SelectBack();
            CreateRoom();
            JoinRoom();
            JoinRdmRoom();
        }

        private void SelectBack()
        {
            if (VirtualInputManager.Instance.InputAttr.Back)
            {
                GoBack();
            }
        }

        private void CreateRoom()
        {
            if (VirtualInputManager.Instance.InputAttr.MP_CreteLby)
            {
                if (InpFld_CreateRoom.text.Length > 0)
                {
                    Debug.Log("cekcek CreateLobby with InpFld :" + InpFld_CreateRoom.text);
                    PhotonNetwork.CreateRoom(InpFld_CreateRoom.text);
                    SetIsUpdate(false);
                    LoadingLayer.SetActive(true);
                }
                else 
                {
                    Debug.Log("cekcek CreateLobby  InpFld must be filled");
                }
            }
        }

        private void JoinRoom()
        {
            if (VirtualInputManager.Instance.InputAttr.MP_JoinLby)
            {
                if (InpFld_JoinRoom.text.Length > 0)
                {
                    //Debug.Log("cekcek JoinRoom with InpFld :" + InpFld_JoinRoom.text);
                    if (PhotonNetwork.JoinRoom(InpFld_JoinRoom.text))
                    {
                        Debug.Log("cekcek JoinRoom :" + InpFld_JoinRoom.text + " proceed");
                        //LibScenesController.InstanceLibMaster.SetChangeScene(((StateMainmenu)GetMainState()).Scenes[0]);
                        //Formulation.SetMultiPlayer(true);
                        //SetIsUpdate(false);
                        LoadingLayer.SetActive(true);
                        SetIsUpdate(false);
                    }
                    else
                    {
                        Debug.Log("cekcek JoinRoom :" + InpFld_JoinRoom.text + " not proceed");
                    }
                }
                else
                {
                    Debug.Log("cekcek JoinRoom  InpFld must be filled");
                }
            }
        }
        private void JoinRdmRoom()
        {
            if (VirtualInputManager.Instance.InputAttr.MP_JoinRdmLby)
            {
                
                
                if (PhotonNetwork.JoinRandomRoom())
                {
                    LoadingLayer.SetActive(true);
                    SetIsUpdate(false);
                    Debug.Log("cekcek Join Random Room proceed");
                }
                else
                {
                    Debug.Log("cekcek Join Random Room not proceed");
                }
                
            }
        }
        #endregion === Pivate Func ===


        public void GoBack()
        {
            SerializeDisable();
            VirtualInputManager.Instance.InputAttr.NormalizeInput();
            ClassOfMainState.SerializeEnable();
        }
        //public void OnConnected()
        //{
        //    Debug.Log("cekcek OnConnected");
        //}


        public void ToGamePlay()
        {
            //LoadingLayer.SetActive(false);
            Formulation.SetMultiPlayer(true);
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("cekcek ToGamePlay IsMasterClient");
                Formulation.GetInstansLibSceneController().SetChangeScene(((StateMainmenu)GetMainState()).Scenes[0]);
            }
            else 
            {
                Debug.Log("cekcek ToGamePlay IsClient");
                PhotonNetwork.IsMessageQueueRunning = false;
                Formulation.GetInstansLibSceneController().SetChangeScene(((StateMainmenu)GetMainState()).Scenes[0]);
            }
        }

    }

}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  