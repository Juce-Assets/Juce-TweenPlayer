using Juce.TweenComponent.BindableData;
using Juce.TweenComponent.Components;
using System;

namespace Juce.TweenComponent.Utils
{
    public static class ComponentUtils
    {
        public static void CollapseAll(TweenPlayerEditor editor)
        {
            foreach(TweenPlayerComponent component in editor.ActualTarget.Components)
            {
                component.Folded = true;
            }
        }

        public static void ExpandAll(TweenPlayerEditor editor)
        {
            foreach (TweenPlayerComponent component in editor.ActualTarget.Components)
            {
                component.Folded = false;
            }
        }

        public static void ReorderComponent(TweenPlayerEditor editor, int componentIndex, int newComponentIndex)
        {
            if (componentIndex == newComponentIndex)
            {
                return;
            }

            if (componentIndex < 0 || componentIndex >= editor.ActualTarget.Components.Count)
            {
                return;
            }

            newComponentIndex = Math.Min(newComponentIndex, editor.ActualTarget.Components.Count - 1);
            newComponentIndex = Math.Max(newComponentIndex, 0);

            TweenPlayerComponent component = editor.ActualTarget.Components[componentIndex];

            editor.ActualTarget.Components.RemoveAt(componentIndex);
            editor.ActualTarget.Components.Insert(newComponentIndex, component);
        }
    }
}
