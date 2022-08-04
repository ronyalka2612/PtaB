
using UnityEngine;
using UnityEngine.EventSystems;
namespace Com.GNLTest.Test1
{
    public abstract class ButtonMaster : MonoBehaviourBase, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public abstract void OnPointerDown(PointerEventData eventData);
        public abstract void OnDrag(PointerEventData eventData);
        public abstract void  OnPointerUp(PointerEventData eventData);
        public abstract void OnHold(PointerEventData eventData);
    }
}