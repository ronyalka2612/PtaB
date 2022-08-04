
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Com.GNL.URP_MyLib
{

    public class LibMasterInputController : LibSingletonController<LibMasterInputController>
    {

        public LibInputControllerAtrribute LibAttrb;

        public void Awake()
        {
            LibAttrb.IsInputEnable = true;
        }

        public void Start()
        {
            StateFunc.ClearState();

        }



        /* @TODO
         * it will bi called in child with interface called IInputController
         */
        public virtual void StateChanging()
        {
            
        }

        public virtual void SubStateChanging()
        {

        }

        public bool GetLibCheckingAllfind()
        {
            if (LibAttrb.fixedJoystick == null)
            {
                return false;
            }
            return true;

        }


        private void Update()
        {
            if(LibAttrb.IsInputEnable)
                StateFunc.StateUpdate(Update_State, StateFunc.StateChanging(StateChanging, SubStateChanging, CheckingAllfind));
        }

        public virtual void Update_State()
        {
            
        }
        private void FixedUpdate()
        {
            if (LibAttrb.IsInputEnable)
                StateFunc.StateUpdate(Update_StateFU, StateFunc.StateChanging(StateChanging, SubStateChanging, CheckingAllfind));
        }

        public virtual void Update_StateFU()
        {
        }

        private void LateUpdate()
        {
            if (LibAttrb.IsInputEnable)
                StateFunc.StateUpdate(Update_StateLU, StateFunc.StateChanging(StateChanging, SubStateChanging, CheckingAllfind));
        }

        public virtual void Update_StateLU()
        {

        }

        public void LibInputAxis()
        {

            if (VirtualInputManager.Instance.InputAttr.MoveBack
                || VirtualInputManager.Instance.InputAttr.MoveForward
                || VirtualInputManager.Instance.InputAttr.MoveLeft
                || VirtualInputManager.Instance.InputAttr.MoveRight
                )
            {

                Vector2 direction = Vector2.zero;
                if (VirtualInputManager.Instance.InputAttr.MoveForward)
                    direction.y = 1;
                else if (VirtualInputManager.Instance.InputAttr.MoveBack)
                    direction.y = -1;

                if (VirtualInputManager.Instance.InputAttr.MoveRight)
                    direction.x = 1;
                else if (VirtualInputManager.Instance.InputAttr.MoveLeft)
                    direction.x = -1;

                VirtualInputManager.Instance.InputAttr.Move = true;
                VirtualDataInputManager.Instance.AxisFinalInput.x = direction.x;
                VirtualDataInputManager.Instance.AxisFinalInput.y = direction.y;
            }
            else if (LibAttrb.fixedJoystick != null && LibAttrb.fixedJoystick.finalInput.magnitude > LibAttrb.JoypadAxisMinInputMagnitude)
            {
                //Debug.Log("cekcek mag : " + VirtualButtonManager.Instance.BtnAttr.AxisFinalInput.magnitude);
                VirtualInputManager.Instance.InputAttr.Move = true;
                if (LibAttrb.fixedJoystick.Horizontal > LibAttrb.JoypadAxisMinInputMove)
                {
                    VirtualDataInputManager.Instance.AxisFinalInput.x = LibAttrb.fixedJoystick.Horizontal * LibAttrb.JoypadAxisAcc > 1 ? 1 : LibAttrb.fixedJoystick.Horizontal * LibAttrb.JoypadAxisAcc;
                }
                else if (LibAttrb.fixedJoystick.Horizontal < -LibAttrb.JoypadAxisMinInputMove)
                {
                    VirtualDataInputManager.Instance.AxisFinalInput.x = LibAttrb.fixedJoystick.Horizontal * LibAttrb.JoypadAxisAcc < -1 ? -1 : LibAttrb.fixedJoystick.Horizontal * LibAttrb.JoypadAxisAcc;
                }
                else
                {
                    VirtualDataInputManager.Instance.AxisFinalInput.x = 0;
                }

                if (LibAttrb.fixedJoystick.Vertical > LibAttrb.JoypadAxisMinInputMove)
                {
                    VirtualDataInputManager.Instance.AxisFinalInput.y = LibAttrb.fixedJoystick.Vertical * LibAttrb.JoypadAxisAcc > 1 ? 1 : LibAttrb.fixedJoystick.Vertical * LibAttrb.JoypadAxisAcc;
                }
                else if (LibAttrb.fixedJoystick.Vertical < -LibAttrb.JoypadAxisMinInputMove)
                {
                    VirtualDataInputManager.Instance.AxisFinalInput.y = LibAttrb.fixedJoystick.Vertical * LibAttrb.JoypadAxisAcc < -1 ? -1 : LibAttrb.fixedJoystick.Vertical * LibAttrb.JoypadAxisAcc;
                }
                else
                {
                    VirtualDataInputManager.Instance.AxisFinalInput.y = 0;
                }

            }
            else if (VirtualInputManager.Instance.InputAttr.Move) // it is making not set over and over
            {

                VirtualInputManager.Instance.InputAttr.Move = false;
                VirtualDataInputManager.Instance.AxisFinalInput = Vector2.zero;
            }
        }
        public void LibControllButton(ref bool VIM, MY_BTN_CODE btnCode, bool down = false, bool pressed = false, bool up = false,
           bool PCUseAll = false,
           bool PCUseBtn = false,
           bool PhoneUseKey = false
           )
        {
            LibControllButton(ref VIM, KeyCode.None, btnCode, down, pressed, up, PCUseAll, PCUseBtn, PhoneUseKey);
        }

        public void LibControllButton(ref bool VIM, KeyCode keyCode, bool down = false, bool pressed = false, bool up = false,
           bool PCUseAll = false,
           bool PCUseBtn = false,
           bool PhoneUseKey = false
           )
        {
            LibControllButton(ref VIM, keyCode, MY_BTN_CODE.Btn_None, down, pressed, up,PCUseAll, PCUseBtn, PhoneUseKey);
        }

        public void LibControllButton(ref bool VIM, KeyCode keyCode, MY_BTN_CODE BtnCode, bool down = false, bool pressed = false, bool up = false, 
            bool PCUseAll = false, 
            bool PCUseBtn = false, 
            bool PhoneUseKey = false
            )
        {
            //if ((!AllPlatformUseButton && LibGameSetting.IsPlatformWindows) || AndroidUseKeyCode
            if ((LibGameSetting.IsPlatformWindows && !PCUseBtn)
                ||(LibGameSetting.IsPlatformWindows && PCUseAll)
                || (LibGameSetting.IsPlatformAndroid && PhoneUseKey)
                    )
            {
                bool flag = true;
                if (Input.GetKeyDown(keyCode) && down)
                {
                    VIM = true;
                    //if (kmcCode == KeyCode.Space)
                    //{
                    //    Debug.Log("cekcekcek Keyboard kmcCode: " + kmcCode.ToString() + " down: " + VIM);
                    //}
                    return;
                }
                else if (Input.GetKey(keyCode) && pressed)
                {
                    VIM = true;
                    //if (kmcCode == KeyCode.Alpha0)
                    //{
                    //    Debug.Log("cekcekcek Keyboard kmcCode: " + kmcCode.ToString() + " pressed: " + VIM);
                    //}
                return;
                }
                else if (Input.GetKeyUp(keyCode) && up)
                {
                    VIM = true;
                    //if (kmcCode == KeyCode.Alpha0)
                    //    Debug.Log("cekcekcek Keyboard kmcCode: " + kmcCode.ToString() + " up: " + VIM);
                return;
                }
                else
                {
                    //if (kmcCode == KeyCode.Alpha0)
                    //{
                    //    Debug.Log("cekcekcek Keyboard kmcCode: " + kmcCode.ToString() + " Key Lose: " + VIM);
                    //}
                    flag = false;
                    VIM = false;
                }
#if UNITY_EDITOR
                if ( LibGameSetting.IsUnityPlayerUseAndroidUI || PCUseAll)

#else
                    if(PCUseAll)
#endif
                    {
                        if (VirtualButtonManager.Instance.AnyBtn(BtnCode))
                        {
                            VIM = true;
                            //if (kmcCode == KeyCode.Alpha0)
                            //{
                            //    Debug.Log("cekcekcek Button_Screen kmcCode: " + kmcCode.ToString() + " Btn: " + VIM);
                            //}
                            return;
                        }
                        else if(!flag)
                        {
                            //if (kmcCode == KeyCode.Alpha0)
                            //{
                            //    Debug.Log("cekcekcek Button_Screen kmcCode: " + kmcCode.ToString() + " Btn Lose: " + VIM);
                            //}
                            VIM = false;
                        }
                    }
            }
            else if ( LibGameSetting.IsPlatformAndroid || (PCUseBtn && LibGameSetting.IsPlatformWindows)
#if UNITY_EDITOR
                        || LibGameSetting.IsUnityPlayerUseAndroidUI
#endif
                    )
            {
                if (VirtualButtonManager.Instance.AnyBtn(BtnCode))
                {
                    VIM = true;
                    //if (kmcCode == KeyCode.Escape)
                    //{
                    //    Debug.Log("cekcekcek Button_Screen kmcCode: " + kmcCode.ToString() + " Btn : " + VIM + " true");
                    //}
                    return;
                }
                else
                {
                    //if (kmcCode == KeyCode.Escape)
                    //{
                    //    Debug.Log("cekcekcek Button_Screen kmcCode: " + kmcCode.ToString() + " Btn : " + VIM + " false");
                    //}
                    VIM = false;
                }
            }
        }
        public void LibControllButtonEnableDisable(ref bool VIM, KeyCode kmcCode)
        {
            LibControllButtonEnableDisable(ref VIM, kmcCode, MY_BTN_CODE.Btn_None);
        }

        public void LibControllButtonEnableDisable(ref bool VIM, MY_BTN_CODE btnCode)
        {
            LibControllButtonEnableDisable(ref VIM, KeyCode.None, btnCode);
        }

        public void LibControllButtonEnableDisable(ref bool VIM, KeyCode kmcCode, MY_BTN_CODE btnCode)
        {
            if (!VIM &&
                (Input.GetKeyDown(kmcCode) || VirtualButtonManager.Instance.AnyBtn(btnCode))
                )
            {
                //VirtualInputDelayManager.Instance.AddBtn((byte)VirtualButtonManager.BtnAndroidName.WALK, VirtualInputDelayManager.Instance.TimeDelay);
                VIM = true;
            }
            else if (VIM &&
                (Input.GetKeyDown(kmcCode) || VirtualButtonManager.Instance.AnyBtn(btnCode)
                ))
            {
                //VirtualInputDelayManager.Instance.AddBtn((byte)VirtualButtonManager.BtnAndroidName.WALK, VirtualInputDelayManager.Instance.TimeDelay);
                VIM = false;
            }
        }
    }

    [Serializable]
    public class LibInputControllerAtrribute
    {
#if UNITY_EDITOR
        [TextArea(5, 10)]
        [SerializeField] private string Notes;
#endif
        [Header("If You Use this you should call CheckingAllfindLib in CheckingAllfind from parent class (LibInputController) ")]
        public bool IsUseLibJoyStickLibrary = true;

        [LibReadOnly] public JoyPadController fixedJoystick;

        [Header("AtributJoypad Library")]
        public float JoypadAxisAcc = 1;
        public float JoypadAxisMinInputMove = 0.1f;
        public float JoypadAxisMinInputMagnitude = 0.4f;



        public bool IsInputEnable { get; internal set; }
        //[Space(10)]

        //[Header("Length from start to end touch, Hover to see details")]
        //[Tooltip("Default if x and y is whan 1, then when touch in center will give -1 to the max left and 1 to the max right")]
        //public Vector2 LengthRadius_Android;
        //public Vector2 LengthRadius_Windows;
    }
}
        