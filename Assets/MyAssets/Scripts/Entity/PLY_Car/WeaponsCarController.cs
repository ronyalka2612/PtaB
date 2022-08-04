using MyBox;
using System;
using System.Collections.Generic;
using UnityEngine;

using Com.GNL.URP_MyLib;
using System.Collections;

namespace Com.GNL.URP_MyLibProjectTest
{
    public class WeaponsCarController : MonoBehaviourMyBase
    {
        //#if UNITY_EDITOR

        //        [TextArea(5, 10)]
        //        [SerializeField] private string Notes;
        //#endif
        //        [Header("Who controlling the weapons")]
        //        public PLY_CarRB CarController;


        //        [Header("Transform Rig Root")]
        //        [SerializeField] private Transform Rootransform;
        //        [Header("Use Sample")]
        //        [SerializeField] bool _isUseSample;
        //        [SerializeField] bool _isUseInGame;
        //        [SerializeField] bool _isSampleStillSame;


        //        public Weapon[] WeaponUsed = new Weapon[1];


        //        public IDictionary<int, GameObject> WeaponsObj;

        //        #region == Declaration for UNITY EDITOR ==
        //#if UNITY_EDITOR
        //        public WeaponLiveTransfom[] WeaponLiveTrns = new WeaponLiveTransfom[1];
        //#endif
        //        #endregion

        //        #region == Function for UNITY EDITOR ==
        //#if UNITY_EDITOR
        //        void UpdateInEditor()
        //        {
        //            if (WeaponUsed.Length > 0)
        //            {
        //                for (int x = 0; x < GetCountWeapon(); x++)
        //                {
        //                    //GetWeapon(WeaponLiveTrns[x].IdAttach.ToString()).transform.localPosition = WeaponLiveTrns[x].Pos;
        //                    ////GetWeapon(WeaponLiveTrns[x].IdAttach.ToString()).transform.localRotation = WeaponLiveTrns[x].Rot;
        //                    //GetWeapon(WeaponLiveTrns[x].IdAttach.ToString()).transform.localScale = WeaponLiveTrns[x].Scl;

        //                    //GetWeapon(WeaponLiveTrns[x].IdAttach.ToString()).transform.localPosition += WeaponLiveTrns[x].PosAdd;
        //                    //GetWeapon(WeaponLiveTrns[x].IdAttach.ToString()).transform.localRotation = WeaponLiveTrns[x].RotAdd;
        //                    //GetWeapon(WeaponLiveTrns[x].IdAttach.ToString()).transform.localScale += WeaponLiveTrns[x].SclAdd;
        //                }
        //            }
        //        }
        //#endif
        //        #endregion
        //        void Reset()
        //        {
        //#if UNITY_EDITOR
        //            WeaponLiveTrns = new WeaponLiveTransfom[WeaponUsed.Length];
        //#endif

        //        }

        //        private void Awake()
        //        {
        //            _isUseSample = false;

        //            for (int i = 0; i < WeaponUsed.Length; i++)
        //            {
        //                if (WeaponUsed[i].WeaponData == null)
        //                {
        //                    return;
        //                }
        //                GameObject wpnTranform = LibFormulation.GetChildWithName(transform.parent, ConvertAttach(WeaponUsed[i].WeaponAttachTo)).gameObject;
        //                if (wpnTranform.transform.childCount > 0)
        //                {
        //                    // it does not Destroy couse is ust for unity Editor
        //#if UNITY_EDITOR
        //                    //if (Application.isEditor)
        //                    //{
        //                    //    UnityEditor.EditorApplication.delayCall += () =>
        //                    //    {
        //                            DestroyImmediate(wpnTranform.transform.GetChild(0).gameObject);
        //                    //    };
        //                    //}
        //                    //else
        //#endif
        //                        //Destroy(wpnTranform.transform.GetChild(0).gameObject);
        //                }
        //            }
        //        }

        //        void OnValidate()
        //        {
        //#if UNITY_EDITOR

        //            if (WeaponLiveTrns.Length != WeaponUsed.Length)
        //            {
        //                Array.Resize(ref WeaponLiveTrns, WeaponUsed.Length);
        //            }


