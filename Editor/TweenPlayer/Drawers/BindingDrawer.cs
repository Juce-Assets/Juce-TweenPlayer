using Juce.TweenComponent.Bindings;
using Juce.TweenComponent.Logic;
using Juce.TweenComponent.Utils;
using UnityEditor;
using UnityEngine;

namespace Juce.TweenComponent.Drawers
{
    public static class BindingDrawer
    {
        public static void Draw(
            TweenPlayerEditor bindingPlayerEditor,
            SerializedProperty parentSerializedProperty,
            EditorBinding editorBinding
            )
        {
            editorBinding.Binding.BindingEnabled = bindingPlayerEditor.SerializedPropertiesData.BindingEnabledProperty.boolValue;

            bool bindingIsDisabled = !editorBinding.Binding.WantsToBeBinded
                || !bindingPlayerEditor.SerializedPropertiesData.BindingEnabledProperty.boolValue;

            if (bindingIsDisabled)
            {
                DrawBindingDisabledBinding(
                    bindingPlayerEditor,
                    parentSerializedProperty,
                    editorBinding
                    );

                EditorGUILayout.Space(3);

                return;
            }

            DrawBindingEnabledBinding(
                bindingPlayerEditor,
                editorBinding
                );
        }

        private static void DrawBindingDisabledBinding(
            TweenPlayerEditor bindingPlayerEditor,
            SerializedProperty parentSerializedProperty,
            EditorBinding editorBinding
            )
        {
            editorBinding.Binding.Binded = false;

            editorBinding.SerializedProperty = parentSerializedProperty.FindPropertyRelative(editorBinding.Name);

            EditorGUILayout.BeginHorizontal();
            {
                if (bindingPlayerEditor.SerializedPropertiesData.BindingEnabledProperty.boolValue)
                {
                    editorBinding.Binding.WantsToBeBinded = EditorGUILayout.Toggle(
                        editorBinding.Binding.WantsToBeBinded, 
                        GUILayout.Width(25)
                        );
                }

                GUILayout.Label(editorBinding.FormatedName);

                EditorGUILayout.Space();

                SerializedPropertyUtils.DrawSerializedPropertyChildren(editorBinding.SerializedProperty, showNames: false);
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
                editorBinding.Binding.WantsToBeBinded = EditorGUILayout.Toggle(
                    editorBinding.Binding.WantsToBeBinded, 
                    GUILayout.Width(25)
                    );

                bool bindableDataFound = bindingPlayerEditor.ToolData.SelectedEditorBindableData != null;

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
                        $" property at {bindingPlayerEditor.ToolData.SelectedEditorBindableData.Name}",
                        EditorStyles.wordWrappedLabel, GUILayout.ExpandWidth(false));
                }
                else
                {

                    GUILayout.Label(
                        $"{editorBinding.FormatedName}", 
                        GUILayout.ExpandWidth(false)
                        );

                    if (bindingPlayerEditor.ToolData.SelectedEditorBindableData == null)
                    {
                        editorBinding.Binding.Binded = false;
                        GUILayout.Label("?");
                        return;
                    }

                    bool found = TryGetBindedVariableIndexLogic.Execute(editorBinding, out int index);

                    if(!found)
                    {
                        index = -1;
                    }

                    int newIndex = EditorGUILayout.Popup(index, editorBinding.BindableFields);

                    if (newIndex >= 0)
                    {
                        editorBinding.Binding.BindedVariableName = editorBinding.BindableFields[newIndex];
                    }

                    GUILayout.Label(
                        $"[{editorBinding.Type.Name}]",
                        GUILayout.ExpandWidth(false)
                        );

                    editorBinding.Binding.Binded = newIndex >= 0;
                }
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
    }
}
