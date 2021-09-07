using Juce.TweenPlayer.BindableData;
using UnityEditor;
using UnityEngine;

namespace Juce.TweenPlayer.Drawers
{
    public static class BindableDataDrawer
    {
        public static void Draw(TweenPlayerEditor editor)
        {
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.Label($"Bindings enabled");
                    EditorGUILayout.PropertyField(editor.SerializedPropertiesData.BindingEnabledProperty, new GUIContent(""));
                }
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                if (!editor.SerializedPropertiesData.BindingEnabledProperty.boolValue)
                {
                    return;
                }

                if (editor.ToolData.EditorBindableDatas.Count == 0)
                {
                    EditorGUILayout.HelpBox($"There is no avaliable {nameof(IBindableData)} on the project. Please," +
                        $"create a class that inherits from {nameof(IBindableData)}.", MessageType.Warning);
                    return;
                }

                EditorBindableData editorBindableData = GetEditorBindableData(
                    editor, 
                    editor.SerializedPropertiesData.BindableDataUidProperty.stringValue
                    );

                editor.ToolData.SelectedEditorBindableData = editorBindableData;

                EditorGUILayout.BeginHorizontal();
                {
                    if (editorBindableData != null)
                    {
                        GUILayout.Label($"Binded data:");
                        GUILayout.Label($"{editorBindableData.Name}", EditorStyles.boldLabel);
                    }

                    if (GUILayout.Button("Select"))
                    {
                        BindableDataContextMenuDrawer.Draw(editor);
                    }

                    if (editorBindableData != null)
                    {
                        if (GUILayout.Button("X"))
                        {
                            editor.SerializedPropertiesData.BindableDataUidProperty.stringValue = string.Empty;
                        }
                    }
                }
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                if (editorBindableData != null)
                {
                    if (!editor.ToolData.ShowBindedDataProperties)
                    {
                        if (GUILayout.Button("Show properties"))
                        {
                            editor.ToolData.ShowBindedDataProperties = true;
                        }
                    }
                    else
                    {
                        if (GUILayout.Button("Hide properties"))
                        {
                            editor.ToolData.ShowBindedDataProperties = false;
                        }
                    }
                }

                if (editor.ToolData.ShowBindedDataProperties)
                {
                    DrawBindableDataProperties(editor, editorBindableData);
                }
            }
        }

        private static void DrawBindableDataProperties(
            TweenPlayerEditor bindingPlayerEditor, 
            EditorBindableData editorBindableData
            )
        {
            if (editorBindableData == null)
            {
                return;
            }

            EditorGUILayout.LabelField("Properties:");

            bindingPlayerEditor.ToolData.ShowBindedDataPropertiesScrollViewPosition = EditorGUILayout.BeginScrollView(
                    bindingPlayerEditor.ToolData.ShowBindedDataPropertiesScrollViewPosition
                    );
            {
                foreach (EditorBindableDataField field in editorBindableData.Fields)
                {
                    EditorGUILayout.LabelField($"- [{field.Type.Name}] {field.Name}");
                }

                foreach (EditorBindableDataProperty property in editorBindableData.Properties)
                {
                    EditorGUILayout.LabelField($"- [{property.Type.Name}] {property.Name}");
                }
            }
            EditorGUILayout.EndScrollView();
        }

        private static EditorBindableData GetEditorBindableData(
            TweenPlayerEditor bindingPlayerEditor, 
            string bindableDataUid
            )
        {
            for (int i = 0; i < bindingPlayerEditor.ToolData.EditorBindableDatas.Count; ++i)
            {
                EditorBindableData editorBindableData = bindingPlayerEditor.ToolData.EditorBindableDatas[i];

                if (string.Equals(bindableDataUid, editorBindableData.Uid))
                {
                    return editorBindableData;
                }
            }

            return null;
        }
    }
}
