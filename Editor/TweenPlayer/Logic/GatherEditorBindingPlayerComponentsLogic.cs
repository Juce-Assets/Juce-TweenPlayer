using Juce.TweenComponent.Components;
using Juce.TweenComponent.Attributes;
using Juce.TweenComponent.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Juce.TweenComponent.Logic
{
    public static class GatherEditorBindingPlayerComponentsLogic
    {
        public static void Execute(TweenPlayerEditor editor)
        {
            editor.ToolData.EditorPlayerComponents.Clear();

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

                bool documentationFound = ReflectionUtils.TryGetAttribute(
                    type,
                    out TweenPlayerComponentDocumentation documentationAttribute
                    );

                string documentation = string.Empty;

                if (documentationFound)
                {
                    documentation = documentationAttribute.Documentation;
                }

                editor.ToolData.EditorPlayerComponents.Add(new EditorTweenPlayerComponent(
                    type,
                    attribute.Name,
                    attribute.MenuPath,
                    color,
                    useAsBackground,
                    documentation
                    ));
            }
        }
    }
}
