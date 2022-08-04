using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

using Com.GNL.URP_MyLib;
using Photon.Realtime;
using ExitGames.Client.Photon;

namespace Com.GNL.URP_MyLibProjectTest
{
    public class PhotonController : MonoBehavMoveObj, IConnectionCallbacks, IMatchmakingCallbacks, IInRoomCallbacks, ILobbyCallbacks, IWebRpcCallback, IErrorInfoCallback, IPunObservable
    {
        
        #region === Attributes ===
        [Header("Player Prefab")]
        public GameObject PlayerPrefab;
        public bool IsNeedChekingIfAlreadyExist;

        [Header("GO POS")]
        public GameObject GO_POS;

        [Header("Player Pos location (Based on BoxCollider of GO_POS)")]
        [MyBox.ReadOnly] [SerializeField] private float minX = -201;
        [MyBox.ReadOnly] [SerializeField] private float maxX = -45.5f;
        [MyBox.ReadOnly] [SerializeField] private float minY = 1;
        [MyBox.ReadOnly] [SerializeField] private float maxY = 3;
        [MyBox.ReadOnly] [SerializeField] private float minZ = 66.3f;
        [MyBox.ReadOnly] [SerializeField] private float maxZ = 170.52f;

        [Header("Referance GameObect ")]
        public CameraController CameraController;
        public GameObject GO_PlayerParent;
        public GameObject GO_EnemyParent;
        public CinemachineFreeLook CameraChinFL;


        private BoxCollider collGO_POS;
        private PhotonView pv;

        private bool isPlayerAlreadyExist = false;

        #endregion === Attributes ===

        #region === Getter Setter ===


        #endregion === Getter Setter ===

        #region === State Changing ===
        private void Awake()
        {
            
        }

        private void OnValidate()
        {
            if (collGO_POS == null)
            {
                collGO_POS = GO_POS.GetComponent<BoxCollider>();
            }
            minX = GO_POS.transform.position.x - (collGO_POS.size.x / 2);
            maxX = GO_POS.transform.position.x + (collGO_POS.size.x / 2);
            minY = GO_POS.transform.position.y - (collGO_POS.size.y / 2);
            maxY = GO_POS.transform.position.y + (collGO_POS.size.y / 2);
            minZ = GO_POS.transform.position.z - (collGO_POS.size.z / 2);
            maxZ = GO_POS.transform.position.z + (collGO_POS.size.z / 2);
        }

        
        void Start()
        {
            if (Formulation.GetInstansLibGameController().IsMultiPlayer)
            {
                PhotonNetwork.AddCallbackTarget(this);
                PhotonNetwork.AutomaticallySyncScene = true;
                if (PhotonNetwork.IsMasterClient)
                {
                    //ExitGames.Client.Photon.Hashtable expectedValue = new ExitGames.Client.Photon.Hashtable();
                    //expectedValue.Add("curScn", LibEdSceneUtilities.ScenesAdditive.SS_MAIN_GP.ToString());

                    //PhotonNetwork.CurrentRoom.SetCustomProperties(expectedValue);
                }
                else 
                {
                    PhotonNetwork.IsMessageQueueRunning = true;
                    PhotonNetwork.SendAllOutgoingCommands();
                }
            }

            if (collGO_POS == null)
            {
                collGO_POS = GO_POS.GetComponent<BoxCollider>();
            }
        }

        private void Initiate()
        {

            Vector3 randomPos = new Vector3(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY), UnityEngine.Random.Range(minZ, maxZ));

