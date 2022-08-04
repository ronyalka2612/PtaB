using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Com.GNL.URP_MyLib
{
    public class LibGlobalVolumeController : LibMasterGlobalVolumeController, ILibController
    {
        // Start is called before the first frame update
        void Start()
        {
            //this.enabled = false;
            StateFunc.ClearState();

            if (GBAndroid != null && (LibGameSetting.IsPlatformWindows))
            {
                GBAndroid.gameObject.SetActive(false);
            }
            else if ((GBWindows != null && LibGameSetting.IsPlatformAndroid ))
            {

                GBWindows.gameObject.SetActive(false);
            }


#if UNITY_EDITOR
            if (LibGameSetting.IsUnityPlayerUseAndroidPreRender)
            {
                GBAndroid.gameObject.SetActive(true);
                GBWindows.gameObject.SetActive(false);
            }
#endif

        }

        #region === State Changing ===
        public override void StateChanging()
        {
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == State MAINMENU ==
                case LibEdStateUtilities.GameStates.MAINMENU:
                    // changing just for state main menu needed, do changing substate in substatechanging
                    break;
                #endregion
                #region == State MAIN_GP ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    
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
                    //SubState_MainMenu_Changing();
                    break;
                #endregion
                #region == State MAIN_GP ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    break;
                    #endregion
            }

        }

        public override void ChekingAllFind()
        {
            StateFunc.SetFindAll(true);
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
                #region == State MAIN_GP ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    break;
                    #endregion
            }
        }

        #endregion === State Update ===
        #region === State Called in Update ===
        #endregion === State Called in Update ===

    }

}