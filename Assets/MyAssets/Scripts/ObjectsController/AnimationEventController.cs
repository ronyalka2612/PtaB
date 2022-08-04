using MyBox;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

using Com.GNL.URP_MyLib;

namespace Com.GNL.URP_MyLibProjectTest
{
    public class AnimationEventController : MonoBehaviourMyBase
    {
        //[Header("Any Controller that Have Event")]
        //public GameObject GOController;

        ////[HideInInspector] public Animation AnmEvent1;
        //[Header("List Entity that use Anim Event")]
        ////[MyBox.ReadOnly] [SerializeField] private PlayerController _PC = null;
        ////[MyBox.ReadOnly] [SerializeField] private EnemyController _EC = null;


        //[HideInInspector] public AnimationEvent AnmEvent;


        //public enum ANIM_STATE
        //{
        //    PlayerStateMovement,
        //    PlayerStateJump_move,
        //    PlayerStateJump_idle,
        //    PlayerStateFall_landing,
        //    PlayerStateFall_air,
        //    COUNT
        //}

        //enum OBJECT_CONTROLLER
        //{
        //    Player1,
        //    EnemyLead,
        //    COUNT
        //}

        //public void Initialization()
        //{
        //    StateFunc.ClearState();

        //    AnmEvent = new AnimationEvent();


        //    if (GOController.name == OBJECT_CONTROLLER.Player1.ToString())
        //    {
        //        _PC = GOController.GetComponent<PlayerController>();
        //        //Debug.Log("cekcekcek Player1 _PC:"+ _PC);
        //        //Debug.Log("cekcekcek Player1 _EC:" + _EC);
        //    }

        //    else if (GOController.name == OBJECT_CONTROLLER.EnemyLead.ToString())
        //    {
        //        _EC = GOController.GetComponent<EnemyController>();
        //        //Debug.Log("cekcekcek EnemyLead _PC:" + _PC);
        //        //Debug.Log("cekcekcek EnemyLead _EC:" + _EC);
        //    }
        //}

        //private void OnValidate()
        //{
        //    if (GOController.name == OBJECT_CONTROLLER.Player1.ToString() && _PC == null)
        //    {
        //        _PC = GOController.GetComponent<PlayerController>();
        //        //Debug.Log("cekcekcek Player1 _PC:" + _PC);
        //        //Debug.Log("cekcekcek Player1 _EC:" + _EC);
        //    }

        //    else if (GOController.name == OBJECT_CONTROLLER.EnemyLead.ToString() && _EC == null)
        //    {
        //        _EC = GOController.GetComponent<EnemyController>();
        //        //Debug.Log("cekcekcek EnemyLead _PC:" + _PC);
        //        //Debug.Log("cekcekcek EnemyLead _EC:" + _EC);
        //    }
        //}
        //void Start()
        //{
        //    Initialization();
        //}
        


        //private void StateChanging()
        //{
            
        //    switch (VirtualStateManager.Instance.CurState)
        //    {
        //        #region == State MAINMENU ==
        //        case LibEdStateUtilities.GameStates.MAINMENU:
        //            State_MAINMENU_Changing();
        //            break;
        //        #endregion
        //        #region == State GAMEPLAY ==
        //        case LibEdStateUtilities.GameStates.GAMEPLAY:
                    
        //            break;
        //            #endregion
        //    }
        //}


        //private void SubStateChanging()
        //{

        //    switch (VirtualStateManager.Instance.CurState)
        //    {
        //        #region == State MAINMENU ==
        //        case LibEdStateUtilities.GameStates.MAINMENU:
        //            StateFunc.SubState(SubState_MainMenu_Changing, true);
        //            //SubStateMainMenuChanging();
        //            break;
        //        #endregion
        //        #region == State GAMEPLAY ==
        //        case LibEdStateUtilities.GameStates.GAMEPLAY:
        //            //_curSubState = StateFunc.SubState(SubState_Gameplay_Changing, VirtualStateManager.Instance.CurSubStateActive, _curSubState, true);
        //            //SubStateGameplayChanging();
        //            break;
        //            #endregion
        //    }
        //}