            //if use photon //online
            if (Formulation.CheckUsePhoton() && PhotonNetwork.IsConnected)
            {
                //PhotonNetwork.GetPhotonView(PhotonNetwork.MasterClient.)
                Debug.Log("cekcek photonController Count :" + PhotonNetwork.CountOfPlayersOnMaster);

                GameObject playerSpawn = PhotonNetwork.Instantiate(PlayerPrefab.name, randomPos, Quaternion.identity);
                //for (int i = 0; i< PhotonNetwork.CountOfPlayersOnMaster; i++)
                {
                    pv = playerSpawn.GetComponent<PhotonView>();
                    if (Formulation.IsPhotonViewMine(pv))
                    {
                        Debug.Log("cekcek photon Awake PLayer _PV.name :" + pv.name);
                        playerSpawn.transform.parent = GO_PlayerParent.transform;
                    }
                    else 
                    {
                        Debug.Log("cekcek photon Awake Enemy _PV.name :" + pv.name);
                        playerSpawn.transform.parent = GO_EnemyParent.transform;
                        
                    }
                }
            }
            //if not use // offline
            else
            {
                if(isPlayerAlreadyExist)
                { 
                    GameObject playerSpawn = Instantiate(PlayerPrefab, randomPos, Quaternion.identity);
                    Debug.Log("cekcek Awake Single Player");
                    playerSpawn.transform.parent = GO_PlayerParent.transform;
                }
            }
            collGO_POS.enabled = false;
        }

        void ChekingPlayer()
        {
            if(IsNeedChekingIfAlreadyExist)
            {
                LibFormulation.FindObjectByTagThenName(Utilities.TAG.Player.ToString(), Utilities.FIND_GO.ETY_Ball.ToString());
                isPlayerAlreadyExist = true;
            }
        }

        private void StateChanging()
        {
            //if(StateFunc.GetFindAll())
            

            { 
                Initiate();
            }
        }


        private void SubStateChanging()
        {

        }

        private void ChekingAllFind()
        {
            //switch (VirtualStateManager.Instance.CurState)
            //{
            //    #region == State GAMEPLAY ==
            //    case LibEdStateUtilities.GameStates.MAIN_GP:
            //        if (CameraController.ObjFocus)
            //            StateFunc.SetFindAll(true);
            //        else
            //            StateFunc.SetFindAll(false);
            //        break;
            //    #endregion
            //    //default:
            //    //    StateFunc.SetFindAll(true);
            //    //    break;
            //}
            StateFunc.SetFindAll(true);

        }


        #endregion === State Changing ===
        #region === Base State Update ===
        public override void Update_Obj()
        {
            StateFunc.StateUpdate(Update_State, StateFunc.StateChanging(StateChanging, SubStateChanging, ChekingAllFind, ExitSerialize));
        }

        private void ExitSerialize(LibEdStateUtilities.GameStates lastState)
        {
            switch (lastState)
            {
                #region == State GAMEPLAY ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    break;
                    #endregion
            }
        }

