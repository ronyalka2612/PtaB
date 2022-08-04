
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.GNL.URP_MyLib
{
    public abstract class ButtonMaster : MonoBehaviourMyBase, IDragHandler, IPointerDownHandler, IPointerUpHandler, ILibPointerHoldHandler
    {
        public abstract void OnPointerDown(PointerEventData eventData);
        public abstract void OnDrag(PointerEventData eventData);
        public abstract void  OnPointerUp(PointerEventData eventData);
        public abstract void OnHold(PointerEventData eventData, MY_BTN_CODE btn);
    }
}