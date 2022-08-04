using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

namespace Com.GNL.URP_MyLib
{
    public abstract class BaseSubstate
    {

        [Tooltip("Just Disbale update")]
        public bool IsUpdate;

        //[LibReadOnly]
        //[HideInInspector]
        //public LibMasterScenesController ScnController;
        [LibReadOnly]
        [HideInInspector]
        public BaseState ClassOfMainState;


        [LibReadOnly]
        [HideInInspector]
        public GameObject SubState;
        [LibReadOnly]
        [HideInInspector]
        public GameObject CanvasScreen;


        [Header("Listed UI")]
        [LibReadOnly]
        [HideInInspector]
        public GameObject ListUIAndroid;
        [LibReadOnly]
        [HideInInspector]
        public GameObject ListUIWindows;
        [LibReadOnly]
        [HideInInspector]
        public GameObject ListUIAdditional;
        [LibReadOnly]
        [HideInInspector]
        public GameObject ListUiUsed;

        [HideInInspector]
        public bool IsUiUsedActive = false;
        private bool isFindAll = false;
        private string NameSubstateCanvasScreen;
        private bool IsUpdateDefault = false;

        // implement this interface for state
        #region === interface IStates ===
        // when already change to the scene that any this class, there is time to do something just for that time
        // one time after load the scene
        public abstract void MySttStart();
        public abstract bool MySttFind();

        // when in one any many state butthe state curent not active, and you one to do something when state enable, put on this function
        // one time after use this state
        public abstract void MySttEnable(bool isFindAll);

        public abstract void MySttUpdate();
        public abstract void MySttDisable();

        public abstract void MySttExit();

        #endregion === interface IStates ===

        #region === interface ISerializeMBState ===


        public void SetIsUpdate(bool update)
        {
            IsUpdate = update;
        }


        public void SerializeState(BaseState classOfMainState, string NameSubstateCanvas)
        {
            IsUpdateDefault = IsUpdate;
            ClassOfMainState = classOfMainState.ClassOfMainState;
            //ScnController = classOfMainState.ScnController;
            NameSubstateCanvasScreen = NameSubstateCanvas;
            SerializeStart(false);
        }

        //public LibMasterScenesController GetControllerState()
        //{
        //    return ScnController;
        //}


        public BaseState GetMainState()
        {
            return ClassOfMainState;
        }

        public void SerializeStart()
        {
            SubState = LibFormulation.FindObjectByTagThenName(LibUtilities.TAG.SUBSTATE.ToString(), NameSubstateCanvasScreen);
            //CanvasScreen = Data_StateGameplay.CanvasScreen;
            if (SubState != null)
            {
                CanvasScreen = SubState.GetComponent<LibMasterCanvasController>().CanvasScreen;
                LibFormulation.CanvasInitialitation(SubState, ref CanvasScreen, ref ListUiUsed, ref ListUIWindows, ref ListUIAndroid, ref ListUIAdditional, true);
            }
            MySttStart();
        }


        public void SerializeStart(bool showingUIUsed)
        {
            SubState = LibFormulation.FindObjectByTagThenName(LibUtilities.TAG.SUBSTATE.ToString(), NameSubstateCanvasScreen);
            //CanvasScreen = Data_StateGameplay.CanvasScreen;
            if (SubState != null)
            {
                CanvasScreen = SubState.GetComponent<LibMasterCanvasController>().CanvasScreen;
                LibFormulation.CanvasInitialitation(SubState, ref CanvasScreen, ref ListUiUsed, ref ListUIWindows, ref ListUIAndroid, ref ListUIAdditional, showingUIUsed);
            }
            MySttStart();
        }

        public void SerializeEnable()
        {
            IsUiUsedActive = true;
            MySttEnable(isFindAll);
            if (isFindAll)
            {
                LibFormulation.UIAdditionalActive(ref ListUIAdditional, true);
                ListUiUsed.SetActive(IsUiUsedActive);
            }

            IsUpdate = true;

        }

        public void SerializeUpdate()
        {
            if (IsUpdate)
            {
                MySttUpdate();
            }
        }
        public bool SerializeFind()
        {
            if (!MySttFind() || ListUIAndroid == null || ListUIWindows == null || ListUIAdditional == null)
            {
                Debug.Log("cekcekcek Substate :" + NameSubstateCanvasScreen+ 
                    ", ListUIAndroid:" + (ListUIAndroid == null) +
                    ", ListUIWindows:" + (ListUIWindows == null) +
                    ", ListUIAdditional:" + (ListUIAdditional == null)
                    );
                isFindAll = false;
            }
            else
                isFindAll = true;
            return isFindAll;
        }
        public void SerializeDisable()
        {
            IsUiUsedActive = false;
            MySttDisable();
            LibFormulation.UIAdditionalActive(ref ListUIAdditional, true);
            ListUiUsed.SetActive(IsUiUsedActive);
            IsUpdate = false;
        }

        public void SerializeExit()
        {
            MySttExit();
            IsUpdate = IsUpdateDefault;
            //OnDestroy();
        }
        protected void OnDestroy()
        {
            //Debug.Log("cekcek OnDestroy in MonoBehaviourMyBase");
            foreach (FieldInfo field in GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {


                Type fieldType = field.FieldType;
                if (typeof(LibMasterSceneConstruct[]) == (fieldType))
                {
                    continue;
                }


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

        #endregion === interface ISerializeMBState ===
    }
}
