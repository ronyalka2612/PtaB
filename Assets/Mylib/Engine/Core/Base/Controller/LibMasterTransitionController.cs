using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.GNL.URP_MyLib
{
    public class LibMasterTransitionController : MonoBehaviourMyBase
    {

        [Header("Transition Used")]
        public GameObject TST_USE;

        [Header("All Transition")]
        public TransitionFading Tst_Fading;


        private void Update()
        {
            StateFunc.StateUpdate(Update_State, StateFunc.StateChanging(StateChanging, SubStateChanging, CheckingAllfind));
        }

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


        public virtual void StateChanging()
        {
            
        }



        public virtual void SubStateChanging()
        {

        }


    }
}