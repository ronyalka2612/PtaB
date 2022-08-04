using System.Collections;
using UnityEngine;
using System;

namespace Com.GNL.URP_MyLib
{
    public class VirtualInputManager : LibSingletonManager<VirtualInputManager>
    {
        private void Awake()
        {
            InputAttr = new LibEdInputAttr();
            KeyAttr = new LibEdKeyAttr();
            LibFormulation.AwakeSingletonObj(this.gameObject);
        }

        public LibEdKeyAttr KeyAttr;
        public LibEdInputAttr InputAttr;
    }
}