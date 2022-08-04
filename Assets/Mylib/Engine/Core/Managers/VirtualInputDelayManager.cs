using System.Collections.Generic;
using UnityEngine;

namespace Com.GNL.URP_MyLib
{
    public class VirtualInputDelayManager : LibSingletonManager<VirtualInputDelayManager>
    {
        //    private void Awake()
        //    {
        //        Formulation.AwakeSingletonObj(this.gameObject);
        //    }

        //    private readonly IDictionary<byte, float> _btnDelayAndroidDictionary;
        //    private readonly List<byte> _listBtnDelayAndroid = new List<byte>();
        //    private void Update()
        //    {
        //        float timestamp = Time.deltaTime;
        //        for(int i=0;i< _btnDelayAndroidDictionary.Count;i++)
        //        {
        //            _btnDelayAndroidDictionary.TryGetValue(_listBtnDelayAndroid[i], out float time);
        //            if (time - timestamp < 0f)
        //            {
        //                _btnDelayAndroidDictionary.Remove(_listBtnDelayAndroid[i]);
        //                _listBtnDelayAndroid.RemoveAt(i);
        //            }
        //            else
        //                _btnDelayAndroidDictionary[_listBtnDelayAndroid[i]] = time - timestamp;
        //        }
        //    }
        //    protected internal VirtualInputDelayManager()
        //    {
        //        _btnDelayAndroidDictionary = new Dictionary<byte, float>();
        //    }


        //    public float TimeDelay = 0.5f;

        //    public virtual void AddBtn(byte key, float value)
        //    {
        //        lock (_btnDelayAndroidDictionary)
        //        {
        //            _btnDelayAndroidDictionary[key] = value;
        //            _listBtnDelayAndroid.Add(key);
        //            VirtualButtonManager.Instance.RemoveBtn(key);
        //        }
        //    }

        //    public virtual void SetBtn(byte key, float value)
        //    {
        //        lock (_btnDelayAndroidDictionary)
        //        {
        //            _btnDelayAndroidDictionary[key] = value;
        //        }
        //    }
        //    public virtual float GetBtn(byte key)
        //    {
        //        lock (_btnDelayAndroidDictionary)
        //        {
        //            if (_btnDelayAndroidDictionary.TryGetValue(key, out float value))
        //            {
        //                return value;
        //            }

        //            return 0;
        //        }
        //    }

        //    public virtual bool AnyBtn(byte key)
        //    {
        //        lock (_btnDelayAndroidDictionary)
        //        {
        //            if (_btnDelayAndroidDictionary.ContainsKey(key))
        //            {
        //                return true;
        //            }
        //            return false;
        //        }
        //    }

        //    public virtual void ClearBtn()
        //    {
        //        lock (_btnDelayAndroidDictionary)
        //        {
        //            _btnDelayAndroidDictionary.Clear();
        //        }
        //    }
        //    public virtual bool RemoveBtn(byte key)
        //    {
        //        lock (_btnDelayAndroidDictionary)
        //        {
        //            return _btnDelayAndroidDictionary.Remove(key);
        //        }
        //    }

        //    public virtual int GetCountBtn()
        //    {
        //        lock (_btnDelayAndroidDictionary)
        //        {
        //            return _btnDelayAndroidDictionary.Count;
        //        }
        //    }
    }
}