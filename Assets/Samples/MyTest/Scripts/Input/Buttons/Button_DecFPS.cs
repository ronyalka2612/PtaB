using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.GNLTest.Test1
{
    public class Button_DecFPS : ButtonMaster
    {
        PointerEventData pointer;
        private void Update()
        {
            if (!InputHandle.BTN_DecFPS && pointer != null)
            {
                OnHold(pointer);
            }
        }
        public override void OnHold(PointerEventData eventData)
        {
            InputHandle.BTN_DecFPS = true;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            InputHandle.BTN_DecFPS = true;
            pointer = eventData;
        }

        public override void OnDrag(PointerEventData eventData)
        {
            InputHandle.BTN_DecFPS = true;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            InputHandle.BTN_DecFPS = false;
            pointer = null;
        }
    }
}