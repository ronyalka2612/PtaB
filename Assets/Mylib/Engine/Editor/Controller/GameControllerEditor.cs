using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Com.GNL.URP_MyLib;

[CustomEditor(typeof(LibMasterGameController))]
public class GameControllerEditor : Editor
{

    private LibMasterGameController myTarget;
    private enum Tabulation
    {
        Game_Setting,
        Game_Mode_Setting,
        Debug_Setting,
        Reference,
        Count
    }
    private enum ListSkip
    {
        //Skip1, //StateStarter
        //Skip2, //StateMainmenu
        Count
    }

    private enum ListSkipBUtSaved
    {
        //SkipSaved1, //StateStarter
        //SkipSaved2, //StateStarter
        Count
    }

    private enum ListSerializable
    {
        //SttStarter, //StateStarter
        //SttMainmenu, //StateMainmenu
        //SttGameplay, //SttGameplay
        Count
    }

    private enum MyList
    {
        //SttStarter, //StateStarter
        //SttMainmenu, //StateMainmenu
        //SttGameplay,
        Count
    }

    private enum ListSkipingChildAttribut
    {
        _curSubState, // List<LibEdStateUtilities.GameSubStates>
        Count
    }

    private string[] TabulationsName;
    private SerializedObject soTarget;
    private List<SerializedProperty>[] Tab;
    private List<SerializedProperty> ListSerialize;
    private SerializedProperty Prop;

    //void OnEnable()
    //{
    //    Debug.Log("GameController OnEnable");
    //    myTarget = (LibMasterGameController)target;

    //    TabulationsName = new string[(int)Tabulation.Count];
    //    Tab = new List<SerializedProperty>[(int)Tabulation.Count];
    //    for (int i = 0; i < (int)Tabulation.Count; i++)
    //    {
    //        TabulationsName[i] = ((Tabulation)i).ToString();
    //        Tab[i] = new List<SerializedProperty>();
    //    }

    //    ListSerialize = new List<SerializedProperty>();

    //    soTarget = new SerializedObject(target);
    //    ListingAttribute();

    //}
    
    //public override void OnInspectorGUI()
    //{
    //    //base.OnInspectorGUI();
    //    soTarget.Update();
    //    EditorGUI.BeginChangeCheck();
    //    myTarget.CurrentTab = GUILayout.Toolbar(myTarget.CurrentTab, TabulationsName);
    //    //myTarget.CurrentTab = 0;
    //    if (Tab != null)
    //    {
    //        for (int j = 0; j < Tab.Length; j++)
    //        {
    //            if (myTarget.CurrentTab == j)
    //            {
    //                if (Tab[j] != null)
    //                {

    //                    foreach (SerializedProperty item in Tab[j])
    //                    {
    //                        EditorGUILayout.PropertyField(item);
    //                    }
    //                    break;
    //                }
    //            }
    //        }
    //    }
    //    //int legnthEnum = Enum.GetNames(typeof(StatesController.States)).Length;
    //    //for (int j = 0; j < legnthEnum; j++)
    //    //{
    //    //    if ((int)myTarget.State == j)
    //    //    {
    //    //        EditorGUILayout.PropertyField(ListSerialize[j]);
    //    //    }
    //    //}
    //    if (EditorGUI.EndChangeCheck())
    //    {
    //        soTarget.ApplyModifiedProperties();
    //        //GUI.FocusControl(null);
    //    }


    //}
    //private void OnValidate()
    //{
    //    Debug.Log("StatesController OnValidate");
    //    ListingAttribute();
    //}
    //bool isSerializable = false;
    //bool isSkipingChildAttribute = false;
    //bool isSkipingSaved = false;
    //bool isSkiping = false;

