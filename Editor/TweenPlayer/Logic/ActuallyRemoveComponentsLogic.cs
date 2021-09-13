using Juce.TweenPlayer.Components;

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
                editor.ActualTarget.Components.Remove(component);
            }

            editor.ToolData.ComponentsIndexToRemove.Clear();
            editor.ToolData.ComponentsToRemove.Clear();
        }
    }
}
