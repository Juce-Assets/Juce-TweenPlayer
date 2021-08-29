using Juce.TweenPlayer.Components;
using Juce.TweenPlayer.Reflection;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Juce.TweenPlayer.Utils
{
    public static class EditorComponentUtils
    {
        public static List<EditorTweenPlayerComponent> GatherEditorComponents()
        {
            List<EditorTweenPlayerComponent> ret = new List<EditorTweenPlayerComponent>();

            List<Type> types = ReflectionUtils.GetInheritedTypes(typeof(TweenPlayerComponent));

            foreach (Type type in types)
            {
                bool found = ReflectionUtils.TryGetAttribute(type, out TweenPlayerComponentAttribute attribute);

                if (!found)
                {
                    continue;
                }

                bool colorFound = ReflectionUtils.TryGetAttribute(
                    type,
                    out TweenPlayerComponentColorAttribute colorAttribute
                    );

                Color color = new Color(0, 0, 0, 0);
                bool useAsBackground = false;

                if (colorFound)
                {
                    color = colorAttribute.Color;
                    useAsBackground = colorAttribute.UseAsBackground;
                }

                ret.Add(new EditorTweenPlayerComponent(
                    type,
                    attribute.Name,
                    attribute.MenuPath,
                    color,
                    useAsBackground
                    ));
            }

            return ret;
        }

        public static bool TryGetCachedEditorPlayerComponent(
            TweenPlayerEditor editor,
            Type componentType, 
            out EditorTweenPlayerComponent editorPlayerComponent
            )
        {
            bool found = editor.CachedEditorPlayerComponents.TryGetValue(componentType, out editorPlayerComponent);

            if (found)
            {
                return true;
            }

            foreach (EditorTweenPlayerComponent component in editor.EditorPlayerComponents)
            {
                if (componentType == component.Type)
                {
                    editor.CachedEditorPlayerComponents.Add(componentType, component);

                    editorPlayerComponent = component;
                    return true;
                }
            }

            editorPlayerComponent = null;
            return false;
        }
    }
}
