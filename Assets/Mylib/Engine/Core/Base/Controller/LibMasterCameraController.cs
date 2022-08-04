
using System;
using UnityEngine;

namespace Com.GNL.URP_MyLib
{
    public abstract class LibMasterCameraController : LibSingletonController<LibMasterCameraController>
    {
        //[HideInInspector] public int CurrentTab;
        //public string ToolbarTab;
        //public string Tab1;
        //public bool empty;
        //public string Tab2;

        [Header("Touch Screen For Rotate Gameplay")]
        [SerializeField] public Canvas CanvasUIGameplay;

        [Header("Length from start to end touch, Hover to see details")]
        [Tooltip("Default if x and y is whan 1, then when touch in center will give -1 to the max left and 1 to the max right")]
        public Vector2 LengthRadius_Android = new Vector2(3.5f, 2f);
        public Vector2 LengthRadius_Windows = new Vector2(3.5f, 2f);

        [HideInInspector]
        public Vector2 CamMove =  new Vector2();


        [Space(10)]
        [Header("Speed Moving camera")]
        public float LookSpeedX_PC = 500f;
        public float LookSpeedY_PC = 8f;
        [Space(10)]
        public float LookSpeedX_ADR = 500f;
        public float LookSpeedY_ADR = -8f;


        //public string TabEnd;

        private float _lookSpeedX = 500f;
        private float _lookSpeedY = 8f;

        private Vector2 _input = Vector2.zero;
        private Vector2 _finalInput = Vector2.zero;
        private Vector2 _posStart = Vector2.zero;


        // Start is called before the first frame update
        void Start()
        {
            StateFunc.ClearState();

        }

        // Update is called once per frame


        #region === State Changing ===


        // StateChanging dan Substate Chaning haurs dipisah, karna substae bisa berubah walaupun state tidak berubah, untuk updatenya bisa sama
        public abstract void StateChanging();

        public abstract void SubStateChanging();



        #endregion === State Changing ===

        #region === Stete Update ===

        private void Update()
        {
            GetAnySelectionData();
            StateFunc.StateUpdate(Update_State, StateFunc.StateChanging(StateChanging, SubStateChanging, CheckingAllfind));
        }

        public abstract void Update_State();
        private void FixedUpdate()
        {
            StateFunc.StateUpdate(Update_StateFU, StateFunc.StateChanging(StateChanging, SubStateChanging, CheckingAllfind));
        }

        public abstract void Update_StateFU();

        private void LateUpdate()
        {
            StateFunc.StateUpdate(Update_StateLU, StateFunc.StateChanging(StateChanging, SubStateChanging, CheckingAllfind));
        }

        public abstract void Update_StateLU();

        //private void State_MAIN_GP_Update()
        //{
        //    ControlCam();
        //    //ModeFightCam();
        //}

        #endregion === Stete Update ===

        #region === Stete Called IN Update ===

        public bool LibControlCam(Vector2 camMove)
        {
            if (VirtualInputManager.Instance.InputAttr.MoveCam)
            {
                //Debug.Log("cekcekcek camera MoveCam windows:"+ LibGameSetting.IsPlatformWindows+", android:"+ LibGameSetting.IsPlatformAndroid);
                if (LibGameSetting.IsPlatformWindows
#if UNITY_EDITOR
                        //&& LibGameSetting.IsUnityPlayerUseAndroidUI
#endif
                    )

                {
                    //Debug.Log("cekcekcek pc camera");
                    Vector2 mouseInput = Input.mousePosition;
                    CountingInputMoveCam(mouseInput, LengthRadius_Windows, camMove);
                    //Debug.Log("cekcekcek pc _input x=" + _input.x + "&& _input y=" + _input.y);
                    //Debug.Log("cekcekcek android _finalInput x=" + _finalInput.x + "&& _finalInput y=" + _finalInput.y);

                }
                else if (LibGameSetting.IsPlatformAndroid
#if UNITY_EDITOR
                        || LibGameSetting.IsUnityPlayerUseAndroidUI
#endif
                        )
                {
                    //Debug.Log("cekcekcek android camera");
                    if (VirtualButtonManager.Instance.AnyBtn(MY_BTN_CODE.Btn_MoveCam))
                        CountingInputMoveCam(VirtualButtonManager.Instance.GetBtn(MY_BTN_CODE.Btn_MoveCam).position, LengthRadius_Android, camMove);
                    //Debug.Log("cekcekcek android _input x=" + _finalInput.x + "&& _input y=" + _finalInput.y);
                }
                return true;

            }
            else
            {
                ClearInputMoveCam();
                return false;
            }
        }

