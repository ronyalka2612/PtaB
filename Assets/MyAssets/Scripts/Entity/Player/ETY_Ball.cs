using MyBox;
using System;
using System.Collections.Generic;
using UnityEngine;

using Com.GNL.URP_MyLib;
using Photon.Pun;

namespace Com.GNL.URP_MyLibProjectTest
{


    public class ETY_Ball : MonoBehavMoveObj
    {
        #region === Attributes ===

        [Header("Player config")]

        public float ShootPower = 40;
        public float RotatationSpeed = 0.1f;

        public float MAX_HeightPoint = 1.2f;
        public float MIN_HeightPoint = -1.2f;

        public float MinimalVelocityMagToMoveAgain = 0.1f;

        [Header("Pointer")]
        public GameObject Pointer;

        //[Header("CameraTarget")]
        //public GameObject CamTarget;

        //[Header("CameraTarget")]
        //public GameObject BallObj;

        public float curYPoint = 0;
        public float centerYPoint = 0;

        [Header("Attachment")]
        public GameObject GO_CanvasInGame;

        private bool IsReleaseBall = false;
        private bool IsCanReleaseBall = false;
        private bool IsPointing = false;

        private Rigidbody _RB = null;

        private CanvasGroup _canvasGroup;

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
            _RB = GetComponent<Rigidbody>();
            Pointer.transform.position = Camera.main.transform.TransformDirection(Vector3.forward);
            centerYPoint = Pointer.transform.position.normalized.y;
            _canvasGroup = GO_CanvasInGame.GetComponent<CanvasGroup>();
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
                Attacking();
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
        private void Locomotion()
        {

        }
        private void Locomotion_FU()
        {
            if (_RB.velocity.magnitude > MinimalVelocityMagToMoveAgain)
            {
                _canvasGroup.alpha = 0;
                IsCanReleaseBall = false;
            }
            else 
            {
                _canvasGroup.alpha = 1;
                IsCanReleaseBall = true;
            }
            

        }


        private void AttackInput()
        {
            if (VirtualInputManager.Instance.InputAttr.BreakRear)
            {
                IsReleaseBall = true;
            }
            else
            {
                IsReleaseBall = false;
            }

            if (VirtualInputManager.Instance.InputAttr.Gas || VirtualInputManager.Instance.InputAttr.MoveCam)
            {
                IsPointing = true;
            }
            else
            {
                IsPointing = false;
            }


            //if analog 
            if (VirtualDataInputManager.Instance.AxisFinalInput.x > 0)
            {
                //_ballXRot += VirtualDataInputManager.Instance.AxisFinalInput.x * RotatationSpeed;
            }
            else if (VirtualDataInputManager.Instance.AxisFinalInput.x < 0)
            {
                //_ballXRot -= VirtualDataInputManager.Instance.AxisFinalInput.x * RotatationSpeed;
            }
            if (VirtualDataInputManager.Instance.AxisFinalInput.y > 0)
            {
                //_ballYRot += VirtualDataInputManager.Instance.AxisFinalInput.y * RotatationSpeed;
                if (curYPoint <= MAX_HeightPoint)
                {
                    curYPoint += VirtualDataInputManager.Instance.AxisFinalInput.y * RotatationSpeed;
                    IsPointing = true;
                }

            }
            else if (VirtualDataInputManager.Instance.AxisFinalInput.y < 0)
            {
                //_ballYRot += VirtualDataInputManager.Instance.AxisFinalInput.y * RotatationSpeed;
                if (curYPoint >= MIN_HeightPoint)
                {
                    curYPoint += VirtualDataInputManager.Instance.AxisFinalInput.y * RotatationSpeed;
                    IsPointing = true;
                }
            }
        }

        Vector3 direction = new Vector3();
        private void Attacking()
        {

            if (IsPointing)
            {
                Pointer.transform.position = Camera.main.transform.TransformDirection(Vector3.forward);
                centerYPoint = Pointer.transform.position.normalized.y;
                //direction = Vector3.Scale(Pointer.transform.position.normalized, new Vector3(1, curYPoint, 1)) ;
                direction = Pointer.transform.position.normalized - new Vector3(0, curYPoint, 0);
                direction = direction.normalized;
            }



            if (IsReleaseBall && IsCanReleaseBall)
            {
                _RB.velocity = direction * ShootPower;
            }
        }

       


        //// simple function to add a curved bias towards 1 for a value in the 0-1 range
        //private static float CurveFactor(float factor)
        //{
        //    return 1 - (1 - factor) * (1 - factor);
        //}


        //// unclamped version of Lerp, to allow value to exceed the from-to range
        //static float ULerp(float from, float to, float value)
        //{
        //    return (1.0f - value) * from + value * to;
        //}


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            //Vector3 directionDraw = Pointer.transform.position * 5;
            Vector3 directionDraw = Pointer.transform.position * 5;
            Gizmos.DrawRay(transform.position, direction);
        }

        #endregion === Other Function ===
    }

    #region === Other Class ===
    

    #endregion === Other Class ===
}