
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

using Com.GNL.URP_MyLibProjectTest;

namespace Com.GNL.URP_MyLib
{
    public class LibStatesController : LibMasterStatesController, ILibController, ILibControllerExit, ILibStateController
    {
        [HideInInspector] public int CurrentTab;
        public string ToolbarTab;

        #region === Declare Public vairiable ===
        public string Tab1;
        //[Header("SceneController")]
        //public LibScenesController ScnController;

        [Space(10)]
        [Header("State Setting")]
        [Min(0)]
        public LibEdStateUtilities.GameStates State;

        public string SkipSaved1;
        [Space(10)]
        [Header("State Starter")]
        public StateStarter SttStarter;
        public string SerializableEnd2;
        [Space(10)]
        [Header("State Mainmenu")]
        public StateMainmenu SttMainmenu;
        public string SerializableEnd3;
        [Space(10)]
        [Header("State Mainmenu")]
        public StateMainmenu2 SttMainmenu2;
        public string SerializableEnd4;
        //[Space(10)]
        //[Header("State MultiPlayer")]
        //public StateMultiPlayer SttMultiPlayer;
        //public string SerializableEnd4;
        [Space(10)]
        [Header("State MAIN_GP")]
        public StateGP_PinBall SttMAIN_GP;
        public string SerializableEnd5;

        public string SkipSavedEnd1;


        public string TabEnd;
        #endregion === Declare Public vairiable ===

        [HideInInspector]
        public BaseState LastState;

        //#region === Singleton_Lib ===
        //private static LibStatesController _instance;
        //public static LibStatesController InstanceLib
        //{
        //    get
        //    {

        //        if (_instance == null)
        //        {
        //            {
        //                //Scene activeScene = SceneManager.GetSceneAt(0);

        //                var instances = FindObjectsOfType<LibStatesController>();
        //                //GameObject obj = instance.;
        //                var count = instances.Length;
        //                if (count > 0)
        //                {
        //                    if (count == 1)
        //                    {
        //                        Debug.Log("cekcekcek Is InstanceLib LibStatesController T name :" + instances[0].name + " hanya 1");
        //                        return _instance = instances[0];
        //                    }
        //                    Debug.LogWarning($"[{nameof(LibStatesController)}] There should never be more than one {nameof(LibStatesController)} in the scene, but {count} were found. The first instance found will be used, and all others will be destroyed.");
        //                    for (var i = 1; i < instances.Length; i++)
        //                        Destroy(instances[i]);
        //                    Debug.Log("cekcekcek Is InstanceLib LibStatesController T name :" + instances[0].name + " tingal 1");
        //                    return _instance = instances[0];
        //                }

        //                Debug.Log($"[{nameof(LibStatesController)}] An instance is needed in the scene and no existing instances were found, so a new instance will be created.");
        //                return _instance = new GameObject($"({nameof(LibStatesController)})")
        //                           .AddComponent<LibStatesController>();
        //            }
        //        }

        //        return _instance;
        //    }
        //}
        //#endregion === Singleton_Lib ===
        public override void LibSerialize()
        {
            SttStarter.Serialize(SttStarter, LibEdStateUtilities.GameStates.STARTER.ToString());
            SttMainmenu.Serialize(SttMainmenu, LibEdStateUtilities.GameStates.MAINMENU.ToString());
            SttMainmenu.Serialize(SttMainmenu2, LibEdStateUtilities.GameStates.MAINMENU2.ToString());
            //SttMultiPlayer.Serialize(SttMultiPlayer, LibEdStateUtilities.GameStates.MULTI_PLAYER.ToString());
            SttMAIN_GP.Serialize(SttMAIN_GP, LibEdStateUtilities.GameStates.MAIN_GP.ToString());
        }




