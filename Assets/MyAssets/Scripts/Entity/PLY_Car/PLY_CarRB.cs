using MyBox;
using System;
using System.Collections.Generic;
using UnityEngine;

using Com.GNL.URP_MyLib;
using Photon.Pun;

namespace Com.GNL.URP_MyLibProjectTest
{


    public class PLY_CarRB : MonoBehavMoveObj
    {
        #region === Attributes ===

        [Header("Setting Car Controll")]
        [Header("Waepons Control")]
        [SerializeField] private WeaponsCarController _WeaponCarController;
        [SerializeField] 
        [Min(1)]private int CountWeapon =1;
        [SerializeField] [MyBox.ReadOnly] private ProjectileController[] _ProectilesController;
        [SerializeField] [MyBox.ReadOnly] private WeaponReady _WeaponReady;

        [Header("Motor Control")]
        [SerializeField] private CAR_DRIVE_TYPE _CarDriveType = CAR_DRIVE_TYPE.RearWheelDrive;
        [SerializeField] private float _MotorForce;
        [SerializeField] private float _MotorForceFront;
        [SerializeField] private float ReverseTorque = 500;
        [SerializeField] private float _slipLimitForward = 0.3f;
        [SerializeField] private float _slipLimitSide = 0.3f;
        [SerializeField] private float _addIncMtrForce = 10;
        [SerializeField] private float _decIncMtrForce = 10;
        [Range(0, 1)]
        [SerializeField] private float _tractionControl = 1f; 
        [SerializeField] private SPEED_TYPE _SpeedType = SPEED_TYPE.KPH;
        //[SerializeField] public float MaxRpm = 1000;
        [SerializeField] public float MaxSpeed = 1000;
        [SerializeField] public float MaxBackSpeed = 30;
        [SerializeField] public float BreakTurnAngle = 50;

        //[SerializeField] public float DownForce = 100;

        [Header("Seetting Gear")]
        [SerializeField] private int NoOfGears = 5;
        [SerializeField] private float RevRangeBoundary = 1f;
        [SerializeField] private float _ChangeGearTimes = 1f;
        [Range(0, 2)]
        [SerializeField] private float _GearUpFactorMtr = 1f;
        [Range(0, 2)]
        [SerializeField] private float _GearDownFactorMtr = 1f;

        [Header("Break Control")]
        [SerializeField] private float _BreakForcefront = 800;
        [SerializeField] private float _BreakForceRear = 600;
        [SerializeField] private float _BreakForceIdle; 
        [SerializeField] private float _multyDecIncMtrForceOnBreaking = 10; 

         [Header("Steer Control")]
        [SerializeField] private float _MaxSteerAngle;
        [SerializeField] private float _VelSteerAngle;
        [SerializeField] private float _MinimumSpeedToSlide = 10f;
        [SerializeField] private float _gimbalLock = 10f;
        [SerializeField] private bool _IsUseSteerHelper = false;
        [Range(0, 1)]
        [SerializeField] private float _SteerHelperInit = 0.644f;
        [Range(0, 1)]
        [SerializeField] private float _SteerHelperDrft = 0.644f;
        [Range(0, 1)]
        [SerializeField] private float _AnalogInputRangeForAxisX = 0.4f;

        [Space(10)]
        [Header("Setting Wheel")]
        [SerializeField] private float _SpeedThreshold = 600f;
        [SerializeField] private int _StepsBelowThreshold = 20;
        [SerializeField] private int _StepsAboveThreshold = 20;

        [Header("Wheels SetUp ")]
        [SerializeField] private CarWheelsSetup _CarWheelsSetupInit;
        [SerializeField] private CarWheelsSetup _CarWheelsSetupDrift;

        [Header("Setting Mass Center")]
        [SerializeField] private Transform _T_CenterMass;

        [Header("Wheels Transform")]
        [SerializeField] private Transform _FrontLeftWheelTrans;
        [SerializeField] private Transform _FrontRightWheelTrans;
        [SerializeField] private Transform _RearLeftWheelTrans;
        [SerializeField] private Transform _RearRightWheelTrans;

        [Header("Wheels GameObect ")]
        [SerializeField] private GameObject _FrontLeftWheelCollider;
        [SerializeField] private GameObject _FrontRightWheelCollider;
        [SerializeField] private GameObject _RearLeftWheelCollider;
        [SerializeField] private GameObject _RearRightWheelCollider;

        [Header("CameraTarget")]
        public GameObject CamTarget;


        private Transform[] _wheelTrans;
        private WheelCollider[] _wheelColls;
        private PLY_CarWheelEffect[] _wheelEffects;

        //[Space(10)]
        [Header("Status Control ")]
        //[SerializeField]
        private CAR_SETUP _curCarSetup = CAR_SETUP.INIT;

