using Juce.TweenComponent.Bindings;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Juce.TweenComponent
{
    public class EditorTweenPlayerComponent
    {
        public Type Type { get; }
        public string Name { get; }
        public string MenuPath { get; }
        public Color Color { get; }
        public bool UseColorAsBackground { get; }
        public string Documentation { get; }

        public List<EditorBinding> EditorBindings { get; private set; } = new List<EditorBinding>();

        public EditorTweenPlayerComponent(
            Type type,
            string name,
            string menuPath,
            Color color,
            bool useColorAsBackground,
            string documentation
            )
        {
            Type = type;
            Name = name;
            MenuPath = menuPath;
            Color = color;
            UseColorAsBackground = useColorAsBackground;
            Documentation = documentation;
        }
    }
}
