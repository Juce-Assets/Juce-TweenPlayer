using Juce.Tweening;
using UnityEditor;
using UnityEngine;

namespace Juce.TweenComponent.Drawers
{
    public static class LoopModeDrawer
    {
        public static void Draw(TweenPlayerEditor editor)
        {
            LoopMode loopMode = (LoopMode)editor.SerializedPropertiesData.LoopModeProperty.enumValueIndex;

            bool loopingEnabled = loopMode == LoopMode.UntilManuallyStopped || loopMode == LoopMode.XTimes;
            bool needsLoopsCount = loopMode == LoopMode.XTimes;

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.Label($"Loop mode:");

                    LoopMode newLoopMode = (LoopMode)EditorGUILayout.EnumPopup("", loopMode);
                    editor.SerializedPropertiesData.LoopModeProperty.enumValueIndex = (int)newLoopMode;

                    loopingEnabled = newLoopMode == LoopMode.UntilManuallyStopped || newLoopMode == LoopMode.XTimes;
                    needsLoopsCount = newLoopMode == LoopMode.XTimes;
                }
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                if (loopingEnabled)
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        GUILayout.Label($"Loop reset mode:");

                        ResetMode resetMode = (ResetMode)editor.SerializedPropertiesData.LoopResetModeProperty.enumValueIndex;
                        ResetMode newResetMode = (ResetMode)EditorGUILayout.EnumPopup("", resetMode);
                        editor.SerializedPropertiesData.LoopResetModeProperty.enumValueIndex = (int)newResetMode;
                    }
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.EndHorizontal();
                }

                if (needsLoopsCount)
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        GUILayout.Label($"Loops:");

                        int loops = editor.SerializedPropertiesData.LoopsProperty.intValue;
                        int newLoops = EditorGUILayout.IntField("", loops);
                        editor.SerializedPropertiesData.LoopsProperty.intValue = newLoops;
                    }
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
    }
}
