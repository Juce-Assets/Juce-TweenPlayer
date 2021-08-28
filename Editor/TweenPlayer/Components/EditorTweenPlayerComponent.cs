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

        public EditorTweenPlayerComponent(
            Type type,
            string name,
            string menuPath,
            Color color
            )
        {
            Type = type;
            Name = name;
            MenuPath = menuPath;
            Color = color;
        }
    }
}
