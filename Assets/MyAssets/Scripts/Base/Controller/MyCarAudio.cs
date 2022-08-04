using System;
using UnityEngine;
using Random = UnityEngine.Random;
using Com.GNL.URP_MyLib;

namespace Com.GNL.URP_MyLibProjectTest
{
    public class MyCarAudio : MonoBehaviourMyBase
    {
        // This script reads some of the car's current properties and plays sounds accordingly.
        // The engine sound can be a simple single clip which is looped and pitched, or it
        // can be a crossfaded blend of four clips which represent the timbre of the engine
        // at different RPM and Throttle state.

        // the engine clips should all be a steady pitch, not rising or falling.

        // when using four channel engine crossfading, the four clips should be:
        // lowAccelClip : The engine at low revs, with throttle open (i.e. begining acceleration at very low speed)
        // highAccelClip : Thenengine at high revs, with throttle open (i.e. accelerating, but almost at max speed)
        // lowDecelClip : The engine at low revs, with throttle at minimum (i.e. idling or engine-braking at very low speed)
        // highDecelClip : Thenengine at high revs, with throttle at minimum (i.e. engine-braking at very high speed)

        // For proper crossfading, the clips pitches should all match, with an octave offset between low and high.


        public enum EngineAudioOptions // Options for the engine audio
        {
            Simple, // Simple style audio
            FourChannel // four Channel audio
        }

        [Header("Vehicle Obect")]
        public PLY_CarRB GO_Vehicle;

        public EngineAudioOptions engineSoundStyle = EngineAudioOptions.FourChannel;// Set the default audio options to be four channel
        public AudioClip lowAccelClip;                                              // Audio clip for low acceleration
        public AudioClip lowDecelClip;                                              // Audio clip for low deceleration
        public AudioClip highAccelClip;                                             // Audio clip for high acceleration
        public AudioClip highDecelClip;                                             // Audio clip for high deceleration
        public float pitchMultiplier = 1f;                                          // Used for altering the pitch of audio clips
        public float lowPitchMin = 1f;                                              // The lowest possible pitch for the low sounds
        public float lowPitchMax = 6f;                                              // The highest possible pitch for the low sounds
        public float highPitchMultiplier = 0.25f;                                   // Used for altering the pitch of high sounds
        public float maxRolloffDistance = 500;                                      // The maximum distance where rollof starts to take place
        public float dopplerLevel = 1;                                              // The mount of doppler effect used in the audio
        public bool useDoppler = true;                                              // Toggle for using doppler

        private AudioSource m_LowAccel; // Source for the low acceleration sounds
        private AudioSource m_LowDecel; // Source for the low deceleration sounds
        private AudioSource m_HighAccel; // Source for the high acceleration sounds
        private AudioSource m_HighDecel; // Source for the high deceleration sounds
        private bool m_StartedSound; // flag for knowing if we have started sounds


        #region === State Changing ===

        private void StateChanging()
        {
            switch (VirtualStateManager.Instance.CurState)
            {

                #region == State MAIN_GP ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    State_MAIN_GP_Changing();
                    break;
                    #endregion
            }

        }

        private void SubStateChanging()
        {
            
        }

        private void ChekingAllFind()
        {
            {
                StateFunc.SetFindAll(true);
            }
        }


        private void State_MAIN_GP_Changing()
        {
            Initialize_MAIN_GP();
        }


        private void Initialize_MAIN_GP()
        {
            
        }

        #endregion === State Changing ===

        #region === Master State === 
        void Update()
        {
            StateFunc.StateUpdate(Update_State, StateFunc.StateChanging(StateChanging, SubStateChanging, ChekingAllFind));
        }

        private void Update_State()
        {
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == State GAMEPLAY ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    State_MAIN_GP_Update();
                    StateFunc.SubState(SubState_MAIN_GP_Update);
                    break;
                    #endregion
            }
        }

        private void FixedUpdate()
        {
            StateFunc.StateUpdate(Update_State_FU, StateFunc.StateChanging(StateChanging, SubStateChanging, ChekingAllFind));

        }

        private void Update_State_FU()
        {
            switch (VirtualStateManager.Instance.CurState)
            {

                #region == State MAIN_GP ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    State_MAIN_GP_FixedUpdate();

                    break;
                    #endregion
            }
        }

        private void LateUpdate()
        {
            StateFunc.StateUpdate(Update_State_LU, StateFunc.StateChanging(StateChanging, SubStateChanging, ChekingAllFind));
        }

