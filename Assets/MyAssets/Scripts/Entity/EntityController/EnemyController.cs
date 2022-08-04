using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Com.GNL.URP_MyLib;

namespace Com.GNL.URP_MyLibProjectTest
{
    public class EnemyController : MonoBehaviourMyBase
    {



        //private Animator _anim;
        //private List<EnemyStateData> _listAbilityData;



        //// Start is called before the first frame update
        //public enum ANIM_PARAM
        //{
        //    // locomotion
        //    isMove,
        //    isCombat,
        //    isForceTransition,
        //    COUNT

        //}

        //void Start()
        //{
        //    StateFunc.ClearState();

        //    Initialization();
        //}

        //private void Initialization()
        //{
        //    if (_anim == null)
        //    {
        //        _anim = this.gameObject.GetComponentInChildren<Animator>();

        //    }
        //}

        //// Update is called once per frame
        //void Update()
        //{
        //    StateFunc.StateUpdate(Update_State, StateFunc.StateChanging(StateChanging, SubStateChanging, CheckingAllfind));
        //}

        //private void Update_State()
        //{
        //    switch (VirtualStateManager.Instance.CurState)
        //    {
        //        #region == State MAINMENU ==
        //        case LibEdStateUtilities.GameStates.MAINMENU:
        //            break;
        //        #endregion
        //    }
        //}



        //private void StateChanging()
        //{
            
        //    switch (VirtualStateManager.Instance.CurState)
        //    {
        //        #region == State MAINMENU ==
        //        case LibEdStateUtilities.GameStates.MAINMENU:
                        
        //            break;
        //        #endregion
        //    }
        //}


        //private void SubStateChanging()
        //{
           
        //    switch (VirtualStateManager.Instance.CurState)
        //    {
        //        #region == State MAINMENU ==
        //        case LibEdStateUtilities.GameStates.MAINMENU:
        //            StateFunc.SubState(SubState_MainMenu_Changing, true);
        //            break;
        //        #endregion
        //    }
            
        //}


        //public override void CheckingAllfind()
        //{
        //    if (VirtualStateManager.Instance.CurState == LibEdStateUtilities.GameStates.GAMEPLAY && _anim == null)
        //    {
        //        StateFunc.SetFindAll(false);
        //        StateFunc.ClearState();
        //    }
        //    else
        //    {
        //        StateFunc.SetFindAll(true);
        //    }
        //}

        //private void SubState_Gameplay_Changing()
        //{
        //    Initialization();
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

        //private void SubState_Gameplay_Changing(LibEdStateUtilities.GameSubStates substate)
        //{
            
        //    switch (substate)
        //    {
        //        #region == SubState MAINMENU_FIRST ==
        //        case LibEdStateUtilities.GameSubStates.GAMEPLAY_NORMAL_PLAYING:

        //            break;
        //        #endregion
        //        #region == SubState MAINMENU_FIRST ==
        //        case LibEdStateUtilities.GameSubStates.GAMEPLAY_PAUSE:

        //            break;
        //            #endregion
        //    }
            
        //}

        //private void OnCollisionEnter(Collision collision)
        //{
        //    // never get here
        //    //if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        //    //{
        //    //    Debug.Log("enemy onCol enter col with : " + collision.collider.gameObject.layer);
        //    //}

        //    //Debug.Log(" onCol OnCollisionEnter col with : " + collision.transform.name);

        //}


        //private void OnCollisionStay(Collision collision)
        //{
        //    //Debug.Log(" onCol OnCollisionStay col with : " + collision.transform.name);
        //}

        //private void OnCollisionExit(Collision collision)
        //{
        //    //Debug.Log(" onCol OnCollisionExit col with : " + collision.transform.name);
        //}


        //private void OnTriggerEnter(Collider other)
        //{
        //    //Debug.Log("onTri OnTriggerEnter with :" + other.transform.name);
        //}
        //private void OnTriggerStay(Collider other)
        //{
        //    //Debug.Log("onTri OnTriggerStay with :" + other.transform.name);
        //}
        //private void OnTriggerExit(Collider other)
        //{
        //    //Debug.Log("onTri OnTriggerExit with :" + other.transform.name);

        //}


        //public EnemyController GetEnemyController()
        //{
        //    return this;
        //}

        //public void SetListAbilityData(List<EnemyStateData> listAbilityData)
        //{
        //    if(listAbilityData != null && listAbilityData.Count > 0)
        //        _listAbilityData = listAbilityData;
        //}

        //public void AnimEventUpdate(EnemyAnimEventData Evt)
        //{
        //    if (_listAbilityData != null)
        //    {
        //        foreach (EnemyStateData d in _listAbilityData)
        //        {
        //            d.OnUpdateEventAbility(Evt, _anim);
        //        }
        //    }
        //}
    }
}