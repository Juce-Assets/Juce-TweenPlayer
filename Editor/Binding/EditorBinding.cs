using Juce.TweenPlayer.Utils;
using System;

namespace Juce.TweenPlayer.Bindings
{
    public class EditorBinding 
    {
        public Type Type { get; }
        public string Name { get; }
        public string FormatedName { get; }
        public Binding Binding { get; }
        public string[] BindableFields { get; set; } = Array.Empty<string>();

        public EditorBinding(
            Type type, 
            string name,
            Binding binding
            )
        {
            Type = type;
            Name = name;
            FormatedName = Name.FirstCharToUpper();
            Binding = binding;
        }
    }
}
