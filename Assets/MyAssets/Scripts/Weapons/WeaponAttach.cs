using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.GNL.URP_MyLibProjectTest
{
    [System.Serializable]
    public class WeaponAttach
    {
        public WeaponAttachData WeaponAttachDt;
        public WeaponUtilities.WEAPON_ATTACH WeaponAttachToName;
        
        [Header("Transform")]
        public Vector3 PosSpesific;
        public Quaternion RotSpesific;
        public Vector3 SclSpesific;

        public WeaponAttach(WeaponAttachData WeaponAttachDt, Vector3 PosSpesific, Quaternion RotSpesific, Vector3 SclSpesific, WeaponUtilities.WEAPON_ATTACH WeaponAttachToName) {
            this.WeaponAttachDt = WeaponAttachDt;
            this.PosSpesific = PosSpesific;
            this.RotSpesific = RotSpesific;
            this.SclSpesific = SclSpesific;
            this.WeaponAttachToName = WeaponAttachToName;
        }
    }
}