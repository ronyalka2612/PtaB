using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.GNLTest.Test1
{
    public class Button_Jump : ButtonMaster
    {
        PointerEventData pointer;
        private void Update()
        {
            if (!InputHandle.BTN_Jump && pointer != null)
            {
                OnHold(pointer);
            }
        }
        public override void OnHold(PointerEventData eventData)
        {
            InputHandle.BTN_Jump = false;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            InputHandle.BTN_Jump = true;
            pointer = eventData;
        }

        public override void OnDrag(PointerEventData eventData)
        {
            InputHandle.BTN_Jump = false;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            InputHandle.BTN_Jump = false;
            pointer = null;
        }
    }
}