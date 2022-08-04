using Com.GNL.URP_MyLib;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Com.GNL.URP_MyLibProjectTest
{
    public static class Formulation
    {
        public static ETY_Ball FindPlyBall()
        {
            GameObject[] allPlayer = GameObject.FindGameObjectsWithTag(Utilities.TAG.Player.ToString());
            foreach (GameObject g in allPlayer)
            {
                if (g.GetComponent<ETY_Ball>())
                    return g.GetComponent<ETY_Ball>();
            }
            return null;
        }

        public static void QuitGameFromState(BaseState baseState)
        {

            baseState.SerializeDisable();
            GetInstansLibGameController().UnPause();
            VirtualInputManager.Instance.InputAttr.NormalizeInput();
            baseState.ClassOfMainState.SerializeEnable();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }



        public static void QuitGameFromSubState(BaseSubstate baseSubState)
        {

            baseSubState.SerializeDisable();
            GetInstansLibGameController().UnPause();
            VirtualInputManager.Instance.InputAttr.NormalizeInput();
            baseSubState.ClassOfMainState.SerializeEnable();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        public static bool CheckUsePhoton()
        {
            if (GetInstansLibGameController().IsUsePhotonPUN)
            {
                return true;
            }
            Debug.Log("cekcekcek No using Photon, please check LibGameController IsUsePhotonPUN");
            return false;
        }

        public static bool IsPhotonViewMine(PhotonView pv)
        {
            return (PhotonNetwork.IsConnected && pv.IsMine);
        }

        public static LibScenesController GetInstansLibSceneController()
        {
            return ((LibScenesController)LibMasterScenesController.InstanceLibMaster);
        }
        public static LibStatesController GetInstansLibStatesController()
        {
            return ((LibStatesController)LibMasterStatesController.InstanceLibMaster);
        }
        public static LibGameController GetInstansLibGameController()
        {
            return ((LibGameController)LibMasterGameController.InstanceLibMaster);
        }
        public static LibInputController GetInstansLibInputController()
        {
            return ((LibInputController)LibMasterInputController.InstanceLibMaster);
        }
        public static LibCameraController GetInstansLibCameraController()
        {
            return ((LibCameraController)LibMasterCameraController.InstanceLibMaster);
        }
        public static LibSelectionController GetInstansLibSelectionController()
        {
            return ((LibSelectionController)LibMasterSelectionController.InstanceLibMaster);
        }


        public static void SetMultiPlayer(bool flag)
        {
            if(GetInstansLibGameController() != null)
                GetInstansLibGameController().IsMultiPlayer = flag;
        }

        public static bool GetMultiPlayer()
        {
            return GetInstansLibGameController().IsMultiPlayer;
        }

        public static float GetTimeCheckConnection()
        {
            return GetInstansLibGameController().TimeRepetedCheckingConnection;
        }

        public static void SetConnectToServerPhotonPUN(bool flag)
        {
            GetInstansLibGameController().IsConnectToServerPhotonPUN = flag;
        }
        public static bool GetConnectToServerPhotonPUN()
        {
            return GetInstansLibGameController().IsConnectToServerPhotonPUN;
        }

        public static bool TryConnectToServerPhotonPUN()
        {
            return GetInstansLibGameController().IsConnectToServerPhotonPUN;
        }
    }
}