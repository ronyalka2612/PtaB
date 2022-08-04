using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


namespace Com.GNL.URP_MyLib
{
    public class LibMasterStatesController : LibSingletonController<LibMasterStatesController>
    {
        //[HideInInspector] public int CurrentTab;
        //public string ToolbarTab;

        #region === Declare Public vairiable ===
        //public string Tab1;
        //public string SkipingChildAttributeEnd;

        //public string SkipSaved1;

        //public StateStarter SttStarter;
        //public string SerializableEnd2;
        //public StateMainmenu SttMainmenu;
        //public string SerializableEnd3;
        //public StateGameplay SttGameplay;
        //public string SerializableEnd4;
        //public StateGP_PinBall SttMAIN_GP;
        //public string SerializableEnd5;

        //public string SkipSavedEnd1;


        //[Space(10)]
        //[Header("State Setting")]
        //public States State;
        //public string TabEnd;
        #endregion === Declare Public vairiable ===

        //public List<BaseState> ListState = new List<BaseState>();
        //public List<BaseSubstate> ListSubState = new List<BaseSubstate>();

        //public BaseState CurState;
        //public IDictionary<LibEdStateUtilities.GameSubStates, BaseSubstate> CurSubStateDictionary = new Dictionary<LibEdStateUtilities.GameSubStates, BaseSubstate>();
        //private LibEdStateUtilities.GameSubStates[] indexSubState;
        //public virtual void AddSubState(LibEdStateUtilities.GameSubStates key, BaseSubstate value)
        //{
        //    lock (CurSubStateDictionary)
        //    {
                
        //        if (!CurSubStateDictionary.ContainsKey(key))
        //        {
        //            Indexing(key);
        //            CurSubStateDictionary[key] = value;
        //        }
        //    }
        //}

        //private void Indexing(LibEdStateUtilities.GameSubStates value)
        //{
        //    if (indexSubState == null)
        //    {
        //        indexSubState = new LibEdStateUtilities.GameSubStates[1];
        //    }
        //    else if (indexSubState.Length > 0)
        //    {
        //        LibEdStateUtilities.GameSubStates[] temp = indexSubState;
        //        indexSubState = new LibEdStateUtilities.GameSubStates[temp.Length+1];

        //        for (int i =0;i< temp.Length; i++)
        //        {
        //            indexSubState[i] = temp[i];
        //        }
        //    }

        //    indexSubState[indexSubState.Length-1] = value;
        //}

        //public virtual BaseSubstate GetSubState(LibEdStateUtilities.GameSubStates key)
        //{
        //    lock (CurSubStateDictionary)
        //    {
        //        if (CurSubStateDictionary.TryGetValue(key, out BaseSubstate value))
        //        {
        //            return value;
        //        }

        //        return null;
        //    }
        //}

        //public virtual bool AnySubState(LibEdStateUtilities.GameSubStates key)
        //{
        //    lock (CurSubStateDictionary)
        //    {
        //        if (CurSubStateDictionary.ContainsKey(key))
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //}

        //public virtual void ClearSubState()
        //{
        //    lock (CurSubStateDictionary)
        //    {
        //        CurSubStateDictionary.Clear();
        //    }
        //}

        //public virtual void RemoveSubStateNoNeed()
        //{
        //    lock (CurSubStateDictionary)
        //    {
        //        for (int i = 0; i < CurSubStateDictionary.Count; i++)
        //        {
        //            CurSubStateDictionary.Remove(indexSubState[i]);
        //        }
        //        if(indexSubState != null)
        //        indexSubState = null;
        //    }
        //}

        //public virtual bool RemoveSubState(LibEdStateUtilities.GameSubStates key)
        //{
        //    lock (CurSubStateDictionary)
        //    {
        //        return CurSubStateDictionary.Remove(key);
        //    }
        //}

        //public virtual int GetCountSubState()
        //{
        //    lock (CurSubStateDictionary)
        //    {
        //        return CurSubStateDictionary.Count;
        //    }
        //}


        private void OnValidate()
        {
            
        }

        private void Awake()
        {

            //if (_instance == null)
            //{
            //    _instance = this;
            //}

        }

        private void Start()
        {
            StateFunc.ClearState();
            LibSerialize();

        }

        public virtual void LibSerialize()
        {
            
        }

        
        public virtual void StateChanging()
        {
            

        }

        public virtual void SubStateChanging()
        {

        }



        public virtual void ExitSerialize(LibEdStateUtilities.GameStates lastState)
        {
            
        }


        private void Update()
        {
            StateFunc.StateUpdate(Update_State, StateFunc.StateChanging(StateChanging, SubStateChanging, CheckingAllfind, ExitSerialize));
        }

        public virtual void Update_State()
        {

        }
        private void FixedUpdate()
        {
            StateFunc.StateUpdate(Update_StateFU, StateFunc.StateChanging(StateChanging, SubStateChanging, CheckingAllfind, ExitSerialize));
        }

        public virtual void Update_StateFU()
        {

        }

        private void LateUpdate()
        {
            StateFunc.StateUpdate(Update_StateLU, StateFunc.StateChanging(StateChanging, SubStateChanging, CheckingAllfind, ExitSerialize));
        }

        public virtual void Update_StateLU()
        {

        }


    }
}