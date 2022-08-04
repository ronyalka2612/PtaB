using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using System;
using Unity.Collections;
using Unity.Jobs;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine.Rendering;

namespace Com.GNLTest.Test1
{
    public class SpawnerOld : MonoBehaviour
    {
        [Header("Entity settinng")]
        public GameObject PrefabObject;
        [Range(0, 10)]
        public float OffsetY = 2;
        [MyBox.ReadOnly] [SerializeField] Mesh Mesh;
        [SerializeField] Material Material;

        [Header("Old One Setting")]
        [Range(0, 10000)]
        public int Jumlah = 10;

        [Header("ECS Setting")]
        [Range(0, 50000)]
        public int JumlahECS = 10;

        [Header("Use ECS")]
        public bool isUseECS = false;
        List<GameObject> _listObj;
        [Header("Use Both")]
        public bool isUseBoth = false;

        private float _posY = 0;
        private float _addScale;

        //private float _posYECS = 1;
        private float _addScaleECS;

        [MyBox.ReadOnly] [SerializeField]private int _curJumlah;
        // Start is called before the first frame update
        void Start()
        {

            #region === ECS ===
            _addScaleECS = PrefabObject.transform.localScale.y / 2;

            //World world = World.DefaultGameObjectInjectionWorld;
            //EntityManager entityManager = world.EntityManager;

            //EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);

            //Mesh = PrefabObject.GetComponent<MeshFilter>().sharedMesh;
            //Material = PrefabObject.GetComponent<Renderer>().sharedMaterials[0];
            //RenderMeshDescription desc = new RenderMeshDescription();
            //try
            //{
            //    desc = new RenderMeshDescription(
            //        Mesh,
            //        Material,
            //        shadowCastingMode: ShadowCastingMode.Off,
            //        receiveShadows: false);
            //}
            //catch (Exception e)
            //{
            //    Debug.LogException(e, this);
            //    Debug.LogWarning("cekcek warning");
            //}

            //EntityArchetype entyAT = entityManager.CreateArchetype(
            //    typeof(Rigidbody)
            //    );

            //// Create empty base entity
            //Entity prototype = entityManager.CreateEntity();

            //// Call AddComponents to populate base entity with the components required
            //// by Hybrid Renderer
            //RenderMeshUtility.AddComponents(
            //    prototype,
            //    entityManager,
            //    desc);
            //entityManager.AddComponentData(prototype, new LocalToWorld());

            //// Spawn most of the entities in a Burst job by cloning a pre-created prototype entity,
            //// which can be either a Prefab or an entity created at run time like in this sample.
            //// This is the fastest and most efficient way to create entities at run time.

            //SpawnJob spawnJob = new SpawnJob
            //{

            //    Prototype = prototype,
            //    Ecb = ecb.AsParallelWriter(),
            //    AddScale = _addScaleECS,
            //    PosY = 1,
            //    OffsetY = this.OffsetY

            //};

            //JobHandle spawnHandle = spawnJob.Schedule(JumlahECS, 128);
            //spawnHandle.Complete();

            //ecb.Playback(entityManager);
            //ecb.Dispose();
            //entityManager.DestroyEntity(prototype);
            #endregion


            #region === Old One ===
            _addScale = _addScaleECS;
            _listObj = new List<GameObject>();
            for (int i = 0; i < Jumlah; i++)
            {
                if (i == 0)
                    _posY = 0;
                _listObj.Add(Instantiate(PrefabObject, new Vector3(0,_posY,0), new Quaternion()));
                _posY += _addScale + OffsetY;
            }
            #endregion
        }

        private bool _isTransition = false;
        private bool _isConvertToECS = false;
        private bool _isExecutionSpwaner = false;
        // Update is called once per frame
        void Update()
        {
            InputMaping();

            if (_listObj != null)
                _curJumlah = _listObj.Count;

            RelocateObjects();



            if (!isUseECS)
            {
                ExecutionSpwaner();
            }
            if (isUseECS)
            {
                ExecutionSpwanerECS();
            }

            ConvertToECS();

        }