        //            if (_isUseSample)
        //            {
        //                for (int i = 0; i < WeaponUsed.Length; i++)
        //                {
        //                    Transform wpnTranform = LibFormulation.GetChildWithName(transform.parent, ConvertAttach(WeaponUsed[i].WeaponAttachTo));
        //                    if (wpnTranform.childCount < 1)
        //                    {
        //                        if (!Application.isPlaying)
        //                            InitiateWeaponGO(WeaponUsed[i].WeaponData, WeaponUsed[i].WeaponAttachTo);
        //                        else
        //                            InitiateWeapon(WeaponUsed[i].WeaponData, WeaponUsed[i].WeaponAttachTo);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                for (int i = 0; i < WeaponUsed.Length; i++)
        //                {
        //                    if (WeaponUsed[i].WeaponData == null)
        //                    {
        //                        return;
        //                    }
        //                    GameObject wpnTranform = LibFormulation.GetChildWithName(transform.parent, ConvertAttach(WeaponUsed[i].WeaponAttachTo)).gameObject;
        //                    if (wpnTranform.transform.childCount > 0)
        //                    {
        //                        // it does not Destroy couse is ust for unity Editor
        //#if UNITY_EDITOR
        //                        if (Application.isEditor)
        //                        {
        //                            UnityEditor.EditorApplication.delayCall += () =>
        //                            {
        //                                DestroyImmediate(wpnTranform.transform.GetChild(0).gameObject);
        //                            };
        //                        }
        //                        else
        //#endif
        //                            Destroy(wpnTranform.transform.GetChild(0).gameObject);
        //                    }
        //                }
        //            }
        //#endif
        //        }

        //        void Start()
        //        {
        //            StateFunc.ClearState();

        //            WeaponsObj = new Dictionary<int, GameObject>();
        //            //UseWeapon();
        //            if (_isUseSample)
        //            {
        //                for (int i = 0; i < WeaponUsed.Length; i++)
        //                {
        //                    Transform wpnTranform = LibFormulation.GetChildWithName(transform.parent, ConvertAttach(WeaponUsed[i].WeaponAttachTo));
        //                    if (wpnTranform.childCount > 0)
        //                    {
        //                        //DeleteWeapon(i);
        ////#if UNITY_EDITOR
        ////                        if (Application.isEditor)
        ////                        {
        ////                            UnityEditor.EditorApplication.delayCall += () =>
        ////                            {
        ////                                DestroyImmediate(wpnTranform.transform.GetChild(0).gameObject);
        ////                            };
        ////                        }
        ////                        else
        ////#endif
        //                            Destroy(wpnTranform.transform.GetChild(0).gameObject);
        //                    }
        //                }
        //            }
        //            if (_isUseInGame)
        //            {
        //#if UNITY_EDITOR
        //                _isUseSample = true;
        //#endif
        //                for (int i = 0; i < WeaponUsed.Length; i++)
        //                {
        //                    Transform wpnTranform = LibFormulation.GetChildWithName(transform.parent, ConvertAttach(WeaponUsed[i].WeaponAttachTo));
        //                    if (wpnTranform.childCount < 1)
        //                    {
        //                        InitiateWeapon(WeaponUsed[i].WeaponData, WeaponUsed[i].WeaponAttachTo);
        //                    }
        //                }
        //                CarController.CheckWeapon();
        //            }

        //#if UNITY_EDITOR
        //            for (int x = 0; x < GetCountWeapon(); x++)
        //            {
        //                //WeaponLiveTrns[x].IdAttach = WeaponUsed[x].WeaponAttachTo;
        //                ////WeaponLiveTrns[x].Pos = GetWeapon(WeaponLiveTrns[x].IdAttach.ToString()).transform.localPosition;
        //                ////WeaponLiveTrns[x].Rot = GetWeapon(WeaponLiveTrns[x].IdAttach.ToString()).transform.localRotation;

        //                //WeaponLiveTrns[x].Pos = WeaponUsed[x].WeaponData.GetPositionOri(WeaponLiveTrns[x].IdAttach.ToString());
        //                ////WeaponLiveTrns[x].Rot = WeaponUsed[x].WeaponData.GetRotationOri(WeaponLiveTrns[x].IdAttach.ToString());
        //                //WeaponLiveTrns[x].Scl = WeaponUsed[x].WeaponData.Weapon3DModel.transform.localScale;

