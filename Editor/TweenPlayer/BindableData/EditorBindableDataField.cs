using System;

namespace Juce.TweenComponent.BindableData
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
