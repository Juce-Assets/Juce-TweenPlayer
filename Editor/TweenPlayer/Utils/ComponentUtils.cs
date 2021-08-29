using Juce.TweenPlayer.BindableData;
using Juce.TweenPlayer.Components;
using System;

namespace Juce.TweenPlayer.Utils
{
    public static class ComponentUtils
    {
        public static void CollapseAll(TweenPlayerEditor editor)
        {
            foreach(TweenPlayerComponent component in editor.ActualTarget.BindingPlayerComponents)
            {
                component.Folded = true;
            }
        }

        public static void ExpandAll(TweenPlayerEditor editor)
        {
            foreach (TweenPlayerComponent component in editor.ActualTarget.BindingPlayerComponents)
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

            if (componentIndex < 0 || componentIndex >= editor.ActualTarget.BindingPlayerComponents.Count)
            {
                return;
            }

            newComponentIndex = Math.Min(newComponentIndex, editor.ActualTarget.BindingPlayerComponents.Count - 1);
            newComponentIndex = Math.Max(newComponentIndex, 0);

            TweenPlayerComponent component = editor.ActualTarget.BindingPlayerComponents[componentIndex];

            editor.ActualTarget.BindingPlayerComponents.RemoveAt(componentIndex);
            editor.ActualTarget.BindingPlayerComponents.Insert(newComponentIndex, component);
        }
    }
}