        public override void StateChanging()
        {
            //this.enabled = false;
            switch (VirtualStateManager.Instance.CurState)
            {
                //Starter just on here, will not call on other
                #region == State STARTER == 
                case LibEdStateUtilities.GameStates.STARTER:
                    SttStarter.SerializeStart();

                    break;
                #endregion
                #region == State MAINMENU ==
                case LibEdStateUtilities.GameStates.MAINMENU:
                    SttMainmenu.SerializeStart();

                    break;
                #endregion
                //#region == State MULTI_PLAYER ==
                //case LibEdStateUtilities.GameStates.MULTI_PLAYER:
                //    //Gameplay still not have State to control in that state
                //    SttMultiPlayer.SerializeStart();
                //    break;
                //#endregion
                #region == State MAIN_GP ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    //Gameplay still not have State to control in that state
                    SttMAIN_GP.SerializeStart();
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
                    //Debug.Log("cekcek SubStateChanging MAINMENU");
                    StateFunc.SubState(SubState_MainMenu_Changing, true);

                    break;
                #endregion
                //#region == State MULTI_PLAYER ==
                //case LibEdStateUtilities.GameStates.MULTI_PLAYER:
                //    StateFunc.SubState(SubState_MultiPlayer_Changing, true);

                //    break;
                //#endregion
                #region == State MAIN_GP ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    StateFunc.SubState(SubState_MAIN_GP_Changing, true);
                    break;
                    #endregion
            }

        }

        private void SubState_MultiPlayer_Changing(LibEdStateUtilities.GameSubStates curSubState)
        {
            
        }

        public override void CheckingAllfind()
        {
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == State MAINMENU ==
                case LibEdStateUtilities.GameStates.MAINMENU:
                    // find must be find all component first that use should make it aviable /is aactive true, and disable it in state and substate,
                    if (!SttMainmenu.SerializeFind())
                    {
                        //StateFunc.ClearState();
                        StateFunc.SetFindAll(false);

                        Debug.Log("cekcekcek Allfind StateControl MAINMENU false");
                    }
                    else
                    {
                        StateFunc.SetFindAll(true);
                        CheckingEnable(SttMainmenu);
                        CheckingEnable(SttMainmenu.SubStt_Option);
                        CheckingEnable(SttMainmenu.SubStt_MultiPlayer);

                        Debug.Log("cekcekcek Allfind StateControl MAINMENU true");
                    }

                    break;
                #endregion
                #region == State MAINMENU2 ==
                case LibEdStateUtilities.GameStates.MAINMENU2:
                    // find must be find all component first that use should make it aviable /is aactive true, and disable it in state and substate,
                    if (!SttMainmenu2.SerializeFind())
                    {
                        //StateFunc.ClearState();
                        StateFunc.SetFindAll(false);

                        Debug.Log("cekcekcek Allfind StateControl MAINMENU2 false");
                    }
                    else
                    {
                        StateFunc.SetFindAll(true);
                        CheckingEnable(SttMainmenu2);
                        CheckingEnable(SttMainmenu2.SubStt_Option2);

                        Debug.Log("cekcekcek Allfind StateControl MAINMENU2true");
                    }

                    break;
                #endregion
                #region == State MULTI_PLAYER ==

                //case LibEdStateUtilities.GameStates.MULTI_PLAYER:

                //    if (!SttMultiPlayer.SerializeFind())
                //    {
                //        StateFunc.ClearState();
                //        StateFunc.SetFindAll(false);
                //    }
                //    else
                //    {
                //        StateFunc.SetFindAll(true);
                //        base.CheckingEnable(SttMultiPlayer);
                //    }
                //    break;
                #endregion == State MULTI_PLAYER ==

                #region == State MAIN_GP ==
                case LibEdStateUtilities.GameStates.MAIN_GP:

                    if (!SttMAIN_GP.SerializeFind() || !SttMAIN_GP.SubStt_MAIN_GPPause.SerializeFind())
                    {
                        //StateFunc.ClearState();
                        StateFunc.SetFindAll(false);
                        Debug.Log("cekcekcek Allfind StateControl MAIN_GP false");
                    }
                    else
                    {
                        StateFunc.SetFindAll(true);
                        base.CheckingEnable(SttMAIN_GP);
                        base.CheckingEnable(SttMAIN_GP.SubStt_MAIN_GPPause);
                        Debug.Log("cekcekcek Allfind StateControl MAIN_GP true");
                        //base.AddSubState(LibEdStateUtilities.GameSubStates.MAIN_GP_GAMEPLAY_PAUSE, SttMAIN_GP.SubStt_MAIN_GPPause);
                    }
                    break;
                    #endregion
            }

        }



