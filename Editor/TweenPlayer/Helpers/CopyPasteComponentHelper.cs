using Juce.TweenPlayer.Components;
using UnityEditor;

namespace Juce.TweenPlayer.Helpers
{
    public static class CopyPasteComponentHelper
    {
        private static TweenPlayerComponent copiedComponent;

        public static bool HasCopiedComponent => copiedComponent != null;

        public static void Copy(TweenPlayerComponent component)
        {
            if(component == null)
            {
                return;
            }

            copiedComponent = component;
        }

        public static void PasteAsNew(TweenPlayer tweenPlayer, TweenPlayerComponent destination)
        {
            if (copiedComponent == null)
            { 
                return;
            }

            int index = tweenPlayer.BindingPlayerComponents.Count;

            for(int i = 0; i < tweenPlayer.BindingPlayerComponents.Count; ++i)
            {
                TweenPlayerComponent component = tweenPlayer.BindingPlayerComponents[i];

                if (component == destination)
                {
                    index = i;
                    break;
                }
            }

            TweenPlayerComponent newComponent = tweenPlayer.AddComponent(copiedComponent.GetType(), index + 1);

            EditorUtility.CopySerializedManagedFieldsOnly(copiedComponent, newComponent);
        }

        public static bool CanPasteValues(TweenPlayerComponent destination)
        {
            if (copiedComponent == null)
            {
                return false;
            }

            if (destination == null)
            {
                return false;
            }

            if(destination == copiedComponent)
            {
                return false;
            }

            return copiedComponent.GetType() == destination.GetType();
        }

        public static void PasteValues(TweenPlayerComponent destination)
        {
            if (copiedComponent == null)
            {
                return;
            }

            if(destination == null)
            {
                return;
            }

            EditorUtility.CopySerializedManagedFieldsOnly(copiedComponent, destination);
        }
    }
}