        //[SerializeField]
        float thrustTorque = 0;
        //[SerializeField]
        private float _curGimbalLock;
        private float _oldRotation;
        private bool _isBreakingFront = false;
        private bool _isBreakingRear = false;
        private float _curBreakForce;
        private float _curSteerAngle;
        private float _curMotorForce = 0;
        private float _finTempSteerAngle = 0;
        //private float _curRpm = 0;
        //private bool _isGas = false;
        private float _AccelInput = 0;
        private float _AxisInput = 0;
        private float _BrakeInput = 0;
        private float _LastAccelInput = 0;
        private float _CurGearNum = 0; 
        private float _GearFactor = 0;
        private float _curChangeGearTimes=0;
        private bool _isChangeGearTime=false;
        private CHANGE_GEAR _StateGeare = CHANGE_GEAR.none;


        public Rigidbody _RB = null;
        public PhotonView _PV = null;

        #endregion === Attributes ===

        #region === Getter Setter ===

        public float GetCurrentSpeed ()
        {
            if(_SpeedType == SPEED_TYPE.KPH)
                return _RB.velocity.magnitude * 2.23693629f;  
            else
                return _RB.velocity.magnitude * 3.6f;
        }

        public float Revs { get; private set; }


        public float GetCurRPM()
        {
            return Vector3.Angle(transform.forward, _RB.velocity);
        }

        public float GetCurMotorForce()
        {
            return _curMotorForce;
        }

        public float GetAccInput()
        {
            return _AccelInput;
        }

        public float GetBrakeInput()
        {
            return _BrakeInput;
        }

        public float GetGear()
        {
            return _CurGearNum;
        }


        #endregion === Getter Setter ===

        #region === Enum attributes ===

        enum WHEEL_POS
        {
            FrontLeft,
            FrontRight,
            RearLeft,
            RearRight
        }

        internal enum CAR_DRIVE_TYPE
        {
            FrontWheelDrive,
            RearWheelDrive,
            FourWheelDrive
        }

        internal enum SPEED_TYPE
        {
            MPH,
            KPH
        }


        private enum WHEEL_FUNCTION
        {
            WHEELS_4,
            WHEELS_FRONT,
            WHEELS_REAR,
        }

        private enum CAR_SETUP
        {
            INIT,
            DRIFT,
        }

        enum CHANGE_GEAR
        {
            none,
            Up,
            Down
        }

        private enum BREAK
        {
            None,
            Front,
            Rear,
            All
        }


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

            _RB = GetComponent<Rigidbody>();

            _PV = GetComponent<PhotonView>();

            _ProectilesController = new ProjectileController[CountWeapon];
            _WeaponReady.IsWeaponSRdy = new bool[CountWeapon - 1];

            

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
            //_cam = Camera.main;
            //_CC = GetComponent<CharacterController>();
            //Projectile.SetActive(true);
            _curChangeGearTimes = _ChangeGearTimes;
            _RB.centerOfMass = _T_CenterMass.localPosition;
            InitWheels();
            CheckWeapon();
        }

        private void InitWheels()
        {
            _wheelColls = new WheelCollider[4];
            _wheelColls[0] = _FrontLeftWheelCollider.GetComponent<WheelCollider>();
            _wheelColls[1] = _FrontRightWheelCollider.GetComponent<WheelCollider>();
            _wheelColls[2] = _RearLeftWheelCollider.GetComponent<WheelCollider>();
            _wheelColls[3] = _RearRightWheelCollider.GetComponent<WheelCollider>();

            _wheelEffects = new PLY_CarWheelEffect[4];
            _wheelEffects[0] = _FrontLeftWheelCollider.GetComponent<PLY_CarWheelEffect>();
            _wheelEffects[1] = _FrontRightWheelCollider.GetComponent<PLY_CarWheelEffect>();
            _wheelEffects[2] = _RearLeftWheelCollider.GetComponent<PLY_CarWheelEffect>();
            _wheelEffects[3] = _RearRightWheelCollider.GetComponent<PLY_CarWheelEffect>();

            _wheelTrans = new Transform[4];
            _wheelTrans[0] = _FrontLeftWheelTrans;
            _wheelTrans[1] = _FrontRightWheelTrans;
            _wheelTrans[2] = _RearLeftWheelTrans;
            _wheelTrans[3] = _RearRightWheelTrans;


            for (int i = 0; i < _wheelColls.Length; i++)
            {
                CarWheelSetup(_wheelColls[i],_CarWheelsSetupInit); 
                WheelColliderConfig(_wheelColls[i]);
            }
        }


        public void CheckWeapon()
        {
            //if (!_WeaponReady.IsWeaponNormalRdy)
            //{
            //    Debug.Log("cekcekcek GetCountWeapon:" + _WeaponCarController.GetCountWeapon());
            //    if(_WeaponCarController.GetCountWeapon()>0)
            //    { 
            //        _WeaponReady.IsWeaponNormalRdy = true;
            //        _ProectilesController[0] =  _WeaponCarController.GetWeapon(0).transform.GetChild(0).GetComponent<ProjectileController>();
                    
            //        for (int i = 1; i < _WeaponCarController.GetCountWeapon(); i++)
            //        {
            //            _WeaponReady.IsWeaponSRdy[i - 1] = true;
            //            _ProectilesController[i] = _WeaponCarController.GetWeapon(i).transform.GetChild(0).GetComponent<ProjectileController>();
            //        }
            //    }
            //}
        }

