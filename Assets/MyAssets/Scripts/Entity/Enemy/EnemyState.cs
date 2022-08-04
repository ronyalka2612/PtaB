using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.GNL.URP_MyLibProjectTest
{
    public class EnemyState : StateMachineBehaviour
    {
        //public List<EnemyStateData> ListAbilityData;

        //private EnemyController _enemyController;

        ////private PlayerInput PlayerInput;

        //public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        //{
        //    //Debug.Log("cekcekcek OnStateMachineEnter");
        //}

        //public void EnterAll(EnemyState enemyState, Animator animator, AnimatorStateInfo stateInfo)
        //{
        //    GetController(animator).SetListAbilityData(ListAbilityData);
        //    foreach (EnemyStateData d in ListAbilityData)
        //    {
        //        d.OnEnterAbility(enemyState, animator, stateInfo);
        //    }
        //}

        //public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    EnterAll(this, animator, stateInfo);
        //}

        //public void UpdateAll(EnemyState enemyState, Animator animator, AnimatorStateInfo stateInfo)
        //{

        //    GetController(animator).SetListAbilityData(ListAbilityData);
        //    foreach (EnemyStateData d in ListAbilityData)
        //    {
        //        d.OnUpdateAbility(enemyState, animator, stateInfo);
        //    }
        //}

        //public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    UpdateAll(this, animator, stateInfo);
        //    //Debug.Log("cekcekcek OnStateUpdate");
        //}

        //public void ExitAll(EnemyState enemyState, Animator animator, AnimatorStateInfo stateInfo)
        //{
        //    GetController(animator).SetListAbilityData(ListAbilityData);
        //    foreach (EnemyStateData d in ListAbilityData)
        //    {
        //        d.OnExitAbility(enemyState, animator, stateInfo);
        //    }
        //}

        //public override void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    ExitAll(this, animator, stateInfo);
        //}

        //public EnemyController GetController(Animator animator)
        //{
        //    if (_enemyController == null)
        //    {
        //        _enemyController = animator.GetComponentInParent<EnemyController>();
        //    }
        //    return _enemyController;
        //}



    }

}