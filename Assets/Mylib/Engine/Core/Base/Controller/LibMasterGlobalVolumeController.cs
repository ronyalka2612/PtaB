using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.GNL.URP_MyLib
{
    public class LibMasterGlobalVolumeController : MonoBehaviourMyBase
    {
        public GameObject GBAndroid;
        public GameObject GBWindows;
        // Start is called before the first frame update
        void Start()
        {
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
        public virtual void StateChanging()
        {
            
        }

        public virtual void SubStateChanging()
        {

        }

        public virtual void ChekingAllFind()
        {
            
        }


        #endregion === State Changing ===
        #region === State Update ===
        private void Update()
        {
            StateFunc.StateUpdate(Update_State, StateFunc.StateChanging(StateChanging, SubStateChanging, CheckingAllfind));
        }

        public virtual void Update_State()
        {

        }
        private void FixedUpdate()
        {
            StateFunc.StateUpdate(Update_StateFU, StateFunc.StateChanging(StateChanging, SubStateChanging, CheckingAllfind));
        }

        public virtual void Update_StateFU()
        {

        }

        private void LateUpdate()
        {
            StateFunc.StateUpdate(Update_StateLU, StateFunc.StateChanging(StateChanging, SubStateChanging, CheckingAllfind));
        }

        public virtual void Update_StateLU()
        {

        }

        #endregion === State Update ===
        #region === State Called in Update ===
        #endregion === State Called in Update ===

    }

}