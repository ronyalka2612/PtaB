using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.GNL.URP_MyLib
{
    [Serializable]
    public class LibMasterSceneConstruct
    {

        //rework
        public LibEdSceneUtilities.ScenesAdditive[] SceneAdditive = new LibEdSceneUtilities.ScenesAdditive[0];
        public LibEdStateUtilities.GameStates State = LibEdStateUtilities.GameStates.NO_STATE;
        public List<LibEdStateUtilities.GameSubStates> Substates = new List<LibEdStateUtilities.GameSubStates>();

    }
}