        private void CarWheelSetup(WheelCollider wheelCollider, CarWheelsSetup _CarWheelsSetupInit)
        {
            wheelCollider.forceAppPointDistance = _CarWheelsSetupInit.ForceAppPointDistance;

            JointSpring _tempSusSpring = new JointSpring();
            _tempSusSpring.spring = _CarWheelsSetupInit.SuspensionSpring.Spring;
            _tempSusSpring.damper = _CarWheelsSetupInit.SuspensionSpring.Damper;
            _tempSusSpring.targetPosition = _CarWheelsSetupInit.SuspensionSpring.TargetPosition;

            wheelCollider.suspensionSpring = _tempSusSpring;

            WheelFrictionCurve _tempForwardFriction = new WheelFrictionCurve();
            _tempForwardFriction.extremumSlip = _CarWheelsSetupInit.FrictionForward.ExtrenumSlip;
            _tempForwardFriction.extremumValue = _CarWheelsSetupInit.FrictionForward.ExtrenumValue;
            _tempForwardFriction.asymptoteSlip = _CarWheelsSetupInit.FrictionForward.AsymptoteSlip;
            _tempForwardFriction.asymptoteValue = _CarWheelsSetupInit.FrictionForward.AsymptoteValue;
            _tempForwardFriction.stiffness = _CarWheelsSetupInit.FrictionForward.Stiffness;

            wheelCollider.forwardFriction = _tempForwardFriction;

            WheelFrictionCurve _tempSidewaysFriction = new WheelFrictionCurve();
            _tempSidewaysFriction.extremumSlip = _CarWheelsSetupInit.FrictionForward.ExtrenumSlip;
            _tempSidewaysFriction.extremumValue = _CarWheelsSetupInit.FrictionForward.ExtrenumValue;
            _tempSidewaysFriction.asymptoteSlip = _CarWheelsSetupInit.FrictionForward.AsymptoteSlip;
            _tempSidewaysFriction.asymptoteValue = _CarWheelsSetupInit.FrictionForward.AsymptoteValue;
            _tempSidewaysFriction.stiffness = _CarWheelsSetupInit.FrictionForward.Stiffness;

            wheelCollider.sidewaysFriction = _tempSidewaysFriction;
        }

        private void WheelColliderConfig(WheelCollider wheel)
        {
            wheel.ConfigureVehicleSubsteps(_SpeedThreshold, _StepsBelowThreshold, _StepsAboveThreshold);
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
                    if (!Formulation.GetMultiPlayer() || Formulation.IsPhotonViewMine(_PV))
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
                    if (!Formulation.GetMultiPlayer() || Formulation.IsPhotonViewMine(_PV) )
                    { 
                        State_MAIN_GP_FixedUpdate();
                    }
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

            //if (VirtualInputManager.Instance.InputAttr.ResetPlayer)
            //{
            //    this.transform.rotation = Utilities.SpawnerTransform.rotation;
            //    this.transform.position = Utilities.SpawnerTransform.position;
            //}
            //if (VirtualInputManager.Instance.InputAttr.BreakFront)
            //{
            //    _isBreakingFront = true;
            //}
            //else
            //{
            //    _isBreakingFront = false;
            //}

            //if (VirtualInputManager.Instance.InputAttr.BreakRear)
            //{
            //    _isBreakingRear = true;
            //}
            //else
            //{
            //    _isBreakingRear = false;
            //}
            //if (VirtualDataInputManager.Instance.AxisFinalInput.x > _AnalogInputRangeForAxisX)
            //{
            //    _AxisInput = VirtualDataInputManager.Instance.AxisFinalInput.x;
            //}
            //else if (VirtualDataInputManager.Instance.AxisFinalInput.x < -_AnalogInputRangeForAxisX)
            //{
            //    _AxisInput = VirtualDataInputManager.Instance.AxisFinalInput.x;
            //}
            //else 
            //{
            //    _AxisInput = 0;
            //}


            //if (VirtualInputManager.Instance.InputAttr.MoveForward  || VirtualInputManager.Instance.InputAttr.Gas)
            //{
            //    _AccelInput = 1;
            //    _BrakeInput = 0;
            //}
            //else if (VirtualInputManager.Instance.InputAttr.MoveBack )
            //{
            //    _AccelInput = -1;
            //    _BrakeInput = -1;
            //}
            //else
            //{
            //    _AccelInput = 0;
            //    _BrakeInput = 0;
            //}

            //if (VirtualInputManager.Instance.InputAttr.Reverse)
            //{
            //    _AccelInput *= -1;
            //}

        }

        private bool IsUseAttackNormal;

        private void AttackInput()
        {
            //if (VirtualInputManager.Instance.InputAttr.AttackNormal)
            //{
            //    IsUseAttackNormal = true;
            //}
            //else
            //{
            //    IsUseAttackNormal = false;
            //}
        }


