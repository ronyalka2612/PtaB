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
using Unity.Burst;

namespace Com.GNLTest.Test1
{
    public class SpawnerDirect : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
    {


        public GameObject Prefab;
        public int CountX;
        public int CountY;

        // Referenced prefabs have to be declared so that the conversion system knows about them ahead of time
        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(Prefab);
        }

        // Lets you convert the editor data representation to the entity optimal runtime representation
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var spawnerData = new Spawner_FromEntity
            {
                // The referenced prefab will be converted due to DeclareReferencedPrefabs.
                // So here we simply map the game object to an entity reference to that prefab.
                Prefab = conversionSystem.GetPrimaryEntity(Prefab),
                CountX = CountX,
                CountY = CountY,
            };
            dstManager.AddComponentData(entity, spawnerData);
        }
    }
    public struct Spawner_FromEntity : IComponentData
    {
        public int CountX;
        public int CountY;
        public Entity Prefab;
    }

    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial class SpawnerSystem_FromEntity : SystemBase
    {
        BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;
        EntityCommandBufferSystem m_EntityCommandBufferSystemCons;
        EntityManager EM;

        private bool _isConvertToECS;
        private bool _isDeleteEntity;
        protected override void OnCreate()
        {
            // Cache the BeginInitializationEntityCommandBufferSystem in a field, so we don't have to create it every frame
            m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
            m_EntityCommandBufferSystemCons = World.GetOrCreateSystem<EntityCommandBufferSystem>();
            EM = World.DefaultGameObjectInjectionWorld.EntityManager;
        }

        protected override void OnUpdate()
        {
            InputMaping();

            SpawnerByEntity();

            DeleteEntity();
        }

        private void DeleteEntity()
        {
            if (_isDeleteEntity)
            {
                var commandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer().AsParallelWriter();

                Entities
                    .ForEach((Entity entity, int entityInQueryIndex) =>
                {
                    
                    commandBuffer.DestroyEntity(entityInQueryIndex, entity);
                    
                }).ScheduleParallel();

                

                m_EntityCommandBufferSystem.AddJobHandleForProducer(Dependency);
            }
        }

        private void SpawnerByEntity()
        {
            if(_isConvertToECS)
            { 
                //Instead of performing structural changes directly, a Job can add a command to an EntityCommandBuffer to perform such changes on the main thread after the Job has finished.
                //Command buffers allow you to perform any, potentially costly, calculations on a worker thread, while queuing up the actual insertions and deletions for later.
                var commandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer().AsParallelWriter();


                Entities
                    .WithName("SpawnerDirect")
                    .WithBurst(FloatMode.Default, FloatPrecision.Standard, true)
                    .ForEach((Entity entity, int entityInQueryIndex, in Spawner_FromEntity spawnerFromEntity, in LocalToWorld location) =>
                    {
                        for (var x = 0; x < spawnerFromEntity.CountX; x++)
                        {
                            for (var y = 0; y < spawnerFromEntity.CountY; y++)
                            {
                                var instance = commandBuffer.Instantiate(entityInQueryIndex, spawnerFromEntity.Prefab);
                                // Place the instantiated in a grid with some noise
                                var position = math.transform(location.Value,
                                        new float3(x * 1.3F, noise.cnoise(new float2(x, y) * 0.21F) * 2, y * 1.3F));
                                commandBuffer.SetComponent(entityInQueryIndex, instance, new Translation { Value = position });
                                //commandBuffer.SetComponent(entityInQueryIndex, instance, new NameEntity { Value = spawnerFromEntity.PrefabName+(y+x) });
                            }
                        }
                        commandBuffer.DestroyEntity(entityInQueryIndex, entity);
                    }).ScheduleParallel();

                // SpawnJob runs in parallel with no sync point until the barrier system executes.
                // When the barrier system executes we want to complete the SpawnJob and then play back the commands (Creating the entities and placing them).
                // We need to tell the barrier system which job it needs to complete before it can play back the commands.
                m_EntityCommandBufferSystem.AddJobHandleForProducer(Dependency);
                Debug.Log("_isConvertToECS Done");
            }


        }

        private void InputMaping()
        {
            if (Input.GetKey(KeyCode.Alpha3))
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

            if (Input.GetKey(KeyCode.Alpha4))
            {
                if (!VirtualInputDelayManager.Instance.AnyBtn("_isDeleteEntity"))
                {

                    Debug.Log("_isDeleteEntity");
                    _isDeleteEntity = true;
                    VirtualInputDelayManager.Instance.AddBtn("_isDeleteEntity", VirtualInputDelayManager.Instance.TimeDelay);
                }
            }
            else
            {
                _isDeleteEntity = false;
            }



        }
    }
}