using System;
using System.Collections.Generic;

namespace Juce.TweenPlayer.BindableData
{
    public class EditorBindableData
    {
        public Type Type { get; }
        public string Name { get; }
        public string MenuPath { get; }
        public string Uid { get; }
        public IReadOnlyList<EditorBindableDataField> Fields { get; }
        public IReadOnlyList<EditorBindableDataProperty> Properties { get; }

        public EditorBindableData(
            Type type,
            string name,
            string menuPath,
            string uid,
            IReadOnlyList<EditorBindableDataField> fields,
            IReadOnlyList<EditorBindableDataProperty> properties
            )
        {
            Type = type;
            Name = name;
            MenuPath = menuPath;
            Uid = uid;
            Fields = fields;
            Properties = properties;
        }
    }
}
