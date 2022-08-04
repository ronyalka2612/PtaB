using System;
using UnityEngine;
using MyBox;
using Com.GNL.URP_MyLib;

namespace Com.GNL.URP_MyLibProjectTest
{
    [Serializable]
    public class StateStarter : BaseState, IStates
    {

        [Header("Without Loaded")]
        [Tooltip("Just for Unity editor, work with youa already listing are scenes in hirarcy, and you set the state that active in the scene that you adjust")]
        public bool IsWithoutLoad;
        [ConditionalField(nameof(IsWithoutLoad))] 
        public LibMasterSceneConstruct ScenesWithoutLoad;
        
        [ConditionalField(nameof(IsWithoutLoad), true)]
        public LibMasterSceneConstruct ScenesFromStart;

        public override bool MySttFind()
        {
            return true;
        }

        //when you nee to do someting when state not active, but still same scene, you can do in this something in this func
        //mostly to do with sub state
        public override void MySttDisable() 
        {
            
        }
        // when in one any many state butthe state curent not active, and you one to do something when state enable, put on this function
        // one time after use this state
        public override void MySttEnable(bool isFindALl) 
        {
            
        }

        public override void MySttExit() 
        {
            
        }

        // when already change to the scene that any this class, there is time to do something just for that time
        // one time after load the scene
        public override void MySttStart() 
        {
            if (Formulation.GetInstansLibSceneController().StateFunc.GetFindAll())
            {
#if UNITY_EDITOR
                if (IsWithoutLoad)
                    Formulation.GetInstansLibSceneController().SetChangeScene((LibMasterSceneConstruct)ScenesWithoutLoad, false);
                else
#endif
                    Formulation.GetInstansLibSceneController().SetChangeScene(ScenesFromStart);
            }



        }

        public override void MySttUpdate() 
        {
//            if(LibMasterScenesController.Instance.StateFunc.GetFindAll())
//            { 
//#if UNITY_EDITOR
//                if (IsWithoutLoad)
//                    LibScenesController.Instance.SetChangeScene((LibMasterSceneConstruct)ScenesWithoutLoad, false);
//                else
//#endif
//                    LibScenesController.Instance.SetChangeScene(ScenesFromStart);
//            }

        }


        public void LoadScene_MAINMENU()
        {
            
        }

    }


}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  