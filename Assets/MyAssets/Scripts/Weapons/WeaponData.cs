using System;
using UnityEngine;

namespace Com.GNL.URP_MyLibProjectTest
{
    [CreateAssetMenu(fileName = "WeaponData_", menuName = "GNL/Weapon/WeaponData")]
    [Serializable]
    public class WeaponData : ScriptableObject
    {
        // Start is called before the first frame update
        [Header("Weapon Data ")]
        public string IdWeapon;
        public GameObject Weapon3DModel;
        public WeaponUtilities.WEAPON_TYPE Name;
        public float AttackDamage;
        [Space(10)]

        //public int PossibilityToAttach;
        [Header("If Weapon Can Used in Multple pos attached ")]
        [SerializeField] private WeaponAttach[] WeaponAttachTo;
        void Reset()
        {
        }

        void OnValidate()
        {
            
        }

        private void Awake()
        {
            
        }
        private void OnEnable()
        {

        }
        private void OnDisable()
        {

        }
        private void OnDestroy()
        {

        }

        public WeaponAttach GetWeaponAttach(string nameId)
        {
            for(int i=0; i < WeaponAttachTo.Length ; i++ )
            {
                if(WeaponAttachTo[i].WeaponAttachToName.ToString() == nameId)
                    return WeaponAttachTo[i];
            }
            Debug.LogWarning("GetWeaponAttach of '" + nameId + "' fail.");
            return null;
        }

        public Vector3 GetPositionAdditional(string nameId)
        {
            for (int i = 0; i < WeaponAttachTo.Length; i++)
            {
                if (WeaponAttachTo[i].WeaponAttachToName.ToString() == nameId)
                    return WeaponAttachTo[i].WeaponAttachDt.PosToAttact + WeaponAttachTo[i].PosSpesific;
            }
            Debug.LogWarning("GetPositionAddition of '"+ nameId+"' fail.");
            return new Vector3();
        }

        public Quaternion GetRotationAdditional(string nameId)
        {
            for (int i = 0; i < WeaponAttachTo.Length; i++)
            {
                if (WeaponAttachTo[i].WeaponAttachToName.ToString() == nameId)
                {
                    Quaternion rot = WeaponAttachTo[i].WeaponAttachDt.RotToAttact;
                    return rot;
                }
            }
            Debug.LogWarning("GetRotationAddition of '" + nameId + "' fail.");
            return new Quaternion();
        }

        public Vector3 GetPositionOri(string nameId)
        {
            for (int i = 0; i < WeaponAttachTo.Length; i++)
            {
                if (WeaponAttachTo[i].WeaponAttachToName.ToString() == nameId)
                    return WeaponAttachTo[i].WeaponAttachDt.PosToAttact;
            }
            Debug.LogWarning("GetPositionOri of '" + nameId + "' fail.");
            return new Vector3();
        }

        public Quaternion GetRotationOri(string nameId)
        {
            for (int i = 0; i < WeaponAttachTo.Length; i++)
            {
                if (WeaponAttachTo[i].WeaponAttachToName.ToString() == nameId)
                    return WeaponAttachTo[i].WeaponAttachDt.RotToAttact;
            }
            Debug.LogWarning("GetRotationOri of '" + nameId + "' fail.");
            return new Quaternion();
        }

        public Vector3 GetPositionJustAdditional(string nameId)
        {
            for (int i = 0; i < WeaponAttachTo.Length; i++)
            {
                if (WeaponAttachTo[i].WeaponAttachToName.ToString() == nameId)
                    return WeaponAttachTo[i].PosSpesific;
            }
            Debug.LogWarning("GetPositionOri of '" + nameId + "' fail.");
            return new Vector3();
        }

        public Quaternion GetRotationJustAdditional(string nameId)
        {
            for (int i = 0; i < WeaponAttachTo.Length; i++)
            {
                if (WeaponAttachTo[i].WeaponAttachToName.ToString() == nameId)
                    return WeaponAttachTo[i].RotSpesific;
            }
            Debug.LogWarning("GetRotationOri of '" + nameId + "' fail.");
            return new Quaternion();
        }

        public Vector3 GetScaleJustAdditonal(string nameId)
        {
            for (int i = 0; i < WeaponAttachTo.Length; i++)
            {
                if (WeaponAttachTo[i].WeaponAttachToName.ToString() == nameId)
                    return WeaponAttachTo[i].SclSpesific;
            }
            Debug.LogWarning("GetPositionOri of '" + nameId + "' fail.");
            return new Vector3();
        }


    }
}