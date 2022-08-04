
using System;
using UnityEngine;


namespace Com.GNL.URP_MyLib
{
    [Serializable]
    public class MyButtonAtrribute
    {
        [Header("Config Btn Input")]
        [SerializeField] public bool BtnDown = true;
        [SerializeField] public bool BtnDrag = false;
        [SerializeField] public bool BtnHold = false;
        [SerializeField] public bool BtnUP = false;

        [Header("Config BtnDisableEnable")]
        [Tooltip("When it's true, all button is off except _BtnDown")]
        [SerializeField] public bool IsBtnDisableEnable = false;

        [Header("Setup Button")]
        [Tooltip("What function that will call with this button?")]
        [SerializeField] public MY_BTN_CODE ButtonFunction;


    }
    
}