        void Update_State()
        {
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == State GAMEPLAY ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    State_MAIN_GP_Update();
                    StateFunc.SubState(SubState_MAIN_GP_Update);
                    break;
                #endregion
            }
        }

        private void SubState_MAIN_GP_Update(LibEdStateUtilities.GameSubStates curSubState)
        {

        }

        private void State_MAIN_GP_Update()
        {
            
            
        }

        public override void Update_FU_Obj()
        {
            
        }

        public override void Update_LU_Obj()
        {
            
        }

        private new void OnDestroy()
        {
            base.OnDestroy();
            PhotonNetwork.RemoveCallbackTarget(this);
        }



        #endregion === Base State Update ===

        private List<ParticleCollisionEvent> _colEvents = new List<ParticleCollisionEvent>();


        #region === OnCallBack Photon ===
        #region === IConnectionCallbacks ===

        public void OnConnectedToMaster()
        {
            Debug.Log("cekcek PhotonController OnConnectedToMaster");
            
        }

        public void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log("cekcek PhotonController OnDisconnected, cause : " + cause);
        }

        public void OnRegionListReceived(RegionHandler regionHandler)
        {
            Debug.Log("cekcek PhotonController OnRegionListReceived regionHandler HostAndPort : " + regionHandler.BestRegion.HostAndPort);
        }

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {
            Debug.Log("cekcek PhotonController OnCustomAuthenticationResponse");
        }

        public void OnCustomAuthenticationFailed(string debugMessage)
        {
            Debug.Log("cekcek PhotonController OnCustomAuthenticationFailed debugMessage:" + debugMessage);
        }

        public void OnConnected()
        {
            Debug.Log("cekcek PhotonController OnConnected ");
            base.OnDestroy();
        }

        #endregion === IConnectionCallbacks ===
        #region === ILobbyCallbacks ===

        public void OnJoinedLobby()
        {
            Debug.Log("cekcek PhotonController OnJoinedLobby");
        }
        public void OnLeftLobby()
        {
            Debug.Log("cekcek PhotonController OnLeftLobby ");
        }

        public void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            
            Debug.Log("cekcek PhotonController OnRoomListUpdate ");
        }

        public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
        {
            Debug.Log("cekcek PhotonController OnLobbyStatisticsUpdate ");
        }


        #endregion === ILobbyCallbacks ===

        #region === IInRoomCallbacks ===


        public void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log("cekcek PhotonController OnPlayerEnteredRoom newPlayer : " + newPlayer);
        }

        public void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log("cekcek PhotonController OnPlayerLeftRoom otherPlayer : " + otherPlayer);
            PhotonNetwork.RemoveRPCs(otherPlayer);
        }

        public void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
        {
            Debug.Log("cekcek PhotonController OnRoomPropertiesUpdate, Photon.Hashtable : " + propertiesThatChanged);
        }

        public void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
        {
            Debug.Log("cekcek PhotonController OnPlayerPropertiesUpdate, targetPlayer : " + targetPlayer + ", Photon.Hashtable : " + changedProps);
        }

        public void OnMasterClientSwitched(Player newMasterClient)
        {
            Debug.Log("cekcek PhotonController OnMasterClientSwitched, newMasterClient :" + newMasterClient);
        }

        #endregion === IInRoomCallbacks ===

        #region === IMatchmakingCallbacks ===

        public void OnFriendListUpdate(List<FriendInfo> friendList)
        {
            Debug.Log("cekcek PhotonController OnFriendListUpdate");
        }

        public void OnCreatedRoom()
        {
            Debug.Log("cekcek PhotonController OnCreatedRoom");
        }

        public void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log("cekcek PhotonController OnCreateRoomFailed, returnCode : " + returnCode + ", message : " + message);
        }

        public void OnJoinedRoom()
        {
            Debug.Log("cekcek PhotonController OnJoinedRoom");
        }

        public void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log("cekcek PhotonController OnJoinRoomFailed, returnCode : " + returnCode + ", message : " + message);
        }

        public void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("cekcek PhotonController OnJoinRandomFailed, returnCode : " + returnCode + ", message : " + message);
        }

        public void OnLeftRoom()
        {
            Interupted();
            Debug.Log("cekcek PhotonController OnLeftRoom");
        }

        #endregion === IMatchmakingCallbacks ===

        #region === IErrorInfoCallback ===



        public void OnErrorInfo(ErrorInfo errorInfo)
        {
            Debug.Log("cekcek PhotonController OnErrorInfo, errorInfo : " + errorInfo);
        }


        #endregion === IErrorInfoCallback ===


        #region === IWebRpcCallback ===

        public void OnWebRpcResponse(OperationResponse response)
        {
            Debug.Log("cekcek PhotonController OperationResponse, response:" + response);
        }

        #endregion === IWebRpcCallback ===

        #region === IPunObservable ===

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            Debug.Log("cekcek PhotonController OnPhotonSerializeView");
        }

        #endregion === IPunObservable ===

        #endregion === OnCallBack Photon ===

        #region === OnCollision ===

        #endregion === OnCollision ===


        #region === Other Function ===
        //private void FailWhenJoining()
        //{
        //    NormalizeConntection();

        //}
        private void NormalizeConntection()
        {

            PhotonNetwork.Disconnect();
        }
        private void Interupted()
        {
            NormalizeConntection();
            
        }

        #endregion === Other Function ===
    }



}