        private void Locomotion()
        {

        }


        private void Attacking()
        {
            if (_WeaponReady.IsWeaponNormalRdy)
            {
                _ProectilesController[0].SetIsShoot(IsUseAttackNormal);
            }
        }


        private void Locomotion_FU()
        {


            HandleSteering(_AxisInput);

            if (!_isChangeGearTime)
                ApplyDrive(_AccelInput, _BrakeInput, CHANGE_GEAR.none);
            CapSpeed();

            BreakingSystem(_BrakeInput);

            CalculateRevs();
            GearChanging();

            CheckForWheelSpin();
            if (!_isChangeGearTime)
                TractionControl();

            UpdateWheels();
            ExitUpdateCondition();
            
        }

        private void ExitUpdateCondition()
        {
            if (_AccelInput != 0)
                _LastAccelInput = _AccelInput;

            if (_curChangeGearTimes < _ChangeGearTimes)
            {
                _isChangeGearTime = true;
                _curChangeGearTimes += Time.fixedDeltaTime;
            }
            else
            {
                _isChangeGearTime = false;
                _StateGeare = CHANGE_GEAR.none;
            }
        }


        private void ApplyDrive(float accel, float BrakeInput, CHANGE_GEAR changeGear = CHANGE_GEAR.none)
        {
            bool isChangeGear = false;
            switch (changeGear)
            {
                case CHANGE_GEAR.Up:
                    _curMotorForce *= _GearUpFactorMtr;
                    //_RB.velocity = _RB.velocity-(_RB.velocity*(2/10));
                    isChangeGear = true;
                    break;
                case CHANGE_GEAR.Down:
                    _curMotorForce *= _GearDownFactorMtr;
                    //_RB.velocity = _RB.velocity - (_RB.velocity * (2 / 10));
                    isChangeGear = true;
                    break;
            }
            switch (_CarDriveType)
            {
                case CAR_DRIVE_TYPE.FourWheelDrive:
                    
                    thrustTorque = accel * (_curMotorForce / 4f);
                    for (int i = 0; i < 4; i++)
                    {
                        _wheelColls[i].motorTorque = thrustTorque;
                        if (isChangeGear)
                        {
                            //if (i < 2)
                            //    _wheelColls[i].brakeTorque = _BreakForcefront / 8;
                            //else
                            //    _wheelColls[i].brakeTorque = _BreakForceRear / 8;
                        }
                    }
                    break;

                case CAR_DRIVE_TYPE.FrontWheelDrive:
                   
                    thrustTorque = accel * (_curMotorForce / 2f) ;
                    _wheelColls[0].motorTorque = _wheelColls[1].motorTorque = thrustTorque;
                    if (isChangeGear)
                    {
                        //_wheelColls[0].brakeTorque = _wheelColls[1].brakeTorque = _BreakForcefront / 8;
                    }
                    break;

                case CAR_DRIVE_TYPE.RearWheelDrive:
                    
                    thrustTorque = accel * (_curMotorForce / 2f) ;
                    _wheelColls[2].motorTorque = _wheelColls[3].motorTorque = thrustTorque;
                    if (isChangeGear)
                    {
                        //_wheelColls[2].brakeTorque = _wheelColls[3].brakeTorque = _BreakForceRear / 8;
                    }
                    break;

            }

            

        }

        private void CapSpeed()
        {
            float speed = _RB.velocity.magnitude;
            switch (_SpeedType)
            {
                case SPEED_TYPE.MPH:

                    speed *= 2.23693629f;
                    if (_AccelInput >= 0 || _LastAccelInput >= 0)
                    {
                        if (speed > MaxSpeed)
                        { 
                            _RB.velocity = (MaxSpeed / 2.23693629f) * _RB.velocity.normalized;
                            _curMotorForce = 0;
                        }
                    }
                    else 
                    {
                        if (speed > MaxBackSpeed)
                        {
                            _RB.velocity = (MaxBackSpeed / 2.23693629f) * _RB.velocity.normalized;
                            _curMotorForce = 0;
                        }
                    }
                    break;

                case SPEED_TYPE.KPH:
                    speed *= 3.6f;
                    if (_AccelInput >= 0 || _LastAccelInput >= 0)
                    {
                        if (speed > MaxSpeed)
                        {
                            _RB.velocity = (MaxSpeed / 3.6f) * _RB.velocity.normalized;
                            _curMotorForce = 0;
                        }
                    }
                    else
                    {
                        if (speed > MaxBackSpeed)
                        { 
                            _RB.velocity = (MaxBackSpeed / 3.6f) * _RB.velocity.normalized;
                            _curMotorForce = 0;
                        }
                    }
                    break;
            }
        }



