using Com.GNL.URP_MyLib;
using MyBox;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.GNL.URPProduction
{


    public class PLY_BallController : MonoBehavMoveObj
    {
        [Header("Setting Game")]
        public CharacterController _CC;
        public PLY_BallModel BallModel;
        public float Gravity = 50f;
        public GameObject[] Cones;

        [Header("Setting Control")]
        [Range(0.001f, 1000)]
        public float RotateSpeed = 200f;
        public float CharacterSpeed = 2f;
        public float CharacterAnimAcceleration = 5.5f;
        public float CharacterAnimDeceleration = 4.5f;
        public float CharacterDirSpeed =2f;
        public float CharacterDirAcceleration = 0.1f;
        public float CharacterDirDeceleration = 0.1f;

        [Header("Setting Control Jump")]
        public float JumpPower = 12;
        public float JumpHeightMax = 3f;
        public float TimeMaxFall = 3f;
        public float FallHeightMax = 20f;
        public float MomentumPower = 1005f;

        [Range(1, 5)]
        public float FallAcc = 2f;
        public AnimationCurve JumpGraph;
        public AnimationCurve JumpFallGraph;
        public AnimationCurve JumpFallMomentumGraph;

        [Header("Batasan control")]
        public float CharacterSpeedMAX = 3.5f;
        public float FallVeloActInUp = 0.0085f;
        public float FallVeloActInDw = 0.04f;


        [Header("Status control")]
        public bool IsInJump;
        private float TimerFall = 0;
        private float _velocityAir = 0;
        //private float _velocityAirMomentum = 0;
        private Vector3 finalTargetDirMove;
        private Vector3 _targetDirection;
        private float _turnSpeedMultiplier;
        private Vector2 lastDir;
        private Vector3 TargetDirModel;

        private bool IsHaveMomentum;
        public bool IsUsedJumpMoementum = false;

        private float _curSpeed = 0f;
        private Quaternion _freeRotation;

        //private VirtualStateManager.GameStates _curState = VirtualStateManager.GameStates.NO_STATE;
        //private List<VirtualStateManager.GameSubStates> _curSubState = new List<VirtualStateManager.GameSubStates>();

        private Camera _cam;

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
                #region == State MAIN_GP ==
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
                #region == State GAMEPLAY ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    StateFunc.SubState(SubState_GP_3_Changing, true);
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
            _CC = GetComponent<CharacterController>();
            //Projectile.SetActive(true);
        }

        #endregion === State Changing ===

        #region === State Update ===
        // Update is called once per frame
        public override void Update_Obj()
        {
            StateFunc.StateUpdate(Update_State, StateFunc.StateChanging(StateChanging, SubStateChanging, ChekingAllFind));
        }


        public override void Update_FU_Obj()
        {
            StateFunc.StateUpdate(Update_State_FU, StateFunc.StateChanging(StateChanging, SubStateChanging, ChekingAllFind));
        }

        public override void Update_LU_Obj()
        {
            StateFunc.StateUpdate(Update_State_LU, StateFunc.StateChanging(StateChanging, SubStateChanging, ChekingAllFind));
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

        private void Update_State_FU()
        {
            //switch (VirtualStateManager.Instance.CurState)
            //{

            //    #region == State MAIN_GP ==
            //    case LibEdStateUtilities.GameStates.MAIN_GP:
                    
            //        State_MAIN_GP_FixedUpdate();
            //        break;
            //        #endregion
            //}
        }

        private void Update_State_LU()
        { 
        
        }



        private void State_GP_3_Update()
        {
            if (!LibGameSetting.IsPause)
            {

                ConesUpdate();
                MovingInput();
                Locomotion();
                Animation();
                
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


            //Debug.DrawLine(this.transform.position, Cones[(int)Direction.front].transform.position, Color.green);
            //Debug.DrawLine(this.transform.position, Cones[(int)Direction.right].transform.position, Color.blue);
            //Debug.DrawLine(this.transform.position, Cones[(int)Direction.left].transform.position, Color.red);
            //Debug.DrawLine(this.transform.position, Cones[(int)Direction.back].transform.position, Color.black);
        }

        private void MovingInput()
        {
           

            if (_CC.isGrounded && VirtualInputManager.Instance.InputAttr.Jump)
            {
                SetVelocityAirJump(JumpPower * (Time.deltaTime));
            }


            if (VirtualInputManager.Instance.InputAttr.Move)
            {
                if (GetCurSpeed() < CharacterSpeedMAX)
                {
                    SetCurSpeed(GetCurSpeed() + Time.deltaTime * CharacterAnimAcceleration);
                    if (GetCurSpeed() > CharacterSpeedMAX)
                    {
                        SetCurSpeed(CharacterSpeedMAX);
                    }
                }
                else if (GetCurSpeed() > CharacterSpeedMAX)
                {
                    SetCurSpeed(GetCurSpeed() - Time.deltaTime * CharacterAnimDeceleration);
                    if (GetCurSpeed() < CharacterSpeedMAX)
                    {
                        SetCurSpeed(CharacterSpeedMAX);
                    }
                }

                //
                //if(VirtualDataInputManager.Instance.AxisFinalInput.x > 0.1f || VirtualDataInputManager.Instance.AxisFinalInput.x < 0.1f)
                if(VirtualInputManager.Instance.InputAttr.MoveRight || VirtualInputManager.Instance.InputAttr.MoveLeft)
                {
                    lastDir.x += (VirtualDataInputManager.Instance.AxisFinalInput.x * CharacterDirSpeed * CharacterDirAcceleration * Time.deltaTime);

                }
                else
                {
                    if (lastDir.x > 0.05f)
                        lastDir.x -= CharacterDirSpeed * CharacterDirAcceleration * Time.deltaTime;
                    else if (lastDir.x < -0.05f)
                        lastDir.x += CharacterDirSpeed * CharacterDirAcceleration * Time.deltaTime;
                    else
                        lastDir.x = 0;
                }

                //if (VirtualDataInputManager.Instance.AxisFinalInput.y > 0.1f || VirtualDataInputManager.Instance.AxisFinalInput.y < 0.1f)
                if (VirtualInputManager.Instance.InputAttr.MoveForward || VirtualInputManager.Instance.InputAttr.MoveBack)
                {
                    lastDir.y += (VirtualDataInputManager.Instance.AxisFinalInput.y * CharacterDirSpeed * CharacterDirAcceleration * Time.deltaTime);

                }
                else
                {
                    if (lastDir.y > 0.05f)
                        lastDir.y -= CharacterDirSpeed * CharacterDirAcceleration * Time.deltaTime;
                    else if (lastDir.y < -0.05f)
                        lastDir.y += CharacterDirSpeed * CharacterDirAcceleration * Time.deltaTime;
                    else
                        lastDir.y = 0;
                }


                lastDir.x = Mathf.Clamp(lastDir.x, -1f, 1f);
                lastDir.y = Mathf.Clamp(lastDir.y, -1f, 1f);
            }
            else
            {
                if (GetCurSpeed() > 0)
                {
                    SetCurSpeed(GetCurSpeed() - Time.deltaTime * CharacterAnimDeceleration);
                    lastDir.x *= (Mathf.Clamp(GetCurSpeed() / CharacterSpeedMAX, 0, 1));
                    lastDir.y *= (Mathf.Clamp(GetCurSpeed() / CharacterSpeedMAX ,0, 1));
                    
                }
                lastDir.x = Mathf.Clamp(lastDir.x, -1f, 1f);
                lastDir.y = Mathf.Clamp(lastDir.y, -1f, 1f);
            }
        }

        private void Locomotion()
        {

            if (!_CC.isGrounded && IsInJump)
            {
                float length = transform.position.y - curY;
                if (length < JumpHeightMax - 0.1f)
                {
                    float times = length / JumpHeightMax;
                    times = times < 0.001f ? 0.001f : times;
                    SetVelocityAir(JumpPower * JumpGraph.Evaluate(times) * (Time.deltaTime));
                }
                else
                {
                    curY = transform.position.y - (JumpHeightMax);
                    SetVelocityAir(0);
                    IsInJump = false;
                    TimerFall = 0;
                }
            }

            //ContingMomentum();

            if (!_CC.isGrounded && !IsInJump)
            {
                //if had momentum / have speed it will call jump, 
                

                //if (GetVelocityAir() > 0)
                //{
                //    SetVelocityAir(GetVelocityAir() - (Gravity * Time.deltaTime));
                //}
                //else 
                if (TimerFall < TimeMaxFall)
                {
                    
                    if (!IsUsedJumpMoementum)
                    { 
                        SetVelocityAir(GetVelocityAir()-(JumpFallGraph.Evaluate(TimerFall / TimeMaxFall) * (Time.deltaTime)));

                        TimerFall += Time.deltaTime;
                    }
                    //else if(IsUsedJumpMoementum)
                    //{ 
                    //    SetVelocityAir(MomentumPower  * JumpFallMomentumGraph.Evaluate(TimerFall  / (TimeMaxFall)) * (Time.deltaTime));

                    //    TimerFall = TimerFall+ (Time.deltaTime*6);
                    //    Debug.Log("cekcekcek momentum jump");
                    //}
                    if (IsHaveMomentum && !IsUsedJumpMoementum && TimerFall / (TimeMaxFall) >= 0.2f / 10f)
                    {
                        IsUsedJumpMoementum = true;
                        //SetVelocityAirJumpMoementum(_velocityAirMomentum * (Time.deltaTime));
                    }


                    //Debug.Log("timerFall :" + timerFall);
                    //Debug.Log("velocity Air:" + GetVelocityAir());
                }
                else if (TimerFall >= TimeMaxFall)
                {
                    // falling
                    SetVelocityAir(GetVelocityAir()-(Gravity * (Time.deltaTime)));
                }
                
            }

            //Gravitation when grounded keep it falling
            if (_CC.isGrounded && _velocityAir < 0 && !VirtualInputManager.Instance.InputAttr.Jump)
            {
                curY = 0;
                TimerFall = 0;
                SetVelocityAir(0f);
                IsUsedJumpMoementum = false;
            }

            //when landing need update where the player should move by arrow and camera angle
            //if (_CC.isGrounded)
            {
                UpdateTargetDirection();
            }



            //Vector3 targetDir = Vector3.zero;
            finalTargetDirMove = _targetDirection * GetCurSpeed() * CharacterSpeed * Time.deltaTime;
            
            // when velocity still on that area, player will fall dawn or go up

            //else if (_velocityAir > FallVeloActInUp || _velocityAir < -FallVeloActInDw)
            {
                finalTargetDirMove = new Vector3(finalTargetDirMove.x, _velocityAir, finalTargetDirMove.z);
                _CC.Move(finalTargetDirMove);
            }


            

        }

        private void ContingMomentum()
        {
            if (_CC.isGrounded)
            {
                //counting momentum
                Momentum = (LastPos - transform.position);
                MomentumY = Mathf.Abs(Momentum.y);
                Momentum.y = 0;
                MomentumMag = Momentum.magnitude;
                //if (MomentumY > 0.01f && MomentumY <1f)
                if (MomentumMag > 0.03f)
                {
                    IsHaveMomentum = true;
                    //_velocityAirMomentum = Mathf.Clamp( Math.Abs( MomentumY * MomentumPower),0,3);
                }
                else
                {
                    IsHaveMomentum = false;
                    MomentumY = 0;
                }

                LastPos = transform.position;
                Debug.DrawLine(this.transform.position, this.transform.position + Momentum * 10f, Color.cyan);
            }
        }

        private Vector3 LastPos = new Vector3() ;
        private Vector3 Momentum = new Vector3();
        private float MomentumY;
        private float MomentumMag;

        private void Animation()
        {
            //counting move when any input
            if (_targetDirection.magnitude > 0.1f)
            {
                // set the player's direction
                //Vector3 lookDirection = _targetDirection.normalized;
                //_freeRotation = Quaternion.LookRotation(lookDirection, _CC.transform.up);
                //float diferenceRotation = _freeRotation.eulerAngles.y - _CC.transform.eulerAngles.y;
                //float eulerY = 0;
                //if (diferenceRotation < 0 || diferenceRotation > 0)
                //{
                //    eulerY = _freeRotation.eulerAngles.y;
                //}
                //var euler = new Vector3(lastDir.x, lastDir.y, 0);
                //_CC.transform.rotation = Quaternion.Slerp(_CC.transform.rotation, Quaternion.Euler(euler), TurnSpeed * _turnSpeedMultiplier * Time.deltaTime);


                //freeRotEular = _freeRotation.eulerAngles;
                //CC_transform = _CC.transform.eulerAngles;

                //_CC.transform.eulerAngles =
                //_CC.transform.Rotate(Vector3.right * Time.deltaTime * TurnSpeed * lastDir.x);
                //_CC.transform.Rotate(Vector3.forward * Time.deltaTime * TurnSpeed * lastDir.y);
                //_CC.transform.Rotate(euler * Time.deltaTime * TurnSpeed * Mathf;
                
            }
            if (VirtualInputManager.Instance.InputAttr.Move)
            {
                TargetDirModel.y = 0;
                //TargetDirModel = new Vector3(_targetDirection.normalized.x, this.transform.position.y, _targetDirection.normalized.z);
            }
            //Debug.DrawLine(this.transform.position, this.transform.position + _targetDirection.normalized * 3, Color.red);
            BallModel.AnimUpdate(TargetDirModel, RotateSpeed * CharacterSpeed, lastDir, Cones);
        }

        public virtual void UpdateTargetDirection()
        {

            _turnSpeedMultiplier = 1f;
            Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);

            //get the right-facing direction of the referenceTransform
            Vector3 right = Camera.main.transform.TransformDirection(Vector3.right);

            // determine the direction the player will face based on input and the referenceTransform's right and forward directions
            //_targetDirection = VirtualDataInputManager.Instance.AxisFinalInput.x * right + VirtualDataInputManager.Instance.AxisFinalInput.y * forward;
            _targetDirection = lastDir.x * right + lastDir.y * forward;
            _targetDirection.y = 0;
            TargetDirModel = (lastDir.y * right )+ ( lastDir.x * forward * -1);
            TargetDirModel = TargetDirModel.normalized;


            if (_targetDirection.magnitude > 1f)
            {
                _targetDirection = Vector3.ClampMagnitude(_targetDirection, 1.0f);
            }
        }

        //private Vector3 _destProjectile;
        //private float _tempTimeProjectile = 0;
        //private void ShootProjectile()
        //{
        //    _tempTimeProjectile += Time.deltaTime;
        //    if (_tempTimeProjectile >= ProjectileDelays)
        //    {
        //        _tempTimeProjectile = 0;
        //        Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        //        RaycastHit hit;

        //        if (Physics.Raycast(ray, out hit))
        //        {
        //            if (hit.transform.tag == Utilities.TAG.ENEMY.ToString())
        //            {

        //            }
        //            _destProjectile = hit.point;
        //        }
        //        _destProjectile = ray.GetPoint(1000);

        //        InstantiateProjectile(Projectile, LeftFirePoint.position, ProjectileTimeLife);
        //        InstantiateProjectile(Projectile, RightFirePoint.position, ProjectileTimeLife);
        //    }
        //}

        //private void InstantiateProjectile(GameObject projectile, Vector3 pos, float timeLife)
        //{
        //    var projectileObj = Instantiate(projectile, pos, Quaternion.identity) as GameObject;
        //    Destroy(projectileObj, timeLife);
        //}

        private void SubState_GP_3_Update(LibEdStateUtilities.GameSubStates subState)
        {
            switch (subState)
            {

                #region == SubState GP_3_NORMAL_PLAYING ==
                case LibEdStateUtilities.GameSubStates.MAIN_GP_GAMEPLAY_PAUSE:
                    if (LibGameSetting.IsPause)
                    {

                    }
                    break;
                #endregion
                //#region == SubState GP_3_GAMEPLAY_PAUSE ==
                //case LibEdStateUtilities.GameSubStates.GP_3_GAMEPLAY_PAUSE:
                //    if (GameSetting.IsPause)
                //    {

                //    }
                //    break;
                //    #endregion
            }
        }

        #endregion === State Update ===

        #region === State Fixed Update ===
        //private void FixedUpdate()
        //{
        //    StateFunc.StateUpdate(Update_State_FU, StateFunc.StateChanging(StateChanging, SubStateChanging, ChekingAllFind));
        //}

        //private void Update_State_FU()
        //{
        //    switch (VirtualStateManager.Instance.CurState)
        //    {

        //        #region == State GAMEPLAY ==
        //        case LibEdStateUtilities.GameStates.MAIN_GP:
        //            State_Gameplay_FixedUpdate();

        //            break;
        //            #endregion
        //    }
        //}

        //private void State_Gameplay_FixedUpdate()
        //{
        //    if (!GameSetting.IsPause)
        //    {

        //    }
        //}

        #endregion === State Fixed Update ===


        #region === State Late Update ===
        //private void LateUpdate()
        //{
        //    StateFunc.StateUpdate(Update_State_LU, StateFunc.StateChanging(StateChanging, SubStateChanging, ChekingAllFind));
            
        //}

        //private void Update_State_LU()
        //{
        //    switch (VirtualStateManager.Instance.CurState)
        //    {

        //        #region == State GP_3 ==
        //        case VirtualStateManager.GameStates.GP_3:
        //            State_GP_3_LateUpdate();


        //            break;
        //            #endregion
        //    }
        //}

        //private void State_GP_3_LateUpdate()
        //{
        //    if (!GameSetting.IsPause)
        //    {

        //    }
        //}

        #endregion === State Late Update ===


        //public Vector3 GetDirection(Vector3 startPos)
        //{
        //    return _destProjectile - startPos;
        //}

        //public Vector3 GetStartPos(ShooterPosition whichShooterPos)
        //{
        //    switch (whichShooterPos)
        //    {
        //        case ShooterPosition.Right:
        //            return RightFirePoint.position;
        //            break;
        //        case ShooterPosition.Left:
        //            return LeftFirePoint.position;
        //            break;
        //    }
        //    return Vector3.zero;
        //}

        float curY;
        public void SetVelocityAirJump(float vel)
        {
            curY = transform.position.y;
            _velocityAir = vel * JumpGraph.Evaluate(0);
            IsInJump = true;
        }

        public void SetVelocityAirJumpMoementum(float vel)
        {
            _velocityAir = vel * JumpFallMomentumGraph.Evaluate(0);

        }

        public void SetVelocityAir(float vel)
        {
            _velocityAir = vel;
        }

        public float GetVelocityAir()
        {
            return _velocityAir;
        }

        public float GetCurSpeed()
        {
            return _curSpeed;
        }

        public void SetCurSpeed(float curSpeed)
        {
            _curSpeed = curSpeed < 0 ? 0 : curSpeed > CharacterSpeedMAX ? CharacterSpeedMAX : curSpeed;
        }

    }
}