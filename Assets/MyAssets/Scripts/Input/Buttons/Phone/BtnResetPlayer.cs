using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Com.GNL.URP_MyLib;

namespace Com.GNL.URP_MyLibProjectTest
{
    public class BtnResetPlayer : MyButtonMaster, IDragHandler, IPointerDownHandler, IPointerUpHandler, ILibPointerHoldHandler
    {
        [SerializeField] private MyButtonAtrribute MyButtonAttribut;

        private void Function()
        {
            base.SetMyButton(
                MyButtonAttribut.ButtonFunction,
                ref MyButtonAttribut.BtnDown,
                ref MyButtonAttribut.BtnDrag,
                ref MyButtonAttribut.BtnHold,
                ref MyButtonAttribut.BtnUP,
                MyButtonAttribut.IsBtnDisableEnable);
        }

        [SerializeField] private Transform FirstTransformPlayer;

        private void Awake()
        {
            Utilities.SpawnerTransform = FirstTransformPlayer;
            Function();
        }

        private void OnGUI()
        {
            Function();
        }

        private void OnValidate()
        {
            Function();
        }

        public override void SetEffectButton()
        {
            
        }
    }


}
