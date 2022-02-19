using Juce.TweenComponent.Components;
using Juce.TweenComponent.Logic;
using System.Collections.Generic;
using UnityEditor;

namespace Juce.TweenComponent.Helpers
{
    public static class CopyPasteComponentHelper
    {
        private static readonly List<TweenPlayerComponent> copiedComponents = new List<TweenPlayerComponent>();

        public static bool HasCopiedComponents => copiedComponents.Count > 0;
        public static bool HasMultipleCopiedComponents => copiedComponents.Count > 1;

        public static void Copy(IReadOnlyList<TweenPlayerComponent> components)
        {
            if (components == null)
            {
                return;
            }

            if(components.Count == 0)
            {
                return;
            }

            copiedComponents.Clear();

            copiedComponents.AddRange(components);
        }

        public static void Copy(TweenPlayerComponent component)
        {
            if(component == null)
            {
                return;
            }

            Copy(new TweenPlayerComponent[] { component });
        }

        public static void PasteAsNew(
            TweenPlayerEditor editor, 
            TweenPlayerComponent destination, 
            int destinationOffset = 0
            )
        {
            if (copiedComponents.Count == 0)
            { 
                return;
            }

            int index = editor.ActualTarget.Components.Count;

            for (int i = 0; i < editor.ActualTarget.Components.Count; ++i)
            {
                TweenPlayerComponent component = editor.ActualTarget.Components[i];

                if (component == destination)
                {
                    index = i;
                    break;
                }
            }

            index += destinationOffset;

            UndoHelper.BeginUndo();

            for(int i = 0; i < copiedComponents.Count; ++i)
            {
                TweenPlayerComponent copiedComponent = copiedComponents[i];

                TweenPlayerComponent newComponent = CreateComponentLogic.Execute(
                    editor,
                    copiedComponent.GetType(), 
                    index + i + 1
                    );

                PasteComponentValuesLogic.Execute(editor, copiedComponent, newComponent);
            }

            UndoHelper.EndUndo();
        }

        public static bool CanPasteValues(TweenPlayerComponent destination)
        {
            if (copiedComponents.Count != 1)
            {
                return false;
            }

            if (destination == null)
            {
                return false;
            }

            TweenPlayerComponent copiedComponent = copiedComponents[0];

            if (destination == copiedComponent)
            {
                return false;
            }

            return copiedComponent.GetType() == destination.GetType();
        }

        public static void PasteValues(TweenPlayerEditor editor, TweenPlayerComponent destination)
        {
            bool canPasteValues = CanPasteValues(destination);

            if (!canPasteValues)
            {
                return;
            }

            TweenPlayerComponent copiedComponent = copiedComponents[0];

            PasteComponentValuesLogic.Execute(editor, copiedComponent, destination);
        }
    }
}