        private void BreakingSystem(float brakeInput)
        {
            // Breakig System
            if (_isBreakingFront)
            {
                _curBreakForce = _BreakForcefront;
                ApplyBreaking(BREAK.Front, _BreakForcefront);
            }
            if (_isBreakingRear)
            {
                _curBreakForce = _BreakForceRear;
                ApplyBreaking(BREAK.Rear, _BreakForceRear);
            }

            if (!_isBreakingFront && !_isBreakingRear && _AccelInput == 0)
            {
                _curBreakForce = _BreakForceIdle;
                ApplyBreaking(BREAK.All, _curBreakForce);
            }
            //else if (brakeInput>0)
            //{
            //    if( _CarDriveType ==  CAR_DRIVE_TYPE.FourWheelDrive)
            //    {
            //        _curBreakForce = _BreakForceRear;
            //        ApplyBreaking(BREAK.All, _curBreakForce);
            //    }
            //    if (_CarDriveType == CAR_DRIVE_TYPE.RearWheelDrive)
            //    {
            //        _curBreakForce = _BreakForceRear;
            //        ApplyBreaking(BREAK.All, _curBreakForce);
            //    }
            //    if (_CarDriveType == CAR_DRIVE_TYPE.FourWheelDrive)
            //    {
            //        _curBreakForce = _BreakForceRear;
            //        ApplyBreaking(BREAK.All, _curBreakForce);
            //    }
            //    ApplyBreaking(BREAK.All, _curBreakForce);
            //    ApplyBreaking(BREAK.All, _curBreakForce);
            //}
            else
            if (!_isBreakingFront && !_isBreakingRear)
            {
                _curBreakForce = 0f;
                ApplyBreaking(BREAK.None, 0);
            }
        }

        private void ApplyBreaking(BREAK flag, float breakVal)
        {
            //bool IsDrift = false;
            if (flag == BREAK.Front)
            {
                for (int i = 0; i < 2; i++)
                {
                    _wheelColls[i].brakeTorque = breakVal;
                    if (Vector3.Angle(transform.forward, _RB.velocity) > BreakTurnAngle && _wheelColls[i].motorTorque != 0)
                    {
                        //IsDrift = true;
                        //_wheelColls[i].motorTorque = _wheelColls[i].motorTorque / 2;
                        _wheelColls[i].motorTorque = 0;
                    }
                    //if (GetCurrentSpeed() > 5 && Vector3.Angle(transform.forward, _RB.velocity) < 50f)
                    //{
                    //    _wheelColls[i].brakeTorque = breakVal;
                    //}
                    //else if (breakVal > 0)
                    //{
                    //    _wheelColls[i].brakeTorque = 0f;
                    //    //_wheelColls[i].motorTorque = -ReverseTorque;
                    //    _wheelColls[i].motorTorque = 0;
                    //}
                }

                CarSetup(WHEEL_FUNCTION.WHEELS_4, CAR_SETUP.DRIFT);
            }
            else if (flag == BREAK.Rear)
            {
                for (int i = 2; i < 4; i++)
                {
                    _wheelColls[i].brakeTorque = breakVal;
                    if (Vector3.Angle(transform.forward, _RB.velocity) > BreakTurnAngle && _wheelColls[i].motorTorque != 0)
                    {
                        //IsDrift = true;
                        //_wheelColls[i].motorTorque = _wheelColls[i].motorTorque / 2;
                        _wheelColls[i].motorTorque = 0;
                    }
                    //if (GetCurrentSpeed() > 5 && Vector3.Angle(transform.forward, _RB.velocity) < 50f)
                    //{
                    //    _wheelColls[i].brakeTorque = breakVal;
                    //}
                    //else if (breakVal > 0)
                    //{
                    //    _wheelColls[i].brakeTorque = 0f;
                    //    //_wheelColls[i].motorTorque = -ReverseTorque;
                    //    _wheelColls[i].motorTorque = 0;
                    //}
                }
                CarSetup(WHEEL_FUNCTION.WHEELS_4, CAR_SETUP.DRIFT);
            }
            else if (flag == BREAK.All)
            {
                for (int i = 0; i < 4; i++)
                {
                    _wheelColls[i].brakeTorque = breakVal;
                    if (Vector3.Angle(transform.forward, _RB.velocity) > BreakTurnAngle && _wheelColls[i].motorTorque !=0)
                    {
                        //IsDrift = true;
                        //_wheelColls[i].motorTorque = _wheelColls[i].motorTorque / 2;
                        _wheelColls[i].motorTorque = 0;
                    }
                    //if (GetCurrentSpeed() > 5 && Vector3.Angle(transform.forward, _RB.velocity) < 50f)
                    //{
                    //    _wheelColls[i].brakeTorque = breakVal;
                    //    if (_curMotorForce >= 0)
                    //    {
                    //        _curMotorForce = 0;
                    //    }
                    //}
                    //else if(breakVal > 0)
                    //{
                    //    _wheelColls[i].brakeTorque = 0f;
                    //    //_wheelColls[i].motorTorque = -ReverseTorque;
                    //    _wheelColls[i].motorTorque = 0;
                    //}
                }
                CarSetup(WHEEL_FUNCTION.WHEELS_4, CAR_SETUP.INIT);
            }
            else
            {
                
                for (int i = 0; i < 4; i++)
                {
                    //if (Vector3.Angle(transform.forward, _RB.velocity) > BreakTurnAngle && _wheelColls[i].motorTorque != 0)
                    //{
                    //    IsDrift = true;
                    //    _wheelColls[i].motorTorque = _wheelColls[i].motorTorque / 2;
                    //}
                    _wheelColls[i].brakeTorque = 0;
                }
                CarSetup(WHEEL_FUNCTION.WHEELS_4, CAR_SETUP.INIT);
            }
            //if (!IsDrift)
            //    CarSetup(WHEEL_FUNCTION.WHEELS_4, CAR_SETUP.INIT);
            //else
            //    CarSetup(WHEEL_FUNCTION.WHEELS_4, CAR_SETUP.DRIFT);
            SpeedBreakingApply();


        }

