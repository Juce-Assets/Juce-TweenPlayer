﻿using Juce.TweenPlayer.Components;

namespace Juce.TweenPlayer.Logic
{
    public static class RemoveComponentLogic
    {
        public static void Execute(TweenPlayerEditor editor, TweenPlayerComponent component)
        {
            editor.ToolData.ComponentsToRemove.Add(component);
        }

        public static void Execute(TweenPlayerEditor editor, int index)
        {
            editor.ToolData.ComponentsIndexToRemove.Add(index);
        }
    }
}
