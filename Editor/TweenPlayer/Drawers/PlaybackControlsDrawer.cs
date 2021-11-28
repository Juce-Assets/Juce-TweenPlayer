using UnityEditor;
using UnityEngine;

namespace Juce.TweenPlayer.Drawers
{
    public static class PlaybackControlsDrawer
    {
        public static void Draw(TweenPlayerEditor editor)
        {                                         
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.Label("Show Progress Bars (can slow down editor)");

                    editor.SerializedPropertiesData.ShowProgressBarsProperty.boolValue = EditorGUILayout.Toggle(
                        editor.SerializedPropertiesData.ShowProgressBarsProperty.boolValue
                        );
                }
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                EditorGUI.BeginDisabledGroup(!Application.isPlaying);
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button("Play"))
                        {
                            editor.ActualTarget.Play();
                        }

                        if (GUILayout.Button("Complete"))
                        {
                            editor.ActualTarget.Complete();
                        }

                        if (GUILayout.Button("Kill"))
                        {
                            editor.ActualTarget.Kill();
                        }

                        if (GUILayout.Button("Reset"))
                        {
                            editor.ActualTarget.Reset();
                        }

                        if (GUILayout.Button("Replay"))
                        {
                            editor.ActualTarget.Replay();
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUI.EndDisabledGroup();
            }
            
        }
    }
}
