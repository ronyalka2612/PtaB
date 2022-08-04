using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Com.GNL.URP_MyLibProjectTest;

namespace Com.GNL.URP_MyLib
{
    public class LibTransitionController : LibMasterTransitionController, ILibController
    {


        public override void StateChanging()
        {
            //this.enabled = false;
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == State MAINMENU ==
                case LibEdStateUtilities.GameStates.MAINMENU:

                    break;
                #endregion
            }
        }


        public override void Update_State()
        {
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == Input State MAINMENU ==
                case LibEdStateUtilities.GameStates.MAINMENU:
                    break;
                #endregion
            }
        }


        public override void SubStateChanging()
        {
            

        }

        public override void CheckingAllfind()
        {
            StateFunc.SetFindAll(true);
        }

        private void State_Gameplay_Changing()
        {
            State_Gameplay_Initiate();
        }

        private void State_Gameplay_Initiate()
        {

        }

    }
}