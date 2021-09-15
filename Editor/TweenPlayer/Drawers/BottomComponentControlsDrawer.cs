using Juce.TweenPlayer.Helpers;
using UnityEditor;
using UnityEngine;

namespace Juce.TweenPlayer.Drawers
{
    public static class BottomComponentControlsDrawer
    {
        public static void Draw(TweenPlayerEditor editor)
        {
            if (editor.ActualTarget.Components.Count == 0)
            {
                if (CopyPasteComponentHelper.HasCopiedComponents)
                {
                    if (GUILayout.Button("Paste as new"))
                    {
                        CopyPasteComponentHelper.PasteAsNew(editor, destination: null);

                        EditorUtility.SetDirty(editor.ActualTarget);
                    }
                }
            }

            if (GUILayout.Button("Add Component"))
            {
                ComponentsListContextMenuDrawer.Draw(editor);
            }
        }
    }
}
