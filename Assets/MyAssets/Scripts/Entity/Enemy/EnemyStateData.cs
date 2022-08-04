using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.GNL.URP_MyLibProjectTest
{
    public abstract class EnemyStateData : ScriptableObject
    {
        public abstract void OnEnterAbility(EnemyState enemyState, Animator animator, AnimatorStateInfo stateInfo);
        public abstract void OnUpdateAbility(EnemyState enemyState, Animator animator, AnimatorStateInfo stateInfo);
        public abstract void OnUpdateEventAbility(EnemyAnimEventData Evt, Animator animator);
        public abstract void OnExitAbility(EnemyState enemyState, Animator animator, AnimatorStateInfo stateInfo);
    }
}
