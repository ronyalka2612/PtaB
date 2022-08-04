using Com.GNL.URP_MyLib;
using Com.GNL.URP_MyLibProjectTest;
using MyBox;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.GNL.URPProduction
{


    public class PLY_BallControllerRB : MonoBehavMoveObj
    {
        [Header("Setting Game")]
        public float Gravity = 50f;
        public GameObject[] Cones;

        [Header("Setting Control")]
        public float CharacterMoveSpeed;
        public float HighPosToBelowOfBody = 1;

        [Header("Setting Control Jump")]
        public float JumpPower = 5;
        public float JumpHeightMax = 3f;


        [Header("Status control")]
        public bool IsInJump, IsGrounded;
        public float _velocityAir = 0;
        public float _tempVelocityAir = 0;
        private Vector3 _targetDirection;

        private Camera _cam;
        private Rigidbody _RB;


        public enum Direction
        { 
            front,
            right,
            left, 
            back
        }


        public enum ShooterPosition
        { 
            Left,
            Right,
            Count
        }

        #region === State Changing ===

        private void StateChanging()
        {
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == State MAINMENU ==
                case LibEdStateUtilities.GameStates.MAINMENU:
                    break;
                #endregion
                #region == State GP_3 ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    State_GP_3_Changing();
                    break;
                    #endregion
            }

        }

        private void SubStateChanging()
        {
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == State GP_3 ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    StateFunc.SubState(SubState_GP_3_Changing);
                    break;
                    #endregion
            }

        }

        private void ChekingAllFind()
        {
            //if (VirtualStateManager.Instance.CurState == VirtualStateManager.GameStates.GAMEPLAY && _anim == null || _mainCamera == null || _characterController == null || InputPlayer == null)
            //{
            //    StateFunc.IsFindAll = true;
            //    StateFunc.clearState(ref _curState, ref _curSubState);
            //}
            //else
            {
                StateFunc.SetFindAll(true);
            }
        }



        private void SubState_GP_3_Changing(LibEdStateUtilities.GameSubStates substate)
        {
            switch (substate)
            {

                #region == SubState GAMEPLAY_PAUSE ==
                case LibEdStateUtilities.GameSubStates.MAIN_GP_GAMEPLAY_PAUSE:

                    break;
                    #endregion
            }

        }

        private void State_GP_3_Changing()
        {
            Initialize();
        }

        private void Initialize()
        {
            _cam = Camera.main;
            _RB = GetComponent<Rigidbody>();
        }

        #endregion === State Changing ===

        #region === State Update ===
        // Update is called once per frame
        public override void Update_Obj()
        {
            StateFunc.StateUpdate(Update_State, StateFunc.StateChanging(StateChanging, SubStateChanging, ChekingAllFind));

        }

        private void Update_State()
        {
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == State GP_3 ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    State_GP_3_Update();
                    StateFunc.SubState(SubState_GP_3_Update);
                    break;
                    #endregion
            }
        }

        private void State_GP_3_Update()
        {
            if (!LibGameSetting.IsPause)
            {

                //ConesUpdate();
                MovingInput();
                Locomotion();
                //Animation();

                //ShootProjectile();
            }
        }

        private void ConesUpdate()
        {
            Cones[(int)Direction.front].transform.position =  new Vector3 (
                this.transform.position.x + Camera.main.transform.forward.normalized.x, 
                this.transform.position.y, 
                this.transform.position.z + Camera.main.transform.forward.normalized.z) ;
            Cones[(int)Direction.right].transform.position = new Vector3(
                this.transform.position.x + Camera.main.transform.right.normalized.x,
                this.transform.position.y,
                this.transform.position.z + Camera.main.transform.right.normalized.z);
            Cones[(int)Direction.left].transform.position = new Vector3(
                this.transform.position.x - Camera.main.transform.right.normalized.x,
                this.transform.position.y,
                this.transform.position.z - Camera.main.transform.right.normalized.z);
            Cones[(int)Direction.back].transform.position = new Vector3(
                this.transform.position.x - Camera.main.transform.forward.normalized.x,
                this.transform.position.y,
                this.transform.position.z - Camera.main.transform.forward.normalized.z);


        }

        private void OnCollisionStay(Collision collision)
        {
            if (IsGroundedRC() &&
                (collision.gameObject.layer == (int)Utilities.LAYER.GROUND
                || collision.gameObject.layer == (int)Utilities.LAYER.WALL
                || collision.gameObject.layer == (int)Utilities.LAYER.ACTIVE_OBJECT))
            {
                //Vector3 right = Camera.main.transform.TransformDirection(Vector3.right);
                //Debug.DrawLine(this.transform.position + (right * 2f), this.transform.position + (right * 2f) + (Vector3.up * -1 * transform.localScale.y), Color.green);
                IsGrounded = true;

                IsInJump = false;
            }
            else
            {
                IsGrounded = false;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (IsGroundedRC() &&   
                (  collision.gameObject.layer == (int)Utilities.LAYER.GROUND
                || collision.gameObject.layer == (int)Utilities.LAYER.WALL
                || collision.gameObject.layer == (int)Utilities.LAYER.ACTIVE_OBJECT))
            {
                //Vector3 right = Camera.main.transform.TransformDirection(Vector3.right);
                ////Debug.DrawLine(this.transform.position + (right * 2f), this.transform.position + (right * 2f) + (Vector3.up * -1 * transform.localScale.y), Color.green);
                IsGrounded = true;

                IsInJump = false;

            }
            else
            {
                IsGrounded = false;
            }

        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.layer == (int)Utilities.LAYER.GROUND
                || collision.gameObject.layer == (int)Utilities.LAYER.WALL
                || collision.gameObject.layer == (int)Utilities.LAYER.ACTIVE_OBJECT)
            {
                IsGrounded = false;
            }

        }

        private bool IsGroundedRC()
        { 
            return Physics.Raycast(transform.position, -Vector3.up, HighPosToBelowOfBody + 0.1f);
        }

        private bool IsDistanceToGroundWhileJumpdRC()
        {
            return IsInJump && !Physics.Raycast(transform.position, -Vector3.up, HighPosToBelowOfBody + JumpHeightMax );
        }

        private void MovingInput()
        {

            Vector3 right = Camera.main.transform.TransformDirection(Vector3.right);
            if ((IsGrounded) && VirtualInputManager.Instance.InputAttr.Jump)
            {
                SetVelocityAir(JumpPower);
                IsInJump = true;


            }
            //Debug.DrawLine(this.transform.position + (right * 2f * -1), this.transform.position + (right * 2f * -1) + (Vector3.up* -1 * transform.localScale.y), Color.red);

            UpdateTargetDirection();

            
        }

        private void SetVelocityAir(float velocityAir)
        {
            _velocityAir = velocityAir;
        }

        private void Locomotion()
        {
            
        }
        private void Locomotion_FU()
        {
            LocomoptionAir();


            _RB.AddForce(new Vector3(_targetDirection.x * CharacterMoveSpeed, 0, _targetDirection.z * CharacterMoveSpeed));
            if (_velocityAir != 0)
            {
                _RB.velocity = new Vector3(_RB.velocity.x, _velocityAir, _RB.velocity.z);
                SetVelocityAir(0);
            }

        }

        private void LocomoptionAir()
        {
            if (_RB.velocity.y > 0.1 || _RB.velocity.y < -0.1)
            {
                _targetDirection = Vector3.ClampMagnitude(_targetDirection * 10, 1.0f);
            }
        }


        private void Animation()
        {
            
        }

        public virtual void UpdateTargetDirection()
        {

            //_turnSpeedMultiplier = 1f;
            Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);

            //get the right-facing direction of the referenceTransform
            Vector3 right = Camera.main.transform.TransformDirection(Vector3.right);

            // determine the direction the player will face based on input and the referenceTransform's right and forward directions
            _targetDirection = VirtualDataInputManager.Instance.AxisFinalInput.x * right + VirtualDataInputManager.Instance.AxisFinalInput.y * forward;
            //_targetDirection = lastDir.x * right + lastDir.y * forward;
            _targetDirection.y = 0;


            if (_targetDirection.magnitude > 1f)
            {
                _targetDirection = Vector3.ClampMagnitude(_targetDirection, 1.0f);
            }
        }

       

        private void SubState_GP_3_Update(LibEdStateUtilities.GameSubStates subState)
        {
            switch (subState)
            {

                #region == SubState GAMEPLAY_PAUSE ==
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
        public override void Update_FU_Obj()
        {
            StateFunc.StateUpdate(Update_State_FU, StateFunc.StateChanging(StateChanging, SubStateChanging, ChekingAllFind));

        }

        private void Update_State_FU()
        {
            switch (VirtualStateManager.Instance.CurState)
            {

                #region == State GP_3 ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    State_Gameplay_FixedUpdate();

                    break;
                    #endregion
            }
        }

        private void State_Gameplay_FixedUpdate()
        {
            if (!LibGameSetting.IsPause)
            {
                Locomotion_FU();
            }
        }

        #endregion === State Fixed Update ===


        #region === State Late Update ===
        public override void Update_LU_Obj()
        {

            StateFunc.StateUpdate(Update_State_LU, StateFunc.StateChanging(StateChanging, SubStateChanging, ChekingAllFind));


        }

        private void Update_State_LU()
        {
            switch (VirtualStateManager.Instance.CurState)
            {

                #region == State GP_3 ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    State_GP_3_LateUpdate();


                    break;
                    #endregion
            }
        }

        private void State_GP_3_LateUpdate()
        {
            if (!LibGameSetting.IsPause)
            {

            }
        }

        #endregion === State Late Update ===

    }
}