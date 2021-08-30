using Juce.TweenPlayer.Components;
using Juce.TweenPlayer.Helpers;
using Juce.TweenPlayer.Utils;
using UnityEditor;
using UnityEngine;

namespace Juce.TweenPlayer.Drawers
{
    public static class ComponentsDrawer
    {
        public static void Draw(TweenPlayerEditor editor)
        {
            if (editor.ComponentsProperty.arraySize == 0)
            {
                EditorGUILayout.LabelField("No components added. Press Add Component to add a new " +
                    "twening component", EditorStyles.wordWrappedLabel);
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("Components");

                    if (editor.ActualTarget.Components.Count > 0)
                    {
                        if (GUILayout.Button("Copy All"))
                        {
                            CopyPasteComponentHelper.Copy(editor.ActualTarget.Components);
                        }
                    }
                }
                EditorGUILayout.EndHorizontal();
            }

            for (int i = 0; i < editor.ComponentsProperty.arraySize; ++i)
            {
                TweenPlayerComponent component = editor.ActualTarget.Components[i];

                SerializedProperty componentSerializedProperty = editor.ComponentsProperty.GetArrayElementAtIndex(i);

                if(component == null)
                {
                    DrawNullComponent(editor, i);
                    continue;
                }

                ComponentDrawer.Draw(
                    editor,
                    component,
                    componentSerializedProperty,
                    i
                    );
            }

            Event e = Event.current;

            // Finish dragging
            int startIndex;
            int endIndex;
            bool dragged = editor.ComponentsDraggable.ResolveDragging(e, out startIndex, out endIndex);

            if (dragged)
            {
                ComponentUtils.ReorderComponent(editor, startIndex, endIndex);

                EditorUtility.SetDirty(editor.target);
            }
        }

        private static void DrawNullComponent(TweenPlayerEditor bindingPlayerEditor, int componentIndex)
        {
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Label("Null component");

                if(GUILayout.Button("Remove"))
                {
                    bindingPlayerEditor.RemoveComponent(componentIndex);
                }
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
    }
}
