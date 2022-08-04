using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Com.GNL.URP_MyLib;

namespace Com.GNL.URP_MyLibProjectTest
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ProjectileController : MonoBehavMoveObj
    {
        #region === Attributes ===

        [Header("Referance")]
        [SerializeField]
        private GameObject[] Spark;
        //[SerializeField] 
        //private PLY_CarRB _Player;
        //[SerializeField]
        //private WeaponController _WeaponController;
        //Rigidbody _rigidbody;

        [Header("Controll Bullet")]
        [SerializeField]
        private float ShootDelay = 0.5f ;

        private ParticleSystem _ParticleSystem;
        private GameObject[] _ParticleSystemSpark;

        private bool _IsShoot;
        private float _curTimeNormalShoot;

        #endregion === Attributes ===
        #region === Getter Setter ===

        public bool GetIsShoot()
        {
            return _IsShoot;
        }
        public void SetIsShoot(bool flag)
        {
            _IsShoot = flag;
        }

        #endregion === Getter Setter ===

        #region === State Changing ===
        private void Awake()
        {
            InstantSpark();
            
            _ParticleSystem = GetComponent<ParticleSystem>();
        }

        private void OnValidate()
        {
            
        }

        private void InstantSpark()
        {
            if (_ParticleSystemSpark == null)
            {
                _ParticleSystemSpark = new GameObject[Spark.Length];
                _ParticleSystemSpark[0] = Instantiate(Spark[0], transform);
            }
        }

        void Start()
        {
        }



        private void StateChanging()
        {
            
        }

        private void initiate()
        {
            
        }

        private void SubStateChanging()
        {

        }

        private void ChekingAllFind()
        {
            if (VirtualStateManager.Instance.CurState == LibEdStateUtilities.GameStates.MAIN_GP)
            {
                if (_ParticleSystem == null)
                {
                    StateFunc.SetFindAll(false);
                    //StateFunc.ClearState();
                }
                else 
                {
                    StateFunc.SetFindAll(true);
                }
            }
            else
                StateFunc.SetFindAll(true);
        }


        #endregion === State Changing ===
        #region === Base State Update ===
        public override void Update_Obj()
        {
            StateFunc.StateUpdate(Update_State, StateFunc.StateChanging(StateChanging, SubStateChanging, ChekingAllFind));
        }

        void Update_State()
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

        private void SubState_MAIN_GP_Update(LibEdStateUtilities.GameSubStates curSubState)
        {

        }

        private void State_MAIN_GP_Update()
        {
            Shooting();
            
        }

        private void Shooting()
        {


            if (_IsShoot && _curTimeNormalShoot > ShootDelay)
            {
                //Debug.Log("cekcekcek _IsShoot");
                _IsShoot = false;
                _ParticleSystem.Play();
                _curTimeNormalShoot = 0;
            }
            else 
            {
                if(_curTimeNormalShoot <= ShootDelay)
                _curTimeNormalShoot += Time.deltaTime;

            }

        }

        public override void Update_FU_Obj()
        {
            
        }

        public override void Update_LU_Obj()
        {
            
        }

        #endregion === Base State Update ===

        private List<ParticleCollisionEvent> _colEvents = new List<ParticleCollisionEvent>();


        #region === OnCollision ===
        private void OnParticleCollision(GameObject other)
        {

            //Debug.Log("cekcekcek hithithit other:" + other);
            int events = _ParticleSystem.GetCollisionEvents(other, _colEvents);
            for (int i = 0; i < events; i++)
            {
                for (int j = 0; j < Spark.Length; j++)
                {
                    
                    _ParticleSystemSpark[0].transform.SetPositionAndRotation(_colEvents[i].intersection, Quaternion.LookRotation(_colEvents[i].normal));
                    _ParticleSystemSpark[0].GetComponent<ParticleSystem>().Play();
                    //if (Spark[j] != null)
                    //{
                    //StartCoroutine(InstansSpark(Spark[j], i));
                    //GameObject sparkIns = Instantiate(Spark[j], _colEvents[i].intersection, Quaternion.LookRotation(_colEvents[i].normal));
                    //sparkIns.hideFlags = HideFlags.HideInHierarchy;
                    //}
                }
            }
        }

        private IEnumerator InstansSpark(GameObject gameObject, int idEvents)
        {
            if (gameObject != null)
            {
                GameObject sparkIns = Instantiate(gameObject, _colEvents[idEvents].intersection, Quaternion.LookRotation(_colEvents[idEvents].normal));
                sparkIns.hideFlags = HideFlags.HideInHierarchy;
            }
            yield return null;
        }
        #endregion === OnCollision ===


        #region === Other Function ===

        #endregion === Other Function ===
    }
}