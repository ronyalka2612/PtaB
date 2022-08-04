using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.GNLTest.Test1
{
    public class Button_Move : ButtonMaster
    {
        PointerEventData pointer;
        private void Update()
        {
            if (!InputHandle.BTN_Move && pointer != null)
            {
                OnHold(pointer);
            }
        }
        public override void OnHold(PointerEventData eventData)
        {
            InputHandle.BTN_Move = true;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            InputHandle.BTN_Move = true;
            pointer = eventData;
        }

        public override void OnDrag(PointerEventData eventData)
        {
            InputHandle.BTN_Move = true;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            InputHandle.BTN_Move = false;
            pointer = null;
        }
    }
}