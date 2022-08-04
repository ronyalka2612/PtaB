using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.GNL.URP_MyLib
{
    public interface ILibPointerHoldHandler 
    {
        void OnHold(PointerEventData eventData, MY_BTN_CODE btn);

        //in the update must something like this
        //private void Update()
        //{
        //    //condition that call in OnPointerDown or something like that
        //    if (VirtualButtonManager.Instance.AnyBtn((byte)VirtualButtonManager.BtnAndroidName.MOVE_CAM)) 
        //    {
        //        //call function OnHold( and give param PointerEventData ); a must
        //        OnHold(VirtualButtonManager.Instance.GetBtn((byte)VirtualButtonManager.BtnAndroidName.MOVE_CAM)) 
        //    }
        //}

        // why need that secod parameters, couse in one button can multiple assign buttons
    }
}