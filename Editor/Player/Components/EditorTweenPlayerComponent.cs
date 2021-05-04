using System;

namespace Juce.TweenPlayer
{
    public class EditorTweenPlayerComponent
    {
        public Type Type { get; }
        public string Name { get; }
        public string MenuPath { get; }
        public string[] FieldsNames { get; }

        public EditorTweenPlayerComponent(
            Type type,
            string name,
            string menuPath
            )
        {
            Type = type;
            Name = name;
            MenuPath = menuPath;
        }
    }
}
