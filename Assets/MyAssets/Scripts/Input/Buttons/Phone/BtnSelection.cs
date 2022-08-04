using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

using Com.GNL.URP_MyLib;

namespace Com.GNL.URP_MyLibProjectTest
{

    //[RequireComponent(typeof(CinemachineFreeLook))]
    public class BtnSelection : ButtonMaster 
    {


        private void Start()
        {
            //Debug.Log("cekcekcek Start test button");
        }
        private void Update()
        {
            Update_Button();
        }

        private void Update_Button()
        {
            if (VirtualButtonManager.Instance.AnyBtn(MY_BTN_CODE.Btn_MoveCam))
            {
                OnHold(VirtualButtonManager.Instance.GetBtn(MY_BTN_CODE.Btn_MoveCam), MY_BTN_CODE.Btn_MoveCam);
            }
            if(VirtualButtonManager.Instance.AnyBtn(MY_BTN_CODE.Btn_AimTarget))
            {
                OnHold(VirtualButtonManager.Instance.GetBtn(MY_BTN_CODE.Btn_AimTarget), MY_BTN_CODE.Btn_AimTarget);
            }
        }


        public override void OnPointerDown(PointerEventData eventData)
        {
            VirtualButtonManager.Instance.AddBtn(MY_BTN_CODE.Btn_MoveCam, eventData);
            VirtualButtonManager.Instance.AddBtn(MY_BTN_CODE.Btn_AimTarget, eventData);
        }

        public override void OnHold(PointerEventData eventData, MY_BTN_CODE btn)
        {
            if (btn == MY_BTN_CODE.Btn_AimTarget)
            {
                if (VirtualButtonManager.Instance.AnyBtn(MY_BTN_CODE.Btn_AimTarget))
                    VirtualButtonManager.Instance.RemoveBtn(MY_BTN_CODE.Btn_AimTarget);
            }
            else if (btn == MY_BTN_CODE.Btn_MoveCam)
            {
                //OnDrag(eventData);
            }

        }
        public override void OnDrag(PointerEventData eventData)
        {
            
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if(VirtualButtonManager.Instance.AnyBtn(MY_BTN_CODE.Btn_AimTarget))
                VirtualButtonManager.Instance.RemoveBtn(MY_BTN_CODE.Btn_AimTarget);

            if (VirtualButtonManager.Instance.AnyBtn(MY_BTN_CODE.Btn_MoveCam))
                VirtualButtonManager.Instance.RemoveBtn(MY_BTN_CODE.Btn_MoveCam);
        }

       
    }
}