        private void InputMaping()
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                if (!VirtualInputDelayManager.Instance.AnyBtn("_isConvertToECS"))
                {

                    Debug.Log("_isConvertToECS");
                    _isConvertToECS = true;
                    VirtualInputDelayManager.Instance.AddBtn("_isConvertToECS", VirtualInputDelayManager.Instance.TimeDelay);
                }
            }
            else 
            {
                _isConvertToECS = false;
            }
            if (Input.GetKey(KeyCode.Alpha0))
            {
                if (!VirtualInputDelayManager.Instance.AnyBtn("_isTransition"))
                {

                    Debug.Log("_isTransition");
                    _isTransition = true;
                    VirtualInputDelayManager.Instance.AddBtn("_isTransition", VirtualInputDelayManager.Instance.TimeDelay);
                }
            }
            else 
            {
                _isTransition = false;
            }



            if (Input.GetKey(KeyCode.Space))
            {
                
                if (!VirtualInputDelayManager.Instance.AnyBtn("_isExecutionSpwaner"))
                {
                    Debug.Log("_isExecutionSpwaner");
                    _isExecutionSpwaner = true;
                    VirtualInputDelayManager.Instance.AddBtn("_isExecutionSpwaner", VirtualInputDelayManager.Instance.TimeDelay);
                }
            }
            else
            {
                _isExecutionSpwaner = false;
            }

            
        }

        private void RelocateObjects()
        {
            
            //change from old one to ecs
            if (!isUseECS && _isTransition)
            {
                _isTransition = false;
                isUseECS = true;
            }
            else if (isUseECS && _isTransition)
            {
                _isTransition = false;
                isUseECS = false;
            }
        }
        private void ExecutionSpwanerECS()
        {

        }
        private void ConvertToECS()
        {
            if (_isConvertToECS)
            {
                if (_listObj != null && _listObj.Count > 0)
                {
                    for (int i = 0; i < Jumlah; i++)
                    {
                        _listObj[i].AddComponent<ConvertToEntity>(); 
                    }
                    _listObj.Clear();
                }


            }
        }


        private void ExecutionSpwaner()
        {
            if (_isExecutionSpwaner)
            {
                if (_listObj == null)
                {
                    _listObj = new List<GameObject>();
                }

                if (_listObj.Count > Jumlah)
                {
                    int j = 0;
                    for (int i = _listObj.Count-1; i+1 > Jumlah; i--)
                    {
                        //_listObj[i] = null;
                        Destroy(_listObj[i]);
                        _listObj.RemoveAt(i);
                        _posY -= (_addScale + OffsetY);
                        j++;
                    }
                    Debug.Log("Delete count:" + j);
                }
                else if (_listObj.Count < Jumlah)
                {
                    int j = 0;
                    for (int i = _listObj.Count; i < Jumlah; i++)
                    {
                        _listObj.Add(Instantiate(PrefabObject, new Vector3(0, _posY, 0), new Quaternion()));
                        _posY += _addScale + OffsetY;
                        j++;
                    }
                    Debug.Log("Add count:" + j);
                }
                
            }
        }

        

        [BurstCompatible]
        public struct SpawnJob : IJobParallelFor
        {
            public Entity Prototype;
            //public int EntityCount;
            public EntityCommandBuffer.ParallelWriter Ecb;
            public float PosY ;
            public float AddScale;
            public float OffsetY;

            public void Execute(int index)
            {
                // Clone the Prototype entity to create a new entity.
                Entity e = Ecb.Instantiate(index, Prototype);
                // Prototype has all correct components up front, can use SetComponent to
                // set values unique to the newly created entity, such as the transform.
                Ecb.SetComponent(index, e, new LocalToWorld { Value = ComputeTransform(index) });
                //Ecb.SetComponent(index, e, new Rigidbody { Value = SetRB() });
            }


            public float4x4 ComputeTransform(int index)
            {
                float y = (index)+((AddScale + OffsetY) * index);
                Debug.Log("cekcekcek index :" + index + ", PosY:" + y);
                float3 f3 = new float3(3, y, 0);
                float4x4 pos = float4x4.Translate(f3);
                return pos;
            }
        }
    }


}