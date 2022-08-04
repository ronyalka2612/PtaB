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
    public class PlayerC1 : MonoBehaviour
    {
        [Header("Refereance")]
        public CharacterController CC;

        [Header("Setting Game")]
        public float Gravity = 50f;

        [Header("Setting Control")]
        [Range(0.001f, 20)]
        public float TurnSpeed = 13.33f;
        public float CharacterSpeed = 2f;
        public float CharacterAnimAcceleration = 5.5f;
        public float CharacterAnimDeceleration = 4.5f;

        [Header("Setting Control Jump")]
        public float JumpPower = 2;
        public float JumpHeightMax = 3f;
        public float TimeMaxFall = 2f;
        public float FallHeightMax = 20f;

        [Range(1, 5)]
        public float FallAcc = 2f;
        public AnimationCurve JumpGraph;
        public AnimationCurve JumpFallGraph;

        [Header("Jump control")]
        public bool isInJump;

        [Header("Batasan control")]
        public float CharacterSpeedMAX = 3.5f;
        public float FallVeloActInUp = 0.0085f;
        public float FallVeloActInDw = 0.04f;

        
        private float _velocityAir = 0;



        private Vector3 _targetDirection;
        private float _turnSpeedMultiplier;

        private float _curSpeed = 0f;
        private Quaternion _freeRotation;

        private void Update()
        {
            
            ControlMovement();
            //Locomotion();
            Locomotion2();
        }


        private void FixedUpdate()
        {

            //if (!CC.isGrounded && !isInJump)
            //{
            //    SetVelocityAir(_velocityAir - Gravity * (Time.fixedDeltaTime));
            //}

        }


        private void ControlMovement()
        {
            if (CC.isGrounded && InputHandle.Jump)
            {
                SetVelocityAirJump(JumpPower * (Time.deltaTime));
            }


            if (InputHandle.Move)
            { 
                if (GetCurSpeed() < CharacterSpeedMAX)
                {
                    SetCurSpeed(GetCurSpeed() + Time.deltaTime * CharacterAnimAcceleration);
                    if (GetCurSpeed() > CharacterSpeedMAX)
                    {
                        SetCurSpeed(CharacterSpeedMAX);
                    }
                }
                else if (GetCurSpeed() > CharacterSpeedMAX)
                {
                    SetCurSpeed(GetCurSpeed() - Time.deltaTime * CharacterAnimDeceleration);
                    if (GetCurSpeed() < CharacterSpeedMAX)
                    {
                        SetCurSpeed(CharacterSpeedMAX);
                    }
                }
            }
            else
            {
                if (GetCurSpeed() > 0)
                {
                    SetCurSpeed(GetCurSpeed() - Time.deltaTime * CharacterAnimDeceleration);
                }
            }
        }

        float timerFall =0;

        private void Locomotion()
        {
            
            if (!CC.isGrounded && isInJump)
            {
                float length = transform.position.y - curY;
                if (length < JumpHeightMax-0.1f)
                {
                    float times = length / JumpHeightMax;
                    times = times < 0.001f ? 0.001f: times;
                    SetVelocityAir(JumpPower * JumpGraph.Evaluate(times) * (Time.deltaTime));
                }
                else
                {
                    curY = transform.position.y - (JumpHeightMax);
                    SetVelocityAir(0);
                    isInJump = false;
                    timerFall = 0;
                }
            }

            if (!CC.isGrounded && !isInJump)
            {
                
                if (timerFall < TimeMaxFall)
                {
                    SetVelocityAir(-Gravity * JumpFallGraph.Evaluate(timerFall/ TimeMaxFall) * (Time.deltaTime));
                    timerFall += Time.deltaTime;

                    //Debug.Log("timerFall :" + timerFall);
                    //Debug.Log("velocity Air:" + GetVelocityAir());
                }
                else if (timerFall >= TimeMaxFall)
                {
                    // falling
                    SetVelocityAir(-Gravity * (Time.deltaTime));
                }
            }

            //Gravitation when grounded keep it falling
            if (CC.isGrounded && _velocityAir < 0 && !InputHandle.Jump)
            {
                curY = 0;
                timerFall = 0;
                SetVelocityAir(0f);
            }

            //when landing need update where the player should move by arrow and camera angle
            if (CC.isGrounded)
            {
                UpdateTargetDirection();
            }

            Vector3 targetDir = Vector3.zero;
            //counting move when any input
            if (_targetDirection.magnitude > 0.1f)
            {
                // set the player's direction
                Vector3 lookDirection = _targetDirection.normalized;
                _freeRotation = Quaternion.LookRotation(lookDirection, CC.transform.up);
                var diferenceRotation = _freeRotation.eulerAngles.y - CC.transform.eulerAngles.y;
                var eulerY = CC.transform.eulerAngles.y;
                if (diferenceRotation < 0 || diferenceRotation > 0)
                {
                    eulerY = _freeRotation.eulerAngles.y;
                }
                var euler = new Vector3(0, eulerY, 0);
                //if (!_anim.GetBool(PlayerController.ANIM_PARAM.isInCombating.ToString()))
                CC.transform.rotation = Quaternion.Slerp(CC.transform.rotation, Quaternion.Euler(euler), TurnSpeed * _turnSpeedMultiplier * Time.deltaTime);


                // set the player's Movement
                targetDir = _targetDirection * GetCurSpeed() * CharacterSpeed * Time.deltaTime;
                targetDir = new Vector3(targetDir.x, GetVelocityAir(), targetDir.z);
                CC.Move(targetDir);

            }
            // when velocity still on that area, player will fall dawn or go up

            else if (_velocityAir > FallVeloActInUp || _velocityAir < -FallVeloActInDw)

            {
                targetDir = new Vector3(0f, _velocityAir, 0f);
                CC.Move(targetDir);
            }
        }



        private void Locomotion2()
        {
            if (!CC.isGrounded && isInJump)
            {
                float length = transform.position.y - curY;
                if (length < JumpHeightMax - 0.1f)
                {
                    float times = length / JumpHeightMax;
                    times = times < 0.001f ? 0.001f : times;
                    SetVelocityAir(JumpPower * JumpGraph.Evaluate(times) * (Time.deltaTime));
                }
                else
                {
                    curY = transform.position.y - (JumpHeightMax);
                    SetVelocityAir(0);
                    isInJump = false;
                    timerFall = 0;
                }
            }

            if (!CC.isGrounded && !isInJump)
            {

                if (timerFall < TimeMaxFall)
                {
                    SetVelocityAir(-Gravity * JumpFallGraph.Evaluate(timerFall / TimeMaxFall) * (Time.deltaTime));
                    timerFall += Time.deltaTime;

                    //Debug.Log("timerFall :" + timerFall);
                    //Debug.Log("velocity Air:" + GetVelocityAir());
                }
                else if (timerFall >= TimeMaxFall)
                {
                    // falling
                    SetVelocityAir(-Gravity * (Time.deltaTime));
                }
            }

            //Gravitation when grounded keep it falling
            if (CC.isGrounded && _velocityAir < 0 && !InputHandle.Jump)
            {
                curY = 0;
                timerFall = 0;
                SetVelocityAir(0f);
            }

            //when landing need update where the player should move by arrow and camera angle
            if (CC.isGrounded)
            {
                UpdateTargetDirection();
            }

            Vector3 targetDir = Vector3.zero;
            //counting move when any input
            if (_targetDirection.magnitude > 0.1f)
            {
                // set the player's direction
                Vector3 lookDirection = _targetDirection.normalized;
                _freeRotation = Quaternion.LookRotation(lookDirection, CC.transform.up);
                var diferenceRotation = _freeRotation.eulerAngles.y - CC.transform.eulerAngles.y;
                var eulerY = CC.transform.eulerAngles.y;
                if (diferenceRotation < 0 || diferenceRotation > 0)
                {
                    eulerY = _freeRotation.eulerAngles.y;
                }
                var euler = new Vector3(0, eulerY, 0);
                //if (!_anim.GetBool(PlayerController.ANIM_PARAM.isInCombating.ToString()))
                CC.transform.rotation = Quaternion.Slerp(CC.transform.rotation, Quaternion.Euler(euler), TurnSpeed * _turnSpeedMultiplier * Time.deltaTime);


                // set the player's Movement
                targetDir = _targetDirection * GetCurSpeed() * CharacterSpeed * Time.deltaTime;
                targetDir = new Vector3(targetDir.x, GetVelocityAir(), targetDir.z);
                CC.Move(targetDir);

            }
            // when velocity still on that area, player will fall dawn or go up

            else if (_velocityAir > FallVeloActInUp || _velocityAir < -FallVeloActInDw)

            {
                targetDir = new Vector3(0f, _velocityAir, 0f);
                CC.Move(targetDir);
            }
        }

        public virtual void UpdateTargetDirection()
        {
            
            _turnSpeedMultiplier = 1f;
            Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);

            //get the right-facing direction of the referenceTransform
            Vector3 right = Camera.main.transform.TransformDirection(Vector3.right);

            // determine the direction the player will face based on input and the referenceTransform's right and forward directions
            _targetDirection = InputValueHandle.AxisFinalInput.x * right + InputValueHandle.AxisFinalInput.y * forward;


            if (_targetDirection.magnitude > 1f)
            {
                _targetDirection = Vector3.ClampMagnitude(_targetDirection, 1.0f);
            }
        }

        float curY;
        public void SetVelocityAirJump(float vel)
        {
            curY = transform.position.y;
            _velocityAir = vel * JumpGraph.Evaluate(0);
            isInJump = true;
        }
        public void SetVelocityAir(float vel)
        {
            _velocityAir = vel;
        }

        public float GetVelocityAir()
        {
            return _velocityAir;
        }

        public float GetCurSpeed()
        {
            return _curSpeed;
        }

        public void SetCurSpeed(float curSpeed)
        {
            _curSpeed = curSpeed < 0 ? 0 : curSpeed > CharacterSpeedMAX ? CharacterSpeedMAX : curSpeed;
        }

    }
}