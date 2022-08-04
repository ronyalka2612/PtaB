using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

using Com.GNL.URP_MyLib;

namespace Com.GNL.URP_MyLibProjectTest
{

    //[RequireComponent(typeof(CinemachineFreeLook))]
    public class BtnMove : ButtonMaster //, IPointerHoldHandler
    {
        private void Update()
        {
            Update_Button();
        }

        private void FixedUpdate()
        {
            //Update_Button();
        }

        private void Update_Button()
        {
            if (VirtualButtonManager.Instance.AnyBtn(MY_BTN_CODE.Btn_Move))
            {
                OnHold(VirtualButtonManager.Instance.GetBtn(MY_BTN_CODE.Btn_Move), MY_BTN_CODE.Btn_Move);
            }
        }


        public override void OnPointerDown(PointerEventData eventData)
        {
            VirtualButtonManager.Instance.AddBtn(MY_BTN_CODE.Btn_Move, eventData);

            if (VirtualButtonManager.Instance.AnyBtn(MY_BTN_CODE.Btn_Move))
            {
                VirtualButtonManager.Instance.RemoveBtn(MY_BTN_CODE.Btn_Move);
            }
            if (VirtualButtonManager.Instance.AnyBtn(MY_BTN_CODE.Btn_AimTarget))
            {
                VirtualButtonManager.Instance.RemoveBtn(MY_BTN_CODE.Btn_AimTarget);
            }
        }
        public override void OnHold(PointerEventData eventData, MY_BTN_CODE btn)
        {
            // Comment below it if you wanna use hold
            //if (VirtualButtonManager.Instance.AnyBtn(MY_BTN_CODE.Move))
            //    VirtualButtonManager.Instance.RemoveBtn(MY_BTN_CODE.Move);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            // Comment below it if you wanna use OnDrag
            //if (VirtualButtonManager.Instance.AnyBtn(MY_BTN_CODE.Move))
            //    VirtualButtonManager.Instance.RemoveBtn(MY_BTN_CODE.Move);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (VirtualButtonManager.Instance.AnyBtn(MY_BTN_CODE.Btn_Move))
                VirtualButtonManager.Instance.RemoveBtn(MY_BTN_CODE.Btn_Move);

            if (VirtualButtonManager.Instance.AnyBtn(MY_BTN_CODE.Btn_MoveCam))
            {
                VirtualButtonManager.Instance.RemoveBtn(MY_BTN_CODE.Btn_MoveCam);
            }

            if (VirtualButtonManager.Instance.AnyBtn(MY_BTN_CODE.Btn_AimTarget))
            {
                VirtualButtonManager.Instance.RemoveBtn(MY_BTN_CODE.Btn_AimTarget);
            }

        }

    }
}
