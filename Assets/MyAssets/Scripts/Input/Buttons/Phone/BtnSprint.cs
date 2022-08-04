using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Com.GNL.URP_MyLib;

namespace Com.GNL.URP_MyLibProjectTest
{

    public class BtnSprint : ButtonMaster
    {
        public ButtonMaster btnJump;


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
            //if (VirtualButtonManager.Instance.AnyBtn(MY_BTN_CODE.Sprint))
            //{
            //    OnHold(VirtualButtonManager.Instance.GetBtn(MY_BTN_CODE.Sprint), MY_BTN_CODE.Sprint);
            //}
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            //Debug.Log("pos btn Jump x :" + btnJump.transform.position.x + ", y :" + btnJump.transform.position.y);
            //VirtualButtonManager.Instance.AddBtn(MY_BTN_CODE.Btn_Sprint, eventData);
        }
        public override void OnHold(PointerEventData eventData, MY_BTN_CODE btn)
        {
            // Comment below it if you wanna use hold
            //if (VirtualButtonManager.Instance.AnyBtn(MY_BTN_CODE.Move))
            //    VirtualButtonManager.Instance.RemoveBtn(MY_BTN_CODE.Move);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            
            //if (eventData != null)
            //{
            //    var raycastResults = new List<RaycastResult>();
            //    EventSystem.current.RaycastAll(eventData, raycastResults);

            //    if (VirtualButtonManager.Instance.AnyBtn(MY_BTN_CODE.Jump))
            //    {
            //        VirtualButtonManager.Instance.RemoveBtn(MY_BTN_CODE.Jump);
            //    }

            //    if (raycastResults.Count > 0)
            //    {
            //        foreach (var result in raycastResults)
            //        {
            //            if (result.gameObject.name.Equals(btnJump.name))
            //            {
            //                if (!VirtualButtonManager.Instance.AnyBtn(MY_BTN_CODE.Jump))
            //                    VirtualButtonManager.Instance.AddBtn(MY_BTN_CODE.Jump, eventData);
            //            }

            //        }
            //    }
            //}
        }


        public override void OnPointerUp(PointerEventData eventData)
        {
            //if (VirtualButtonManager.Instance.AnyBtn(MY_BTN_CODE.Jump))
            //    VirtualButtonManager.Instance.RemoveBtn(MY_BTN_CODE.Jump);
            //if (VirtualButtonManager.Instance.AnyBtn(MY_BTN_CODE.Sprint))
            //    VirtualButtonManager.Instance.RemoveBtn(MY_BTN_CODE.Sprint);
        }
    }
}
