
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.GNL.URP_MyLib
{
    public class LibMasterGameController : LibSingletonController<LibMasterGameController>
    {
        //[HideInInspector] public int CurrentTab;
        //public string ToolbarTab;
        #region === Public Properety ===

        //public string Tab1;
        [Header("Game Setting Always update?")]
        public bool IsUpdateChanging = true;

        [Header("Game Setting")]
        [Tooltip("Default value will use pc setting")]
        public float Gravity_PC = 0.3f;
        public float Gravity_Android = 1.7f;

        [Header("Aplication Target FrameRate")]
        public int FrameRates_PC = 60;
        public int FrameRates_Android = 30;

        //public string Tab2;
        [Header("Slow Mode")]
        public bool IsUseSlowMode = false;
        [Range(0, 1)]
        [Tooltip("0-1 range, 1 is max of slowmo, the game like paused")]
        public float SlowModeVal = 0f;
        [Range(0, 0.1f)]
        public float MulTimeFixedDT = 0.01666667f;

        //[Header("Blur Test")]
        //public bool IsUseBlurTest;
        //public float BlurTestGaussianStart = 0;
        //public float BlurTestGaussianEnd = 0;

        //public string Tab3;

        [Header("UI Debug")]
        [Tooltip("Like showing FPS and etc")]
        public bool IsUsingUnityDebug;

        [Tooltip("IsUsingUnityDebug false, then it's automaticly false too")]
        public bool IsUsingUnityDebugToPlatform;
        public bool IsForAndroid;
        public bool IsForWindows;


        [Header("Wanna test something android in unity player?")]
        [Tooltip("Always false for run in android")]
        public bool IsUnityPlayerUseAndroidUI;
        public bool IsUnityPlayerUseAndroidPreRender;

        [Header("Just Test in Unity Editor")]
        public bool IsJustTestInUnityEditor;

        //public string Tab4;
        [Header("Reference Global Setting")]
        [LibReadOnly] public GameObject LibGlobalSetting;

        //[Header("Reference Player")]
        //[LibReadOnly] public GameObject _PC;

        //[Header("SceneController")]
        //public ScenesController ScnController;


        //[Header("TEST Instantiate")]
        //public GameObject prefabAddBox;
        //public GameObject prefabAddRobot;

        //public string TabEnd;
        #endregion === Public Properety ===

        #region === Renderer Global Volume Properety ===

        public GameObject LibGO_GlobalVolume;

        //private DepthOfField GV_DepthOfField;

        #endregion === Renderer  Global Volume Properety ===


        private float _curSlowModeVal = 0;
        private float _curMulTimeFixedDT = 0;
        public bool ItsNeedTobePauseWhenPause { get; set; }

        private float _defaultSlowModeVal = 0f;
        [LibReadOnly] [SerializeField] private float _defaultMulTimeFixedDT = 0.01666667f;


        private void OnValidate()
        {
            if (IsUseSlowMode)
            {
                _curSlowModeVal = SlowModeVal;
                _curMulTimeFixedDT = MulTimeFixedDT;
            }
            else
            {
                _curSlowModeVal = _defaultSlowModeVal;
                _curMulTimeFixedDT = 0.01666667f;
            }
        }

        private void Awake()
        {
            InitiationAwake();
        }

        //bool StateFunc.IsFindAll = false;
        private void Start()
        {

            StateFunc.ClearState();
        }

        private void InitiationAwake()
        {
            //Debug.Log("cekcekcek fxDelta:" + Time.fixedDeltaTime);
            ItsNeedTobePauseWhenPause = false;
            _defaultMulTimeFixedDT = Time.fixedDeltaTime;


            if (Application.platform == RuntimePlatform.Android)
            {
                LibGameSetting.IsPlatformAndroid = true;
            }

            if (Application.platform == RuntimePlatform.WindowsEditor
                || Application.platform == RuntimePlatform.WindowsPlayer
                || Application.platform == RuntimePlatform.WSAPlayerARM
                || Application.platform == RuntimePlatform.WSAPlayerX64
                || Application.platform == RuntimePlatform.WSAPlayerX86)
            {
                LibGameSetting.IsPlatformWindows = true;
            }

            if (IsUsingUnityDebug)
            {
                LibGameSetting.debugScreen = true;
            }
            else
            {
                IsUsingUnityDebugToPlatform = false;
                LibGameSetting.debugScreen = false;
            }


#if UNITY_EDITOR
            IsUsingUnityDebugToPlatform = false;
#elif UNITY_STANDALONE || UNITY_WSA
            IsForAndroid = false;
#elif UNITY_ANDROID
            IsForWindows = false;
#endif

            LibGameSetting.IsUsingUnityDebug = IsUsingUnityDebug;
            LibGameSetting.IsUsingUnityDebugToPlatform = IsUsingUnityDebugToPlatform;
            LibGameSetting.IsForAndroid = IsForAndroid;
            LibGameSetting.IsForWindows = IsForWindows;


            if (LibGameSetting.IsPlatformWindows
#if UNITY_EDITOR
                        && !LibGameSetting.IsUnityPlayerUseAndroidUI
#endif
                )
            {
                Application.targetFrameRate = FrameRates_PC;
                LibGameSetting.Gravity = Gravity_PC;

            }
            else if (LibGameSetting.IsPlatformAndroid
#if UNITY_EDITOR
                        || LibGameSetting.IsUnityPlayerUseAndroidUI
#endif
                        )
            {
                Application.targetFrameRate = FrameRates_Android;
                LibGameSetting.Gravity = Gravity_Android;
            }
            else
            {
                Application.targetFrameRate = FrameRates_PC;
                LibGameSetting.Gravity = Gravity_PC;
            }


           

#if UNITY_EDITOR
            if (LibGameSetting.IsPlatformAndroid)
            {
                IsUnityPlayerUseAndroidUI = false;
            }

            LibGameSetting.IsUnityPlayerUseAndroidPreRender = IsUnityPlayerUseAndroidPreRender;
            LibGameSetting.IsUnityPlayerUseAndroidUI = IsUnityPlayerUseAndroidUI;


            //LibGameSetting.IsJustTestInUnityEditor = IsJustTestInUnityEditor;
#endif
        }

        private void LibGameSettingUpdate()
        {
            if(IsUpdateChanging)
            { 
                #region === Game Setting Awake ===
                if (LibGameSetting.IsPlatformWindows
#if UNITY_EDITOR
                            && !LibGameSetting.IsUnityPlayerUseAndroidUI
#endif
                    )
                {
                    LibGameSetting.Gravity = Gravity_PC;
                    Application.targetFrameRate = FrameRates_PC;
                }
                else if (LibGameSetting.IsPlatformAndroid
#if UNITY_EDITOR
                            || LibGameSetting.IsUnityPlayerUseAndroidUI
#endif
                            )
                {
                    LibGameSetting.Gravity = Gravity_Android;
                    Application.targetFrameRate = FrameRates_Android;
                }
                else
                {
                    LibGameSetting.Gravity = Gravity_PC;
                    Application.targetFrameRate = FrameRates_PC;
                }
                #endregion
            }
        }



        #region  === State Changing ===
        public virtual void StateChanging()
        {
            
        }

        public virtual void SubStateChanging()
        {
            
        }

        public override void CheckingAllfind()
        {
           

        }

        //public void FindInitiateState_MAIN_GP()
        public void LibFindLibGlobalVolume()
        {
            //_PC = GameObject.FindWithTag( LibUtilities.TAG.Player.ToString()).gameObject;

            LibGlobalSetting = LibFormulation.FindObjectByTagThenName( LibUtilities.TAG.CONTROLLER.ToString(), LibUtilities.FIND_GO.LibGlobalVolume.ToString());

            
            if (LibGlobalSetting)
            {

                if ( LibGameSetting.IsPlatformWindows
#if UNITY_EDITOR
                        && !LibGameSetting.IsUnityPlayerUseAndroidUI
#endif
                )
                {
                    LibGO_GlobalVolume = LibGlobalSetting.GetComponent<LibMasterGlobalVolumeController>().GBWindows;

                }
                else if (LibGameSetting.IsPlatformAndroid
#if UNITY_EDITOR
                        || LibGameSetting.IsUnityPlayerUseAndroidUI
#endif
                        )
                {
                    LibGO_GlobalVolume = LibGlobalSetting.GetComponent<LibMasterGlobalVolumeController>().GBAndroid;
                }
                else
                {
                    LibGO_GlobalVolume = LibGlobalSetting.GetComponent<LibMasterGlobalVolumeController>().GBWindows;
                }
                //Volume volume = GO_GlobalVolume.GetComponent<Volume>();
                //if (volume.profile.TryGet<DepthOfField>(out DepthOfField tempDof))
                //{
                //    GV_DepthOfField = tempDof;
                //}
                //LibGameSetting.DepthOfField_Start = GV_DepthOfField.gaussianStart.value;
                //LibGameSetting.DepthOfField_End = GV_DepthOfField.gaussianEnd.value;

                //BlurTestGaussianStart = LibGameSetting.DepthOfField_Start;
                //BlurTestGaussianEnd = LibGameSetting.DepthOfField_End;
            }
            
        }

        #endregion  === State Changing ===
        #region  === State Update ===
        private void Update()
        {
            LibGameSettingUpdate();
            #region == Slow Mode ==
            if (IsUseSlowMode || Time.timeScale != 1)
            {
                Time.timeScale = 1 - _curSlowModeVal;
                Time.fixedDeltaTime = Time.timeScale * _curMulTimeFixedDT;
            }
            //Debug.Log("cekcekcek fxDelta:"+ Time.fixedDeltaTime);
            #endregion
            StateFunc.StateUpdate(Update_State, StateFunc.StateChanging(StateChanging, SubStateChanging, CheckingAllfind));
            
        }



        #endregion  === State Update ===

        

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

        #region  === State Called in Update ===


        #endregion  === State Update ===

        #region  === Other Function ===

        public void SetNeedPauseWhenPause(bool flag)
        {
            ItsNeedTobePauseWhenPause = flag;
        }
        public bool GetNeedPauseWhenPause()
        {
            return ItsNeedTobePauseWhenPause;
        }

        public void SetSloMo(float slowTime=0, float slowTimeFixed = 0.01666667f)
        {
            IsUseSlowMode = true;
            _curSlowModeVal = slowTime;
            _curMulTimeFixedDT = slowTimeFixed;
        }

        public void SetSloMoDefault()
        {
            IsUseSlowMode = false;
            _curSlowModeVal = 0;
            _curMulTimeFixedDT = MulTimeFixedDT;
        }

        public void Pause()
        {
            IsUseSlowMode = true;
            _curSlowModeVal = 1;
            _curMulTimeFixedDT = 0;
            LibGameSetting.IsPause = true;
            //GV_Blury(0, 0);
            LibPauseEffect();
        }

        public virtual void LibPauseEffect()
        { 
        
        }

        public void UnPause()
        {
            IsUseSlowMode = false;
            _curSlowModeVal = 0;
            _curMulTimeFixedDT = MulTimeFixedDT;
            LibGameSetting.IsPause = false;
            //GV_Blury(LibGameSetting.DepthOfField_Start, LibGameSetting.DepthOfField_End);
            LibUnPauseEffect();
        }

        public virtual void LibUnPauseEffect()
        {

        }

        private void TestBlur()
        {

        }

        #endregion  === State Update ===
    }

    //public class GameObjectsMove
    //{
    //    private readonly IDictionary<int, MonoBehavMoveObj> _ObjectMoveMB;
    //    protected internal GameObjectsMove()
    //    {
    //        _ObjectMoveMB = new Dictionary<int, MonoBehavMoveObj>();
    //    }


    //    public virtual void AddBtn(int key, MonoBehavMoveObj value)
    //    {
    //        lock (_ObjectMoveMB)
    //        {
    //            if (!_ObjectMoveMB.ContainsKey(key))
    //                _ObjectMoveMB[key] = value;
    //        }
    //    }
    //    public virtual MonoBehavMoveObj GetBtn(int key)
    //    {
    //        lock (_ObjectMoveMB)
    //        {
    //            if (_ObjectMoveMB.TryGetValue(key, out MonoBehavMoveObj value))
    //            {
    //                return value;
    //            }

    //            return null;
    //        }
    //    }

    //    public virtual bool AnyBtn(int key)
    //    {
    //        lock (_ObjectMoveMB)
    //        {
    //            if (_ObjectMoveMB.ContainsKey(key))
    //            {
    //                return true;
    //            }
    //            return false;
    //        }
    //    }

    //    public virtual void ClearBtn()
    //    {
    //        lock (_ObjectMoveMB)
    //        {
    //            _ObjectMoveMB.Clear();
    //        }
    //    }
    //    public virtual bool RemoveBtn(int key)
    //    {
    //        lock (_ObjectMoveMB)
    //        {
    //            return _ObjectMoveMB.Remove(key);
    //        }
    //    }

    //    public virtual int GetCountBtn()
    //    {
    //        lock (_ObjectMoveMB)
    //        {
    //            return _ObjectMoveMB.Count;
    //        }
    //    }
    //}
}