using System.Collections.Generic;
using UnityEngine;

namespace Com.GNLTest.Test1
{
    public class VirtualInputDelayManager : Singleton<VirtualInputDelayManager>
    {
        private void Awake()
        {
           
        }

        private readonly IDictionary<string, float> _btnDelayDictionary;
        private readonly List<string> _listBtnDelay = new List<string>();
        private void Update()
        {
            float timestamp = Time.deltaTime;
            for(int i=0;i< _btnDelayDictionary.Count;i++)
            {
                _btnDelayDictionary.TryGetValue(_listBtnDelay[i], out float time);
                if (time - timestamp < 0f)
                {
                    _btnDelayDictionary.Remove(_listBtnDelay[i]);
                    _listBtnDelay.RemoveAt(i);
                }
                else
                    _btnDelayDictionary[_listBtnDelay[i]] = time - timestamp;
            }
        }
        protected internal VirtualInputDelayManager()
        {
            _btnDelayDictionary = new Dictionary<string, float>();
        }


        public float TimeDelay = 0.5f;

        public virtual void AddBtn(string key, float value)
        {
            lock (_btnDelayDictionary)
            {
                _btnDelayDictionary[key] = value;
                _listBtnDelay.Add(key);
            }
        }

        public virtual void SetBtn(string key, float value)
        {
            lock (_btnDelayDictionary)
            {
                _btnDelayDictionary[key] = value;
            }
        }
        public virtual float GetBtn(string key)
        {
            lock (_btnDelayDictionary)
            {
                if (_btnDelayDictionary.TryGetValue(key, out float value))
                {
                    return value;
                }

                return 0;
            }
        }

        public virtual bool AnyBtn(string key)
        {
            lock (_btnDelayDictionary)
            {
                if (_btnDelayDictionary.ContainsKey(key))
                {
                    return true;
                }
                return false;
            }
        }

        public virtual void ClearBtn()
        {
            lock (_btnDelayDictionary)
            {
                _btnDelayDictionary.Clear();
            }
        }
        public virtual bool RemoveBtn(string key)
        {
            lock (_btnDelayDictionary)
            {
                return _btnDelayDictionary.Remove(key);
            }
        }

        public virtual int GetCountBtn()
        {
            lock (_btnDelayDictionary)
            {
                return _btnDelayDictionary.Count;
            }
        }
    }
}