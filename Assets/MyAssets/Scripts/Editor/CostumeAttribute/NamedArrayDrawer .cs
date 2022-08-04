using UnityEngine;
using UnityEditor;


namespace Com.GNL.URP_MyLibProjectTest
{

    [CustomPropertyDrawer(typeof(NamedArrayAttribute))]
    public class NamedArrayDrawer : PropertyDrawer
    {

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            try
            {
                NamedArrayAttribute atb = (NamedArrayAttribute)attribute;
                int pos = int.Parse(property.propertyPath.Split('[', ']')[1]);
                //Debug.Log("cek atribut pos" + pos);
                if (atb.isUseSplit && (pos % atb.names.Length == 0))
                {
                    EditorGUI.DrawRect(rect, new Color(0.2f, 0.2f, 0.2f, 1));
                }
                if (atb.isUseIncrement)
                {
                    string increment = label.text.Substring(8); //the default text is :"element ", and it's catch the number
                    string final = atb.names[pos % atb.names.Length] + " " + increment;
                    EditorGUI.PropertyField(rect, property, new GUIContent(final), true);
                }
                else
                {
                    EditorGUI.PropertyField(rect, property, new GUIContent(atb.names[pos % atb.names.Length]), true);
                }
            }
            catch
            {
                //Debug.Log("cek cek b");
                EditorGUI.PropertyField(rect, property, label, true);
            }
        }
    }
}