using Juce.TweenComponent.Components;
using System;
using UnityEditor;

namespace Juce.TweenComponent.Logic
{
    public static class CreateComponentLogic
    {
        public static TweenPlayerComponent Execute(TweenPlayerEditor editor, Type componentType)
        {
            Undo.RegisterCompleteObjectUndo(editor.ActualTarget, $"Add {componentType.Name}");

            return editor.ActualTarget.AddTweenPlayerComponent(componentType);
        }

        public static TweenPlayerComponent Execute(TweenPlayerEditor editor, Type componentType, int index)
        {
            Undo.RegisterCompleteObjectUndo(editor.ActualTarget, $"Add {componentType.Name}");

            return editor.ActualTarget.AddTweenPlayerComponent(componentType, index);
        }
    }
}