        //                //WeaponLiveTrns[x].PosAdd += WeaponUsed[x].WeaponData.GetPositionJustAdditional(WeaponLiveTrns[x].IdAttach.ToString());
        //                //WeaponLiveTrns[x].RotAdd = WeaponUsed[x].WeaponData.GetRotationJustAdditional(WeaponLiveTrns[x].IdAttach.ToString());
        //                //WeaponLiveTrns[x].SclAdd += WeaponUsed[x].WeaponData.GetScaleJustAdditonal(WeaponLiveTrns[x].IdAttach.ToString());
        //            }
        //#endif
        //        }


        //        void Update()
        //        {
        //            StateFunc.StateUpdate(Update_State, StateFunc.StateChanging(StateChanging, SubStateChanging, CheckingAllfind));
        //        }

        //        private void Update_State()
        //        {
        //            switch (VirtualStateManager.Instance.CurState)
        //            {
        //                case LibEdStateUtilities.GameStates.MAIN_GP:
        //#if UNITY_EDITOR
        //                    UpdateInEditor();
        //#endif
        //                    ControlWeapon();
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }

        //        private void StateChanging()
        //        {
        //            switch (VirtualStateManager.Instance.CurState)
        //            {
        //                #region == State MAINMENU ==
        //                case LibEdStateUtilities.GameStates.MAINMENU:

        //                    break;
        //                #endregion
        //                #region == State MAIN_GP ==
        //                case LibEdStateUtilities.GameStates.MAIN_GP:
        //                    State_MAIN_GP_Changing();
        //                    break;
        //                    #endregion
        //            }

        //        }
        //        private void SubStateChanging()
        //        {


        //        }


        //        public override void CheckingAllfind()
        //        {
        //            StateFunc.SetFindAll(true);
        //        }

        //        private void State_MAIN_GP_Changing()
        //        {
        //            UseWeapon();
        //        }



        //        private void ControlWeapon()
        //        {
        //            if (VirtualInputManager.Instance.InputAttr.AddWeapon)
        //            {
        //                if(WeaponUsed.Length > 0)
        //                    UseWeapon(GetCountWeapon());
        //            }

        //            if (VirtualInputManager.Instance.InputAttr.DeleteWeapon)
        //            {
        //                if (GetCountWeapon() > 0)
        //                    DeleteWeapon(GetCountWeapon());
        //            }
        //        }


        //        void UseWeapon()
        //        {

        //            for (int i = 0; i < WeaponUsed.Length; i++)
        //            {
        //                InitiateWeapon(WeaponUsed[i].WeaponData, WeaponUsed[i].WeaponAttachTo);
        //            }
        //            CarController.CheckWeapon();
        //        }

        //        private void InitiateWeapon(WeaponData weaponData, WeaponUtilities.WEAPON_ATTACH WeaponAtc)
        //        {
        //            string weaponAtcName = WeaponAtc.ToString();

        //            Transform wpnTranform = LibFormulation.GetChildWithName(transform.parent, ConvertAttach(WeaponAtc));
        //            if (wpnTranform.childCount < 1)
        //            {
        //                int id = GetCountWeapon();
        //                GameObject wpn = Instantiate(weaponData.Weapon3DModel, new Vector3(), new Quaternion(), wpnTranform);
        //                AddWeapon(id, wpn);
        //                GetWeapon(id).transform.localPosition = weaponData.GetPositionAdditional(weaponAtcName);
        //                GetWeapon(id).transform.localRotation = weaponData.GetRotationJustAdditional(weaponAtcName);
        //            }
        //            else
        //            {
        //                Debug.Log("already have weapon on :" + wpnTranform.name + ", count = " + wpnTranform.childCount);
        //            }
        //        }
        //#if UNITY_EDITOR
        //        // ust for cheking viusla in unity editor
        //        private void InitiateWeaponGO(WeaponData weaponData, WeaponUtilities.WEAPON_ATTACH WeaponAtc)
        //        {
        //            string weaponAtcName = WeaponAtc.ToString();
        //            Transform wpnTranform = LibFormulation.GetChildWithName(transform.parent, ConvertAttach(WeaponAtc));
        //            if (wpnTranform.childCount < 1)
        //            {
        //                GameObject wpn = Instantiate(weaponData.Weapon3DModel, new Vector3(), new Quaternion(), wpnTranform);
        //                //CarController
        //                wpn.transform.localPosition = weaponData.GetPositionAdditional(weaponAtcName);
        //                wpn.transform.localRotation = weaponData.GetRotationJustAdditional(weaponAtcName);
        //            }
        //            else
        //            {
        //                Debug.Log("already have weapon on :" + wpnTranform.name + ", count = " + wpnTranform.childCount);
        //            }
        //        }
        //#endif

