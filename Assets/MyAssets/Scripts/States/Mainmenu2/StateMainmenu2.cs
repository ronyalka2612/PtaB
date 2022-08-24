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
    public class StateMainmenu2 : BaseState
    {

        //[Header("All Child")]
        //public StateGP_PinBall_Pause SubStt_MAIN_GPPause;
        //[Space(10)]

        [Header("All Child")]
        public StateMainmenu2Option SubStt_Option2;

        [Header("Set Change State from here")]
        public LibMasterSceneConstruct[] Scenes;

        private GameObject LoadingLayer_main;
        //[HideInInspector] public LibMasterGameController CtrlGame;

        public override void Serialize(BaseState classOfMainState, string nameState)
        {
            base.Serialize(classOfMainState, nameState);
            SubStt_Option2 = new StateMainmenu2Option(classOfMainState, LibEdStateUtilities.GameSubStates.MAINMENU2_OPTION.ToString());

        }

        public override bool MySttFind()
        {
            if (SubStt_Option2.SerializeFind() 
                )
            {
                Debug.Log("cekcekcek StateMainMenu Find Substate : Fine");
                if (LoadingLayer_main
                    )
                    return true;
            }

            bool isFindAll = false;
                isFindAll = true;
            
            return isFindAll;
        }

        //when you nee to do someting when state not active, but still same scene, you can do in this something in this func
        //mostly to do with sub state, it called in FindALL with chaker iSenable for forst time(set on inspector State Controller)
        public override void MySttDisable()
        {
           

        }
        // when in one any many state butthe state curent not active, and you one to do something when state enable, put on this function
        // one time after use this state
        public override void MySttEnable(bool isFindAll)
        {
            if (isFindAll)
                LoadingLayer_main.SetActive(false);
        }


        public override void MySttExit() 
        {
            SubStt_Option2.SerializeExit();
        }

        // when already change to the scene that any this class, there is time to do something just for that time
        // one time after load the scene
        public override void MySttStart() 
        {
            
        }

        public override void MySttUpdate() 
        {

        }

    }

}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  