        private void SpeedBreakingApply()
        {
            if(Vector3.Angle(transform.forward, _RB.velocity) > BreakTurnAngle)
            {
                //if (GetCurrentSpeed() >= 20 && GetCurrentSpeed() <= 40)
                //{
                    if (_isBreakingFront)
                    {
                        _wheelColls[(int)WHEEL_POS.FrontLeft].motorTorque = 0;
                        _wheelColls[(int)WHEEL_POS.FrontRight].motorTorque = 0;
                    }
                    if (_isBreakingRear)
                    {
                        _wheelColls[(int)WHEEL_POS.RearLeft].motorTorque = 0;
                        _wheelColls[(int)WHEEL_POS.RearRight].motorTorque = 0;
                    }
                //}
                //else if(GetCurrentSpeed() != 0)
                //{
                //    if (_isBreakingFront)
                //    {
                //        _wheelColls[(int)WHEEL_POS.FrontLeft].motorTorque = _wheelColls[(int)WHEEL_POS.FrontLeft].motorTorque / 2;
                //        _wheelColls[(int)WHEEL_POS.FrontRight].motorTorque = _wheelColls[(int)WHEEL_POS.FrontRight].motorTorque / 2;
                //    }
                //    if (_isBreakingRear)
                //    {
                //        _wheelColls[(int)WHEEL_POS.RearLeft].motorTorque = _wheelColls[(int)WHEEL_POS.RearLeft].motorTorque / 2;
                //        _wheelColls[(int)WHEEL_POS.RearRight].motorTorque = _wheelColls[(int)WHEEL_POS.RearLeft].motorTorque / 2;
                //    }
                //}
            }
        }


        private void TractionControl()
        {
            WheelHit wheelHit;
            switch (_CarDriveType)
            {
                case CAR_DRIVE_TYPE.FourWheelDrive:
                    // loop through all wheels
                    for (int i = 0; i < 4; i++)
                    {
                        _wheelColls[i].GetGroundHit(out wheelHit);

                        AdjustTorque(wheelHit.forwardSlip);
                    }
                    break;

                case CAR_DRIVE_TYPE.RearWheelDrive:
                    _wheelColls[2].GetGroundHit(out wheelHit);
                    AdjustTorque(wheelHit.forwardSlip);

                    _wheelColls[3].GetGroundHit(out wheelHit);
                    AdjustTorque(wheelHit.forwardSlip);
                    break;

                case CAR_DRIVE_TYPE.FrontWheelDrive:
                    _wheelColls[0].GetGroundHit(out wheelHit);
                    AdjustTorque(wheelHit.forwardSlip);

                    _wheelColls[1].GetGroundHit(out wheelHit);
                    AdjustTorque(wheelHit.forwardSlip);
                    break;
            }
        }


        private void AdjustTorque(float forwardSlip)
        {
            if (_isBreakingFront || _isBreakingRear)
            {
                _curMotorForce -= (_decIncMtrForce * _multyDecIncMtrForceOnBreaking);
                if (_curMotorForce < 0)
                {
                    _curMotorForce = 0;
                }
            }
            else
            if (forwardSlip >= _slipLimitForward && _curMotorForce >= 0 && _AccelInput!=0)
            {
                _curMotorForce -= _decIncMtrForce * _tractionControl;
                if (_curMotorForce < 0)
                {
                    _curMotorForce = 0;
                }
            }
            else if(_AccelInput > 0)
            {
                _curMotorForce += _addIncMtrForce * _tractionControl;
                if (_curMotorForce > _MotorForce)
                {
                    _curMotorForce = _MotorForce;
                }
            }
            else if (_AccelInput < 0 && _CurGearNum == 0)
            {
                _curMotorForce += _addIncMtrForce * _tractionControl;
                if (_curMotorForce > ReverseTorque)
                {
                    _curMotorForce = ReverseTorque;
                }
            }
            else if (_curMotorForce > 0 && _AccelInput == 0)
            {
                _curMotorForce -= _decIncMtrForce * _tractionControl;
                if (_curMotorForce < 0)
                {
                    _curMotorForce = 0;
                }
            }
        }


