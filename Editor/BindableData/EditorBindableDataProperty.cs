using System;

namespace Juce.TweenPlayer.BindableData
{
    public class EditorBindableDataProperty
    {
        public Type Type { get; }
        public string Name { get; }

        public EditorBindableDataProperty(
            Type type,
            string name
            )
        {
            Type = type;
            Name = name;
        }
    }
}
