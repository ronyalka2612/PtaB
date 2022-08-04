using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Com.GNL.URP_MyLib
{
    public class MyButton : MyButtonMaster, IDragHandler, IPointerDownHandler, IPointerUpHandler, ILibPointerHoldHandler
    {
        [SerializeField] private MyButtonAtrribute MyButtonAttribut;

        [SerializeField]
        private bool isUseDefaultButton = true;
        [SerializeField]
        public Color ColorBtnEnable;


        private Image _imageBtn; 
        private Color ColorDefaultBtnEnable;
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

        private void Awake()
        {
            _imageBtn = this.GetComponent<Image>();
            ColorDefaultBtnEnable = _imageBtn.color;
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
        private bool _isEnableBtn = false;
        public override void SetEffectButton()
        {
            if (isUseDefaultButton && _isBtnDownActive && MyButtonAttribut.IsBtnDisableEnable)
            { 
                if ( _isEnableBtn)
                {
                    _isEnableBtn = false;
                    _imageBtn.color = ColorDefaultBtnEnable;
                }
                else
                {
                    _isEnableBtn = true;
                    _imageBtn.color = ColorBtnEnable;
                }
            }
        }
    }
}