        private void CarSetup(WHEEL_FUNCTION wheelF = WHEEL_FUNCTION.WHEELS_4, CAR_SETUP carSetup = CAR_SETUP.INIT)
        {
            if (_curCarSetup != carSetup)
            {
                _curCarSetup = carSetup;
                CarWheelsSetup tempSetUp = new CarWheelsSetup();
                switch (carSetup)
                {
                    case CAR_SETUP.INIT:
                        tempSetUp = _CarWheelsSetupInit;
                        break;
                    case CAR_SETUP.DRIFT:
                        tempSetUp = _CarWheelsSetupDrift;
                        break;
                }

                switch (wheelF)
                {
                    case WHEEL_FUNCTION.WHEELS_4:
                        for (int i = 0; i < _wheelColls.Length; i++)
                        {
                            CarWheelSetup(_wheelColls[i], tempSetUp);
                        }
                        break;
                    case WHEEL_FUNCTION.WHEELS_FRONT:
                        for (int i = 0; i < _wheelColls.Length - 2; i++)
                        {
                            CarWheelSetup(_wheelColls[i], tempSetUp);
                        }
                        break;
                    case WHEEL_FUNCTION.WHEELS_REAR:
                        for (int i = 2; i < _wheelColls.Length; i++)
                        {
                            CarWheelSetup(_wheelColls[i], tempSetUp);
                        }
                        break;
                }
            }
        }

        //private void FunctionedWheel(CAR_SETUP carSetup)
        //{
        //    switch (carSetup)
        //    {
        //        case CAR_SETUP.INIT:
        //            for (int i = 0; i < _wheelColls.Length; i++)
        //            {
        //                CarWheelSetup(_wheelColls[i], _CarWheelsSetupInit);
        //            }
        //            break;
        //    }
        //}

        

        private void HandleSteering(float axisInput)
        {
            _finTempSteerAngle = _MaxSteerAngle * axisInput;
            if (_curSteerAngle >= _finTempSteerAngle)
            {
                _curSteerAngle -= _VelSteerAngle * Time.fixedDeltaTime;
                if (_curSteerAngle < _finTempSteerAngle)
                    _curSteerAngle = _finTempSteerAngle;
            }
            else if (_curSteerAngle <= _finTempSteerAngle)
            {
                _curSteerAngle += _VelSteerAngle * Time.fixedDeltaTime;
                if (_curSteerAngle > _finTempSteerAngle)
                    _curSteerAngle = _finTempSteerAngle;
            }

            _wheelColls[(int)WHEEL_POS.FrontLeft].steerAngle = _curSteerAngle;
            _wheelColls[(int)WHEEL_POS.FrontRight].steerAngle = _curSteerAngle;

            SteerHelper(_curCarSetup);

        }

        

        private void SteerHelper(CAR_SETUP carSetup)
        {

            float tempSteerHelper = 0f;
            switch (carSetup)
            { 
                case CAR_SETUP.INIT:
                    tempSteerHelper = _SteerHelperInit;
                    break;
                case CAR_SETUP.DRIFT:
                    tempSteerHelper = _SteerHelperDrft;
                    break;
            }
            _curGimbalLock = Mathf.Abs(_oldRotation - transform.eulerAngles.y);
            if (_IsUseSteerHelper)
            {
                for (int i = 0; i < _wheelColls.Length; i++)
                {
                    WheelHit wheelhit;
                    _wheelColls[i].GetGroundHit(out wheelhit);
                    if (wheelhit.normal == Vector3.zero)
                        return; // wheels arent on the ground so dont realign the rigidbody velocity
                }

                // this if is needed to avoid gimbal lock problems that will make the car suddenly shift direction
                
                if (_curGimbalLock < _gimbalLock)
                {
                    var turnadjust = (transform.eulerAngles.y - _oldRotation) * tempSteerHelper;
                    Quaternion velRotation = Quaternion.AngleAxis(turnadjust, Vector3.up);
                    _RB.velocity = velRotation * _RB.velocity;
                }
                _oldRotation = transform.eulerAngles.y;
            }
        }


        private void UpdateWheels()
        {
            for (int i = 0; i < _wheelColls.Length; i++)
            {
                UpdateSingle(_wheelColls[i], _wheelTrans[i]);
            }
        }


        private void UpdateSingle(WheelCollider wheelCollider, Transform wheelTransform)
        {
            Vector3 pos;
            Quaternion rot;
            wheelCollider.GetWorldPose(out pos, out rot);
            wheelTransform.rotation = rot;
            wheelTransform.position = pos;
        }

        private void GearChanging()
        {
            float f = Mathf.Abs(GetCurrentSpeed() / MaxSpeed);
            float upgearlimit = (1 / (float)NoOfGears) * (_CurGearNum + 1);
            float downgearlimit = (1 / (float)NoOfGears) * _CurGearNum;

            if (_CurGearNum > 0 && f < downgearlimit)
            {
                _CurGearNum--;
                _StateGeare = CHANGE_GEAR.Down;
                ApplyDrive(_AccelInput, 0, _StateGeare);
                _curChangeGearTimes = 0;
            }
            if (f > upgearlimit && (_CurGearNum < (NoOfGears - 1)))
            {
                _CurGearNum++;
                _StateGeare = CHANGE_GEAR.Up;
                ApplyDrive(_AccelInput, 0, _StateGeare);
                _curChangeGearTimes = 0;
            }
        }