        //        private string ConvertAttach(WeaponUtilities.WEAPON_ATTACH WeaponAtc)
        //        {
        //            string val = "";
        //            switch ((int)WeaponAtc)
        //            {
        //                case (int)WeaponUtilities.WEAPON_ATTACH.SIDE:
        //                    val = WeaponUtilities.WEAPON_ATTACH_EXIST_BODY.SIDE.ToString();
        //                    break;

        //                    //case (int)WeaponUtilities.WeaponAttach.HAND_RIGHT:
        //                    //case (int)WeaponUtilities.WeaponAttach.HAND_RIGHT_RIGHTSIDE:
        //                    //case (int)WeaponUtilities.WeaponAttach.HAND_RIGHT_LEFTSIDE:
        //                    //case (int)WeaponUtilities.WeaponAttach.HAND_RIGHT_UPSIDE:
        //                    //case (int)WeaponUtilities.WeaponAttach.HAND_RIGHT_DOWNSIDE:
        //                    //    val = WeaponUtilities.WeaponAttachExistBody.HAND_RIGHT.ToString();
        //                    //    break;
        //                    //case (int)WeaponUtilities.WeaponAttach.HAND_LEFT:
        //                    //case (int)WeaponUtilities.WeaponAttach.HAND_LEFT_RIGHTSIDE:
        //                    //case (int)WeaponUtilities.WeaponAttach.HAND_LEFT_LEFTSIDE:
        //                    //case (int)WeaponUtilities.WeaponAttach.HAND_LEFT_UPSIDE:
        //                    //case (int)WeaponUtilities.WeaponAttach.HAND_LEFT_DOWNSIDE:
        //                    //    val = WeaponUtilities.WeaponAttachExistBody.HAND_LEFT.ToString();
        //                    //    break;
        //                    //case (int)WeaponUtilities.WeaponAttach.LEG_RIGHT:
        //                    //case (int)WeaponUtilities.WeaponAttach.LEG_RIGHT_RIGHTSIDE:
        //                    //case (int)WeaponUtilities.WeaponAttach.LEG_RIGHT_LEFTSIDE:
        //                    //case (int)WeaponUtilities.WeaponAttach.LEG_RIGHT_FRONTSIDE:
        //                    //case (int)WeaponUtilities.WeaponAttach.LEG_RIGHT_BACKSIDE:
        //                    //case (int)WeaponUtilities.WeaponAttach.LEG_RIGHT_DOWNSIDE:
        //                    //    val = WeaponUtilities.WeaponAttachExistBody.LEG_RIGHT.ToString();
        //                    //    break;
        //                    //case (int)WeaponUtilities.WeaponAttach.LEG_LEFT:
        //                    //case (int)WeaponUtilities.WeaponAttach.LEG_LEFT_RIGHTSIDE:
        //                    //case (int)WeaponUtilities.WeaponAttach.LEG_LEFT_LEFTSIDE:
        //                    //case (int)WeaponUtilities.WeaponAttach.LEG_LEFT_FRONTSIDE:
        //                    //case (int)WeaponUtilities.WeaponAttach.LEG_LEFT_BACKSIDE:
        //                    //case (int)WeaponUtilities.WeaponAttach.LEG_LEFT_DOWNSIDE:
        //                    //    val = WeaponUtilities.WeaponAttachExistBody.LEG_LEFT.ToString();
        //                    //    break;
        //                    //case (int)WeaponUtilities.WeaponAttach.BELLY:
        //                    //    val = WeaponUtilities.WeaponAttachExistBody.BELLY.ToString();
        //                    //    break;
        //                    //case (int)WeaponUtilities.WeaponAttach.CHEST:
        //                    //case (int)WeaponUtilities.WeaponAttach.BACK:
        //                    //    val = WeaponUtilities.WeaponAttachExistBody.CHEST.ToString();
        //                    //    break;
        //                    //case (int)WeaponUtilities.WeaponAttach.SHOULDER_RIGHT:
        //                    //    val = WeaponUtilities.WeaponAttachExistBody.SHOULDER_RIGHT.ToString();
        //                    //    break;
        //                    //case (int)WeaponUtilities.WeaponAttach.SHOULDER_LEFT:
        //                    //    val = WeaponUtilities.WeaponAttachExistBody.SHOULDER_LEFT.ToString();
        //                    //    break;
        //                    //case (int)WeaponUtilities.WeaponAttach.HEAD:
        //                    //case (int)WeaponUtilities.WeaponAttach.HEAD_UPSIDE:
        //                    //case (int)WeaponUtilities.WeaponAttach.HEAD_RIGHTSIDE:
        //                    //case (int)WeaponUtilities.WeaponAttach.HEAD_LEFTSIDE:
        //                    //    val = WeaponUtilities.WeaponAttachExistBody.HEAD.ToString();
        //                    //    break;
        //            }
        //            return val;
        //        }

