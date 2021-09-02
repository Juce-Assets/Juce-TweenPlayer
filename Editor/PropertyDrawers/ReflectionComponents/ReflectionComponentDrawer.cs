using Juce.TweenPlayer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Juce.TweenPlayer.PropertyDrawers.ReflectionComponents
{
    public static class ReflectionComponentDrawer
    {
        public static void Draw(
            Rect position, 
            SerializedProperty property, 
            GUIContent label,
            Type objectType
            )
        {
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            SerializedProperty componentProperty = property.FindPropertyRelative("Component");
            SerializedProperty propertyNameProperty = property.FindPropertyRelative("PropertyName");

            float lineHeight = EditorGUIUtility.singleLineHeight;

            // Calculate rects
            var componentRect = new Rect(position.x, position.y, position.width, lineHeight);
            var unitRect = new Rect(position.x, position.y + (lineHeight) + 3, position.width, lineHeight);

            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            EditorGUI.PropertyField(componentRect, componentProperty, GUIContent.none);

            if (componentProperty.objectReferenceValue != null)
            {
                List<PropertyInfo> properties = ReflectionUtils.GetProperties(
                    componentProperty.objectReferenceValue.GetType()
                    );

                List<FieldInfo> fields = ReflectionUtils.GetFields(
                    componentProperty.objectReferenceValue.GetType()
                    );

                string[] propertiesNames = properties
                    .Where(i => i.PropertyType == objectType)
                    .Select(i => i.Name)
                    .ToArray();

                string[] fieldsNames = fields
                    .Where(i => i.FieldType == objectType)
                    .Select(i => i.Name)
                    .ToArray();

                string[] combined = propertiesNames.Concat(fieldsNames).ToArray();

                string propertyNamePropertyValue = propertyNameProperty.stringValue;

                int selectedPropertyIndex = -1;

                for (int i = 0; i < combined.Length; ++i)
                {
                    string currentPropertyOrFieldName = combined[i];

                    if (string.Equals(currentPropertyOrFieldName, propertyNamePropertyValue))
                    {
                        selectedPropertyIndex = i;
                        break;
                    }
                }

                selectedPropertyIndex = EditorGUI.Popup(unitRect, selectedPropertyIndex, propertiesNames);

                bool outOfBounds = selectedPropertyIndex < 0 || selectedPropertyIndex > propertiesNames.Length - 1;

                if (!outOfBounds)
                {
                    propertyNameProperty.stringValue = propertiesNames[selectedPropertyIndex];
                }
                else
                {
                    propertyNameProperty.stringValue = string.Empty;
                }

                if(propertiesNames.Length > 0 && outOfBounds)
                {
                    propertyNameProperty.stringValue = propertiesNames[0];
                }
            }

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }

        public static float GetPropertyHeight(
            PropertyDrawer propertyDrawer, 
            SerializedProperty property, 
            GUIContent label
            )
        {
            SerializedProperty componentProperty = property.FindPropertyRelative("Component");

            if(componentProperty.objectReferenceValue == null)
            {
                return EditorGUIUtility.singleLineHeight;
            }

            return (EditorGUIUtility.singleLineHeight * 2) + 3;
        }
    }
}
