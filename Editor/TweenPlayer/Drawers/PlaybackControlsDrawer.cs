using UnityEditor;
using UnityEngine;

namespace Juce.TweenPlayer.Drawers
{
    public static class PlaybackControlsDrawer
    {
        public static void Draw(TweenPlayerEditor editor)
        {
            EditorGUI.BeginDisabledGroup(!Application.isPlaying);
            {
                using (new EditorGUILayout.HorizontalScope(EditorStyles.helpBox))
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
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}
