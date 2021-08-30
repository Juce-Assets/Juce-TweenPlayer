using System;
using UnityEngine;

namespace Juce.TweenPlayer
{
    public class EditorTweenPlayerComponent
    {
        public Type Type { get; }
        public string Name { get; }
        public string MenuPath { get; }
        public Color Color { get; }
        public bool UseColorAsBackground { get; }
        public string Documentation { get; }

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
