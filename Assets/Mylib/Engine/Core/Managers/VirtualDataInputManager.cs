using UnityEngine;

namespace Com.GNL.URP_MyLib
{
    public class VirtualDataInputManager : LibSingletonManager<VirtualDataInputManager>
    {
        private void Awake()
        {
            LibFormulation.AwakeSingletonObj(this.gameObject);
        }

        [Header("GamePlay_Data")]
        public Vector2 AxisFinalInput;
        //public Vector2 AxisFinalInputCam;
    }
}