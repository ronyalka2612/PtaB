
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Com.GNL.URP_MyLib
{
    public static class LibUtilitiesEditor
    {


        public static bool IsSerializable = false;
        public static bool IsSkipingChildAttribute = false;
        public static bool IsSkipingSaved = false;
        public static bool IsSkiping = false;

        public static string SerializeEnd = "SerializableEnd";
        public static string SkipingChildAttributeEnd = "SkipingChildAttributeEnd";
        public static string SkipSavedEnd = "SkipSavedEnd";
        public static string SkipSavedPast = "SkipSaved";
        public static string SkipEnd = "SkipEnd";
        public static string TabEnd = "TabEnd";
        public static string CurString = "Tab";


        public static bool IsConditional()
        {
            return !IsSerializable && !IsSkipingChildAttribute && !IsSkiping;
        }

        public static SIZE_OF_TYPE GetCountByProperty(SerializedProperty property)
        {
            try
            {
                if (property.propertyType == SerializedPropertyType.Vector3)
                {
                    //Debug.Log("name kamu sapa:" + property.type);

                    //int objectSize = System.Runtime.InteropServices.Marshal.SizeOf(property);

                    //Debug.Log("objectSize :" + objectSize);

                    return SIZE_OF_TYPE.Vector3;
                }
                else if (property.propertyType == SerializedPropertyType.Vector2)
                {

                    return SIZE_OF_TYPE.Vector2;
                }
                else if (property.propertyType == SerializedPropertyType.Generic)
                {
                    //Debug.Log("name kamu sapa:" + property.type);
                    //Debug.Log("serializedProperty.CountInProperty :" + property.CountInProperty());
                    //Debug.Log("serializedProperty.CountRemaining :" + property.CountRemaining());


                    if (property.type == SIZE_OF_GENERIC_TYPE.MinMaxFloat.ToString())
                    {
                        return SIZE_OF_TYPE.Value2;
                    }
                }
            }
            catch 
            {
                Debug.Log("cek Editor: property can not be checked");
            }
            return SIZE_OF_TYPE.Default;
        }

        public enum SIZE_OF_TYPE
        {
            Default = 0,
            Vector3 = 3,
            Vector2 = 2,
            Value1 = 2,
            Value2 = 2,
            Value3 = 2,
        }

        public enum SIZE_OF_GENERIC_TYPE
        {
            MinMaxFloat = 2,
        }
    }
}
