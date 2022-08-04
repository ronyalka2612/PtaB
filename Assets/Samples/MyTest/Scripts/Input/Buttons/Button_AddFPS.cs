using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.GNLTest.Test1
{
    public class Button_AddFPS : ButtonMaster
    {
        PointerEventData pointer;
        private void Update()
        {
            if (!InputHandle.BTN_AddFPS && pointer != null)
            {
                OnHold(pointer);
            }
        }
        public override void OnHold(PointerEventData eventData)
        {
            InputHandle.BTN_AddFPS = false;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            InputHandle.BTN_AddFPS = true;
            pointer = eventData;
        }

        public override void OnDrag(PointerEventData eventData)
        {
            InputHandle.BTN_AddFPS = false;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            InputHandle.BTN_AddFPS = false;
            pointer = null;
        }
    }
}