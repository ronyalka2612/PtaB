
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.GNL.URP_MyLib
{
    public abstract class MyButtonMaster : ButtonMaster
    {
        protected MY_BTN_CODE _main_btn2;

        protected bool _isBtnDownFlag = false;
        protected bool _isBtnDragFlag = false;
        protected bool _isBtnUpFlag = false;
        protected bool _isBtnHoldFlag = false;

        protected bool _isBtnDownActive = false;
        protected bool _isBtnDragActive = false;
        protected bool _isBtnUpActive = false;
        protected bool _isBtnHoldActive = false;



        public void SetMyButton(
            //MY_BTN_CODE main_btn,
            MY_BTN_CODE main_btn2,
            ref bool isBtnDownFlag,
            ref bool isBtnDragFlag,
            ref bool isBtnHoldFlag,
            ref bool isBtnUpFlag,
            bool isUseEnableDisable
            )
        {
            //if (isUseMain_btn2)
            //{
            _main_btn2 = main_btn2;
            //}
            //else
            //{
            //    _main_btn = main_btn;
            //}


            if (isUseEnableDisable)
            {
                isBtnDownFlag = true;
                isBtnDragFlag = false;
                isBtnHoldFlag = false;
                isBtnUpFlag = false;

                _isBtnDownFlag = isBtnDownFlag;
                _isBtnDragFlag = isBtnDragFlag;
                _isBtnHoldFlag = isBtnHoldFlag;
                _isBtnUpFlag = isBtnUpFlag;


            }
            else
            {
                _isBtnDownFlag = isBtnDownFlag;
                _isBtnDragFlag = isBtnDragFlag;
                _isBtnHoldFlag = isBtnHoldFlag;
                _isBtnUpFlag = isBtnUpFlag;

                if (_isBtnHoldFlag)
                    _isBtnDownFlag = true;
            }
        }

        public abstract void SetEffectButton();

        private void Update()
        {
            updateAll();
        }

        private void FixedUpdate()
        {
            //updateAll();
        }

        private void LateUpdate()
        {
            //updateAll();
        }

        private void updateAll()
        {
            if (!LibMasterGameController.InstanceLibMaster.ItsNeedTobePauseWhenPause)
            {
                Update_Button();
            }
        }

        private void Update_Button()
        {
            if (_isBtnHoldFlag && _isBtnDownActive)
            {

                if (!VirtualButtonManager.Instance.AnyBtn(_main_btn2))
                {
                    OnHold(VirtualButtonManager.Instance.GetBtn(_main_btn2), _main_btn2);
                }
                else
                {
                    _isBtnDownActive = false;
                    _isBtnHoldActive = true;
                }
            }
            else if (!_isBtnHoldFlag && _isBtnDownActive)
            {
                _isBtnDownActive = false;
                if (VirtualButtonManager.Instance.AnyBtn(_main_btn2))
                {
                    VirtualButtonManager.Instance.RemoveBtn(_main_btn2);
                    _isBtnHoldActive = false;
                    //Debug.Log("cekcekcek _isBtnHoldActive false");
                }
            }
            if (_isBtnUpActive)
            {
                if (VirtualButtonManager.Instance.AnyBtn(_main_btn2))
                {
                    VirtualButtonManager.Instance.RemoveBtn(_main_btn2);
                    _isBtnUpActive = false;
                    //Debug.Log("cekcekcek _isBtnUpActive false");
                }
            }
        }


        public override void OnPointerDown(PointerEventData eventData)
        {
            if (_isBtnDownFlag)
            { 
                VirtualButtonManager.Instance.AddBtn(_main_btn2, eventData);
                _isBtnDownActive = true;
                SetEffectButton();
                //Debug.Log("cekcekcek _isBtnDownActive");
            }
        }
        public override void OnHold(PointerEventData eventData, MY_BTN_CODE btn)
        {
            //mati btnActive akan mati disetiap masuk state button yang lain (kecuali hold dan drag, beda behav)
            //jika btndownflag hidup dan holdflag hidup tidak perlu add btn
            //jika btndownflag hidup dan holdflag mati tidak perlu add btn dan remove btn
            //jika btndownflag mati dan holdflag mati mak tidak melakukan apapun
            if (_isBtnDownFlag)
            {
                _isBtnDownActive = false;
            }


            if (_isBtnHoldFlag)
            {
                if (!VirtualButtonManager.Instance.AnyBtn(_main_btn2))
                {
                    VirtualButtonManager.Instance.AddBtn(_main_btn2, eventData);
                    _isBtnHoldActive = true;
                    //Debug.Log("cekcekcek _isBtnHoldActive true");
                }
            }
            else 
            {
                if (VirtualButtonManager.Instance.AnyBtn(_main_btn2))
                {
                    VirtualButtonManager.Instance.RemoveBtn(_main_btn2);
                    _isBtnHoldActive = false;
                    //Debug.Log("cekcekcek _isBtnHoldActive false");
                }
            }


        }

        public override void OnDrag(PointerEventData eventData)
        {
            

            if (_isBtnDragFlag)
            {
                if (!VirtualButtonManager.Instance.AnyBtn(_main_btn2))
                {
                    VirtualButtonManager.Instance.AddBtn(_main_btn2, eventData);
                    _isBtnDragActive = true;
                    //Debug.Log("cekcekcek _isBtnDragActive true");
                }
            }
            else
            {
                if (VirtualButtonManager.Instance.AnyBtn(_main_btn2))
                {
                    VirtualButtonManager.Instance.RemoveBtn(_main_btn2);
                    _isBtnDragActive = false;
                    //Debug.Log("cekcekcek _isBtnDragActive false");
                }
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {

            _isBtnDownActive = false;
            _isBtnHoldActive = false;
            _isBtnDragActive = false;

            if (_isBtnUpFlag)
            {
                _isBtnUpActive = true;
                //Debug.Log("cekcekcek _isBtnUpActive true");
                if (!VirtualButtonManager.Instance.AnyBtn(_main_btn2))
                {
                    VirtualButtonManager.Instance.AddBtn(_main_btn2, eventData);
                }
            }
            else 
            {
                _isBtnUpActive = false;
                //Debug.Log("cekcekcek _isBtnUpActive false");
                if (VirtualButtonManager.Instance.AnyBtn(_main_btn2))
                {
                    VirtualButtonManager.Instance.RemoveBtn(_main_btn2);
                }
            }


        }
    }

    //[Serializable]
    //public class MyButtonAtrribute2
    //{
    //    [Header("Config Btn Input")]
    //    [SerializeField] private bool _BtnDown = true;
    //    [SerializeField] private bool _BtnDrag = false;
    //    [SerializeField] private bool _BtnHold = false;
    //    [SerializeField] private bool _BtnUP = false;

    //    [Header("Config BtnDisableEnable")]
    //    [Tooltip("When it's true, all button is off except _BtnDown")]
    //    [SerializeField] private bool _IsBtnDisableEnable = false;

    //    [Header("Setup Button")]
    //    [Tooltip("What function that will call with this button?")]
    //    [SerializeField] private KeyCode _ButtonFunction;
    //    [SerializeField] private VirtualButtonManager.MY_BTN_CODE _ButtonFunction2;
    //    [SerializeField] private bool _UseButtonFunction2 = false;


    //}

    //public class MyButtonMaster : MyButton
    //{
    //    public abstract void OnPointerDown(PointerEventData eventData);
    //    public abstract void OnDrag(PointerEventData eventData);
    //    public abstract void OnPointerUp(PointerEventData eventData);
    //    public abstract void OnHold(PointerEventData eventData, KeyCode btn);

    //    public override bool OnBtnDown()
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public override bool OnBtnDrag()
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public override bool OnBtnHold()
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public override bool OnBtnUp()
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public override KeyCode SetMainButton()
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}
    //public class MyButtonUsed : MyButton
    //{

    //    public override bool OnBtnDown(bool isBtnDown)
    //    {
    //        _isBtnDownFlag = isBtnDown;
    //        return _isBtnDownFlag;
    //    }

    //    public override bool OnBtnDrag(bool isBtnDrag)
    //    {
    //        _isBtnDragFlag = isBtnDrag;
    //        return _isBtnDragFlag;
    //    }

    //    public override bool OnBtnHold(bool isBtnHold)
    //    {
    //        _isBtnHoldFlag = isBtnHold;
    //        return _isBtnHoldFlag;
    //    }

    //    public override bool OnBtnUp(bool isBtnUp)
    //    {
    //        _isBtnUpFlag = isBtnUp;
    //        return _isBtnUpFlag;
    //    }

    //    public override KeyCode SetMainButton(KeyCode mainButton)
    //    {
    //        _main_btn = mainButton;
    //        return _main_btn;
    //    }
    //}
}