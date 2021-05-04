using System;

namespace Juce.TweenPlayer.BindableData
{
    public class EditorBindableDataField
    {
        public Type Type { get; }
        public string Name { get; }

        public EditorBindableDataField(
            Type type,
            string name
            )
        {
            Type = type;
            Name = name;
        }
    }
}
