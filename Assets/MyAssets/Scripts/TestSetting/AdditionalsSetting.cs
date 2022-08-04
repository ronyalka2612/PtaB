using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Com.GNL.URP_MyLib;
using System;

namespace Com.GNL.URP_MyLibProjectTest
{
    public class AdditionalsSetting : MonoBehaviourMyBase
    {
        [Header("FPS Visual Setting")]
        [Tooltip("setting tampilan fps pada game")]
        public GameObject GO_Text_FPS;
        public float timer = 3, refresh = 3, avgFramerate;

        [Tooltip("setting tampilan Gameplay RPM")]
        public ETY_Ball PlyBall;
        public GameObject GO_Text_RPM;
        public GameObject GO_Text_SPD;
        public GameObject GO_Text_MtrFrc;
        public GameObject GO_Text_AccInput;
        public GameObject GO_Text_BrkInput;
        public GameObject GO_Text_Gear;
        public GameObject GO_Text_Revs;

        private TextMeshProUGUI _Text_FPS;
        private TextMeshProUGUI _Text_RPM;
        private TextMeshProUGUI _Text_SPD;
        private TextMeshProUGUI _Text_MtrFrc;
        private TextMeshProUGUI _Text_AccInput;
        private TextMeshProUGUI _Text_BrkInput;
        private TextMeshProUGUI _Text_Gear;
        private TextMeshProUGUI _Text_Revs;

        private string _display_FPS;
        private string _display_RPM;
        private string _display_SPD;
        private string _display_MtrFrc;
        private string _display_AccInput;
        private string _display_BrkInput;
        private string _display_Gear;
        private string _display_Revs;



        private void Start()
        {
            


        }
        void Update()
        {
            StateFunc.StateUpdate(Update_State, StateFunc.StateChanging(StateChanging, SubStateChanging, ChekingAllFind));
        }

        private void ChekingAllFind()
        {
            if (VirtualStateManager.Instance.CurState == LibEdStateUtilities.GameStates.MAIN_GP)
            {
                if (PlyBall == null)
                    StateFunc.SetFindAll(false);
                else
                    StateFunc.SetFindAll(true);
            }
            else
                StateFunc.SetFindAll(true);
        }

        private void SubStateChanging()
        {
            
        }

        private void StateChanging()
        {
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == State MAIN_GP ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    if (_Text_FPS == null)
                    {
                        _Text_FPS = GO_Text_FPS.GetComponent<TextMeshProUGUI>();
                        _Text_RPM = GO_Text_RPM.GetComponent<TextMeshProUGUI>();
                        _Text_SPD = GO_Text_SPD.GetComponent<TextMeshProUGUI>();
                        _Text_MtrFrc = GO_Text_MtrFrc.GetComponent<TextMeshProUGUI>();
                        _Text_AccInput = GO_Text_AccInput.GetComponent<TextMeshProUGUI>();
                        _Text_BrkInput = GO_Text_BrkInput.GetComponent<TextMeshProUGUI>();
                        _Text_Gear = GO_Text_Gear.GetComponent<TextMeshProUGUI>();
                        _Text_Revs = GO_Text_Revs.GetComponent<TextMeshProUGUI>();


                        _display_FPS = _Text_FPS.text;
                        _display_RPM = _Text_RPM.text;
                        _display_SPD = _Text_SPD.text;
                        _display_MtrFrc = _Text_MtrFrc.text;
                        _display_AccInput = _Text_AccInput.text;
                        _display_BrkInput = _Text_BrkInput.text;
                        _display_Gear = _Text_Gear.text;
                        _display_Revs = _Text_Revs.text;
                    }

                    if (PlyBall == null)
                    {
                        //Ply_Car = LibFormulation.FindObjectByTagThenName(Utilities.TAG.Player.ToString(), "PLY_Car_RB").GetComponent<PLY_CarRB>();
                        PlyBall = Formulation.FindPlyBall();
                    }

                    break;
                    #endregion
            }
        }


        //PLY_CarRB FindPlyCar()
        //{
        //    GameObject[] allPlayer =  GameObject.FindGameObjectsWithTag(Utilities.TAG.Player.ToString());
        //    foreach(GameObject g in allPlayer)
        //    {
        //        if (g.GetComponent<PLY_CarRB>())
        //            return g.GetComponent<PLY_CarRB>();
        //    }
        //    return null;
        //}

        private void Update_State()
        {
            FindingCarSystem();
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == State MAIN_GP ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    if (LibGameSetting.debugScreen)
                    {
                        float timelapse = Time.deltaTime;
                        timer = timer <= 0 ? refresh : timer -= timelapse;

                        if (timer <= 0)
                        {
                            avgFramerate = (int)(1f / timelapse);
                            _Text_FPS.text = string.Format(_display_FPS, avgFramerate);
                        }
                    }

                    ////Display RPM
                    //_Text_RPM.text = string.Format(_display_RPM, PlyBall.GetCurRPM());

                    ////Display SPD
                    //_Text_SPD.text = string.Format(_display_SPD, PlyBall.GetCurrentSpeed());

                    ////Display MtrFrc
                    //_Text_MtrFrc.text = string.Format(_display_MtrFrc, Ply_Car.GetCurMotorForce());

                    ////Display MtrFrc
                    //_Text_AccInput.text = string.Format(_display_AccInput, Ply_Car.GetAccInput());

                    ////Display MtrFrc
                    //_Text_BrkInput.text = string.Format(_display_BrkInput, Ply_Car.GetBrakeInput());

                    ////Display MtrFrc
                    //_Text_Gear.text = string.Format(_display_Gear, Ply_Car.GetGear());

                    ////Display MtrFrc
                    //_Text_Revs.text = string.Format(_display_Revs, Ply_Car.Revs);
                    break;
                    #endregion
            }
            //Change smoothDeltaTime to deltaTime or fixedDeltaTime to see the difference
            
        }

        private void FindingCarSystem()
        {
            if (PlyBall == null)
            {
                if (!isFindCarProceed)
                {
                    isFindCarProceed = true;
                    PlyBall = Formulation.FindPlyBall();
                    if (PlyBall == null)
                    {
                        isFindCarProceed = false;
                    }
                    //StartCoroutine(FindPlyCarCourutine());
                }
                else
                    return;
            }
        }

        private bool isFindCarProceed = false;
        private IEnumerator FindPlyCarCourutine()
        {
            PlyBall = Formulation.FindPlyBall();
            yield return new WaitForEndOfFrame();
            if (PlyBall == null)
            {
                isFindCarProceed = false;
            }
            yield return null;
        }

        public void ResetValue()
        {
            LibGameSetting.FDS = 0;
        }
    }
}