        public override void ExitSerialize(LibEdStateUtilities.GameStates lastState)
        {
            switch (lastState)
            {
                #region == State STARTER ==
                case LibEdStateUtilities.GameStates.STARTER:
                    SttStarter.SerializeExit();

                    break;
                #endregion
                #region == State MAINMENU ==
                case LibEdStateUtilities.GameStates.MAINMENU:
                    SttMainmenu.SerializeExit();

                    break;
                #endregion
                #region == State MAIN_GP ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    SttMAIN_GP.SerializeExit();
                    break;
                    #endregion
            }
        }

        private void SubState_MainMenu_Changing(LibEdStateUtilities.GameSubStates substate)
        {
            switch (substate)
            {
                #region == SubState MAINMENU_MULTI_PLAYER ==
                case LibEdStateUtilities.GameSubStates.MAINMENU_MULTI_PLAYER:
                    SttMainmenu.SubStt_MultiPlayer.SerializeStart();
                    break;
                #endregion
                #region == SubState MAINMENU_OPTION ==
                case LibEdStateUtilities.GameSubStates.MAINMENU_OPTION:
                    SttMainmenu.SubStt_Option.SerializeStart();
                    break;
                    #endregion
            }

        }



        private void SubState_MAIN_GP_Changing(LibEdStateUtilities.GameSubStates substate)
        {
            switch (substate)
            {
                #region == SubState MAIN_GP_GAMEPLAY_PAUSE ==
                case LibEdStateUtilities.GameSubStates.MAIN_GP_GAMEPLAY_PAUSE:
                    SttMAIN_GP.SubStt_MAIN_GPPause.SerializeStart();
                    break;
                    #endregion
            }

        }

        public override void Update_State()
        {
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == Input State Starter ==
                case LibEdStateUtilities.GameStates.STARTER:
                    SttStarter.SerializeUpdate();
                    break;
                #endregion
                #region == Input State MAINMENU ==
                case LibEdStateUtilities.GameStates.MAINMENU:
                    SttMainmenu.SerializeUpdate();
                    StateFunc.SubState(SubState_MainMenu_Update);
                    break;
                #endregion
                #region == Input State MAIN_GP ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    SttMAIN_GP.SerializeUpdate();
                    StateFunc.SubState(SubState_MAIN_GP_Update);
                    break;
                    #endregion
            }
        }

        private void SubState_MainMenu_Update(LibEdStateUtilities.GameSubStates substate)
        {
            switch (substate)
            {
                #region == SubState MAINMENU_MULTI_PLAYER ==
                case LibEdStateUtilities.GameSubStates.MAINMENU_MULTI_PLAYER:
                    SttMainmenu.SubStt_MultiPlayer.SerializeUpdate();
                    break;
                #endregion
                #region == SubState MAINMENU_OPTION ==
                case LibEdStateUtilities.GameSubStates.MAINMENU_OPTION:
                    SttMainmenu.SubStt_Option.SerializeUpdate();
                    break;
                    #endregion
            }
        }

        private void SubState_MAIN_GP_Update(LibEdStateUtilities.GameSubStates substate)
        {
            switch (substate)
            {
                #region == SubState GAMEPLAY_PAUSE ==
                case LibEdStateUtilities.GameSubStates.MAIN_GP_GAMEPLAY_PAUSE:
                    SttMAIN_GP.SubStt_MAIN_GPPause.SerializeUpdate();
                    break;
                    #endregion
            }
        }

    }
}