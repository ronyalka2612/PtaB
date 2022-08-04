using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Com.GNL.URP_MyLibProjectTest
{
    //[CustomEditor(typeof(PlayerController))]
    //public class PlayerControllerEditor : Editor
    //{

    //    private PlayerController myTarget;
    //    //private enum Tabulation
    //    //{
    //    //    Movement,
    //    //    Jump,
    //    //    AnimationCorecction,
    //    //    Etcetera,
    //    //    Count
    //    //}

    //    //private enum ListSkip
    //    //{
    //    //    //Skip1, //StateStarter
    //    //    //Skip2, //StateMainmenu
    //    //    Count
    //    //}

    //    //private enum ListSkipBUtSaved
    //    //{
    //    //    //SkipSaved1, //StateStarter
    //    //    //SkipSaved2, //StateStarter
    //    //    Count
    //    //}

    //    //private enum ListSerializable
    //    //{
    //    //    PlayerAnmCrt, //AnimationCorrectionOwn
    //    //    PlayerAnmCrtIKC, //AnimationCorrectionIKConstrain
    //    //    Count
    //    //}

    //    //private enum MyList
    //    //{
    //    //    //SttStarter, //StateStarter
    //    //    //SttMainmenu, //StateMainmenu
    //    //    //SttGameplay,
    //    //    Count
    //    //}

    //    //private enum ListSkipingChildAttribut
    //    //{
    //    //    //_curSubState, // List<LibEdStateUtilities.GameSubStates>
    //    //    Count
    //    //}
    //    private string[] TabulationsName;
    //    private SerializedObject soTarget;
    //    private List<SerializedProperty>[] Tab;
    //    private List<SerializedProperty> ListSerialize;
    //    private SerializedProperty Prop;

    //    //void OnEnable()
    //    //{
    //    //    Debug.Log("PlayerControllerEditor OnEnable");
    //    //    myTarget = (PlayerController)target;

    //    //    TabulationsName = new string[(int)Tabulation.Count];
    //    //    Tab = new List<SerializedProperty>[(int)Tabulation.Count];
    //    //    for (int i = 0; i < (int)Tabulation.Count; i++)
    //    //    {
    //    //        TabulationsName[i] = ((Tabulation)i).ToString();
    //    //        Tab[i] = new List<SerializedProperty>();
    //    //    }

    //    //    ListSerialize = new List<SerializedProperty>();

    //    //    soTarget = new SerializedObject(target);
    //    //    ListingAttribute();
    //    //}

    //    //public override void OnInspectorGUI()
    //    //{

    //    //    soTarget.Update();
    //    //    EditorGUI.BeginChangeCheck();
    //    //    myTarget.CurrentTab = GUILayout.Toolbar(myTarget.CurrentTab, TabulationsName);

    //    //    if (Tab != null)
    //    //    {
    //    //        for (int j = 0; j < Tab.Length; j++)
    //    //        {
    //    //            if (myTarget.CurrentTab == j)
    //    //            {
    //    //                if (Tab[j] != null)
    //    //                {
    //    //                    //Debug.Log("PlayerControllerEditor Tab["+j+"].Count ="+ Tab[j].Count);
    //    //                    //for (int i = 0; i < Tab[j].Count; i++)
    //    //                    //{
    //    //                    //    Tab[j].
    //    //                    //    EditorGUILayout.PropertyField(Tab[j][i]);
    //    //                    //}
    //    //                    foreach (SerializedProperty item in Tab[j])
    //    //                    {
    //    //                        EditorGUILayout.PropertyField(item);
    //    //                    }
    //    //                    break;
    //    //                }
    //    //            }
    //    //        }
    //    //    }
    //    //    //int legnthEnum = Enum.GetNames(typeof(StatesController.States)).Length;
    //    //    //for (int j = 0; j < legnthEnum; j++)
    //    //    //{
    //    //    //    if ((int)myTarget.State == j)
    //    //    //    {
    //    //    //        EditorGUILayout.PropertyField(ListSerialize[j]);
    //    //    //    }
    //    //    //}

    //    //    if (EditorGUI.EndChangeCheck())
    //    //    {
    //    //        soTarget.ApplyModifiedProperties();
    //    //    }



    //    //}
    //    //private void OnValidate()
    //    //{
    //    //    Debug.Log("PlayerControllerEditor OnValidate");
    //    //    ListingAttribute();
    //    //}

    //    ////bool isSerializable = false;
    //    ////bool isSkipingChildAttribute = false;
    //    ////bool isSkipingSaved = false;
    //    ////bool isSkiping = false;

    //    //private void ListingAttribute()
    //    //{
    //    //    Prop = serializedObject.GetIterator();
    //    //    if (Prop.NextVisible(true))
    //    //    {
    //    //        int x = -1;
    //    //        int tab = 1;
    //    //        UtilitiesEditor.IsSerializable = false;
    //    //        UtilitiesEditor.IsSkipingChildAttribute = false;
    //    //        UtilitiesEditor.IsSkiping = false;
    //    //        //string serializeEnd = "SerializableEnd";
    //    //        //string skipingChildAttributeEnd = "SkipingChildAttributeEnd";
    //    //        //string skipSavedEnd = "SkipSavedEnd";
    //    //        //string skipSavedPast = "SkipSaved";
    //    //        //string skipEnd = "SkipEnd";
    //    //        //string tabEnd = "TabEnd";
    //    //        //string curString = "Tab";
    //    //        int sizeByType = 0;
    //    //        //Debug.Log(" serializeEnd length =" + serializeEnd.Length);
    //    //        do
    //    //        {
    //    //            if (sizeByType > 0)
    //    //            {

    //    //                //Tab[x].Add(Prop.Copy());
    //    //                sizeByType--;
    //    //                //Prop.NextVisible(true);
    //    //                continue;
    //    //            }
    //    //            if (Prop.name == UtilitiesEditor.TabEnd)
    //    //            {
    //    //                break;
    //    //            }

    //    //            if (Prop.name == ("Tab" + tab))
    //    //            {
    //    //                x++;
    //    //                tab++;
    //    //                UtilitiesEditor.CurString = Prop.name;
    //    //            }
    //    //            else if (UtilitiesEditor.CurString == "Tab" + (tab - 1) && UtilitiesEditor.IsConditional() && sizeByType == 0)
    //    //            {


    //    //                SerializedProperty property = soTarget.FindProperty(Prop.name).Copy();
    //    //                sizeByType = (int)UtilitiesEditor.GetCountByProperty(property);

    //    //                for (int y = 0; y < (int)ListSkip.Count; y++)
    //    //                {
    //    //                    if (Prop.name == ((ListSkip)y).ToString())
    //    //                    {
    //    //                        //Debug.Log("cek isSerializable");
    //    //                        UtilitiesEditor.IsSkiping = true;
    //    //                    }
    //    //                }

    //    //                for (int y = 0; y < (int)ListSkipBUtSaved.Count; y++)
    //    //                {
    //    //                    if (Prop.name == ((ListSkipBUtSaved)y).ToString())
    //    //                    {
    //    //                        //Debug.Log("cek isSkipingSaved true");
    //    //                        UtilitiesEditor.IsSkipingSaved = true;
    //    //                        continue;
    //    //                    }
    //    //                }


    //    //                if (Prop.name.Contains(UtilitiesEditor.SkipSavedEnd))
    //    //                {
    //    //                    //Debug.Log("cek isSkipingSaved false");
    //    //                    UtilitiesEditor.IsSkipingSaved = false;
    //    //                    continue;
    //    //                }
    //    //                else if (Prop.name.Contains(UtilitiesEditor.SkipSavedPast))
    //    //                {
    //    //                    continue;
    //    //                }


    //    //                if (UtilitiesEditor.IsSkipingSaved)
    //    //                {
    //    //                    ListSerialize.Add(property);
    //    //                }
    //    //                else if ((!UtilitiesEditor.IsSkiping && !UtilitiesEditor.IsSkipingSaved))
    //    //                {
    //    //                    Tab[x].Add(property);
    //    //                }

    //    //                for (int y = 0; y < (int)ListSerializable.Count; y++)
    //    //                {
    //    //                    if (Prop.name == ((ListSerializable)y).ToString())
    //    //                    {
    //    //                        //Debug.Log("cek isSerializable");
    //    //                        UtilitiesEditor.IsSerializable = true;
    //    //                    }
    //    //                }
    //    //                for (int y = 0; y < (int)ListSkipingChildAttribut.Count; y++)
    //    //                {
    //    //                    if (Prop.name == ((ListSkipingChildAttribut)y).ToString())
    //    //                    {
    //    //                        //Debug.Log("cek isSerializable");
    //    //                        UtilitiesEditor.IsSkipingChildAttribute = true;
    //    //                    }
    //    //                }

    //    //            }
    //    //            else if (Prop.name.Contains(UtilitiesEditor.SerializeEnd))
    //    //            {
    //    //                UtilitiesEditor.IsSerializable = false;
    //    //            }
    //    //            else if (Prop.name.Contains(UtilitiesEditor.SkipingChildAttributeEnd))
    //    //            {
    //    //                UtilitiesEditor.IsSkipingChildAttribute = false;
    //    //            }
    //    //            else if (Prop.name.Contains(UtilitiesEditor.SkipEnd))
    //    //            {
    //    //                UtilitiesEditor.IsSkiping = false;
    //    //            }
    //    //        }
    //    //        while (Prop.NextVisible(true));

    //    //        //int index = 0;
    //    //        //foreach (SerializedProperty temp in ListSerialize)
    //    //        //{
    //    //        //    Debug.Log("cekcek ListSerialize [" + index + "] :" + temp.name);
    //    //        //    index++;
    //    //        //}
    //    //    }
    //    //}
    //}
}