        private void Update_State_LU()
        {
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == State MAIN_GP ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    State_MAIN_GP_LateUpdate();

                    break;
                    #endregion
            }
        }

        #endregion === Master State === 

        #region === State Update ===
        // Update is called once per frame


        private void State_MAIN_GP_Update()
        {
            if (!LibGameSetting.IsPause)
            {
                AudioUP();
            }
        }
        public float MaxSpeed = 40;
        [Range(0, 1f)]
        public float MinSpeedScl = 0.1f;
        [Range(0, 1f)]
        public float MaxSpeedScl = 1;
        public float MinGas = 0.15f, MaxGas = 0.85f; 
        public float MulitpleGasAdd = 0.15f, MultipleGasDec = 0.85f;

        //private float curVelMagCar =0;
        public float MulitpleVelMag =10f;
        [Space(10)]
        [Header("monitor")]
        public float curGas;
        public float SpeedCar;


        private void AudioUP()
        {
            // get the distance to main camera
            float camDist = (Camera.main.transform.position - transform.position).sqrMagnitude;

            // stop sound if the object is beyond the maximum roll off distance
            if (m_StartedSound && camDist > maxRolloffDistance * maxRolloffDistance)
            {
                StopSound();
            }

            // start the sound if not playing and it is nearer than the maximum distance
            if (!m_StartedSound && camDist < maxRolloffDistance * maxRolloffDistance)
            {
                StartSound();
            }

            if (m_StartedSound)
            {
                // The pitch is interpolated between the min and max values, according to the car's revs.
                //float scaleRpm = (GO_Vehicle.GetCurRPM()*3/4) / GO_Vehicle.MaxRpm ;
                //curVelMagCar = GO_Vehicle._RB.velocity.magnitude;
                ////if (curVelMagCar < Math.Abs(GO_Vehicle._RB.velocity.magnitude))
                ////{
                ////    curVelMagCar += Time.deltaTime * MulitpleVelMag;

                ////    if (curVelMagCar > MaxSpeed)
                ////        curVelMagCar = MaxSpeed;

                ////}
                ////else if(curVelMagCar > Math.Abs(GO_Vehicle._RB.velocity.magnitude))
                ////{
                ////    curVelMagCar -= Time.deltaTime * MulitpleVelMag;

                ////    if (curVelMagCar < 0)
                ////        curVelMagCar = 0;
                ////}
                ////SpeedCar = curVelMagCar / MaxSpeed;

                ////SpeedCar = SpeedCar < MinSpeedScl ? MinSpeedScl : SpeedCar >= MaxSpeedScl? MaxSpeedScl : SpeedCar;


                //float pitch = ULerp(lowPitchMin, lowPitchMax, SpeedCar);
                float pitch = ULerp(lowPitchMin, lowPitchMax, GO_Vehicle.Revs);

                // clamp to minimum pitch (note, not clamped to max for high revs while burning out)
                pitch = Mathf.Min(lowPitchMax, pitch);

                if (engineSoundStyle == EngineAudioOptions.Simple)
                {
                    // for 1 channel engine sound, it's oh so simple:
                    m_HighAccel.pitch = pitch * pitchMultiplier * highPitchMultiplier;
                    m_HighAccel.dopplerLevel = useDoppler ? dopplerLevel : 0;
                    m_HighAccel.volume = 1;
                }
                else
                {
                    // for 4 channel engine sound, it's a little more complex:

                    // adjust the pitches based on the multipliers
                    m_LowAccel.pitch = pitch * pitchMultiplier;
                    m_LowDecel.pitch = pitch * pitchMultiplier;
                    m_HighAccel.pitch = pitch * highPitchMultiplier * pitchMultiplier;
                    m_HighDecel.pitch = pitch * highPitchMultiplier * pitchMultiplier;

                    //if (VirtualDataInputManager.Instance.AxisFinalInput.y >= 0.9f)
                    //{
                    //    curGas += Time.deltaTime * MulitpleGasAdd;
                    //}
                    //else if (VirtualDataInputManager.Instance.AxisFinalInput.y <= -0.9f)
                    //{
                    //    curGas -= Time.deltaTime * MulitpleGasAdd;
                    //}
                    //else if (VirtualDataInputManager.Instance.AxisFinalInput.y < 0.25f && VirtualDataInputManager.Instance.AxisFinalInput.y > 0)
                    //{ 
                    //    curGas -= Time.deltaTime * MultipleGasDec;
                    //    if (VirtualDataInputManager.Instance.AxisFinalInput.y < 0)
                    //    {
                    //        curGas = 0;
                    //    }
                    //}
                    //else if (VirtualDataInputManager.Instance.AxisFinalInput.y> -0.25f && VirtualDataInputManager.Instance.AxisFinalInput.y < -0)
                    //{
                    //    curGas -= Time.deltaTime * MultipleGasDec;
                    //    if (VirtualDataInputManager.Instance.AxisFinalInput.y < 0)
                    //    {
                    //        curGas = 0;
                    //    }
                    //}

                    //curGas = Mathf.Clamp(curGas, MinGas, MaxGas);
                    //// get values for fading the sounds based on the acceleration
                    //float accFade = Mathf.Abs(curGas);

                    float accFade = Mathf.Abs(GO_Vehicle.GetAccInput());
                    float decFade = 1 - accFade;

                    // get the high fade value based on the cars revs
                    float highFade = Mathf.InverseLerp(0.2f, 0.8f, GO_Vehicle.Revs);
                    float lowFade = 1 - highFade;

                    // adjust the values to be more realistic
                    highFade = 1 - ((1 - highFade) * (1 - highFade));
                    lowFade = 1 - ((1 - lowFade) * (1 - lowFade));
                    accFade = 1 - ((1 - accFade) * (1 - accFade));
                    decFade = 1 - ((1 - decFade) * (1 - decFade));

                    // adjust the source volumes based on the fade values
                    m_LowAccel.volume = lowFade * accFade;
                    m_LowDecel.volume = lowFade * decFade;
                    m_HighAccel.volume = highFade * accFade;
                    m_HighDecel.volume = highFade * decFade;

                    // adjust the doppler levels
                    m_HighAccel.dopplerLevel = useDoppler ? dopplerLevel : 0;
                    m_LowAccel.dopplerLevel = useDoppler ? dopplerLevel : 0;
                    m_HighDecel.dopplerLevel = useDoppler ? dopplerLevel : 0;
                    m_LowDecel.dopplerLevel = useDoppler ? dopplerLevel : 0;
                }
            }
        }

        private void SubState_MAIN_GP_Update(LibEdStateUtilities.GameSubStates subState)
        {
            switch (subState)
            {

                #region == SubState MAIN_GP_GAMEPLAY_PAUSE ==
                case LibEdStateUtilities.GameSubStates.MAIN_GP_GAMEPLAY_PAUSE:
                    if (LibGameSetting.IsPause)
                    {

                    }
                    break;
                    #endregion
            }
        }

        #endregion === State Update ===

        #region === State Fixed Update ===


        private void State_MAIN_GP_FixedUpdate()
        {
            if (!LibGameSetting.IsPause)
            {
                Locomotion_FU();
            }
        }

        private void Locomotion_FU()
        {

        }

        #endregion === State Fixed Update ===


        #region === State Late Update ===


        private void State_MAIN_GP_LateUpdate()
        {
            if (!LibGameSetting.IsPause)
            {

            }
        }

        #endregion === State Late Update ===

        #region === StateFunction Collision ===



        private void OnCollisionStay(Collision collision)
        {

        }

        private void OnCollisionEnter(Collision collision)
        {


        }

        private void OnCollisionExit(Collision collision)
        {


        }

        #endregion === StateFunction Collision ===

        private void StartSound()
        {
            // get the carcontroller ( this will not be null as we have require component)
            GO_Vehicle = GetComponent<PLY_CarRB>();

            // setup the simple audio source
            m_HighAccel = SetUpEngineAudioSource(highAccelClip);

            // if we have four channel audio setup the four audio sources
            if (engineSoundStyle == EngineAudioOptions.FourChannel)
            {
                m_LowAccel = SetUpEngineAudioSource(lowAccelClip);
                m_LowDecel = SetUpEngineAudioSource(lowDecelClip);
                m_HighDecel = SetUpEngineAudioSource(highDecelClip);
            }

            // flag that we have started the sounds playing
            m_StartedSound = true;
        }


        private void StopSound()
        {
            //Destroy all audio sources on this object:
            foreach (var source in GetComponents<AudioSource>())
            {
                Destroy(source);
            }

            m_StartedSound = false;
        }


        

        // sets up and adds new audio source to the gane object
        private AudioSource SetUpEngineAudioSource(AudioClip clip)
        {
            // create the new audio source component on the game object and set up its properties
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = clip;
            source.volume = 0;
            source.loop = true;

            // start the clip from a random point
            source.time = Random.Range(0f, clip.length);
            source.Play();
            source.minDistance = 5;
            source.maxDistance = maxRolloffDistance;
            source.dopplerLevel = 0;
            return source;
        }


        // unclamped versions of Lerp and Inverse Lerp, to allow value to exceed the from-to range
        private static float ULerp(float from, float to, float value)
        {
            return (1.0f - value) * from + value * to;
        }
    }
}
