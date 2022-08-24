using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Com.GNL.URP_MyLib;

namespace Com.GNL.URP_MyLibProjectTest
{
    [Serializable]
    public class StateMainmenu2Option : BaseSubstate
    {
        public StateMainmenu2Option(BaseState classOfMainState, string subStateName)
        {
            SerializeState(classOfMainState, subStateName);
        }


        public override bool MySttFind()
        {
            //Debug.Log("cekcekcek StateMainmenuOption MySttFind");
            return true;
        }

        //when you nee to do someting when state not active, but still same scene, you can do in this something in this func
        //mostly to do with sub state
        public override void MySttDisable()
        {
            //Debug.Log("cekcekcek StateMainmenuOption MySttDisable");
        }
        // when in one any many state butthe state curent not active, and you one to do something when state enable, put on this function
        // one time after use this state
        public override void MySttEnable(bool isFindAll)
        {
            //Debug.Log("cekcekcek StateMainmenuOption MySttEnable");
        }

        public override void MySttExit() 
        {
            //Debug.Log("cekcekcek StateMainmenuOption MySttDisable");
        }

        // when already change to the scene that any this class, there is time to do something just for that time
        // one time after load the scene
        public override void MySttStart()
        {
            //Debug.Log("cekcekcek StateMainmenuOption MySttStart");
        }

        public override void MySttUpdate()
        {
            //Debug.Log("cekcekcek StateMainmenuOption MySttUpdate");
            SelectBack();
        }
        private void SelectBack()
        {
            if (VirtualInputManager.Instance.InputAttr.Back)
            {
                SerializeDisable();
                VirtualInputManager.Instance.InputAttr.NormalizeInput();
                //ClassOfMainState.SerializeEnable();
                //cekcek 2
            }
        }


    }

}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  