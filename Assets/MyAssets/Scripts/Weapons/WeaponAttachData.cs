using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.GNL.URP_MyLibProjectTest
{
    [CreateAssetMenu(fileName = "WpnAtch_", menuName = "GNL/Weapon/WeaponAttachData")]
    [System.Serializable]
    public class WeaponAttachData : ScriptableObject
    {
        [Header("Attach Object")]
        public WeaponUtilities.WEAPON_ATTACH AttachPart;
        [Space(10)]
        [Header("Exact Transform")]
        public Vector3 PosToAttact;
        public Quaternion RotToAttact;

        private void Awake()
        {
           
        }

    }
}