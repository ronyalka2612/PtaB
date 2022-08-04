using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.GNL.URP_MyLibProjectTest
{
#if UNITY_EDITOR
    [System.Serializable]
    public class WeaponLiveTransfom 
    {
        [Header("Pos Attach Ori")]
        public Vector3 Pos;
        public Vector3 Scl;

        [Header("Pos Attach Additonal")]
        public Vector3 PosAdd;
        public Quaternion RotAdd;
        public Vector3 SclAdd;
        [HideInInspector]public WeaponUtilities.WeaponAttach IdAttach;

        WeaponLiveTransfom()
        { 

        }
    }
#endif
}