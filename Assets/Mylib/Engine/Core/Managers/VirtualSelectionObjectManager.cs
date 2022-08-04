using System.Collections.Generic;
using UnityEngine;

namespace Com.GNL.URP_MyLib
{
    public class VirtualSelectionObjectManager : LibSingletonManager<VirtualSelectionObjectManager>
    {
        private void Awake()
        {
            LibFormulation.AwakeSingletonObj(this.gameObject);
        }

        private void Update()
        {
            if (!VirtualInputManager.Instance.InputAttr.AimTarget) 
            {
                ClearObjects();
            }
        }

        public enum SelectedObjectName
        {
            ENEMY,
        }
        private readonly IDictionary<byte, GameObject> _selectionObjectDictionary;
        protected internal VirtualSelectionObjectManager()
        {
            _selectionObjectDictionary = new Dictionary<byte, GameObject>();
        }



        public virtual void AddObject(byte key, GameObject value)
        {
            lock (_selectionObjectDictionary)
            {
                if (!_selectionObjectDictionary.ContainsKey(key))
                    _selectionObjectDictionary[key] = value;
            }
        }
        public virtual GameObject GetObject(byte key)
        {
            lock (_selectionObjectDictionary)
            {
                if (_selectionObjectDictionary.TryGetValue(key, out GameObject value))
                {
                    return value;
                }

                return null;
            }
        }

        public virtual bool AnyObject(byte key)
        {
            lock (_selectionObjectDictionary)
            {
                if (_selectionObjectDictionary.ContainsKey(key))
                {
                    return true;
                }
                return false;
            }
        }

        public virtual void ClearObjects()
        {
            lock (_selectionObjectDictionary)
            {
                _selectionObjectDictionary.Clear();
            }
        }
        public virtual bool RemoveObject(byte key)
        {
            lock (_selectionObjectDictionary)
            {
                return _selectionObjectDictionary.Remove(key);
            }
        }

        public virtual int GetCountObjects()
        {
            lock (_selectionObjectDictionary)
            {
                return _selectionObjectDictionary.Count;
            }
        }
    }
}