using Juce.TweenPlayer.Components;
using UnityEditor;

namespace Juce.TweenPlayer.Logic
{
    public static class ActuallyRemoveComponentsLogic
    {
        public static void Execute(TweenPlayerEditor editor)
        {
            foreach (int componentIndex in editor.ToolData.ComponentsIndexToRemove)
            {
                editor.ActualTarget.Components.RemoveAt(componentIndex);
            }

            foreach (TweenPlayerComponent component in editor.ToolData.ComponentsToRemove)
            {
                Undo.RegisterCompleteObjectUndo(editor.ActualTarget, $"Remove {component.GetType().Name}");

                editor.ActualTarget.Components.Remove(component);
            }

            editor.ToolData.ComponentsIndexToRemove.Clear();
            editor.ToolData.ComponentsToRemove.Clear();
        }
    }
}