        //private void ChekingAllFind()
        //{
        //    StateFunc.SetFindAll(true);
        //}


        //private void State_MAINMENU_Changing()
        //{

        //}

        //private void SubState_MainMenu_Changing(LibEdStateUtilities.GameSubStates substate)
        //{
        //    switch (substate)
        //    {
        //        #region == SubState MAINMENU_FIRST ==
        //        case LibEdStateUtilities.GameSubStates.MAINMENU_FIRST:

        //            break;
        //        #endregion
        //        #region == SubState MAINMENU_OPTION ==
        //        case LibEdStateUtilities.GameSubStates.MAINMENU_OPTION:

        //            break;
        //            #endregion
        //    }

        //}

        //void Update()
        //{
        //    StateFunc.StateUpdate(Update_State,StateFunc.StateChanging(StateChanging, SubStateChanging, ChekingAllFind));
        //    //StateChanging();


        //}

        //private void Update_State()
        //{
        //    switch (VirtualStateManager.Instance.CurState)
        //    {
        //        #region == State MAINMENU ==
        //        case LibEdStateUtilities.GameStates.MAINMENU:
        //            StateFunc.SubState(State_MainMenu_Update, true);
        //            break;
        //        #endregion
        //        #region == State GAMEPLAY ==
        //        case LibEdStateUtilities.GameStates.GAMEPLAY:
        //            State_Gameplay_Update();
        //            StateFunc.SubState(State_Gameplay_Update, true);
        //            break;
        //            #endregion
        //    }
        //}

        //private void State_Gameplay_Update()
        //{
        //    // do thing in state gameplay
        //}

        //private void State_MainMenu_Update(LibEdStateUtilities.GameSubStates substate)
        //{
        //    switch (substate)
        //    {
        //        #region == SubState MAINMENU_FIRST ==
        //        case LibEdStateUtilities.GameSubStates.MAINMENU_FIRST:

        //            break;
        //        #endregion
        //        #region == SubState MAINMENU_OPTION ==
        //        case LibEdStateUtilities.GameSubStates.MAINMENU_OPTION:

        //            break;
        //            #endregion
        //    }
        //}

        //private void State_Gameplay_Update(LibEdStateUtilities.GameSubStates substate)
        //{
        //    switch (substate)
        //    {
        //        #region == SubState GAMEPLAY_NORMAL_PLAYING ==
        //        case LibEdStateUtilities.GameSubStates.GAMEPLAY_NORMAL_PLAYING:

        //            break;
        //        #endregion
        //        #region == SubState GAMEPLAY_PAUSE ==
        //        case LibEdStateUtilities.GameSubStates.GAMEPLAY_PAUSE:

        //            break;
        //            #endregion
        //    }
        //}

        //private void FixedUpdate()
        //{
            
        //}
        //private void LateUpdate()
        //{
            
        //}

        //public void UpdateEvent(Object evt)
        //{
        //    if (GOController.name == OBJECT_CONTROLLER.Player1.ToString())
        //    {
        //        //Debug.Log("cek Player1 GOController.name :" + GOController.name);
        //        _PC.AnimEventUpdate((PlayerAnimEventData)evt);

        //    }
        //    else if (GOController.name == OBJECT_CONTROLLER.EnemyLead.ToString())
        //    {
        //        //Debug.Log("cek EnemyLead GOController.name :" + GOController.name);
        //        _EC.AnimEventUpdate((EnemyAnimEventData)evt);
        //    }
        //}

        //public void UpdateEventPlayer(Object evt)
        //{
        //    _PC.AnimEventUpdate((PlayerAnimEventData)evt);
        //}
        //public void UpdateEventEnemy(Object evt)
        //{
        //    _EC.AnimEventUpdate((EnemyAnimEventData)evt);
        //}
    }
}
