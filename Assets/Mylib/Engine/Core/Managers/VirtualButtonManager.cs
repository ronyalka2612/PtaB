using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.GNL.URP_MyLib
{
    public class VirtualButtonManager : LibSingletonManager<VirtualButtonManager>
    {
        public LibEdBtnAttr BtnAttr;
        private void Awake()
        {
            BtnAttr = new LibEdBtnAttr();
            LibFormulation.AwakeSingletonObj(this.gameObject);
        }
        private void Update()
        {
    
        }
        private readonly IDictionary<MY_BTN_CODE, PointerEventData> _btnAndroidDictionary;
        protected internal VirtualButtonManager()
        {
            _btnAndroidDictionary = new Dictionary<MY_BTN_CODE, PointerEventData>();
        }


        public virtual void AddBtn(MY_BTN_CODE key, PointerEventData value)
        {
            lock (_btnAndroidDictionary)
            {
                if (!_btnAndroidDictionary.ContainsKey(key))
                    _btnAndroidDictionary[key] = value;
            }
        }
        public virtual PointerEventData GetBtn(MY_BTN_CODE key)
        {
            lock (_btnAndroidDictionary)
            {
                if (_btnAndroidDictionary.TryGetValue(key, out PointerEventData value))
                {
                    return value;
                }

                return null;
            }
        }

        public virtual bool AnyBtn(MY_BTN_CODE key)
        {
            lock (_btnAndroidDictionary)
            {
                if (_btnAndroidDictionary.ContainsKey(key))
                {
                    return true;
                }
                return false;
            }
        }

        public virtual void ClearBtn()
        {
            lock (_btnAndroidDictionary)
            {
                _btnAndroidDictionary.Clear();
            }
        }
        public virtual bool RemoveBtn(MY_BTN_CODE key)
        {
            lock (_btnAndroidDictionary)
            {
                return _btnAndroidDictionary.Remove(key);
            }
        }

        public virtual int GetCountBtn()
        {
            lock (_btnAndroidDictionary)
            {
                return _btnAndroidDictionary.Count;
            }
        }
    }
}