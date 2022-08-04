using System;
using System.Collections.Generic;
using UnityEngine;


namespace Com.GNL.URP_MyLib
{
    public class LibMasterSelectionController : LibSingletonController<LibMasterSelectionController>
    {

#if UNITY_EDITOR
        [TextArea(5, 10)]
        [LibReadOnly] [SerializeField] private string Notes;
#endif

        private void Start()
        {
            StateFunc.ClearState();
        }
        private void Update()
        {
            StateFunc.StateUpdate(Update_State, StateFunc.StateChanging(StateChanging, SubStateChanging, CheckingAllfind));
        }

        public virtual void Update_State()
        {

        }
        private void FixedUpdate()
        {
            StateFunc.StateUpdate(Update_StateFU, StateFunc.StateChanging(StateChanging, SubStateChanging, CheckingAllfind));
        }

        public virtual void Update_StateFU()
        {

        }

        private void LateUpdate()
        {
            StateFunc.StateUpdate(Update_StateLU, StateFunc.StateChanging(StateChanging, SubStateChanging, CheckingAllfind));
        }

        public virtual void Update_StateLU()
        {

        }

        public virtual void StateChanging()
        {
            
        }



        public virtual void SubStateChanging()
        {


        }

        public override void CheckingAllfind()
        {
        
        }

        #region === Function in this Lib ===

        public void LibAimTarget()
        {
            if (VirtualInputManager.Instance.InputAttr.AimTarget)
            {
                Ray ray = new Ray();
                RaycastHit hit;

                if (LibGameSetting.IsPlatformWindows
                        //#if UNITY_EDITOR
                        //                        && !Utilities.IsUnityPlayerUseAndroidUI
                        //#endif
                        )
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                else if (LibGameSetting.IsPlatformAndroid
                    //#if UNITY_EDITOR
                    //                        || Utilities.IsUnityPlayerUseAndroidUI
                    //#endif
                    )
                    ray = Camera.main.ScreenPointToRay(VirtualButtonManager.Instance.GetBtn(MY_BTN_CODE.Btn_AimTarget).position);
                if (Physics.Raycast(ray, out hit))
                {
                    LibSetObjectSelection(hit);
                    //Debug.Log("cekcekcek raycast3d obj name :" + hit.transform.name);
                    //if (hit.transform.gameObject.tag.Equals(LibUtilities.TAG.ENEMY_LEAD.ToString()))
                    //{
                    //    //Debug.Log("cekcekcek raycast3d ENEMY_LEAD ");
                    //    VirtualSelectionObjectManager.Instance.AddObject((byte)VirtualSelectionObjectManager.SelectedObjectName.ENEMY, hit.transform.gameObject);

                    //    //Debug.Log("cekcekcek targetAnglePlayer :" + targetAnglePlayer);
                    //    //Debug.Log("cekcekcek dudut camera awal :" + CamChineFL[0].m_XAxis.Value);
                    //    //Debug.Log("cekcekcek angle camera final :" + angleCamera);
                    //}
                }
            }
        }


        //public void LibGetAnySelectionData()
        //{
        //    if (VirtualButtonManager.Instance.BtnAttr.AimTarget)
        //    {
        //        if (VirtualSelectionObjectManager.Instance.AnyObject((byte)VirtualSelectionObjectManager.SelectedObjectName.ENEMY))
        //        {
        //            //_hitEnemy = VirtualSelectionObjectManager.Instance.GetObject((byte)VirtualSelectionObjectManager.SelectedObjectName.ENEMY);
        //            //_anim.SetBool(PlayerController.ANIM_PARAM.isInCombating.ToString(), true);
        //            //_anim.SetFloat(PlayerController.ANIM_PARAM.CombatTime.ToString(), _PC.CombatDuration);
        //        }
        //    }
        //}

        public virtual void LibSetObjectSelection(RaycastHit hit)
        {
            //if (hit.transform.gameObject.tag.Equals(LibUtilities.TAG.ENEMY_LEAD.ToString()))
            //{
            //    if (VirtualSelectionObjectManager.Instance.AnyObject((byte)VirtualSelectionObjectManager.SelectedObjectName.ENEMY))
            //    {

            //        //    //_hitEnemy = VirtualSelectionObjectManager.Instance.GetObject((byte)VirtualSelectionObjectManager.SelectedObjectName.ENEMY);
            //        //    //_anim.SetBool(PlayerController.ANIM_PARAM.isInCombating.ToString(), true);
            //        //    //_anim.SetFloat(PlayerController.ANIM_PARAM.CombatTime.ToString(), _PC.CombatDuration);
            //        //}
            //    }
            // }
        }


        #endregion === Function in this Lib ===

    }


    //[Serializable]
    //public class LibSelectionControllerAtrribute
    //{
   
    //}

}