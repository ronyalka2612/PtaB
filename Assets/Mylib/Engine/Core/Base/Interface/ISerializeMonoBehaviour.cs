
using System.Collections;
using UnityEngine;

namespace Com.GNL.URP_MyLib
{
    public interface ISerializeMonoBehaviour
    {
        void SerializeReset();
        void SerializeOnValidate();
        void SerializeAwake();
        void Serialize(Object classOfSerialize);
        void SerializeStart();
        void SerializeUpdate();
        void SerializeFixedUpdate();
        void SerializeLateUpdate();
        void SerializeStartCoroutine(IEnumerator enumerator);

        void SerializeOnDisable();

        void SerializeOnDrawGizmosSelected();
    }

}