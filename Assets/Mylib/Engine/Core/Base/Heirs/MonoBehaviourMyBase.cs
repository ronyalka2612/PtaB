using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Internal;

namespace Com.GNL.URP_MyLib
{
    public class MonoBehaviourMyBase : MonoBehaviour
    {
        // Start is called before the first frame update
        public StateFunction StateFunc;
        //public delegate void FuncStateChanging(LibEdStateUtilities.GameSubStates curState);
        public MonoBehaviourMyBase()
        {
            StateFunc = new StateFunction(this);
        }
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
        private void OnEnable()
        {
            CheckingAllfind();
        }

        public virtual void CheckingAllfind()
        {
            
        }

        public void CheckingEnable(BaseSubstate bState)
        {
            if (bState.IsUpdate)
            {
                bState.SerializeEnable();
            }
            else
            {
                bState.SerializeDisable();
            }
        }

        public void CheckingEnable(BaseState bState)
        {
            if (bState.IsUpdate)
            {
                bState.SerializeEnable();
            }
            else
            {
                bState.SerializeDisable();
            }
        }
    }

    
    public class StateFunction
    {

        public delegate void FuncStateChanging();

        public delegate void FuncCheckingAllfind();

        public delegate void FuncExitSerialize(LibEdStateUtilities.GameStates lastState);

        public delegate void FunSubStateChanging();

        public delegate void FunSubStateUpdate();

        public delegate void FunStateUpdate();

        public delegate void FuncSubState(LibEdStateUtilities.GameSubStates curSubState);


        private bool IsFindAll = false;
        private LibEdStateUtilities.GameStates CurState = LibEdStateUtilities.GameStates.NO_STATE;
        private List<LibEdStateUtilities.GameSubStates> ListCurSubState = new List<LibEdStateUtilities.GameSubStates>();

        public MonoBehaviourMyBase _based;
        private bool isUseFunc4;
        public StateFunction(MonoBehaviourMyBase based)
        {
            isUseFunc4 = true ;
            _based = based;
        }
        public void SetFindAll(bool isFindAll)
        {
            IsFindAll = isFindAll;
        }

        public bool GetFindAll()
        {
            return IsFindAll;
        }

        public LibEdStateUtilities.GameStates CheckCurState()
        {
            return CurState;
        }

        public List<LibEdStateUtilities.GameSubStates> GetSubStates()
        {
            return ListCurSubState;
        }
        public List<LibEdStateUtilities.GameSubStates> SubState(FuncSubState myFunc, bool isChangeState = false)
        {
            //its mean do change scene
            if (isChangeState)
            {
                int i = 0;
                bool isSame = false;
                for (int j = 0; j < VirtualStateManager.Instance.CurSubStateActive.Count; j++)
                {
                    foreach (LibEdStateUtilities.GameSubStates substate in VirtualStateManager.Instance.CurSubStateActive)
                    {
                        if ( i < ListCurSubState.Count)
                        {
                            if (ListCurSubState[i] != substate)
                            {
                                isSame = false;
                            }
                            else if (ListCurSubState[i] == substate)
                            {
                                isSame = true;
                                break;
                            }
                        }
                        else
                        {
                            // jika jumlah tidak sama dengan VSM, maka is same di set true agar tidak di cek dibawah, dan add ke list current dan panggil func
                            isSame = true;
                            ListCurSubState.Add(VirtualStateManager.Instance.CurSubStateActive[i]);
                            //if (IsFindAll)
                                myFunc(VirtualStateManager.Instance.CurSubStateActive[i]);
                            break;
                        }
                        
                    }
                    // beda semua, maka di set
                    if (!isSame)
                    {
                        if (i < ListCurSubState.Count)
                        {
                            ListCurSubState[i] = VirtualStateManager.Instance.CurSubStateActive[i];
                        }
                        else
                        {
                            ListCurSubState.Add(VirtualStateManager.Instance.CurSubStateActive[i]);
                        }
                        //if (IsFindAll)
                            myFunc(VirtualStateManager.Instance.CurSubStateActive[i]);
                    }
                    i++;
                    
                }
                // kalau curent lebih maka tinggal dibuat sama
                if(ListCurSubState.Count > VirtualStateManager.Instance.CurSubStateActive.Count)
                {
                    ListCurSubState = VirtualStateManager.Instance.CurSubStateActive;
                }
                return ListCurSubState.Distinct().ToList();
            }
            //when false, its mean you caallid whn u need update of substate in controller (i mean that parenting with this class)
            else
            {
                for (int j = 0; j < VirtualStateManager.Instance.CurSubStateActive.Count; j++)
                {
                    //if (IsFindAll )
                    //{ 
                        //if(LibMasterStatesController.Instance.CurSubStateDictionary.Count>0 && LibMasterStatesController.Instance.GetSubState(VirtualStateManager.Instance.CurSubStateActive[j]).IsUpdate)
                        myFunc(VirtualStateManager.Instance.CurSubStateActive[j]);
                    //}
                }
                return ListCurSubState;
            }
        }


        public bool StateChanging(FuncStateChanging myFunc, FunSubStateChanging myFunc2, FuncCheckingAllfind myFunc3, FuncExitSerialize myFunc4)
        {
            bool isAlreadySame = true;
            if (CurState != VirtualStateManager.Instance.CurState)
            {
                IsFindAll = false;
                isAlreadySame = false;
                if(isUseFunc4)
                    myFunc4(CurState);
                ClearState();
                CurState = VirtualStateManager.Instance.CurState;
                myFunc();
            }
            myFunc2();

            if (!isAlreadySame)
            {
                myFunc3();
                if (!IsFindAll)
                {
                    ClearState();
                }
            }

            return IsFindAll;
        }

        public bool StateChanging(FuncStateChanging myFunc, FunSubStateChanging myFunc2, FuncCheckingAllfind myFunc3)
        {
            isUseFunc4 = false;
            return StateChanging(myFunc, myFunc2, myFunc3, this.MyFunc4);
        }


        public void ClearState()
        {
            CurState = LibEdStateUtilities.GameStates.NO_STATE;
            ListCurSubState.Clear();
            ListCurSubState.Add(LibEdStateUtilities.GameSubStates.NO_SUBSTATE);
        }


        public void StateUpdate(FunStateUpdate myFunc, bool isFindAll)
        {
            if (isFindAll)
            {
                myFunc();
            }
        }

        private FuncExitSerialize MyFunc4 => null;

    }


    //public class MonoBehaviourMyBaseTest : MonoBehaviour
    //{
    //    // Start is called before the first frame update


    //    protected void OnDestroy()
    //    {


    //        Debug.Log("cekcek OnDestroy in MonoBehaviourMyBaseTest");

    //        foreach (FieldInfo field in GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
    //        {
    //            Type fieldType = field.FieldType;

    //            if (typeof(IList).IsAssignableFrom(fieldType))
    //            {
    //                IList list = field.GetValue(this) as IList;
    //                if (list != null)
    //                {
    //                    list.Clear();
    //                }
    //            }

    //            if (typeof(IDictionary).IsAssignableFrom(fieldType))
    //            {
    //                IDictionary dictionary = field.GetValue(this) as IDictionary;
    //                if (dictionary != null)
    //                {
    //                    dictionary.Clear();
    //                }
    //            }

    //            if (!fieldType.IsPrimitive)
    //            {
    //                field.SetValue(this, null);
    //            }
    //        }

    //        Destroy(this);

    //    }

    //}
}
