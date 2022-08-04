using MyBox;
using System;
using System.Collections.Generic;
using UnityEngine;

using Com.GNL.URP_MyLib;

namespace Com.GNL.URP_MyLibProjectTest
{

    //CM -> chinemachine
    public class CameraCMVirtualController : MonoBehaviourMyBase
    {
        [Header("Setting Car Controll")]
        public GameObject ObjToFollow;
        

        #region === State Changing ===

        private void StateChanging()
        {
            switch (VirtualStateManager.Instance.CurState)
            {
                
                #region == State MAIN_GP ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    State_MAIN_GP_Changing();
                    break;
                    #endregion
            }

        }

        private void SubStateChanging()
        {
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == State GAMEPLAY ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    StateFunc.SubState(SubState_MAIN_GP_Changing);
                    break;
                    #endregion
            }

        }

        private void ChekingAllFind()
        {
            {
                StateFunc.SetFindAll(true);
            }
        }



        private void SubState_MAIN_GP_Changing(LibEdStateUtilities.GameSubStates substate)
        {
            switch (substate)
            {

                #region == SubState MAIN_GP_GAMEPLAY_PAUSE ==
                case LibEdStateUtilities.GameSubStates.MAIN_GP_GAMEPLAY_PAUSE:

                    break;
                    #endregion
            }

        }

        private void State_MAIN_GP_Changing()
        {
            Initialize_MAIN_GP();
        }


        private void Initialize_MAIN_GP()
        {
            //_cam = Camera.main;
            //_CC = GetComponent<CharacterController>();
            //Projectile.SetActive(true);

            //this.transform.position = ObjToFollow.transform.position;
        }

        #endregion === State Changing ===

        #region === Master State === 
        void Update()
        {
            StateFunc.StateUpdate(Update_State, StateFunc.StateChanging(StateChanging, SubStateChanging, ChekingAllFind));
        }

        private void Update_State()
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

        private void FixedUpdate()
        {
            StateFunc.StateUpdate(Update_State_FU, StateFunc.StateChanging(StateChanging, SubStateChanging, ChekingAllFind));

        }

        private void Update_State_FU()
        {
            switch (VirtualStateManager.Instance.CurState)
            {

                #region == State MAIN_GP ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    State_MAIN_GP_FixedUpdate();

                    break;
                    #endregion
            }
        }

        private void LateUpdate()
        {
            StateFunc.StateUpdate(Update_State_LU, StateFunc.StateChanging(StateChanging, SubStateChanging, ChekingAllFind));
        }

        private void Update_State_LU()
        {
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == State MAIN_GP ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    State_MAIN_GP_LateUpdate();

                    break;
                    #endregion
            }
        }

        #endregion === Master State === 

        #region === State Update ===
        // Update is called once per frame


        private void State_MAIN_GP_Update()
        {
            if (!LibGameSetting.IsPause)
            {
                //Locomotion();
            }
        }

        private void Locomotion()
        {
            this.transform.position = ObjToFollow.transform.position;
        }

        private void SubState_MAIN_GP_Update(LibEdStateUtilities.GameSubStates subState)
        {
            switch (subState)
            {

                #region == SubState MAIN_GP_GAMEPLAY_PAUSE ==
                case LibEdStateUtilities.GameSubStates.MAIN_GP_GAMEPLAY_PAUSE:
                    if (LibGameSetting.IsPause)
                    {

                    }
                    break;
                    #endregion
            }
        }

        #endregion === State Update ===

        #region === State Fixed Update ===
        

        private void State_MAIN_GP_FixedUpdate()
        {
            if (!LibGameSetting.IsPause)
            {
                Locomotion_FU();
            }
        }

        private void Locomotion_FU()
        {

        }

        #endregion === State Fixed Update ===


        #region === State Late Update ===


        private void State_MAIN_GP_LateUpdate()
        {
            if (!LibGameSetting.IsPause)
            {

            }
        }

        #endregion === State Late Update ===

        #region === StateFunction Collision ===



        private void OnCollisionStay(Collision collision)
        {

        }

        private void OnCollisionEnter(Collision collision)
        {


        }

        private void OnCollisionExit(Collision collision)
        {


        }

        #endregion === StateFunction Collision ===
    }
}