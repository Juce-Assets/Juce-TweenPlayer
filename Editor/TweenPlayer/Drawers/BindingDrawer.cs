using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Juce.TweenPlayer.Drawers
{
    public static class BindingDrawer
    {
        public static void Draw(
         TweenPlayerEditor bindingPlayerEditor,
         SerializedProperty parentSerializedProperty,
         EditorBinding editorBinding
         )
        {
            editorBinding.Binding.BindingEnabled = bindingPlayerEditor.BindingEnabledProperty.boolValue;

            if (!editorBinding.Binding.WantsToBeBinded || !bindingPlayerEditor.BindingEnabledProperty.boolValue)
            {
                DrawBindingDisabledBinding(
                    bindingPlayerEditor,
                    parentSerializedProperty,
                    editorBinding
                    );

                EditorGUILayout.Space(3);
            }
            else
            {
                DrawBindingEnabledBinding(
                    bindingPlayerEditor,
                    editorBinding
                    );
            }
        }

        public static void UpdateBindingData(
            TweenPlayerEditor bindingPlayerEditor,
            EditorBinding editorBinding
            )
        {
            editorBinding.BindableFields = Array.Empty<string>();

            editorBinding.Binding.BindingEnabled = bindingPlayerEditor.BindingEnabledProperty.boolValue;

            bool bindingEnabled = editorBinding.Binding.WantsToBeBinded && bindingPlayerEditor.BindingEnabledProperty.boolValue;

            if (!bindingEnabled)
            {
                editorBinding.Binding.Binded = false;
            }
            else
            {
                bool hasBindableData = bindingPlayerEditor.SelectedEditorBindableData != null;

                if(!hasBindableData)
                {
                    editorBinding.Binding.Binded = false;
                }
                else
                {
                    string[] fieldsNames = bindingPlayerEditor.SelectedEditorBindableData.Fields.
                        Where(i => editorBinding.Type.IsAssignableFrom(i.Type)).Select(i => i.Name).ToArray();

                    string[] propertiesNames = bindingPlayerEditor.SelectedEditorBindableData.Properties.
                        Where(i => editorBinding.Type.IsAssignableFrom(i.Type)).Select(i => i.Name).ToArray();

                    editorBinding.BindableFields = new string[fieldsNames.Length + propertiesNames.Length];
                    Array.Copy(fieldsNames, editorBinding.BindableFields, fieldsNames.Length);
                    Array.Copy(propertiesNames, 0, editorBinding.BindableFields, fieldsNames.Length, propertiesNames.Length);

                    bool hasBindableProperties = editorBinding.BindableFields.Length > 0;

                    if(!hasBindableProperties)
                    {
                        editorBinding.Binding.Binded = false;
                    }
                    else
                    {
                        bool found = TryGetBindedVariableIndex(editorBinding, out int index);

                        if (found)
                        {
                            editorBinding.Binding.BindedVariableName = editorBinding.BindableFields[index];
                        }

                        editorBinding.Binding.Binded = found;
                    }
                }
            }
        }

        private static bool TryGetBindedVariableIndex(EditorBinding editorBinding, out int index)
        {
            for (int i = 0; i < editorBinding.BindableFields.Length; ++i)
            {
                if (string.Equals(editorBinding.Binding.BindedVariableName, editorBinding.BindableFields[i]))
                {
                    index = i;
                    return true;
                }
            }

            index = 0;
            return false;
        }

        private static void DrawBindingDisabledBinding(
            TweenPlayerEditor bindingPlayerEditor,
            SerializedProperty parentSerializedProperty,
            EditorBinding editorBinding
            )
        {
            editorBinding.Binding.Binded = false;

            SerializedProperty serializedProperty = parentSerializedProperty.FindPropertyRelative(editorBinding.Name);

            EditorGUILayout.BeginHorizontal();
            {
                if (bindingPlayerEditor.BindingEnabledProperty.boolValue)
                {
                    editorBinding.Binding.WantsToBeBinded = EditorGUILayout.Toggle(editorBinding.Binding.WantsToBeBinded, GUILayout.Width(25));
                }

                GUILayout.Label(editorBinding.FormatedName);

                EditorGUILayout.Space();

                SerializedPropertyUtils.DrawSerializedPropertyChildren(serializedProperty, showNames: false);
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        private static void DrawBindingEnabledBinding(
            TweenPlayerEditor bindingPlayerEditor,
            EditorBinding editorBinding
            )
        {
            EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(false));
            {
                editorBinding.Binding.WantsToBeBinded = EditorGUILayout.Toggle(editorBinding.Binding.WantsToBeBinded, GUILayout.Width(25));

                bool bindableDataFound = bindingPlayerEditor.SelectedEditorBindableData != null;

                bool hasBindableProperties = editorBinding.BindableFields.Length > 0;

                if (!bindableDataFound)
                {
                    editorBinding.Binding.Binded = false;

                    GUILayout.Label("Missing bindable data");
                }
                else if (!hasBindableProperties)
                {
                    editorBinding.Binding.Binded = false;

                    GUILayout.Label($"[{editorBinding.Type.Name}] {editorBinding.FormatedName} has no valid bindable" +
                        $" property at {bindingPlayerEditor.SelectedEditorBindableData.Name}",
                        EditorStyles.wordWrappedLabel, GUILayout.ExpandWidth(false));
                }
                else
                {

                    GUILayout.Label($"[{editorBinding.Type.Name}] {editorBinding.Name} binded to", GUILayout.ExpandWidth(false));

                    if (bindingPlayerEditor.SelectedEditorBindableData == null)
                    {
                        editorBinding.Binding.Binded = false;
                        GUILayout.Label("?");
                        return;
                    }

                    bool found = TryGetBindedVariableIndex(editorBinding, out int index);

                    if(!found)
                    {
                        index = -1;
                    }

                    int newIndex = EditorGUILayout.Popup(index, editorBinding.BindableFields);

                    if (newIndex >= 0)
                    {
                        editorBinding.Binding.BindedVariableName = editorBinding.BindableFields[newIndex];
                    }

                    editorBinding.Binding.Binded = newIndex >= 0;
                }
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
    }
}
