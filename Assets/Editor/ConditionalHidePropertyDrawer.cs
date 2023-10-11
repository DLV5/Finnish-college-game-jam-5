using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ConditionalHideAttribute))]
public class ConditionalHidePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
        SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(condHAtt.ConditionalSourceField);

        if (sourcePropertyValue != null)
        {
            bool shouldShow = condHAtt.Inverse ? !sourcePropertyValue.boolValue : sourcePropertyValue.boolValue;

            if (shouldShow)
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }
        else
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
        SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(condHAtt.ConditionalSourceField);

        if (sourcePropertyValue != null)
        {
            bool shouldShow = condHAtt.Inverse ? !sourcePropertyValue.boolValue : sourcePropertyValue.boolValue;

            if (shouldShow)
            {
                return EditorGUI.GetPropertyHeight(property, label);
            }
        }

        return -EditorGUIUtility.standardVerticalSpacing;
    }
}
