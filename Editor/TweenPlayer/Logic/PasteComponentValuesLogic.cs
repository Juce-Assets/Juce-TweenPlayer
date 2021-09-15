using Juce.TweenPlayer.Components;
using UnityEditor;

namespace Juce.TweenPlayer.Logic
{
    public static class PasteComponentValuesLogic
    {
        public static void Execute(
            TweenPlayerEditor editor,
            TweenPlayerComponent origin,
            TweenPlayerComponent destination
            )
        {
            Undo.RegisterCompleteObjectUndo(editor.ActualTarget, $"Paste {origin.GetType().Name}");

            EditorUtility.CopySerializedManagedFieldsOnly(origin, destination);
        }
    }
}
