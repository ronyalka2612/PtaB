
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Com.GNLTest.Test1
{
    public class DebugController : MonoBehaviourBase
    {
        public float Timer = 3, refresh = 3, avgFramerate;
        public Text _Text_FPS;
        public Text _Text_FPS_Value;


        private string _display_FPS;
        private string _display_FPS_value;

        private void Start()
        {
            _display_FPS = _Text_FPS.text;
            _display_FPS_value = _Text_FPS_Value.text;
        }

        private void Update()
        {
            SomeInput();
            CountingFPS();
        }

        private void SomeInput()
        {
            if (InputHandle.DecFPS)
            {
                InputValueHandle.ValueFPS--;
            }

            if (InputHandle.AddFPS)
            {
                InputValueHandle.ValueFPS++;
            }
        }

        private void CountingFPS()
        {
            float timelapse = Time.deltaTime;
            Timer = Timer <= 0 ? refresh : Timer -= timelapse;

            if (Timer <= 0)
            {
                avgFramerate = (int)(1f / timelapse);
                _Text_FPS.text = string.Format(_display_FPS, avgFramerate.ToString());
                _Text_FPS_Value.text = string.Format(_display_FPS_value, InputValueHandle.ValueFPS);
            }
        }
    }
}