        // simple function to add a curved bias towards 1 for a value in the 0-1 range
        private static float CurveFactor(float factor)
        {
            return 1 - (1 - factor) * (1 - factor);
        }


        // unclamped version of Lerp, to allow value to exceed the from-to range
        static float ULerp(float from, float to, float value)
        {
            return (1.0f - value) * from + value * to;
        }


        private void CalculateGearFactor()
        {
            float f = (1 / (float)NoOfGears);
            // gear factor is a normalised representation of the current speed within the current gear's range of speeds.
            // We smooth towards the 'target' gear factor, so that revs don't instantly snap up or down when changing gear.
            var targetGearFactor = Mathf.InverseLerp(f * _CurGearNum, f * (_CurGearNum + 1), Mathf.Abs(GetCurrentSpeed() / MaxSpeed));
                _GearFactor = Mathf.Lerp(_GearFactor, targetGearFactor, Time.deltaTime * 5f);
        }


        private void CalculateRevs()
        {
            // calculate engine revs (for display / sound)
            // (this is done in retrospect - revs are not used in force/power calculations)
            CalculateGearFactor();
            var gearNumFactor = _CurGearNum / (float)NoOfGears;
            var revsRangeMin = ULerp(0f, RevRangeBoundary, CurveFactor(gearNumFactor));
            var revsRangeMax = ULerp(RevRangeBoundary, 1f, gearNumFactor);
            Revs = ULerp(revsRangeMin, revsRangeMax, _GearFactor);
        }


        private void LocomoptionAir()
        {

        }


        private void Animation()
        {

        }




        // checks if the wheels are spinning and is so does three things
        // 1) emits particles
        // 2) plays tiure skidding sounds
        // 3) leaves skidmarks on the ground
        // these effects are controlled through the WheelEffects class
        private void CheckForWheelSpin()
        {
            // loop through all wheels
            for (int i = 0; i < 4; i++)
            {
                WheelHit wheelHit;
                _wheelColls[i].GetGroundHit(out wheelHit);

                // is the tire slipping above the given threshhold
                if ((GetCurrentSpeed() > _MinimumSpeedToSlide) && (Mathf.Abs(wheelHit.forwardSlip) >= _slipLimitForward || Mathf.Abs(wheelHit.sidewaysSlip) >= _slipLimitSide))
                {
                    _wheelEffects[i].EmitTyreSmoke(wheelHit);

                    // avoiding all four tires screeching at the same time
                    // if they do it can lead to some strange audio artefacts
                    if (!AnySkidSoundPlaying())
                    {
                        _wheelEffects[i].PlayAudio();
                    }
                    continue;
                }

                // if it wasnt slipping stop all the audio
                if (_wheelEffects[i].PlayingAudio)
                {
                    _wheelEffects[i].StopAudio();
                }
                // end the trail generation
                _wheelEffects[i].EndSkidTrail();
            }
        }

        private bool AnySkidSoundPlaying()
        {
            for (int i = 0; i < 4; i++)
            {
                if (_wheelEffects[i].PlayingAudio)
                {
                    return true;
                }
            }
            return false;
        }

        

        #endregion === Other Function ===
    }

    #region === Other Class ===
    [Serializable]
    public class WeaponReady
    {
        public bool IsWeaponNormalRdy;
        public bool[] IsWeaponSRdy;
    }

    [Serializable]
    public class CarWheelsSetup
    {
        public float Mass = 20;
        public float Radius = 0.5f;
        public float WheelDampingRate = 0.25f;
        public float SuspensionDistance = 0.3f;
        public float ForceAppPointDistance = 0;
        public Vector3 Center = new Vector3(0, 0, 0);
        public Suspension_Spring SuspensionSpring;
        public Friction_Forward FrictionForward;
        public Friction_Sideways FrictionSideways;
    }
    [Serializable]
    public class Suspension_Spring
    {
        public float Spring = 35000;
        public float Damper = 4500;
        public float TargetPosition = 0.5f;
    }
    [Serializable]
    public class Friction_Forward
    {
        public float ExtrenumSlip= 0.4f;
        public float ExtrenumValue = 1;
        public float AsymptoteSlip = 0.8f;
        public float AsymptoteValue = 0.5f;
        public float Stiffness =1;
    }
    [Serializable]
    public class Friction_Sideways
    {
        public float ExtrenumSlip = 0.2f;
        public float ExtrenumValue = 1;
        public float AsymptoteSlip = 0.5f;
        public float AsymptoteValue = 0.75f;
        public float Stiffness = 1;
    }

    #endregion === Other Class ===
}