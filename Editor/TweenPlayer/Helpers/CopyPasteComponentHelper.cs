using Juce.TweenPlayer.Components;
using System.Collections.Generic;
using UnityEditor;

namespace Juce.TweenPlayer.Helpers
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

        public static void PasteAsNew(TweenPlayer tweenPlayer, TweenPlayerComponent destination)
        {
            if (copiedComponents.Count == 0)
            { 
                return;
            }

            int index = tweenPlayer.BindingPlayerComponents.Count;

            for (int i = 0; i < tweenPlayer.BindingPlayerComponents.Count; ++i)
            {
                TweenPlayerComponent component = tweenPlayer.BindingPlayerComponents[i];

                if (component == destination)
                {
                    index = i;
                    break;
                }
            }

            for(int i = 0; i < copiedComponents.Count; ++i)
            {
                TweenPlayerComponent copiedComponent = copiedComponents[i];

                TweenPlayerComponent newComponent = tweenPlayer.AddTweenPlayerComponent(
                    copiedComponent.GetType(), 
                    index + 1 + i
                    );

                EditorUtility.CopySerializedManagedFieldsOnly(copiedComponent, newComponent);
            }
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

        public static void PasteValues(TweenPlayerComponent destination)
        {
            bool canPasteValues = CanPasteValues(destination);

            if (!canPasteValues)
            {
                return;
            }

            TweenPlayerComponent copiedComponent = copiedComponents[0];

            EditorUtility.CopySerializedManagedFieldsOnly(copiedComponent, destination);
        }
    }
}
