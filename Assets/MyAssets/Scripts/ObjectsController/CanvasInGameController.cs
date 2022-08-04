using MyBox;
using System;
using System.Collections.Generic;
using UnityEngine;

using Com.GNL.URP_MyLib;
using Photon.Pun;
using Cinemachine;

namespace Com.GNL.URP_MyLibProjectTest
{


    public class CanvasInGameController : MonoBehavMoveObj
    {

        #region === Attributes ===

        #endregion === Attributes ===

        #region === Getter Setter ===

        #endregion === Getter Setter ===

        #region === Enum attributes ===


        #endregion === Enum attributes ===

        #region === State Changing ===

        private void OnGUI()
        {

        }

        private void OnValidate()
        {

        }

        private void Awake()
        {

        }

        private void StateChanging()
        {
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == State MAINMENU ==
                case LibEdStateUtilities.GameStates.MAINMENU:
                    break;
                #endregion
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
            StateFunc.SetFindAll(true);
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

        }





        #endregion === State Changing ===

        #region === Base State Update ===
        // Update is called once per frame

        public override void Update_Obj()
        {
            StateFunc.StateUpdate(Update_State, StateFunc.StateChanging(StateChanging, SubStateChanging, ChekingAllFind));
        }


        //new void Update()
        //{
        //    StateFunc.StateUpdate(Update_State, StateFunc.StateChanging(StateChanging, SubStateChanging, ChekingAllFind));
        //}

        private void Update_State()
        {
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == State GAMEPLAY ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    //if (!Formulation.GetMultiPlayer() || Formulation.IsPhotonViewMine(_PV))
                    {
                        
                        State_MAIN_GP_Update();
                        StateFunc.SubState(SubState_MAIN_GP_Update);
                    }
                    break;
                    #endregion
            }
        }
        private void State_MAIN_GP_Update()
        {
            if (!LibGameSetting.IsPause)
            {
                MovingInput();
                AttackInput();
                Locomotion();
            }
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

        public override void Update_FU_Obj()
        {
            StateFunc.StateUpdate(Update_State_FU, StateFunc.StateChanging(StateChanging, SubStateChanging, ChekingAllFind));
        }

        //private void FixedUpdate()
        //{
        //    StateFunc.StateUpdate(Update_State_FU, StateFunc.StateChanging(StateChanging, SubStateChanging, ChekingAllFind));

        //}

        private void Update_State_FU()
        {
            switch (VirtualStateManager.Instance.CurState)
            {

                #region == State MAIN_GP ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    //if (!Formulation.GetMultiPlayer() || Formulation.IsPhotonViewMine(_PV) )
                    //{ 
                        State_MAIN_GP_FixedUpdate();
                    //}
                    break;
                    #endregion
            }
        }

        private void State_MAIN_GP_FixedUpdate()
        {
            if (!LibGameSetting.IsPause)
            {
                //MovingInput();
                Locomotion_FU();
            }
        }


        public override void Update_LU_Obj()
        {
            StateFunc.StateUpdate(Update_State_LU, StateFunc.StateChanging(StateChanging, SubStateChanging, ChekingAllFind));
        }

        //private void LateUpdate()
        //{
        //    StateFunc.StateUpdate(Update_State_LU, StateFunc.StateChanging(StateChanging, SubStateChanging, ChekingAllFind));
        //}

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

        private void State_MAIN_GP_LateUpdate()
        {
            if (!LibGameSetting.IsPause)
            {

            }
        }



        #endregion === Base State Update ===

        #region === On Collision ===
        private void OnCollisionStay(Collision collision)
        {

        }

        private void OnCollisionEnter(Collision collision)
        {


        }

        private void OnCollisionExit(Collision collision)
        {


        }

        #endregion === On Collision ===

        #region === Other Function ===
        private void MovingInput()
        {

        }


        private void AttackInput()
        {
            
        }


        private void Locomotion()
        {

        }




        private void Locomotion_FU()
        {

        }



        

        #endregion === Other Function ===
    }

    #region === Other Class ===
    

    #endregion === Other Class ===
}