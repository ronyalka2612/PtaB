using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.GNL.URP_MyLib
{
    public class VirtualStateManager : LibSingletonManager<VirtualStateManager>
    {
        public void Awake()
        {
            //base.Awake();
            LibFormulation.AwakeSingletonObj(this.gameObject);

            CurSubStateActive = new List<LibEdStateUtilities.GameSubStates>();
            CurState = LibEdStateUtilities.GameStates.STARTER;
            CurSubStateActive.Add(LibEdStateUtilities.GameSubStates.STARTER_SUB);
        }

        public LibEdStateUtilities.GameStates CurState ;
        public List<LibEdStateUtilities.GameSubStates> CurSubStateActive;
    }
}