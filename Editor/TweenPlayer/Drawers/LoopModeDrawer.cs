using Juce.Tween;
using UnityEditor;
using UnityEngine;

namespace Juce.TweenPlayer.Drawers
{
    public static class LoopModeDrawer
    {
        public static void Draw(TweenPlayerEditor editor)
        {
            LoopMode loopMode = (LoopMode)editor.LoopModeProperty.enumValueIndex;

            bool loopingEnabled = loopMode == LoopMode.UntilManuallyStopped || loopMode == LoopMode.XTimes;
            bool needsLoopsCount = loopMode == LoopMode.XTimes;

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.Label($"Loop mode:");

                    LoopMode newLoopMode = (LoopMode)EditorGUILayout.EnumPopup("", loopMode);
                    editor.LoopModeProperty.enumValueIndex = (int)newLoopMode;

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

                        ResetMode resetMode = (ResetMode)editor.LoopResetModeProperty.enumValueIndex;
                        ResetMode newResetMode = (ResetMode)EditorGUILayout.EnumPopup("", resetMode);
                        editor.LoopResetModeProperty.enumValueIndex = (int)newResetMode;
                    }
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.EndHorizontal();
                }

                if (needsLoopsCount)
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        GUILayout.Label($"Loops:");

                        int loops = editor.LoopsProperty.intValue;
                        int newLoops = (int)EditorGUILayout.IntField("", loops);
                        editor.LoopsProperty.intValue = newLoops;
                    }
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
    }
}
