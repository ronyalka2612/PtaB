
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Com.GNL.URP_MyLibProjectTest;

namespace Com.GNL.URP_MyLib
{

    public class LibInputController : LibMasterInputController, ILibController
    {
        [Header("LibInputController attribute")]
#if UNITY_EDITOR
        [TextArea(5, 10)]
        [MyBox.ReadOnly] [SerializeField] private string Notes;
#endif
        [MyBox.ReadOnly] [SerializeField] public JoyPadController MyfixedJoystick;

        [Header("AtributJoypad N")]
        public float JoypadAxisAcc = 1;
        public float JoypadAxisMinInputMove = 0.1f;
        public float JoypadAxisMinInputMagnitude = 0.2f;


        //#region === Singleton_Lib ===
        //private static LibInputController _instance;
        //public static LibInputController InstanceLib
        //{
        //    get
        //    {

        //        if (_instance == null)
        //        {
        //            {
        //                //Scene activeScene = SceneManager.GetSceneAt(0);

        //                var instances = FindObjectsOfType<LibInputController>();
        //                //GameObject obj = instance.;
        //                var count = instances.Length;
        //                if (count > 0)
        //                {
        //                    if (count == 1)
        //                    {
        //                        Debug.Log("cekcekcek Is InstanceLib LibInputController T name :" + instances[0].name + " hanya 1");
        //                        return _instance = instances[0];
        //                    }
        //                    Debug.LogWarning($"[{nameof(LibInputController)}] There should never be more than one {nameof(LibInputController)} in the scene, but {count} were found. The first instance found will be used, and all others will be destroyed.");
        //                    for (var i = 1; i < instances.Length; i++)
        //                        Destroy(instances[i]);
        //                    Debug.Log("cekcekcek Is InstanceLib LibInputController T name :" + instances[0].name + " tingal 1");
        //                    return _instance = instances[0];
        //                }

        //                Debug.Log($"[{nameof(LibInputController)}] An instance is needed in the scene and no existing instances were found, so a new instance will be created.");
        //                return _instance = new GameObject($"({nameof(LibInputController)})")
        //                           .AddComponent<LibInputController>();
        //            }
        //        }

        //        return _instance;
        //    }
        //}
        //#endregion === Singleton_Lib ===

        #region === State Changing ===
        public override void StateChanging()
        {
            //this.enabled = false;
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == State MAINMENU ==
                case LibEdStateUtilities.GameStates.MAINMENU:

                    break;
                #endregion
                #region == State MAIN_GP ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    MAIN_GPChangingInitiate();
                    break;
                    #endregion
            }
        }