        //        public void AddWeapon(int key, GameObject value)
        //        {
        //            lock (WeaponsObj)
        //            {
        //                if (!WeaponsObj.ContainsKey(key))
        //                    WeaponsObj[key] = value;
        //                Debug.Log("cekcekcek AddWeapon");
        //            }
        //        }
        //        public GameObject GetWeapon(int key)
        //        {
        //            lock (WeaponsObj)
        //            {
        //                if (WeaponsObj.TryGetValue(key, out GameObject value))
        //                {
        //                    return value;
        //                }

        //                return null;
        //            }
        //        }

        //        public bool AnyWeapon(int key)
        //        {
        //            lock (WeaponsObj)
        //            {
        //                if (WeaponsObj.ContainsKey(key))
        //                {
        //                    return true;
        //                }
        //                return false;
        //            }
        //        }


        //        public void ClearWeapon()
        //        {
        //            lock (WeaponsObj)
        //            {
        //                WeaponsObj.Clear();
        //            }
        //        }
        //        public bool RemoveWeapon(int key)
        //        {
        //            lock (WeaponsObj)
        //            {
        //                Debug.Log("cekcekcek RemoveWeapon");

        //                return WeaponsObj.Remove(key);
        //            }
        //        }

        //        public int GetCountWeapon()
        //        {
        //            lock (WeaponsObj)
        //            {
        //                return WeaponsObj.Count;
        //            }
        //        }

        //        void DeleteWeapon(int path)
        //        {
        //            //WeaponUtilities.WeaponAttachExistBody.HAND_RIGHT.ToString()
        //            if (AnyWeapon(path))
        //            {
        ////#if UNITY_EDITOR
        ////                if (Application.isEditor)
        ////                {
        ////                    UnityEditor.EditorApplication.delayCall += () =>
        ////                    {
        ////                        DestroyImmediate(WeaponsObj[path]);
        ////                    };
        ////                }
        ////                else
        ////#endif
        //                    Destroy(WeaponsObj[path]);
        //                //Destroy(WeaponsObj[path]);
        //                RemoveWeapon(path);
        //            }
        //            else
        //            {
        //                Debug.Log("No Weapon to delete on : " + path);

        //                foreach (var weapon in WeaponsObj)
        //                {
        //                    Debug.Log("weapon key : " + weapon.Key);
        //                }

        //            }
        //        }

        //        void UseWeapon(int path)
        //        {
        //            //WeaponUtilities.WeaponAttachExistBody.HAND_RIGHT.ToString()))
        //            if (!AnyWeapon(path))
        //            {
        //                InitiateWeapon(WeaponUsed[1].WeaponData, WeaponUsed[1].WeaponAttachTo);
        //            }
        //            else
        //            {
        //                Debug.Log("already have weapon on :" + path);
        //            }
        //        }
        //        //    private void OnDestroy()
        //        //    {
        //        //        //ClearWeapon();
        //        //    }
        //    }

        //    public class WeaponID
        //    {
        //        public int ID = 0;
        //        public string Path;
        //    }
    }
}