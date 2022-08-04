using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.GNL.URP_MyLibProjectTest
{
    //[CreateAssetMenu(fileName = "FT_", menuName = "GNL/Enemy/AbilityData/ForceTransition")]
    //public class ForceTransitionEnemy : EnemyStateData
    //{
    //    [Range(0.01f, 1f)]
    //    public float TransitionTiming;
    //    public override void OnEnterAbility(EnemyState enemyState, Animator animator, AnimatorStateInfo stateInfo)
    //    {
    //        animator.SetBool(PlayerController.ANIM_PARAM.isForceTransition.ToString(), false);
    //    }


    //    public override void OnUpdateAbility(EnemyState enemyState, Animator animator, AnimatorStateInfo stateInfo)
    //    {
    //        if (stateInfo.normalizedTime >= TransitionTiming)
    //        {
    //            animator.SetBool(PlayerController.ANIM_PARAM.isForceTransition.ToString(), true);
    //        }
    //    }

    //    public override void OnExitAbility(EnemyState enemyState, Animator animator, AnimatorStateInfo stateInfo)
    //    {
    //        animator.SetBool(PlayerController.ANIM_PARAM.isForceTransition.ToString(), false);
    //    }


    //    public override void OnUpdateEventAbility(EnemyAnimEventData Evt, Animator animator)
    //    {
    //        //Debug.Log("cekcek **OnUpdateEventAbility** ForceTransition idEvent=" + Evt.id);
    //    }

    //}
}
