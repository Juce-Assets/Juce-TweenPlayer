﻿using UnityEditor;
using UnityEngine;

namespace Juce.TweenPlayer.Drawers
{
    public static class ExecutionModeDrawer
    {
        public static void Draw(TweenPlayerEditor editor)
        {
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Label($"Execution mode:");

                ExecutionMode executionMode = (ExecutionMode)editor.ExecutionModeProperty.enumValueIndex;
                ExecutionMode newExecutionMode = (ExecutionMode)EditorGUILayout.EnumPopup("", executionMode);

                if (executionMode != newExecutionMode)
                {
                    editor.ExecutionModeProperty.enumValueIndex = (int)newExecutionMode;

                    editor.serializedObject.ApplyModifiedProperties();
                }
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
    }
}