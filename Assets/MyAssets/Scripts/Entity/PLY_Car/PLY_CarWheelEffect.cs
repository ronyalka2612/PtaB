using MyBox;
using UnityEngine;

using Com.GNL.URP_MyLib;
using System.Collections;

namespace Com.GNL.URP_MyLibProjectTest
{
    [RequireComponent(typeof(AudioSource))]
    public class PLY_CarWheelEffect : MonoBehaviourMyBase
    {
        #region === Attribute ===
        [SerializeField]
        private TrailRenderer SkidTrail;
        //public static Transform skidTrailsDetachedParent;
        [SerializeField]
        private ParticleSystem skidParticles;
        [Range(-1f, 1f)]
        [SerializeField]
        private float PosHighSkidTrail = -0.5f;
        [SerializeField] 
        private bool UseTestChangeInUpdate = false;

        public bool skidding { get; private set; }
        public bool PlayingAudio { get; private set; }


        private AudioSource m_AudioSource;
        private WheelCollider m_WheelCollider;

        private Vector3 posDefault;

        #endregion === Attribute ===


        #region === State Changing ===

        private void OnGUI()
        {
            
        }

        private void OnValidate()
        {
           
        }
        
        private void Awake()
        {
            m_WheelCollider = GetComponent<WheelCollider>();
            m_AudioSource = GetComponent<AudioSource>();
            m_AudioSource.Stop();
            PlayingAudio = false;
            //SkidTrail.SetActive(false);
            SkidTrail.transform.position = m_WheelCollider.transform.position + (- Vector3.up * m_WheelCollider.radius);
            posDefault = SkidTrail.transform.position;
            SkidTrail.transform.position = posDefault + new Vector3(0, PosHighSkidTrail, 0);

        }

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
            switch (VirtualStateManager.Instance.CurState)
            {
                #region == State GAMEPLAY ==
                case LibEdStateUtilities.GameStates.MAIN_GP:
                    StateFunc.SubState(SubState_MAIN_GP_Changing);
                    break;
                    #endregion
            }

        }

        private void ChekingAllFind()
        {
            //if (VirtualStateManager.Instance.CurState == LibEdStateUtilities.GameStates.MAIN_GP)
            //{
            //    if (skidTrailsDetachedParent == null)
            //    {
            //        StateFunc.SetFindAll(false);
            //    }
            //    else
            //        StateFunc.SetFindAll(true);
            //}
            //else
            {
                StateFunc.SetFindAll(true);
            }
        }



        private void SubState_MAIN_GP_Changing(LibEdStateUtilities.GameSubStates substate)
        {
            switch (substate)
            {

                #region == SubState MAIN_GP_GAMEPLAY_PAUSE ==
                case LibEdStateUtilities.GameSubStates.MAIN_GP_GAMEPLAY_PAUSE:

                    break;
                    #endregion
            }

        }

        private void State_MAIN_GP_Changing()
        {
            Initialize_MAIN_GP();
        }


        private void Initialize_MAIN_GP()
        {

            //skidParticles = transform.root.GetComponentInChildren<ParticleSystem>();

            if (skidParticles == null)
            {
                Debug.LogWarning(" no particle system found on car to generate smoke particles", gameObject);
            }
            else
            {
                skidParticles.Stop();
            }

            PlayingAudio = false;

            //if (skidTrailsDetachedParent == null)
            //{
            //    skidTrailsDetachedParent = LibFormulation.FindObjectByTagThenName(
            //        Utilities.TAG.DETACHED.ToString(), 
            //        Utilities.FIND_GO.Detached.ToString()
            //        ).transform;
            //    //Debug.Log("cekcekcek 2 skidTrailsDetachedParent Name :" + skidTrailsDetachedParent.name);
            //    //Debug.Log("cekcekcek 2 skidTrailsDetachedParent.parent name :" + skidTrailsDetachedParent.parent.name);
            //    //Debug.Log("cekcekcek 2 skidTrailsDetachedParent.parent.parent name :" + skidTrailsDetachedParent.parentname);
            //}

            //Debug.Log("cekcekcek 1 skidTrailsDetachedParent Name :" + skidTrailsDetachedParent.name);

            if (m_AudioSource.isPlaying)
            {
                m_AudioSource.Stop();
                PlayingAudio = false;
            }
        }




        #endregion === State Changing ===

        #region === Base State Update ===

        public void Update()
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
        private void State_MAIN_GP_Update()
        {
            if (!LibGameSetting.IsPause)
            {

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

        private void State_MAIN_GP_FixedUpdate()
        {
            if (!LibGameSetting.IsPause)
            {

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

        private void State_MAIN_GP_LateUpdate()
        {
            if (!LibGameSetting.IsPause)
            {

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

        #endregion === Base State Update ===

        #region === On Collision ===
        private void OnCollisionStay(Collision collision)
        {

        }

        private void OnCollisionEnter(Collision collision)
        {


        }

        private void OnCollisionExit(Collision collision)
        {


        }

        #endregion === On Collision ===

        #region === Other Function ===


        public void EmitTyreSmoke(WheelHit collider)
        {
            
            skidParticles.transform.position = transform.position - transform.up * m_WheelCollider.radius;
            skidParticles.Emit(1);
            if (!skidding)
            {
#if UNITY_EDITOR
                if(UseTestChangeInUpdate)
                {
                    SkidTrail.transform.position = collider.point + new Vector3(0, PosHighSkidTrail, 0);
                }
#endif
                SkidTrail.transform.Rotate(collider.normal);
                SkidTrail.emitting = true;
                skidding = true;
            }
            //if (!skidding)
            //{
            //    StartCoroutine(StartSkidTrail());
            //}
        }


        public void PlayAudio()
        {
            m_AudioSource.Play();
            PlayingAudio = true;
        }


        public void StopAudio()
        {
            m_AudioSource.Stop();
            PlayingAudio = false;
        }


        //public IEnumerator StartSkidTrail()
        //{
        //    skidding = true;
        //    //m_SkidTrail = Instantiate(SkidTrailPrefab);
        //    //while (m_SkidTrail == null)
        //    //{
        //        yield return null;
        //    //}
        //    //m_SkidTrail.parent = transform;
        //    //m_SkidTrail.localPosition = -Vector3.up * m_WheelCollider.radius;
        //}


        public void EndSkidTrail()
        {
            if (!skidding)
            {
                return;
            }
            skidding = false;
            SkidTrail.emitting = false;
            //m_SkidTrail.parent = skidTrailsDetachedParent;
            //Destroy(m_SkidTrail.gameObject, 10);
        }
        #endregion === Other Function ===


    }

}