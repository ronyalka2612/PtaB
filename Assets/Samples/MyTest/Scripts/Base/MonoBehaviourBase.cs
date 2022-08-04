using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Com.GNLTest.Test1
{
    public class MonoBehaviourBase : MonoBehaviour
    {
        // Start is called before the first frame update
        protected void OnDestroy()
        {
            //Debug.Log("cekcek OnDestroy in MonoBehaviourMyBase");
            foreach (FieldInfo field in GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                Type fieldType = field.FieldType;

                if (typeof(IList).IsAssignableFrom(fieldType))
                {
                    IList list = field.GetValue(this) as IList;
                    if (list != null)
                    {
                        list.Clear();
                    }
                }

                if (typeof(IDictionary).IsAssignableFrom(fieldType))
                {
                    IDictionary dictionary = field.GetValue(this) as IDictionary;
                    if (dictionary != null)
                    {
                        dictionary.Clear();
                    }
                }

                if (!fieldType.IsPrimitive)
                {
                    field.SetValue(this, null);
                }
            }

            //Destroy(this);
        }
    }
}