    private void ListingAttribute()
    {
        Prop = serializedObject.GetIterator();
        if (Prop.NextVisible(true))
        {
            int x = -1;
            int tab = 1;
            LibUtilitiesEditor.IsSerializable = false;
            LibUtilitiesEditor.IsSkipingChildAttribute = false;
            LibUtilitiesEditor.IsSkiping = false;
            //string serializeEnd = "SerializableEnd";
            //string skipingChildAttributeEnd = "SkipingChildAttributeEnd";
            //string skipSavedEnd = "SkipSavedEnd";
            //string skipSavedPast = "SkipSaved";
            //string skipEnd = "SkipEnd";
            //string tabEnd = "TabEnd";
            //string curString = "Tab";
            int sizeByType = 0;
            //Debug.Log(" serializeEnd length =" + serializeEnd.Length);
            do
            {
                if (sizeByType > 0)
                {
                    sizeByType--;
                    continue;
                }
                if (Prop.name == LibUtilitiesEditor.TabEnd)
                {
                    break;
                }

                if (Prop.name == ("Tab" + tab))
                {
                    x++;
                    tab++;
                    LibUtilitiesEditor.CurString = Prop.name;
                }
                else if (LibUtilitiesEditor.CurString == "Tab" + (tab - 1) && LibUtilitiesEditor.IsConditional() && sizeByType==0)
                {
                    

                    SerializedProperty property = soTarget.FindProperty(Prop.name).Copy();
                    sizeByType = (int)LibUtilitiesEditor.GetCountByProperty(property);

                    for (int y = 0; y < (int)ListSkip.Count; y++)
                    {
                        if (Prop.name == ((ListSkip)y).ToString())
                        {
                            //Debug.Log("cek isSerializable");
                            LibUtilitiesEditor.IsSkiping = true;
                        }
                    }

                    for (int y = 0; y < (int)ListSkipBUtSaved.Count; y++)
                    {
                        if (Prop.name == ((ListSkipBUtSaved)y).ToString())
                        {
                            //Debug.Log("cek isSkipingSaved true");
                            LibUtilitiesEditor.IsSkipingSaved = true;
                             continue;
                        }
                    }


                    if (Prop.name.Contains(LibUtilitiesEditor.SkipSavedEnd))
                    {
                        //Debug.Log("cek isSkipingSaved false");
                        LibUtilitiesEditor.IsSkipingSaved = false;
                        continue;
                    }
                    else if (Prop.name.Contains(LibUtilitiesEditor.SkipSavedPast))
                    {
                        continue;
                    }


                    if (LibUtilitiesEditor.IsSkipingSaved)
                    {
                        ListSerialize.Add(property);
                    }
                    else if ((!LibUtilitiesEditor.IsSkiping && !LibUtilitiesEditor.IsSkipingSaved))
                    {
                        Tab[x].Add(property);
                    }

                    for (int y = 0; y < (int)ListSerializable.Count; y++)
                    {
                        if (Prop.name == ((ListSerializable)y).ToString())
                        {
                            //Debug.Log("cek isSerializable");
                            LibUtilitiesEditor.IsSerializable = true;
                        }
                    }
                    for (int y = 0; y < (int)ListSkipingChildAttribut.Count; y++)
                    {
                        if (Prop.name == ((ListSkipingChildAttribut)y).ToString())
                        {
                            //Debug.Log("cek isSerializable");
                            LibUtilitiesEditor.IsSkipingChildAttribute = true;
                        }
                    }
                    
                }
                else if (Prop.name.Contains(LibUtilitiesEditor.SerializeEnd))
                {
                    LibUtilitiesEditor.IsSerializable = false;
                }
                else if (Prop.name.Contains(LibUtilitiesEditor.SkipingChildAttributeEnd))
                {
                    LibUtilitiesEditor.IsSkipingChildAttribute = false;
                }
                else if (Prop.name.Contains(LibUtilitiesEditor.SkipEnd))
                {
                    LibUtilitiesEditor.IsSkiping = false;
                }
            }
            while (Prop.NextVisible(true));

            //int index = 0;
            //foreach (SerializedProperty temp in ListSerialize)
            //{
            //    Debug.Log("cekcek ListSerialize [" + index + "] :" + temp.name);
            //    index++;
            //}
        }
    }

    //private bool IsConditional()
    //{
    //    return !isSerializable && !isSkipingChildAttribute && !isSkiping;
    //}

    //private SIZE_OF_TYPE GetCountByProperty(SerializedProperty property)
    //{
    //    if (property.propertyType == SerializedPropertyType.Vector3)
    //    {
    //        return SIZE_OF_TYPE.Vector3;
    //    }
    //    return SIZE_OF_TYPE.Default;
    //}

    //enum SIZE_OF_TYPE { 
    //    Default = 0,
    //    Vector3 = 3,
    //}
}
