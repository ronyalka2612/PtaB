using UnityEngine;

namespace Com.GNLTest.Test1
{
    public class InputController : MonoBehaviour
    {
        [Header("Reference")]
        public FixedJoystick fixedJoystick;
        public GameObject CanvasScreen;

        [Header("Unity MOde")]
        public bool IsUnityUseAndroidUI = false;
        public int TargetFPS = 60;

        [Header("Batasan control")]
        public float JoypadAxisMinInputMagnitude = 0.4f;
        public float JoypadAxisAcc = 1;
        public float JoypadAxisMinInputMove = 0.1f;


        //public bool TouchHandlerCondition(ref Touch[] touch, ref int indexBtnJump)
        //{
        //    if (Input.touchCount > 0 && (Application.platform == RuntimePlatform.Android))
        //    {
        //        touch[Input.touchCount - 1] = Input.GetTouch(Input.touchCount - 1);
        //        indexBtnJump = Input.touchCount - 1;
        //        return true;
        //    }
        //    else if(IsUnityUseAndroidUI) 
        //    { 

        //    }
        //    return false;
        //}
        private void Awake()
        {
            InputValueHandle.ValueFPS = TargetFPS;
        }
        private void Start()
        {
            
            CanvasScreen.SetActive(IsUnityUseAndroidUI);
            if (Application.platform == RuntimePlatform.Android)
            {
                CanvasScreen.SetActive(true);
            }
        }

        private void Update()
        {
            Application.targetFrameRate = InputValueHandle.ValueFPS;
            InputMovement();
        }

        private void LateUpdate()
        {
            
        }

        private void InputMovement()
        {
            //Debug.Log("cekcekcek InputHandle.BTN_Jump :"+ InputHandle.BTN_Jump);
           

            if (Input.GetKey(KeyCode.S))
            {
                InputHandle.MoveBack = true;
            }
            else
            {
                InputHandle.MoveBack = false;
            }

            if (Input.GetKey(KeyCode.W))
            {
                InputHandle.MoveForward = true;
            }
            else
            {
                InputHandle.MoveForward = false;
            }

            if (Input.GetKey(KeyCode.A))
            {
                InputHandle.MoveLeft = true;
            }
            else
            {
                InputHandle.MoveLeft = false;
            }

            if (Input.GetKey(KeyCode.D))
            {
                InputHandle.MoveRight = true;
            }
            else
            {
                InputHandle.MoveRight = false;
            }





            if (Input.GetKeyDown(KeyCode.Space) || InputHandle.BTN_Jump)
            {
                InputHandle.Jump = true;
            }
            else
            {
                InputHandle.Jump = false;
            }

            if (Input.GetKeyDown(KeyCode.Alpha9) || InputHandle.BTN_AddFPS)
            {
                InputHandle.AddFPS = true;
            }
            else
            {
                InputHandle.AddFPS = false;
            }

            if (Input.GetKeyDown(KeyCode.Alpha0) || InputHandle.BTN_DecFPS)
            {
                InputHandle.DecFPS = true;
            }
            else
            {
                InputHandle.DecFPS = false;
            }




            if (InputHandle.MoveBack
                || InputHandle.MoveForward
                || InputHandle.MoveLeft
                || InputHandle.MoveRight
                )
            {

                Vector2 direction = Vector2.zero;
                if (InputHandle.MoveForward)
                    direction.y = 1;
                else if (InputHandle.MoveBack)
                    direction.y = -1;

                if (InputHandle.MoveRight)
                    direction.x = 1;
                else if (InputHandle.MoveLeft)
                    direction.x = -1;

                InputHandle.Move = true;
                InputValueHandle.AxisFinalInput.x = direction.x;
                InputValueHandle.AxisFinalInput.y = direction.y;
            }
            else if (fixedJoystick.finalInput.magnitude > JoypadAxisMinInputMagnitude)
            {
                if (fixedJoystick.Horizontal > JoypadAxisMinInputMove)
                {
                    InputValueHandle.AxisFinalInput.x = fixedJoystick.Horizontal * JoypadAxisAcc > 1 ? 1 : fixedJoystick.Horizontal * JoypadAxisAcc;
                }
                else if (fixedJoystick.Horizontal < -JoypadAxisMinInputMove)
                {
                    InputValueHandle.AxisFinalInput.x = fixedJoystick.Horizontal * JoypadAxisAcc < -1 ? -1 : fixedJoystick.Horizontal * JoypadAxisAcc;
                }
                else
                {
                    InputValueHandle.AxisFinalInput.x = 0;
                }

                if (fixedJoystick.Vertical > JoypadAxisMinInputMove)
                {
                    InputValueHandle.AxisFinalInput.y = fixedJoystick.Vertical * JoypadAxisAcc > 1 ? 1 : fixedJoystick.Vertical * JoypadAxisAcc;
                }
                else if (fixedJoystick.Vertical < -JoypadAxisMinInputMove)
                {
                    InputValueHandle.AxisFinalInput.y = fixedJoystick.Vertical * JoypadAxisAcc < -1 ? -1 : fixedJoystick.Vertical * JoypadAxisAcc;
                }
                else
                {
                    InputValueHandle.AxisFinalInput.y = 0;
                }

                InputHandle.Move = true;
            }
            else
            {
                InputHandle.Move = false;
            }
        }
    }
}