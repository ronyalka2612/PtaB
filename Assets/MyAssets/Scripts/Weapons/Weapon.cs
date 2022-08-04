using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.GNL.URP_MyLibProjectTest
{
    [Serializable]
    public class Weapon
    {
        public WeaponData WeaponData;
        public WeaponUtilities.WEAPON_ATTACH WeaponAttachTo = 0;

        public Weapon(WeaponData WeaponData, WeaponUtilities.WEAPON_ATTACH WeaponAttachTo) {
            this.WeaponData = WeaponData;
            this.WeaponAttachTo = WeaponAttachTo;
        }

    }
}