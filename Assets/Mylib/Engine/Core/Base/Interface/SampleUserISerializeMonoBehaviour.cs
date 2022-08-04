
using System;
using System.Collections;

namespace Com.GNL.URP_MyLib
{
    [Serializable]
    public class SampleUserISerializeMonoBehaviour : ISerializeMonoBehaviour
    {
        private bool IsEnable;

        public void Serialize(UnityEngine.Object classOfSerialize)
        {
            if(IsEnable)
            {
                //_PC is reference from PlayerController, wich PlayerController is inheritance of monobehaviour, and  PlayerController is class that use for to attach this class
                //this._PC = (PlayerController) classOfSerialize; 
            }
        }


        public void SerializeReset()
        {
            if (IsEnable)
            {
            }
        }
        public void SerializeOnValidate()
        {
            if (IsEnable)
            {
            }
        }


        public void SerializeStart()
        {
            if (IsEnable)
            {
                
            }
        }

        public void SerializeUpdate()
        {
            if (IsEnable)
            {
               
            }
        }

        public void SerializeAwake()
        {
            if (IsEnable)
            {

            }
        }

        public void SerializeFixedUpdate()
        {
            if (IsEnable)
            {

            }
        }

        public void SerializeLateUpdate()
        {
            if (IsEnable)
            {
                
            }
        }

        

        public void SerializeOnDisable()
        {
            if (IsEnable)
            {
            }
        }


        


        public void SerializeStartCoroutine(IEnumerator enumerator)
        {
            if (IsEnable)
            {

            }
        }

        public void SerializeOnDrawGizmosSelected()
        {
            if (IsEnable)
            {

            }
        }
    }
}