        public Vector2 GetPosCamMove()
        {
            return CamMove;
        }

        /* if you Use this, dont forget to call SetObjectSelection()
         * And fill it with condition  like this example :
         *  if (VirtualSelectionObjectManager.Instance.AnyObject((byte)VirtualSelectionObjectManager.SelectedObjectName.ENEMY))
         *  {
         *
         *      //_hitEnemy = VirtualSelectionObjectManager.Instance.GetObject((byte)VirtualSelectionObjectManager.SelectedObjectName.ENEMY);
         *      //_anim.SetBool(PlayerController.ANIM_PARAM.isInCombating.ToString(), true);
         *      //_anim.SetFloat(PlayerController.ANIM_PARAM.CombatTime.ToString(), _PC.CombatDuration);
         *  }
         *
         */
        public void GetAnySelectionData()
        {
            if (VirtualInputManager.Instance.InputAttr.AimTarget)
            {
                SetObjectSelection();
            }
        }

        public virtual void SetObjectSelection()
        {
            //if (VirtualSelectionObjectManager.Instance.AnyObject((byte)VirtualSelectionObjectManager.SelectedObjectName.ENEMY))
            //{

            //    //_hitEnemy = VirtualSelectionObjectManager.Instance.GetObject((byte)VirtualSelectionObjectManager.SelectedObjectName.ENEMY);
            //    //_anim.SetBool(PlayerController.ANIM_PARAM.isInCombating.ToString(), true);
            //    //_anim.SetFloat(PlayerController.ANIM_PARAM.CombatTime.ToString(), _PC.CombatDuration);
            //}
        }


        private void CountingInputMoveCam(Vector2 pos, Vector2 lenghtRadius, Vector2 camMove)
        {
            //postart
            if (_posStart == Vector2.zero)
            {
                _posStart = pos;
            }
            //NEW   
            _input = ((pos - _posStart) * lenghtRadius) / (CanvasUIGameplay.pixelRect.size * CanvasUIGameplay.scaleFactor);

            if (_input.magnitude > 0)
            {
                if (_input.magnitude > 1)
                    _input = _input.normalized;
            }
            else
                _input = Vector2.zero;
            _finalInput = _input;

            _lookSpeedX = 0;
            _lookSpeedY = 0;
            if (LibGameSetting.IsPlatformWindows
#if UNITY_EDITOR
                        && !LibGameSetting.IsUnityPlayerUseAndroidUI
#endif
                    )

            {
                _lookSpeedX = LookSpeedX_PC;
                _lookSpeedY = LookSpeedY_PC;
            }
            else if (LibGameSetting.IsPlatformAndroid
#if UNITY_EDITOR
                        || LibGameSetting.IsUnityPlayerUseAndroidUI
#endif
                    )
            {
                _lookSpeedX = LookSpeedX_ADR;
                _lookSpeedY = LookSpeedY_ADR;
            }
            CamMove.x = camMove.x + (_finalInput.x * _lookSpeedX * Time.deltaTime);
            CamMove.y = camMove.y + (_finalInput.y * _lookSpeedY * Time.deltaTime);
        }

        private void ClearInputMoveCam()
        {
            _posStart = Vector2.zero;
            _input = Vector2.zero;
            _finalInput = Vector2.zero;
            VirtualInputManager.Instance.InputAttr.MoveCam = false;
        }

        #endregion === Stete Called IN Update ===
    }

    //[Serializable]
    //public class LibCameraControllerAtrribute
    //{
    //    [Header("Touch Screen For Rotate Gameplay")]
    //    public Canvas CanvasUIGameplay;
    //    [Header("Length from start to end touch, Hover to see details")]
    //    [Tooltip("Default if x and y is whan 1, then when touch in center will give -1 to the max left and 1 to the max right")]
    //    public Vector2 LengthRadius_Android = new Vector2(3.5f, 2f);
    //    public Vector2 LengthRadius_Windows = new Vector2(3.5f, 2f);


    //    [Space(10)]
    //    [Header("Speed Moving camera")]
    //    public float LookSpeedX_PC = 500f;
    //    public float LookSpeedY_PC = 8f;
    //    [Space(10)]
    //    public float LookSpeedX_ADR = 500f;
    //    public float LookSpeedY_ADR = -8f;



    //}
}