        public override void SubStateChanging()
        {
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == State MAINMENU ==
                case LibEdStateUtilities.GameStates.MAINMENU:
                    //Debug.Log("cekcekcek LibInput SubStateChanging");
                    //StateFunc.SubState(SubState_MainMenu_State, true);

                    break;
                #endregion
                #region == SubState_MAIN_GP_Changing ==
                case LibEdStateUtilities.GameStates.MAIN_GP:

                    //StateFunc.SubState(SubState_MAIN_GP_State, true);
                    break;
                    #endregion
            }

        }

        public override void CheckingAllfind()
        {
            //if (!GetLibCheckingAllfind())
            //{
            //    StateFunc.SetFindAll(false);
            //}
            if ((LibGameSetting.IsPlatformAndroid
#if UNITY_EDITOR
                        || LibGameSetting.IsUnityPlayerUseAndroidUI
#endif
                        )
                        && ( MyfixedJoystick == null)
                        && ( VirtualStateManager.Instance.CurState == LibEdStateUtilities.GameStates.MAIN_GP
                            ))
            {
                StateFunc.SetFindAll(false);
                //StateFunc.ClearState();
            }
            else
            {
                StateFunc.SetFindAll(true);
            }
        }


        private void MAIN_GPChangingInitiate()
        {
            //
            if (LibGameSetting.IsPlatformAndroid
#if UNITY_EDITOR
                        || LibGameSetting.IsUnityPlayerUseAndroidUI
#endif
                        )
            {
                GameObject Joypad = LibFormulation.FindObjectByTagThenName(LibUtilities.TAG.JOYPAD.ToString(), LibUtilities.FIND_GO.JoyPad.ToString());
                if(Joypad != null)
                { 
                    MyfixedJoystick = Joypad.GetComponent<JoyPadController>();
                    LibAttrb.fixedJoystick = MyfixedJoystick;
                }
            }
        }
        #endregion === State Changing ===

        #region === Update State ===
        public override void Update_State()
        {
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == Input State MAINMENU ==
                case LibEdStateUtilities.GameStates.MAINMENU:
                    UpdateInput_MAINMENU();
                    StateFunc.SubState(SubState_MainMenu_Update);
                    break;
                #endregion
                #region == Input State MAIN_GP ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    if (!LibGameSetting.IsPause)
                    {
                        UpdateInput_MAIN_GP();
                    }
                    StateFunc.SubState(SubState_MAIN_GP_Pause);
                    
                    break;
                #endregion == Input State Gameplay ==
                default:
                    break;
            }
        }
        #region === State Update FU And LU ===
        public override void Update_StateFU()
        {
            /*
             *  - mau dibuat di update saja 
             *  - membenarkan bug yang ucuma pake input update saja
             *  - sesuatu yaang membutuhkan input di fix update akan dibuat localinput di class itu
             *    dimana input ditangkap di update lalu di condisikan sesuai kebutuhan
             *
             */

            //switch (VirtualStateManager.Instance.CurState)
            //{
            //    #region == Input State MAINMENU ==
            //    case LibEdStateUtilities.GameStates.MAINMENU:
            //        UpdateInput_MAINMENU();
            //        break;
            //    #endregion
            //    #region == Input State Gameplay ==
            //    case LibEdStateUtilities.GameStates.GAMEPLAY:
            //        if (!LibGameSetting.IsPause)
            //        {
            //            UpdateInput_GAMEPLAY();
            //        }
            //        if (!LibGameSetting.IsPause)
            //        {
            //            StateFunc.SubState(SubState_Gameplay_Pause);
            //        }
            //        break;
            //    #endregion == Input State Gameplay ==
            //    #region == Input State MAIN_GP ==
            //    case LibEdStateUtilities.GameStates.MAIN_GP:
            //        if (!LibGameSetting.IsPause)
            //        {
            //            UpdateInput_MAIN_GP();
            //        }
            //        if (LibGameSetting.IsPause)
            //        {
            //            StateFunc.SubState(SubState_MAIN_GP_Pause);
            //        }
            //        break;
            //    #endregion == Input State Gameplay ==
            //    default:
            //        break;
            //}
        }

        public override void Update_StateLU()
        {
            //if (isAlreadyUse)
            //    return;
            //switch (VirtualStateManager.Instance.CurState)
            //{
            //    #region == Input State MAINMENU ==
            //    case LibEdStateUtilities.GameStates.MAINMENU:
            //        UpdateInput_MAINMENU();
            //        break;
            //    #endregion
            //    #region == Input State Gameplay ==
            //    case LibEdStateUtilities.GameStates.GAMEPLAY:
            //        if (!LibGameSetting.IsPause)
            //        {
            //            UpdateInput_GAMEPLAY();
            //        }
            //        if (!LibGameSetting.IsPause)
            //        {
            //            StateFunc.SubState(SubState_Gameplay_Pause);
            //        }
            //        break;
            //    #endregion == Input State Gameplay ==
            //    #region == Input State MAIN_GP ==
            //    case LibEdStateUtilities.GameStates.MAIN_GP:
            //        if (!LibGameSetting.IsPause)
            //        {
            //            UpdateInput_MAIN_GP();
            //        }
            //        if (LibGameSetting.IsPause)
            //        {
            //            StateFunc.SubState(SubState_MAIN_GP_Pause);
            //        }
            //        break;
            //    #endregion == Input State Gameplay ==
            //    default:
            //        break;
            //}
        }
        #endregion === State Update FU And LU ===


        private void SubState_MainMenu_Update(LibEdStateUtilities.GameSubStates substate)
        {
            switch (substate)
            {
                #region == SubState MAINMENU_MULTI_PLAYER ==
                case LibEdStateUtilities.GameSubStates.MAINMENU_MULTI_PLAYER:
                    UpdateInput_MAINMENU_MULTI_PLAYER();
                    //Debug.Log("cekcekcek LibInput SubState MAINMENU_FIRST");
                    break;
                #endregion
                #region == SubState MAINMENU_OPTION ==
                case LibEdStateUtilities.GameSubStates.MAINMENU_OPTION:
                    //Debug.Log("cekcekcek LibInput SubState MAINMENU_OPTION");
                    UpdateInput_MAINMENU_OPTION();
                    break;
                    #endregion
            }
        }

        

        private void SubState_MAIN_GP_Pause(LibEdStateUtilities.GameSubStates substate)
        {
            switch (substate)
            {
                #region == SubState MAIN_GP_GAMEPLAY_PAUSE ==
                case LibEdStateUtilities.GameSubStates.MAIN_GP_GAMEPLAY_PAUSE:

                    if (LibGameSetting.IsPause)
                    {
                        UpdateInput_MAIN_GP_PAUSE();
                    }
                    break;
                    #endregion
            }
        }

        #endregion === Update State ===

        #region === Update all Input MAINMENU ===

        #region === UpdateInput_MAINMENU ===
        private void UpdateInput_MAINMENU()
        {
            #region == Active when pressed, Hold And Up ==
            // when need differen VIM, you need to set by self, copy the inside of thise fucntion, 
            // and make it below of these, make sure inside of this region
            LibControllButton(
                ref VirtualInputManager.Instance.InputAttr.Start_Game,
                MY_BTN_CODE.Btn_Start_Game, 
                up : true, 
                PCUseBtn : true);

            LibControllButton(
                ref VirtualInputManager.Instance.InputAttr.Start_MultiPlayer,
                MY_BTN_CODE.Btn_Start_MultiPlayer,
                up: true,
                PCUseBtn: true);

            LibControllButton(
                ref VirtualInputManager.Instance.InputAttr.Start_Option,
                MY_BTN_CODE.Btn_Start_Option,
                up: true,
                PCUseBtn: true);

            LibControllButton(
                ref VirtualInputManager.Instance.InputAttr.QuitGame,
                MY_BTN_CODE.Btn_Exit,
                up: true, 
                PCUseBtn: true);

            #endregion == Active when pressed, Hold And Up ==

            #region == Active when condisition is pressed and disable, and disable it when it pressed and enable ==
            //LibControllButtonEnableDisable(ref VirtualButtonManager.Instance.BtnAttr.Walk, MY_BTN_CODE.Walk);

            #endregion == Active when condisition is pressed and disable, and disable it when it pressed and enable ==

            #region == When any get input axis ==

            #endregion == When any get input axis ==
        }
        #endregion === UpdateInput_MAINMENU ===

        #region === UpdateInput_MAINMENU_MULTI_PLAYER ===
        private void UpdateInput_MAINMENU_MULTI_PLAYER()
        {
            LibControllButton(
                ref VirtualInputManager.Instance.InputAttr.Back,
                VirtualInputManager.Instance.KeyAttr.Back,
                MY_BTN_CODE.Btn_Back,
                up: true,
                PCUseAll: true,
                PhoneUseKey: true);

            LibControllButton(
                ref VirtualInputManager.Instance.InputAttr.MP_CreteLby,
                MY_BTN_CODE.Btn_MP_CreteLby,
                up: true,
                PCUseBtn: true);

            LibControllButton(
                ref VirtualInputManager.Instance.InputAttr.MP_JoinLby,
                MY_BTN_CODE.Btn_MP_JoinLby,
                up: true,
                PCUseBtn: true);

            LibControllButton(
                ref VirtualInputManager.Instance.InputAttr.MP_JoinRdmLby,
                MY_BTN_CODE.Btn_MP_JoinRdmLby,
                up: true,
                PCUseBtn: true);
        }
        #endregion === UpdateInput_MAINMENU_MULTI_PLAYER ===

        #region === UpdateInput_MAINMENU_OPTION ===
        private void UpdateInput_MAINMENU_OPTION()
        {
            #region == Active when pressed, Hold And Up ==

            LibControllButton(
                ref VirtualInputManager.Instance.InputAttr.Back,
                VirtualInputManager.Instance.KeyAttr.Back,
                MY_BTN_CODE.Btn_Back,
                up: true,
                PCUseAll: true,
                PhoneUseKey: true);

            #endregion == Active when pressed, Hold And Up ==

        }
        #endregion === UpdateInput_MAINMENU_OPTION ===
        #endregion === Update all Input MAINMENU ===

        #region === Update all Input_MAIN_GP ===
        #region === UpdateInput_MAIN_GP ===

        private void UpdateInput_MAIN_GP()
        {
            #region == Active when pressed, Hold And Up ==
            // when need differen VIM, you need to set by self, copy the inside of thise fucntion, 
            // and make it below of these, make sure inside of this region

            //test
            LibControllButton(
                ref VirtualInputManager.Instance.InputAttr.AddRobot,
                VirtualInputManager.Instance.KeyAttr.AddRobot, 
                MY_BTN_CODE.Btn_AddRobot,
                down: true
                );
            LibControllButton(
                ref VirtualInputManager.Instance.InputAttr.Addbox, 
                VirtualInputManager.Instance.KeyAttr.Addbox,
                MY_BTN_CODE.Btn_Addbox,
                down: true
                );
            LibControllButton(
                ref VirtualInputManager.Instance.InputAttr.ResetPlayer,
                VirtualInputManager.Instance.KeyAttr.ResetPlayer,
                MY_BTN_CODE.Btn_ResetPlayer,
                down: true,
                PCUseAll: true
                );

            //actual
            //LibControllButton(
            //    ref VirtualInputManager.Instance.InputAttr.Sprint, 
            //    MY_BTN_CODE.Sprint, 
            //    down : true, 
            //    pressed: true, 
            //    up: true
            //    );

            LibControllButton(
                ref VirtualInputManager.Instance.InputAttr.MoveForward,
                VirtualInputManager.Instance.KeyAttr.MoveForward,
                MY_BTN_CODE.Btn_MoveForward,
                down: true,
                pressed: true,
                up: true
                );

            LibControllButton(
                ref VirtualInputManager.Instance.InputAttr.MoveLeft,
                VirtualInputManager.Instance.KeyAttr.MoveLeft,
                MY_BTN_CODE.Btn_MoveLeftt,
                down: true,
                pressed: true,
                up: true
                );
            LibControllButton(
                ref VirtualInputManager.Instance.InputAttr.MoveBack,
                VirtualInputManager.Instance.KeyAttr.MoveBack,
                MY_BTN_CODE.Btn_MoveBack,
                down: true,
                pressed: true,
                up: true
                );
            LibControllButton(
                ref VirtualInputManager.Instance.InputAttr.MoveRight,
                VirtualInputManager.Instance.KeyAttr.MoveRight,
                down: true,
                pressed: true,
                up: true
                );
            //LibControllButton(
            //    ref VirtualInputManager.Instance.InputAttr.Jump,
            //    VirtualInputManager.Instance.KeyAttr.JUm,
            //    down: true, 
            //    pressed: true
            //    );
            LibControllButton(
                ref VirtualInputManager.Instance.InputAttr.MoveCam,
                VirtualInputManager.Instance.KeyAttr.MoveCam, 
                MY_BTN_CODE.Btn_MoveCam,
                false, 
                true, 
                true
                );

            LibControllButton(
                ref VirtualInputManager.Instance.InputAttr.BreakFront,
                VirtualInputManager.Instance.KeyAttr.BreakFront,
                MY_BTN_CODE.Btn_BreakFront,
                true, 
                true, 
                false
                );
            LibControllButton(
                ref VirtualInputManager.Instance.InputAttr.BreakRear,
                VirtualInputManager.Instance.KeyAttr.BreakRear,
                MY_BTN_CODE.Btn_BreakRear,
                true, 
                true, 
                false
                );
            LibControllButton(
                ref VirtualInputManager.Instance.InputAttr.Gas,
                VirtualInputManager.Instance.KeyAttr.Gas,
                MY_BTN_CODE.Btn_Gas,
                true, 
                true, 
                false);

            //attakcing
            LibControllButton(
                ref VirtualInputManager.Instance.InputAttr.ReleaseBall,
                VirtualInputManager.Instance.KeyAttr.ReleaseBall,
                MY_BTN_CODE.Btn_ReleaseBall,
                down: true,
                pressed: true,
                up: false);
            //LibControllButton(
            //    ref VirtualInputManager.Instance.InputAttr.AttackSkill,
            //    VirtualInputManager.Instance.KeyAttr.AttackSkill,
            //    MY_BTN_CODE.Btn_AttackSkill,
            //    down:true, 
            //    pressed:true, 
            //    up:false);


            //// Pause
            //LibControllButton(
            //    ref VirtualInputManager.Instance.InputAttr.Resume,
            //    VirtualInputManager.Instance.KeyAttr.Escape, 
            //    MY_BTN_CODE.Btn_Back,
            //    true, 
            //    false, 
            //    false, 
            //    PCUseAll: true,
            //    PhoneUseKey: true)
            //    ;
            LibControllButton(
                ref VirtualInputManager.Instance.InputAttr.MenuPause,
                VirtualInputManager.Instance.KeyAttr.Back,
                MY_BTN_CODE.Btn_Back,
                down: true,
                pressed: false,
                up: false, 
                PCUseAll: true, 
                PhoneUseKey: true
                );

            #endregion == Active when pressed, Hold And Up ==

            #region == Active when condisition is pressed and disable, and disable it when it pressed and enable ==
            LibControllButtonEnableDisable(
                ref VirtualInputManager.Instance.InputAttr.Reverse,
                VirtualInputManager.Instance.KeyAttr.Reverse,
                MY_BTN_CODE.Btn_Reverse
                );
            #endregion == Active when condisition is pressed and disable, and disable it when it pressed and enable ==

            #region == When any get input axis ==
            if (LibAttrb.IsUseLibJoyStickLibrary)
            {
                LibInputAxis();
            }
            else
                InputAxis();
            #endregion == When any get input axis ==
        }

        #endregion === Update Input MAIN_GP ===


        #region === UpdateInput_MAIN_GP_PAUSE ===
        private void UpdateInput_MAIN_GP_PAUSE()
        {
            #region == Active when pressed, Hold And Up ==

            // Pause
            LibControllButton(
                ref VirtualInputManager.Instance.InputAttr.Resume,
                VirtualInputManager.Instance.KeyAttr.Back,
                MY_BTN_CODE.Btn_Resume,
                true,
                false,
                false,
                PCUseAll: true,
                PhoneUseKey: true)
                ;
            //LibControllButton(
            //    ref VirtualInputManager.Instance.InputAttr.MenuPause,
            //    VirtualInputManager.Instance.KeyAttr.Escape,
            //    MY_BTN_CODE.Btn_Back,
            //    true,
            //    false,
            //    false,
            //    PCUseAll: true,
            //    PhoneUseKey: true
            //    );
            LibControllButton(
                ref VirtualInputManager.Instance.InputAttr.BackToMainmenu,
                MY_BTN_CODE.Btn_BackMM,
                true,
                false,
                false,
                PCUseBtn: true
                );
            LibControllButton(
                ref VirtualInputManager.Instance.InputAttr.QuitGame,
                MY_BTN_CODE.Btn_Exit,
                true,
                false,
                false,
                PCUseBtn: true
                );

            #endregion == Active when pressed, Hold And Up ==

        }

        #endregion === UpdateInput_MAIN_GP_PAUSE ===
        #endregion === Update all Input_MAIN_GP ===

        #region === Axis Input System ===
        private void InputAxis()
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
            else if (MyfixedJoystick != null && MyfixedJoystick.finalInput.magnitude > JoypadAxisMinInputMagnitude)
            {
                //Debug.Log("cekcek mag : " + VirtualButtonManager.Instance.BtnAttr.AxisFinalInput.magnitude);
                VirtualInputManager.Instance.InputAttr.Move = true;
                if (MyfixedJoystick.Horizontal > JoypadAxisMinInputMove)
                {
                    VirtualDataInputManager.Instance.AxisFinalInput.x = MyfixedJoystick.Horizontal * JoypadAxisAcc > 1 ? 1 : MyfixedJoystick.Horizontal * JoypadAxisAcc;
                }
                else if (MyfixedJoystick.Horizontal < -JoypadAxisMinInputMove)
                {
                    VirtualDataInputManager.Instance.AxisFinalInput.x = MyfixedJoystick.Horizontal * JoypadAxisAcc < -1 ? -1 : MyfixedJoystick.Horizontal * JoypadAxisAcc;
                }
                else
                {
                    VirtualDataInputManager.Instance.AxisFinalInput.x = 0;
                }

                if (MyfixedJoystick.Vertical > JoypadAxisMinInputMove)
                {
                    VirtualDataInputManager.Instance.AxisFinalInput.y = MyfixedJoystick.Vertical * JoypadAxisAcc > 1 ? 1 : MyfixedJoystick.Vertical * JoypadAxisAcc;
                }
                else if (MyfixedJoystick.Vertical < -JoypadAxisMinInputMove)
                {
                    VirtualDataInputManager.Instance.AxisFinalInput.y = MyfixedJoystick.Vertical * JoypadAxisAcc < -1 ? -1 : MyfixedJoystick.Vertical * JoypadAxisAcc;
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

        #endregion === Axis Input System ===
    }
}
        